﻿using BepInEx;
using KKAPI;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using System.Collections.Generic;
using System.IO;
using PseudoRandom;
using System;

namespace KK_Plugins
{
    /// <summary>
    /// Generates random characters by using variational autoencoder.
    /// </summary>
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
    [BepInIncompatibility("info.jbcs.koikatsu.characterrandomizer")]
    [BepInPlugin(GUID, PluginName, Version)]
    public partial class DeepCharacterGenerator : BaseUnityPlugin
    {
        public const string GUID = "tropical-362827.deepcharactergenerator";
        public const string PluginName = "Deep Character Generator";
        public const string PluginNameInternal = "KK" + "_DeepCharacterGenerator";
        public const string Version = "2.0";

        internal void Main() => MakerAPI.RegisterCustomSubCategories += MakerAPI_RegisterCustomSubCategories;

        private void MakerAPI_RegisterCustomSubCategories(object sender, RegisterSubCategoriesEvent e)
        {
            
            var parentCat = MakerConstants.Body.All;
            var cat = new MakerCategory(parentCat.CategoryName, "DeepCharacterGeneratorCategory", parentCat.Position + 5, "Generate");
            e.AddSubCategory(cat);

            var randButton = e.AddControl(new MakerButton("Generate", cat, this));

            string dllpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dlldir = Path.GetDirectoryName(dllpath);
            string paramsdir = System.IO.Path.Combine(dlldir, "params");
            string[] weightsdir = Directory.GetDirectories(paramsdir);
            string[] weightsdir_child = new string[weightsdir.Length];
            for(int i = 0; i < weightsdir.Length; i++)
            {
                weightsdir_child[i] = Path.GetFileName( weightsdir[i] );
            }
            var paramsDropdown = e.AddControl(new MakerDropdown("Parameter", weightsdir_child, cat, 0, this));
            var oddeyeToggle = e.AddControl(new MakerToggle(cat, "Allow odd eye", false, this));
            var facepaintToggle = e.AddControl(new MakerToggle(cat, "Allow face paint", false, this));
            e.AddControl(new MakerSeparator(cat, this));
            var generateBody = e.AddControl(new MakerToggle(cat, "Generate body values", true, this));
            var generateFace = e.AddControl(new MakerToggle(cat, "Generate face values", true, this));
            var generateHair = e.AddControl(new MakerToggle(cat, "Generate hair values", true, this));


            randButton.OnClick.AddListener(() =>
            {
                string choosed_dir = weightsdir[paramsDropdown.Value];
                Console.WriteLine(choosed_dir);

                allowOddeye = oddeyeToggle.Value;
                allowFacePaint = facepaintToggle.Value;

                float[,] weight = LoadCSV( System.IO.Path.Combine( choosed_dir, "weight.csv" ) );
                float[,] bias = LoadCSV( System.IO.Path.Combine( choosed_dir, "bias.csv" ) );

                System.Random random = new System.Random();
                MersenneTwister mt = new MersenneTwister((ulong)random.Next());

                float[,] mu = GaussMatrix(1, 800, mt, 0, 1.0f);
                float[,] sigma = Exp(GaussMatrix(1, 800, mt, 0, 0.5f));
                float[,] epsilon = GaussMatrix(1, 800, mt, 0, 1.0f);

                float[,] ma = GenerateLatent(mt, mu, sigma, epsilon);
                float[,] mb = weight;
                float[,] mc = Add( Dot(ma, mb), bias );
                mc = Sigmoid(mc);
                float[] kk_vector = new float[mc.GetLength(1)];
                for(int i=0; i<kk_vector.Length; i++)
                {
                    kk_vector[i] = mc[0, i];
                }

                if (generateBody.Value) GeneratorBody.GenerateBody(kk_vector);
                if (generateFace.Value) GeneratorFace.GenerateFace(kk_vector);
                if (generateHair.Value) GeneratorHair.GenerateHair(kk_vector);

                MakerAPI.GetCharacterControl().Reload();
            });
        }

        public static float[,] LoadCSV(string filepath)
        {
            List<List<float>> matrix_list = new List<List<float>>();
            StreamReader reader = new StreamReader(filepath, System.Text.Encoding.GetEncoding("UTF-8"));

            while (reader.Peek() >= 0)
            {
                string[] cols = reader.ReadLine().Split(',');
                List<float> row = new List<float>();
                for (int i = 0; i < cols.Length; i++)
                {
                    row.Add(float.Parse(cols[i]));
                }
                matrix_list.Add(row);
            }
            reader.Close();

            float[,] matrix = new float[matrix_list.Count, matrix_list[0].Count];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrix_list[i][j];
                }
            }

            return matrix;
        }

        public static float[] GetRange(float[] array, int start, int count)
        {
            float[] subarray = new float[count];
            for(int i=start; i<count; i++)
            {
                subarray[i] = array[i];
            }
            return subarray;
        }

        public static int AdjustEmocreId(int value, int threshold, int offset)
        {
            if( value > threshold )
            {
                return value + offset;
            }
            else
            {
                return value;
            }
        }

        #region Random utils

        public static ChaFileControl Chararacter => MakerAPI.GetCharacterControl().chaFile;
        public static ChaFileCustom Custom => Chararacter.custom;
        public static bool allowOddeye;
        public static bool allowFacePaint;

        #endregion
    }
}
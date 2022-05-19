using BepInEx;
using KKAPI;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using System.Collections.Generic;
using System.IO;
using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;

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
        public const string PluginNameInternal = "KKS" + "_DeepCharacterGenerator";
        public const string Version = "2.0";

        internal void Main() => MakerAPI.RegisterCustomSubCategories += MakerAPI_RegisterCustomSubCategories;

        private void MakerAPI_RegisterCustomSubCategories(object sender, RegisterSubCategoriesEvent e)
        {
            
            var parentCat = MakerConstants.Body.All;
            var cat = new MakerCategory(parentCat.CategoryName, "DeepCharacterGeneratorCategory", parentCat.Position + 5, "キャラ生成");
            e.AddSubCategory(cat);

            var randButton = e.AddControl(new MakerButton("キャラクターを生成する", cat, this));

            string dllpath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string dlldir = Path.GetDirectoryName(dllpath);
            string paramsdir = System.IO.Path.Combine(dlldir, "params");
            string[] weightsdir = Directory.GetDirectories(paramsdir);
            string[] weightsdir_child = new string[weightsdir.Length];
            for(int i = 0; i < weightsdir.Length; i++)
            {
                weightsdir_child[i] = Path.GetFileName( weightsdir[i] );
            }
            var paramsDropdown = e.AddControl(new MakerDropdown("使う学習パラメーター", weightsdir_child, cat, 0, this));
            var oddeyeToggle = e.AddControl(new MakerToggle(cat, "オッドアイ(非対称な瞳)を許可する", false, this));
            var facepaintToggle = e.AddControl(new MakerToggle(cat, "フェイスペイントの使用を許可する", false, this));
            e.AddControl(new MakerSeparator(cat, this));
            var generateBody = e.AddControl(new MakerToggle(cat, "体型の変更を許可する", true, this));
            var generateFace = e.AddControl(new MakerToggle(cat, "顔の変更を許可する", true, this));
            var generateHair = e.AddControl(new MakerToggle(cat, "髪型の変更を許可する", true, this));


            randButton.OnClick.AddListener(() =>
            {
                string choosed_dir = weightsdir[paramsDropdown.Value];
                Console.WriteLine(choosed_dir);

                allowOddeye = oddeyeToggle.Value;
                allowFacePaint = facepaintToggle.Value;

                double[,] weight = LoadCSV( System.IO.Path.Combine( choosed_dir, "weight.csv" ) );
                double[,] bias = LoadCSV( System.IO.Path.Combine( choosed_dir, "bias.csv" ) );

                System.Random random = new System.Random();

                var M = Matrix<double>.Build;

                var mu = M.Random(1, 800);
                var sigma = Matrix<double>.Exp(M.Random(1, 800, new Normal(0, 0.5f)));
                var epsilon = M.Random(1, 800);
                var ma = mu + Matrix<double>.Exp(sigma * 0.5).PointwiseMultiply(epsilon);
                var mb = M.DenseOfArray(weight);
                var mc = ma * mb + M.DenseOfArray(bias);
                mc = mc.Map(SpecialFunctions.Logistic);

                float[] kk_vector = new float[mc.ColumnCount];
                for(int i=0; i<kk_vector.Length; i++)
                {
                    kk_vector[i] = (float)mc[0, i];
                }

                if (generateBody.Value) GeneratorBody.GenerateBody(kk_vector);
                if (generateFace.Value) GeneratorFace.GenerateFace(kk_vector);
                if (generateHair.Value) GeneratorHair.GenerateHair(kk_vector);

                MakerAPI.GetCharacterControl().Reload();
            });
        }

        public static double[,] LoadCSV(string filepath)
        {
            List<List<double>> matrix_list = new List<List<double>>();
            StreamReader reader = new StreamReader(filepath, System.Text.Encoding.GetEncoding("UTF-8"));

            while (reader.Peek() >= 0)
            {
                string[] cols = reader.ReadLine().Split(',');
                List<double> row = new List<double>();
                for (int i = 0; i < cols.Length; i++)
                {
                    row.Add(double.Parse(cols[i]));
                }
                matrix_list.Add(row);
            }
            reader.Close();

            double[,] matrix = new double[matrix_list.Count, matrix_list[0].Count];
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

        // generates a sampling value following the categorical distribution.
        public static int RandomCategorial(float[] vector)
        {
            double[] vector_ = new double[vector.Length];
            for(int i=0; i<vector.Length; i++)
            {
                vector_[i] = Math.Exp(vector[i]);
            }
            return Categorical.Sample(vector_);
        }

        #region Random utils

        public static ChaFileControl Chararacter => MakerAPI.GetCharacterControl().chaFile;
        public static ChaFileCustom Custom => Chararacter.custom;
        public static bool allowOddeye;
        public static bool allowFacePaint;

        #endregion
    }
}

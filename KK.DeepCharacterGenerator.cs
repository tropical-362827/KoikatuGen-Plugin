using BepInEx;
using KKAPI;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using System.IO;
using System;
using MathNet.Numerics.Distributions;
using KoikatuGen;
using Newtonsoft.Json;

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
            string[] files = Directory.GetFiles(paramsdir, "*.json");
            string[] files_name = new string[files.Length];
            for(int i = 0; i < files.Length; i++)
            {
                files_name[i] = Path.GetFileName( files[i] );
            }
            var paramsDropdown = e.AddControl(new MakerDropdown("使う学習パラメーター", files_name, cat, 0, this));
            var oddeyeToggle = e.AddControl(new MakerToggle(cat, "オッドアイ(非対称な瞳)を許可する", false, this));
            var facepaintToggle = e.AddControl(new MakerToggle(cat, "フェイスペイントの使用を許可する", false, this));
            e.AddControl(new MakerSeparator(cat, this));
            var generateBody = e.AddControl(new MakerToggle(cat, "体型の変更を許可する", true, this));
            var generateFace = e.AddControl(new MakerToggle(cat, "顔の変更を許可する", true, this));
            var generateHair = e.AddControl(new MakerToggle(cat, "髪型の変更を許可する", true, this));


            randButton.OnClick.AddListener(() =>
            {
                string choosed_file = files[paramsDropdown.Value];
                base.Logger.LogDebug("Choosed file is: "+choosed_file);

                allowOddeye = oddeyeToggle.Value;
                allowFacePaint = facepaintToggle.Value;

                StreamReader sr = new StreamReader(choosed_file);
                string str = sr.ReadToEnd();
                sr.Close();
                var model_type = JsonConvert.DeserializeObject<GenerativeModel>(str);

                GenerativeModel model = null;
                if (model_type.type == "VAE")
                {
                    model = JsonConvert.DeserializeObject<VAE>(str);
                }

                var ret = model.Generate();
                float[] kk_vector = new float[ret.ColumnCount];
                for(int i=0; i<kk_vector.Length; i++)
                {
                    kk_vector[i] = (float)ret[0, i];
                }

                if (generateBody.Value) GeneratorBody.GenerateBody(kk_vector);
                if (generateFace.Value) GeneratorFace.GenerateFace(kk_vector);
                if (generateHair.Value) GeneratorHair.GenerateHair(kk_vector);

                MakerAPI.GetCharacterControl().Reload();
            });
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

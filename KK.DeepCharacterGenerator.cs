using BepInEx;
using KKAPI;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using System.IO;
using System;
using MathNet.Numerics.Distributions;
using KoikatuGen;
using Newtonsoft.Json;
using UnityEngine;

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
            var bodypaintToggle = e.AddControl(new MakerToggle(cat, "ボディペイントの使用を許可する", false, this));
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
                allowBodyPaint = bodypaintToggle.Value;

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

                if (generateBody.Value) GeneratorBody.GenerateBody(kk_vector, model);
                if (generateFace.Value) GeneratorFace.GenerateFace(kk_vector, model);
                if (generateHair.Value) GeneratorHair.GenerateHair(kk_vector, model);

                MakerAPI.GetCharacterControl().Reload();
            });
        }

        public static Color GetColor(float[] a, int s)
        {
            return new Color(a[s], a[s + 1], a[s + 2], a[s + 3]);
        }

        public static Vector4 GetVector(float[] a, int s)
        {
            return new Vector4(a[s], a[s + 1], a[s + 2], a[s + 3]);
        }

        public static int GetCategorical(float[] kkvector, int s, int[] categories, string name)
        {
            double[] vector = new double[categories.Length];
            for (int i = 0; i < categories.Length; i++)
            {
                vector[i] = Math.Exp(kkvector[s+i]);
            }
            return categories[Categorical.Sample(vector)];
        }

        #region Random utils

        public static ChaFileControl Chararacter => MakerAPI.GetCharacterControl().chaFile;
        public static ChaFileCustom Custom => Chararacter.custom;
        public static bool allowOddeye;
        public static bool allowFacePaint;
        public static bool allowBodyPaint;

        #endregion
    }
}

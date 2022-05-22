using KoikatuGen;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        private class GeneratorFace
        {

            public static void GenerateFace(float[] vector, GenerativeModel model)
            {
                var face = Custom.face;

                foreach (var c in model.columns)
                {
                    string name = (string)c.name;
                    if (!name.StartsWith("face"))
                    {
                        continue;
                    }
                    int start = (int)c.start;
                    string type = (string)c.type;
                    int[] values = null;
                    if (type == "categorical")
                    {
                        JArray array = (JArray)c.values;
                        values = array.ToObject<int[]>();
                    }
                    switch (name)
                    {
                        // scalar
                        case "face_detailPower":
                            face.detailPower = vector[start];
                            break;
                        case "face_cheekGlossPower":
                            face.cheekGlossPower = vector[start];
                            break;
                        case "face_pupilWidth":
                            face.pupilWidth = vector[start];
                            break;
                        case "face_pupilHeight":
                            face.pupilHeight = vector[start];
                            break;
                        case "face_pupilX":
                            face.pupilX = vector[start];
                            break;
                        case "face_pupilY":
                            face.pupilY = vector[start];
                            break;
                        case "face_eyelineUpWeight":
                            face.eyelineUpWeight = vector[start];
                            break;
                        case "face_lipGlossPower":
                            face.lipGlossPower = vector[start];
                            break;
                        case "face_pupil_0_gradBlend":
                            face.pupil[0].gradBlend = vector[start];
                            break;
                        case "face_pupil_0_gradOffsetY":
                            face.pupil[0].gradOffsetY = vector[start];
                            break;
                        case "face_pupil_0_gradScale":
                            face.pupil[0].gradScale = vector[start];
                            break;
                        case "face_pupil_1_gradBlend":
                            face.pupil[1].gradBlend = vector[start];
                            break;
                        case "face_pupil_1_gradOffsetY":
                            face.pupil[1].gradOffsetY = vector[start];
                            break;
                        case "face_pupil_1_gradScale":
                            face.pupil[1].gradScale = vector[start];
                            break;
                        // vector
                        case "face_shapeValueFace":
                            for (int i = 0; i < face.shapeValueFace.Length; i++)
                            {
                                face.shapeValueFace[i] = vector[start+i];
                            }
                            break;
                        case "face_eyebrowColor":
                            face.eyebrowColor = GetColor(vector, start);
                            break;
                        case "face_hlUpColor":
                            face.hlUpColor = GetColor(vector, start);
                            break;
                        case "face_hlDownColor":
                            face.hlDownColor = GetColor(vector, start);
                            break;
                        case "face_whiteBaseColor":
                            face.whiteBaseColor = GetColor(vector, start);
                            break;
                        case "face_whiteSubColor":
                            face.whiteSubColor = GetColor(vector, start);
                            break;
                        case "face_eyelineColor":
                            face.eyelineColor = GetColor(vector, start);
                            break;
                        case "face_moleColor":
                            face.moleColor = GetColor(vector, start);
                            break;
                        case "face_moleLayout":
                            face.moleLayout = GetVector(vector, start);
                            break;
                        case "face_lipLineColor":
                            face.lipLineColor = GetColor(vector, start);
                            break;
                        case "face_pupil_0_baseColor":
                            face.pupil[0].baseColor = GetColor(vector, start);
                            break;
                        case "face_pupil_0_subColor":
                            face.pupil[0].subColor = GetColor(vector, start);
                            break;
                        case "face_pupil_1_baseColor":
                            face.pupil[1].baseColor = GetColor(vector, start);
                            break;
                        case "face_pupil_1_subColor":
                            face.pupil[1].subColor = GetColor(vector, start);
                            break;
                        case "face_baseMakeup_eyeshadowColor":
                            face.baseMakeup.eyeshadowColor = GetColor(vector, start);
                            break;
                        case "face_baseMakeup_cheekColor":
                            face.baseMakeup.cheekColor = GetColor(vector, start);
                            break;
                        case "face_baseMakeup_lipColor":
                            face.baseMakeup.lipColor = GetColor(vector, start);
                            break;
                        case "face_baseMakeup_paintColor_0":
                            face.baseMakeup.paintColor[0] = GetColor(vector, start);
                            break;
                        case "face_baseMakeup_paintColor_1":
                            face.baseMakeup.paintColor[1] = GetColor(vector, start);
                            break;
                        case "face_baseMakeup_paintLayout_0":
                            face.baseMakeup.paintLayout[0] = GetVector(vector, start);
                            break;
                        case "face_baseMakeup_paintLayout_1":
                            face.baseMakeup.paintLayout[1] = GetVector(vector, start);
                            break;
                        // categorical
                        case "face_detailId":
                            face.detailId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_eyebrowId":
                            face.eyebrowId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_noseId":
                            face.noseId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_hlUpId":
                            face.hlUpId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_hlDownId":
                            face.hlDownId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_whiteId":
                            face.whiteId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_eyelineUpId":
                            face.eyelineUpId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_eyelineDownId":
                            face.eyelineDownId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_moleId":
                            face.moleId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_lipLineId":
                            face.lipLineId = GetCategorical(vector, start, values, name);
                            break;
                        case "face_foregroundEyes":
                            face.foregroundEyes = (byte)GetCategorical(vector, start, values, name);
                            break;
                        case "face_foregroundEyebrow":
                            face.foregroundEyebrow = (byte)GetCategorical(vector, start, values, name);
                            break;
                        default:
                            System.Console.WriteLine("unexpected column: " + name);
                            break;
                    }
                }

                // オッドアイにしない場合
                if (!allowOddeye)
                {
                    face.pupil[1].gradBlend = face.pupil[0].gradBlend;
                    face.pupil[1].gradOffsetY = face.pupil[0].gradOffsetY;
                    face.pupil[1].gradScale = face.pupil[0].gradScale;
                    face.pupil[1].baseColor = face.pupil[0].baseColor;
                    face.pupil[1].subColor = face.pupil[0].subColor;
                    face.pupil[1].id = face.pupil[0].id;
                    face.pupil[1].gradMaskId = face.pupil[0].id;
                }
            }

        }
    }
}

using KoikatuGen;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        private class GeneratorBody
        {

            public static void GenerateBody(float[] vector, GenerativeModel model)
            {
                var body = Custom.body;

                foreach(var c in model.columns)
                {
                    string name = (string)c.name;
                    if(!name.StartsWith("body")){
                        continue;
                    }
                    int start = (int)c.start;
                    string type = (string)c.type;
                    int[] values = null;
                    if(type == "categorical")
                    {
                        JArray array = (JArray)c.values;
                        values = array.ToObject<int[]>();
                    }
                    switch (name)
                    {
                        // scalar
                        case "body_bustSoftness":
                            body.bustSoftness = vector[start];
                            break;
                        case "body_bustWeight":
                            body.bustWeight = vector[start];
                            break;
                        case "body_detailPower":
                            body.detailPower = vector[start];
                            break;
                        case "body_skinGlossPower":
                            body.skinGlossPower = vector[start];
                            break;
                        case "body_nipGlossPower":
                            body.nipGlossPower = vector[start];
                            break;
                        case "body_areolaSize":
                            body.areolaSize = vector[start];
                            break;
                        case "body_nailGlossPower":
                            body.nailGlossPower = vector[start];
                            break;
                        // vector
                        case "body_shapeValueBody":
                            for (int i = 0; i < body.shapeValueBody.Length; i++)
                            {
                                body.shapeValueBody[i] = vector[start+i];
                            }
                            break;
                        case "body_skinMainColor":
                            body.skinMainColor = GetColor(vector, start);
                            break;
                        case "body_skinSubColor":
                            body.skinSubColor = GetColor(vector, start);
                            break;
                        case "body_sunburnColor":
                            body.sunburnColor = GetColor(vector, start);
                            break;
                        case "body_nipColor":
                            body.nipColor = GetColor(vector, start);
                            break;
                        case "body_underhairColor":
                            body.underhairColor = GetColor(vector, start);
                            break;
                        case "body_nailColor":
                            body.nailColor = GetColor(vector, start);
                            break;
                        case "body_paintColor_0":
                            // body.paintColor[0] = GetColor(vector, start);
                            break;
                        case "body_paintColor_1":
                            // body.paintColor[1] = GetColor(vector, start);
                            break;
                        case "body_paintLayout_0":
                            // body.paintLayout[0] = GetVector(vector, start);
                            break;
                        case "body_paintLayout_1":
                            // body.paintLayout[1] = GetVector(vector, start);
                            break;
                        // categorical
                        case "body_skinId":
                            body.skinId = GetCategorical(vector, start, values, name);
                            break;
                        case "body_detailId":
                            body.detailId = GetCategorical(vector, start, values, name);
                            break;
                        case "body_sunburnId":
                            body.sunburnId = GetCategorical(vector, start, values, name);
                            break;
                        case "body_nipId":
                            body.nipId = GetCategorical(vector, start, values, name);
                            break;
                        case "body_underhairId":
                            body.underhairId = GetCategorical(vector, start, values, name);
                            break;
                        case "body_paintId_0":
                            // body.paintId[0] = GetCategorical(vector, start, values, name);
                            break;
                        case "body_paintId_1":
                            // body.paintId[1] = GetCategorical(vector, start, values, name);
                            break;
                        case "body_paintLayoutId_0":
                            // body.paintLayoutId[0] = GetCategorical(vector, start, values, name);
                            break;
                        case "body_paintLayoutId_1":
                            // body.paintLayoutId[1] = GetCategorical(vector, start, values, name);
                            break;
                        default:
                            System.Console.WriteLine("unexpected column: " + name);
                            break;
                    }
                }
            }
        }
    }
}
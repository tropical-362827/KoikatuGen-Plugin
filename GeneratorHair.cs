
using KoikatuGen;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        private class GeneratorHair
        {
            public static void GenerateHair(float[] vector, GenerativeModel model)
            {
                var hair = Custom.hair;

                foreach (var c in model.columns)
                {
                    string name = (string)c.name;
                    if (!name.StartsWith("hair"))
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
                        case "hair_parts_0_length":
                            hair.parts[0].length = vector[start];
                            break;
                        case "hair_parts_1_length":
                            hair.parts[1].length = vector[start];
                            break;
                        case "hair_parts_2_length":
                            hair.parts[2].length = vector[start];
                            break;
                        case "hair_parts_3_length":
                            hair.parts[3].length = vector[start];
                            break;
                        // vector
                        case "hair_parts_0_baseColor":
                            hair.parts[0].baseColor = GetColor(vector, start);
                            break;
                        case "hair_parts_0_startColor":
                            hair.parts[0].startColor = GetColor(vector, start);
                            break;
                        case "hair_parts_0_endColor":
                            hair.parts[0].endColor = GetColor(vector, start);
                            break;
                        case "hair_parts_0_outlineColor":
                            hair.parts[0].outlineColor = GetColor(vector, start);
                            break;
                        case "hair_parts_1_baseColor":
                            hair.parts[1].baseColor = GetColor(vector, start);
                            break;
                        case "hair_parts_1_startColor":
                            hair.parts[1].startColor = GetColor(vector, start);
                            break;
                        case "hair_parts_1_endColor":
                            hair.parts[1].endColor = GetColor(vector, start);
                            break;
                        case "hair_parts_1_outlineColor":
                            hair.parts[1].outlineColor = GetColor(vector, start);
                            break;
                        case "hair_parts_2_baseColor":
                            hair.parts[2].baseColor = GetColor(vector, start);
                            break;
                        case "hair_parts_2_startColor":
                            hair.parts[2].startColor = GetColor(vector, start);
                            break;
                        case "hair_parts_2_endColor":
                            hair.parts[2].endColor = GetColor(vector, start);
                            break;
                        case "hair_parts_2_outlineColor":
                            hair.parts[2].outlineColor = GetColor(vector, start);
                            break;
                        case "hair_parts_3_baseColor":
                            hair.parts[3].baseColor = GetColor(vector, start);
                            break;
                        case "hair_parts_3_startColor":
                            hair.parts[3].startColor = GetColor(vector, start);
                            break;
                        case "hair_parts_3_endColor":
                            hair.parts[3].endColor = GetColor(vector, start);
                            break;
                        case "hair_parts_3_outlineColor":
                            hair.parts[3].outlineColor = GetColor(vector, start);
                            break;
                        // categorical
                        case "hair_glossId":
                            hair.glossId = GetCategorical(vector, start, values, name);
                             break;
                        case "hair_parts_0_id":
                            hair.parts[0].id = GetCategorical(vector, start, values, name);
                            break;
                        case "hair_parts_1_id":
                            hair.parts[1].id = GetCategorical(vector, start, values, name);
                            break;
                        case "hair_parts_2_id":
                            hair.parts[2].id = GetCategorical(vector, start, values, name);
                            break;
                        case "hair_parts_3_id":
                            hair.parts[3].id = GetCategorical(vector, start, values, name);
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

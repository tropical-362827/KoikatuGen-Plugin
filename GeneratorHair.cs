
using UnityEngine;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        private class GeneratorHair
        {
            public static void GenerateHair(float[] vector)
            {
                var hair = Custom.hair;
                int n;

                // scalar values
                n = 21;
                hair.parts[0].length = vector[n++];
                hair.parts[1].length = vector[n++];
                hair.parts[2].length = vector[n++];
                hair.parts[3].length = vector[n++];

                // vector values
                n = 241;
                for(int i=0; i<4; i++)
                {
                    hair.parts[i].baseColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                    hair.parts[i].startColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                    hair.parts[i].endColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                    hair.parts[i].outlineColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                }

                // categorical values
                hair.glossId = RandomCategorial(GetRange(vector, 563, 9));
                hair.parts[0].id = RandomCategorial(GetRange(vector, 985, 69));
                hair.parts[1].id = RandomCategorial(GetRange(vector, 1052, 81));
                hair.parts[2].id = RandomCategorial(GetRange(vector, 1123, 10));

                hair.parts[0].id = AdjustEmocreId(hair.parts[0].id, 58, 141);
                hair.parts[1].id = AdjustEmocreId(hair.parts[1].id, 70, 129);
                hair.parts[2].id = AdjustEmocreId(hair.parts[2].id, 8, 191);
            }

        }
    }
}

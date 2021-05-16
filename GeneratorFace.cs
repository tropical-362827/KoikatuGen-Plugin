using PseudoRandom;
using UnityEngine;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        private class GeneratorFace
        {

            public static void GenerateFace(float[] vector)
            {
                var face = Custom.face;
                System.Random random = new System.Random();
                MersenneTwister mt = new MersenneTwister((ulong)random.Next());
                int n;

                // scalar values;
                n = 0;
                face.detailPower = vector[n++];
                face.cheekGlossPower = vector[n++];
                face.pupilWidth = vector[n++];
                face.pupilHeight = vector[n++];
                face.pupilX = vector[n++];
                face.pupilY = vector[n++];
                face.eyelineUpWeight = vector[n++];
                face.lipGlossPower = vector[n++];
                n = 15;
                face.pupil[0].gradBlend = vector[n++];
                face.pupil[0].gradOffsetY = vector[n++];
                face.pupil[0].gradScale = vector[n++];
                face.pupil[1].gradBlend = vector[n++];
                face.pupil[1].gradOffsetY = vector[n++];
                face.pupil[1].gradScale = vector[n++];
                n = 25;
                for(int i=0; i < face.shapeValueFace.Length; i++)
                {
                    face.shapeValueFace[i] = vector[n++];
                }
                
                // vector values
                n = 77;
                face.eyebrowColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.hlUpColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.hlDownColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.whiteBaseColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.whiteSubColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.eyelineColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.moleColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.moleLayout = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.lipLineColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                n = 181;
                face.pupil[0].baseColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.pupil[0].subColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.pupil[1].baseColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.pupil[1].subColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.baseMakeup.eyeshadowColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.baseMakeup.cheekColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                face.baseMakeup.lipColor = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                // face.baseMakeup.paintColor[0] = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                // face.baseMakeup.paintColor[1] = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                // face.baseMakeup.paintLayout[0] = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);
                // face.baseMakeup.paintLayout[1] = new Color(vector[n++], vector[n++], vector[n++], vector[n++]);

                // categorical values
                face.detailId = allowFacePaint ? RandomCategorial(GetRange(vector, 305, 6), mt) : 0;
                face.eyebrowId = RandomCategorial(GetRange(vector, 305, 17), mt);
                face.noseId = RandomCategorial(GetRange(vector, 328, 7), mt);
                face.hlUpId = RandomCategorial(GetRange(vector, 335, 53), mt);
                face.hlUpId = RandomCategorial(GetRange(vector, 386, 35), mt);
                face.whiteId = RandomCategorial(GetRange(vector, 420, 5), mt);
                face.eyelineUpId = RandomCategorial(GetRange(vector, 425, 62), mt);
                face.eyelineDownId = RandomCategorial(GetRange(vector, 487, 22), mt);
                face.moleId = RandomCategorial(GetRange(vector, 509, 4), mt);
                face.lipLineId = RandomCategorial(GetRange(vector, 513, 7), mt);
                face.foregroundEyes = (byte)RandomCategorial(GetRange(vector, 520, 3), mt);  // why byte?
                face.foregroundEyebrow = (byte)RandomCategorial(GetRange(vector, 523, 3), mt);
                face.pupil[0].id = RandomCategorial(GetRange(vector, 572, 95), mt);
                face.pupil[0].gradMaskId = RandomCategorial(GetRange(vector, 665, 4), mt);
                face.pupil[1].id = RandomCategorial(GetRange(vector, 669, 95), mt);
                face.pupil[1].gradMaskId = RandomCategorial(GetRange(vector, 762, 4), mt);
                face.baseMakeup.eyeshadowId = RandomCategorial(GetRange(vector, 766, 10), mt);
                face.baseMakeup.cheekId = RandomCategorial(GetRange(vector, 776, 10), mt);
                face.baseMakeup.lipId = RandomCategorial(GetRange(vector, 786, 9), mt);
                // face.baseMakeup.paintId[0] = RandomCategorial(GetRange(vector, 795, 38), mt);
                // face.baseMakeup.paintId[1] = RandomCategorial(GetRange(vector, 833, 38), mt);

                // Emotion Creatorsで加わったIDへの対応
                face.eyebrowId = AdjustEmocreId( face.eyebrowId, 14, 185 );
                face.noseId = AdjustEmocreId(face.noseId, 4, 195);
                face.hlUpId = AdjustEmocreId(face.hlUpId, 40, 159);
                face.hlDownId = AdjustEmocreId(face.hlDownId, 25, 174);
                face.eyelineUpId = AdjustEmocreId(face.eyelineUpId, 52, 147);
                face.pupil[0].id = AdjustEmocreId(face.pupil[0].id, 76, 123);
                face.pupil[1].id = AdjustEmocreId(face.pupil[1].id, 76, 123);
                // face.baseMakeup.paintId[0] = AdjustEmocreId(face.baseMakeup.paintId[0], 34, 165);
                // face.baseMakeup.paintId[1] = AdjustEmocreId(face.baseMakeup.paintId[1], 34, 165);


                // オッドアイにしない場合
                if (!allowOddeye)
                {
                    face.pupil[1].gradBlend = face.pupil[0].gradBlend;
                    face.pupil[1].gradOffsetY = face.pupil[0].gradOffsetY;
                    face.pupil[1].gradScale = face.pupil[0].gradScale;

                    Color bc = face.pupil[0].baseColor;
                    Color sc = face.pupil[0].subColor;
                    face.pupil[1].baseColor = new Color(bc.r, bc.g, bc.b, bc.a);
                    face.pupil[1].subColor = new Color(sc.r, sc.g, sc.b, sc.a);

                    face.pupil[1].id = face.pupil[0].id;
                    face.pupil[1].gradMaskId = face.pupil[0].id;
                }
            }

        }
    }
}

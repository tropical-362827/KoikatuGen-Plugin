using UnityEngine;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        private class GeneratorBody
        {

            public static void GenerateBody(float[] vector)
            {
                var body = Custom.body;
                int n;

                // scalar values
                n = 8;
                body.bustSoftness = vector[n++];
                body.bustWeight = vector[n++];
                body.detailPower = vector[n++];
                body.skinGlossPower = vector[n++];
                body.nipGlossPower = vector[n++];
                body.areolaSize = vector[n++];
                body.nailGlossPower = vector[n++];
                n = 113;
                float[] shapeValueBody = new float[body.shapeValueBody.Length];
                for (int i = 0; i < body.shapeValueBody.Length; i++)
                {
                    shapeValueBody[i] = vector[n++];
                }
                body.shapeValueBody = shapeValueBody;

                // vector values
                n = 157;
                body.skinMainColor = new Color( vector[n++], vector[n++], vector[n++], vector[n++] );
                body.skinSubColor = new Color( vector[n++], vector[n++], vector[n++], vector[n++] );
                body.sunburnColor = new Color( vector[n++], vector[n++], vector[n++], vector[n++] );
                body.nipColor = new Color( vector[n++], vector[n++], vector[n++], vector[n++] );
                body.underhairColor = new Color( vector[n++], vector[n++], vector[n++], vector[n++] );
                body.nailColor = new Color( vector[n++], vector[n++], vector[n++], vector[n++] );
                n = 225;
                // body.paintColor = new Color[] {
                //     new Color( vector[n++], vector[n++], vector[n++], vector[n++] ),
                //     new Color( vector[n++], vector[n++], vector[n++], vector[n++] )
                // };
                // body.paintLayout = new Vector4[]
                // {
                //     new Vector4( vector[n++], vector[n++], vector[n++], vector[n++] ),
                //     new Vector4( vector[n++], vector[n++], vector[n++], vector[n++] )
                // };

                // categorical velues
                body.skinId = RandomCategorial( GetRange( vector, 526, 1 ));
                body.detailId = RandomCategorial( GetRange( vector, 527, 4 ));
                body.sunburnId = RandomCategorial( GetRange( vector, 531, 11 ));
                body.nipId = RandomCategorial( GetRange( vector, 542, 7 ));
                body.underhairId = RandomCategorial( GetRange( vector, 549, 14 ));
                // body.paintId = new int[] {
                //     RandomCategorial( GetRange( vector, 870, 37 ), mt ),
                //     RandomCategorial( GetRange( vector, 907, 38 ), mt )
                // };
                // body.paintLayoutId = new int[]
                // {
                //     RandomCategorial( GetRange( vector, 945, 20 ), mt ),
                //     RandomCategorial( GetRange( vector, 965, 20 ), mt )
                // };

                // エモクリで加わったpaintIdへの対応.
                // body.paintId[0] = AdjustEmocreId(body.paintId[0], 34, 166);
                // body.paintId[1] = AdjustEmocreId(body.paintId[1], 34, 165);

            }

        }
    }
}
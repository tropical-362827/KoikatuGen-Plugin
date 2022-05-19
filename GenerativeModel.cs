using System;
using Newtonsoft.Json;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Distributions;
using MathNet.Numerics;

namespace KoikatuGen
{
    public class GenerativeModel
    {
        [JsonProperty("type")]
        public string type { get; set; }

        public virtual Matrix<double> Generate()
        {
            return null;
        }
    }

    public class VAE : GenerativeModel
    {
        [JsonProperty("weight")]
        public double[,] weight { get; set; }
        [JsonProperty("bias")]
        public double[] bias { get; set; }


        public override Matrix<double> Generate()
        {
            double[,] bias_ = new double[1, weight.GetLength(1)];
            for(int i=0; i<weight.GetLength(1); i++)
            {
                bias_[0, i] = bias[i];
            }

            var M = Matrix<double>.Build;
            var mu = M.Random(1, 800);
            var sigma = Matrix<double>.Exp(M.Random(1, 800, new Normal(0, 0.5f)));
            var epsilon = M.Random(1, 800);
            var ma = mu + Matrix<double>.Exp(sigma * 0.5).PointwiseMultiply(epsilon);
            var mb = M.DenseOfArray(weight);
            var mc = ma * mb + M.DenseOfArray(bias_);
            mc = mc.Map(SpecialFunctions.Logistic);

            return mc;
        }
    }
}

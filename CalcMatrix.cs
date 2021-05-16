using System;
using PseudoRandom;

namespace KK_Plugins
{
    public partial class DeepCharacterGenerator
    {
        // calculates the dot product with ma and mb.
        public static float[,] Dot(float[,] ma, float[,] mb)
        {
            if (ma.GetLength(1) != mb.GetLength(0))
            {
                throw new Exception(string.Format("Can't calculate the dot product of ({0}, {1}) and ({2}, {3})", ma.GetLength(0), ma.GetLength(1), mb.GetLength(0), mb.GetLength(1)));
            }

            float[,] answer = new float[ma.GetLength(0), mb.GetLength(1)];
            for (int i = 0; i < ma.GetLength(0); i++)
            {
                for (int n = 0; n < mb.GetLength(1); n++)
                {
                    float sum = 0;
                    for (int k = 0; k < ma.GetLength(1); k++)
                    {
                        sum += ma[i, k] * mb[k, n];
                    }
                    answer[i, n] = sum;
                }
            }
            return answer;
        }

        // generates a matrix following the Gauss distribution.
        public static float[,] GaussMatrix(int height, int width, MersenneTwister mt, float mu, float sigma) 
        {
            float[,] matrix = new float[height, width];

            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    matrix[r, c] = Gauss(mt, mu, sigma);
                }
            }
            return matrix;
        }

        public static float[,] GenerateLatent(MersenneTwister mt, float[,] mu, float[,] sigma, float[,] epsilon)
        {
            float[,] matrix = new float[sigma.GetLength(0), mu.GetLength(1)];

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    matrix[r, c] = mu[r, c] + (float) Math.Exp(sigma[r, c] * 0.5) * epsilon[r, c];
                }
            }
            return matrix;
        }

        public static float Gauss(MersenneTwister mt, float mu, float sigma)
        {
            double x, y, gauss;
            x = mt.genrand_real2();
            y = mt.genrand_real2();
            gauss = Math.Sqrt(-2.0 * Math.Log(x)) * Math.Cos(2.0 * Math.PI * y);
            return mu + (float)(gauss) * sigma;
        }

        // sigmoid function
        public static float[,] Sigmoid(float[,] t)
        {
            for (int y = 0; y < t.GetLength(0); y++)
            {
                for (int x = 0; x < t.GetLength(1); x++)
                {
                    t[y, x] = (float)(1.0 / (1.0 + Math.Exp(-t[y, x])));
                }
            }
            return t;
        }
        public static float[,] Exp(float[,] t)
        {
            for (int y = 0; y < t.GetLength(0); y++)
            {
                for (int x = 0; x < t.GetLength(1); x++)
                {
                    t[y, x] = (float) Math.Exp(t[y, x]);
                }
            }
            return t;
        }
        public static float[,] Add(float[,] a, float[,] b)
        {
            float[,] answer = new float[a.GetLength(0), a.GetLength(1)];
            for (int y = 0; y < a.GetLength(0); y++)
            {
                for (int x = 0; x < a.GetLength(1); x++)
                {
                    answer[y, x] = a[y, x] + b[y, x];
                }
            }
            return answer;
        }

        // generates a sampling value following the categorical distribution.
        public static int RandomCategorial(float[] vector, MersenneTwister mt)
        {

            float sum = 0, cumulative_sum = 0, probability, uniform;
            float[] energy = new float[vector.Length];

            // calculates a state sum. 
            for (int i = 0; i < vector.Length; i++)
            {
                energy[i] = (float)Math.Exp(vector[i]);
                sum += energy[i];
            }
            // normalization & calculates thresholds of the cumulative distribution.
            for (int i = 0; i < vector.Length; i++)
            {
                probability = energy[i] / sum;
                cumulative_sum += probability;
                energy[i] = cumulative_sum;
            }

            uniform = (float)mt.genrand_real2();
            // sampling.
            for (int i = 0; i < vector.Length; i++)
            {
                if (uniform < energy[i])
                {
                    return i;
                }
            }
            return vector.Length - 1;


        }

    }

 }

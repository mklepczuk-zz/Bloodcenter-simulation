using System;
using System.Collections.Generic;

namespace Symulacja
{
    public class Generators
    {
        public static int Range = 950000;

        public float[] Randomvalues = new float[2*Range/3];
        public float[] Randomvalues1 = new float[Range/5];
        public float[] Randomvalues2 = new float[2*Range];
        public float[] Randomvalues3 = new float[3*Range/2];
        public float[] Randomvalues4 = new float[8*Range];
        public float[] Randomvalues5 = new float[6*Range];

        public float[] Values150To200 = new float[2*Range/3];
        public float[] ValuesExponentialCasual = new float[Range/5];
        public float[] ValuesExponentialPacient = new float[2*Range];
        public float[] ValuesExponentialDonor = new float[3*Range/2];
        public float[] ValuesGaussian = new float[2*Range/3];
        public int[] ValuesGeometric = new int[3*Range/2];

        private const int M = 2147483647;

        public void Initialize(long r0)
        {
            Generategaussian(r0);
            Generateuniform150To200(r0);
            GenerateexponentialPacient(r0);
            GenerateexponentialDonor(r0);
            GenerateexponentialCasual(r0);
            Generategeometric(r0);
        }

        private static void Generateuniform(long r0, IList<float> table, int range)
        {
            for (int i = 0;i < range;i++)
            {
                r0 = 16807 * r0 % M;
                float value = (float) r0 / M;
                table[i] = value;
            }
        }

        private void Generategaussian(long r0)
        {
            int counter = 0;
            float wariancja = (float) Math.Sqrt(0.1);
            const int srednia = 600;
            Generateuniform(r0,Randomvalues4,8*Range);

            for (var i = 12; i < 8*Range ;i++)
            {
                float value = 0;
                if ((i - 12) % 12 != 0) continue;

                for (var j = i - 12; j < i; j++) value += wariancja*Randomvalues4[j];

                ValuesGaussian[counter] = value-6*wariancja+srednia;
                counter++;
            }
        }

        private void Generateuniform150To200(long r0)
        {
            Generateuniform((long) ((r0 + M*Randomvalues4[1000000])%M),Randomvalues,2*Range/3);

            for (var i = 0;i < 2*Range/3;i++) Values150To200[i] = Randomvalues[i] * 50 + 150;
        }

        private void GenerateexponentialCasual(long r0)
        {
            Generateuniform((long) ((r0 + M*Randomvalues4[2000000])%M),Randomvalues1,Range/5);
            const float z = (float) 1/2000;

            for (var i = 0;i < Range/5 ;i++) ValuesExponentialCasual[i] = (float) -Math.Pow(z,-1) * (float) Math.Log(Randomvalues1[i]);
        }

        private void GenerateexponentialPacient(long r0)
        {
            Generateuniform((long) ((r0 + M*Randomvalues4[3000000])%M),Randomvalues2,2*Range);
            const double p = (double) 1/300;

            for (var i = 0;i < 2*Range ;i++) ValuesExponentialPacient[i] = (float) -Math.Pow(p,-1) * (float) Math.Log(Randomvalues2[i]);
        }

        private void GenerateexponentialDonor(long r0)
        {
            Generateuniform((long) ((r0 + M*Randomvalues4[4000000])%M),Randomvalues3,Range/2);
            const double p = (double) 1/800;

            for (var i = 0;i < Range/2 ;i++) ValuesExponentialDonor[i] = (float) -Math.Pow(p,-1) * (float) Math.Log(Randomvalues3[i]);
        }

        private void Generategeometric(long r0)
        {
            int value = 1;
            int counter = 0;
            Generateuniform((long) ((r0 + M*Randomvalues4[5000000])%M),Randomvalues5,6*Range);
            const double success = 0.19;

            for (var i = 0;i < 6*Range ;i++)
            {
                if (Randomvalues5[i] > success)
                {
                    value++;
                    continue;
                }

                ValuesGeometric[counter] = value;
                counter++;
                value = 1;
            }
        }
    }
}
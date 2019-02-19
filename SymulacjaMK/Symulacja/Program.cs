using System;
using System.Collections.Generic;
using System.Linq;

namespace Symulacja
{
    public class Program
    {
        public static Generators Generator = new Generators();

        public static void Main(string[] args)
        {
            const int m = 2147483647;
            long r0 = 377003613;
            const int statisticsnumber = 200;
            var emergencyprobability = new float[statisticsnumber];
            var utilizedblood = new float[statisticsnumber];

            for (var i = 0; i < statisticsnumber; i++)
            {
                var simulation = new BloodDonationPoint(new List<Blood>(), new List<Pacient>(), 0);
                var calendar = new Agenda(new List<Process>());

                Generator.Initialize(r0);

                var pacientAppearance = new PacientAppearance(5, 0, simulation.BloodList, simulation.Queue);
                calendar.Events.Add(pacientAppearance);
                calendar.Events.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

                var donorAppearance = new DonorAppearance(0, simulation.BloodList, simulation.Queue);
                calendar.Events.Add(donorAppearance);
                calendar.Events.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

                while (SendOrder.Ordercounter < Generators.Range / 7)
                {
                    calendar.Events[0].Execute(calendar.Events);
                    calendar.Events.RemoveAt(0);
                }

                emergencyprobability[i] = (float) SendEmergencyOrder.Ordercounter * 100 /
                                          (SendEmergencyOrder.Ordercounter + SendOrder.Ordercounter - 13);
                utilizedblood[i] = (float) BloodDonationPoint.BloodUtilized * 100 / BloodDonationPoint.BloodCounter;

                Console.WriteLine(i);
                Console.WriteLine("Percentage of emergency order: " + emergencyprobability[i] + "%");
                Console.WriteLine("Percentage of utilized blood: " + utilizedblood[i] + "%");

                r0 = (long) (Generator.Randomvalues4[8 * Generators.Range - 1] * m % m);
                BloodDonationPoint.Clearcounters();
            }

            float variance = 0;
            for (var i = 0; i < statisticsnumber; i++)
                variance += (float) Math.Pow(emergencyprobability[i], 2) -
                            statisticsnumber * emergencyprobability[i] / (statisticsnumber - 1);
            variance /= statisticsnumber - 1;

            Console.WriteLine("\nStatistics after " + statisticsnumber + " simulations:");
            Console.WriteLine("\nPercentage of emergency order : " + emergencyprobability.Average()+ "%");
            Console.WriteLine("Variance of percentage of emergency order : " + variance);
            Console.WriteLine("Confidence interval of percentage of emergency order : (" +
                              (emergencyprobability.Average() -
                               (float) 1.9719 * Math.Sqrt(variance) / Math.Sqrt(statisticsnumber)) + " , " +
                              (emergencyprobability.Average() +
                               (float) 1.9719 * Math.Sqrt(variance) / Math.Sqrt(statisticsnumber)) + ")");
            Console.WriteLine("\nPercentage of utilized blood: " + utilizedblood.Average()+ "%");

            Console.WriteLine("Parameters used to this simulation: R = " + 300 + " N = " + 60);

            Console.ReadKey();
        }
    }
}
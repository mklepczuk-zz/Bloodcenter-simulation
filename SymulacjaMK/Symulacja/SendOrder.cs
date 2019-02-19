using System.Collections.Generic;

namespace Symulacja
{
    public class SendOrder
    {
        private static int _ordercounter;

        public static int Ordercounter
        {
            get => _ordercounter;
            set => _ordercounter = value;
        }

        public static void Execute(BloodDonationPoint.KindOfDelivery argument, List<Blood> blood, List<Pacient> queue, int simulationTime, List<Process> calendar)
        {
            BloodDelivery temp5 = new BloodDelivery(argument, blood, queue, (int) Program.Generator.ValuesExponentialCasual[Ordercounter] + simulationTime);
            calendar.Add(temp5);
            calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

            BloodDelivery.OrderFlag = false;

            Ordercounter++;
        }
    }
}
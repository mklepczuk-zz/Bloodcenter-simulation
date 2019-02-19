using System.Collections.Generic;

namespace Symulacja
{
    public class SendEmergencyOrder
    {
        private static int _ordercounter;
        private static int _counter;

        public static int Ordercounter
        {
            get => _ordercounter;
            set => _ordercounter = value;
        }

        public static int Counter
        {
            get => _counter;
            set => _counter = value;
        }

        public static void Execute(BloodDonationPoint.KindOfDelivery argument, List<Blood> blood, List<Pacient> queue, int simulationTime, List<Process> calendar)
        {
            BloodDelivery temp6 = new BloodDelivery(argument, blood, queue, (int) Program.Generator.ValuesGaussian[Counter] + simulationTime);
            calendar.Add(temp6);
            calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

            BloodDelivery.EmergencyOrderflag = false;
            Counter++;

            if(SendOrder.Ordercounter > 13)
                Ordercounter++;
        }
    }
}
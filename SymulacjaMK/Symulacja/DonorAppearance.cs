using System.Collections.Generic;

namespace Symulacja
{
    public class DonorAppearance : Process
    {
        private readonly List<Blood> _blood;
        private readonly List<Pacient> _queue;
        private static int _counter;
        private const int R = 300;// jeśli liczba jednostek krwi nie będzie większa od tej liczby to należy dostarczyć krew
        private const int T2 = 20000;// odpowiada za czas, po którym utylizowana jest krew oddana

        public static int Counter
        {
            get => _counter;
            set => _counter = value;
        }

        public DonorAppearance(int time, List<Blood> bloodList, List<Pacient> pacientList)
        {
            _simulationTime = time;
            _blood = bloodList;
            _queue = pacientList;
        }

        public override void Execute(List<Process> calendar)
        {
            Blood bloodDonor = new Blood(1,T2+_simulationTime);
            _blood.Add(bloodDonor);
            _blood.Sort((x, y) => x.SimulationTime.CompareTo(y.SimulationTime));
            BloodDonationPoint.TotalNumberOfBloodUnits++;

            if(SendOrder.Ordercounter > 13)
                BloodDonationPoint.BloodCounter++;

            BloodUtilization temp3 = new BloodUtilization(T2 + _simulationTime, _blood, _queue);
            calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));
            calendar.Add(temp3);

            Transfusion.Execute(_blood,_queue, _simulationTime, calendar);
            if (R >= BloodDonationPoint.TotalNumberOfBloodUnits && BloodDelivery.OrderFlag)
                SendOrder.Execute(BloodDonationPoint.KindOfDelivery.Casual, _blood, _queue, _simulationTime, calendar);

            DonorAppearance temp4 = new DonorAppearance((int) Program.Generator.ValuesExponentialDonor[Counter] + _simulationTime, _blood, _queue);
            calendar.Add(temp4);
            calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

            Counter++;
        }
    }
}
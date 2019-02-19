using System.Collections.Generic;

namespace Symulacja
{
    public class PacientAppearance : Process
    {
        private readonly int _neededBlood;
        private readonly List<Blood> _blood;
        private readonly List<Pacient> _queue;
        private static int _counter;
        private const int R = 300;// jeśli liczba jednostek krwi nie będzie większa od tej liczby to należy dostarczyć krew

        public static int Counter
        {
            get => _counter;
            set => _counter = value;
        }

        public PacientAppearance(int blood, int time, List<Blood> bloodList, List<Pacient> pacientList)
        {
            _neededBlood = blood;
            _simulationTime = time;
            _blood = bloodList;
            _queue = pacientList;
        }

        public override void Execute(List<Process> calendar)
        {
            Pacient human = new Pacient(_neededBlood);
            _queue.Add(human);

            Transfusion.Execute(_blood, _queue, _simulationTime, calendar);
            if (R >= BloodDonationPoint.TotalNumberOfBloodUnits && BloodDelivery.OrderFlag)
                SendOrder.Execute(BloodDonationPoint.KindOfDelivery.Casual, _blood, _queue, _simulationTime, calendar);

            PacientAppearance newAppearance = new PacientAppearance(Program.Generator.ValuesGeometric[Counter],
                (int) Program.Generator.ValuesExponentialPacient[Counter] + _simulationTime, _blood, _queue);
            calendar.Add(newAppearance);
            calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

            Counter++;
        }
    }
}
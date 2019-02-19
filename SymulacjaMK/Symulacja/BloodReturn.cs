using System.Collections.Generic;

namespace Symulacja
{
    public class BloodReturn : Process
    {
        private readonly List<Blood> _blood;
        private readonly List<Pacient> _queue;
        private const int R = 300;// jeśli liczba jednostek krwi nie będzie większa od tej liczby to należy dostarczyć krew

        public BloodReturn(int time, List<Blood> bloodList, List<Pacient> pacientList)
        {
            _simulationTime = time;
            _blood = bloodList;
            _queue = pacientList;
        }
        public override void Execute(List<Process> calendar)
        {
            int numberOfBlood = _blood.Count;
            if (numberOfBlood == 0) return;

            BloodDonationPoint.TotalNumberOfBloodUnits -= _blood[0].NumberOfBloodUnits;
            _blood.RemoveAt(0);

            if(SendOrder.Ordercounter > 13)
                BloodDonationPoint.BloodUtilized++;

            if (R < BloodDonationPoint.TotalNumberOfBloodUnits || !BloodDelivery.OrderFlag) return;

            SendOrder.Execute(BloodDonationPoint.KindOfDelivery.Casual, _blood, _queue, _simulationTime, calendar);
        }
    }
}
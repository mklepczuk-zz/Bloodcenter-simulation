using System;
using System.Collections.Generic;

namespace Symulacja
{
    public class BloodDelivery : Process
    {
        private readonly BloodDonationPoint.KindOfDelivery _argument;
        private readonly List<Blood> _blood;
        private readonly List<Pacient> _queue;
        private static int _counter;
        private static bool _orderFlag = true;
        private static bool _emergencyOrderflag = true;
        private const int N = 60;// odpowiada za liczbę jednostek krwi dostarczanej podczas zamówienia
        private const int Q = 12;// odpowiada za liczbę jednostek krwi dostarczanej podczas zamówienia awaryjnego
        private const int T1 = 8000;// odpowiada za czas, po którym utylizowana jest krew dostarczona
        private const int R = 300;// jeśli liczba jednostek krwi nie będzie większa od tej liczby to należy dostarczyć krew

        public static int Counter
        {
            get => _counter;
            set => _counter = value;
        }

        public static bool OrderFlag
        {
            get => _orderFlag;
            set => _orderFlag = value;
        }

        public static bool EmergencyOrderflag
        {
            get => _emergencyOrderflag;
            set => _emergencyOrderflag = value;
        }

        public BloodDelivery(BloodDonationPoint.KindOfDelivery genre, List<Blood> bloodList, List<Pacient> pacientList,
            int time)
        {
            _argument = genre;
            _blood = bloodList;
            _queue = pacientList;
            _simulationTime = time;
        }

        public override void Execute(List<Process> calendar)
        {
            switch (_argument)
            {
                case BloodDonationPoint.KindOfDelivery.Casual:

                    Blood bloodDelivery = new Blood(N, T1+_simulationTime);
                    _blood.Add(bloodDelivery);
                    _blood.Sort((x, y) => x.SimulationTime.CompareTo(y.SimulationTime));
                    BloodDonationPoint.TotalNumberOfBloodUnits += N;

                    if(SendOrder.Ordercounter > 13)
                        BloodDonationPoint.BloodCounter++;

                    BloodUtilization temp = new BloodUtilization(T1 + _simulationTime, _blood, _queue);
                    calendar.Add(temp);
                    calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

                    Transfusion.Execute(_blood,_queue, _simulationTime, calendar);
                    if (R >= BloodDonationPoint.TotalNumberOfBloodUnits && _orderFlag)
                        SendOrder.Execute(BloodDonationPoint.KindOfDelivery.Casual, _blood, _queue, _simulationTime, calendar);

                    OrderFlag = true;
                    break;

                case BloodDonationPoint.KindOfDelivery.Emergency:

                    Blood bloodEmergency = new Blood(Q,(int) Program.Generator.Values150To200[_counter]+_simulationTime);
                    _blood.Add(bloodEmergency);
                    _blood.Sort((x, y) => x.SimulationTime.CompareTo(y.SimulationTime));
                    BloodDonationPoint.TotalNumberOfBloodUnits += Q;

                    if(SendOrder.Ordercounter > 13)
                        BloodDonationPoint.BloodCounter++;

                    BloodReturn temp2 = new BloodReturn((int) Program.Generator.Values150To200[_counter] + _simulationTime, _blood, _queue);
                    calendar.Add(temp2);
                    calendar.Sort((x, y) => x._simulationTime.CompareTo(y._simulationTime));

                    Transfusion.Execute(_blood,_queue, _simulationTime, calendar);
                    if (R >= BloodDonationPoint.TotalNumberOfBloodUnits && _orderFlag)
                        SendOrder.Execute(BloodDonationPoint.KindOfDelivery.Casual, _blood, _queue, _simulationTime, calendar);

                    _emergencyOrderflag = true;
                    _counter++;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_argument), _argument, null);
            }
        }
    }
}
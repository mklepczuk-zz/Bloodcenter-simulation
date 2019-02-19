using System.Collections.Generic;

namespace Symulacja
{
    public class Transfusion
    {
        public static void Execute(List<Blood> blood, List<Pacient> queue, int simulationTime, List<Process> calendar)
        {
            var numberOfPatients = queue.Count;
            if (numberOfPatients == 0) return;

            var numberOfBlood = blood.Count;
            if (numberOfBlood == 0) return;

            int neededblood = 0;

            foreach (var i in queue) neededblood += i.NumberOfNeededBloodUnits;

            if (BloodDelivery.EmergencyOrderflag && BloodDonationPoint.TotalNumberOfBloodUnits < neededblood)
                SendEmergencyOrder.Execute(BloodDonationPoint.KindOfDelivery.Emergency, blood, queue, simulationTime, calendar);

            while (numberOfPatients > 0 && numberOfBlood > 0 && queue[0].NumberOfNeededBloodUnits > 0)
                {
                    if (queue[0].NumberOfNeededBloodUnits < blood[0].NumberOfBloodUnits)
                    {
                        blood[0].NumberOfBloodUnits -= queue[0].NumberOfNeededBloodUnits;
                        BloodDonationPoint.TotalNumberOfBloodUnits -= queue[0].NumberOfNeededBloodUnits;
                        queue.RemoveAt(0);
                        numberOfPatients = queue.Count;
                        numberOfBlood = blood.Count;
                        continue;
                    }

                    if (numberOfPatients > 0 && numberOfBlood > 0 &&
                        queue[0].NumberOfNeededBloodUnits == blood[0].NumberOfBloodUnits)
                    {
                        BloodDonationPoint.TotalNumberOfBloodUnits -= queue[0].NumberOfNeededBloodUnits;
                        queue.RemoveAt(0);
                        foreach (var i in calendar)
                        {
                            if( i._simulationTime != blood[0].SimulationTime) continue;

                            var type = i.GetType();
                            if (type.Name != "BloodReturn" && type.Name != "BloodUtilization") continue;

                            calendar.Remove(i);
                            break;
                        }
                        blood.RemoveAt(0);
                        numberOfPatients = queue.Count;
                        numberOfBlood = blood.Count;
                        continue;
                    }

                    if (numberOfPatients <= 0 || numberOfBlood <= 0) continue;

                    queue[0].NumberOfNeededBloodUnits -= blood[0].NumberOfBloodUnits;
                    BloodDonationPoint.TotalNumberOfBloodUnits -= blood[0].NumberOfBloodUnits;
                    foreach (var i in calendar)
                    {
                        if( i._simulationTime != blood[0].SimulationTime) continue;

                        var type = i.GetType();
                        if (type.Name != "BloodReturn" && type.Name != "BloodUtilization") continue;

                        calendar.Remove(i);
                        break;
                    }
                    blood.RemoveAt(0);
                    numberOfPatients = queue.Count;
                    numberOfBlood = blood.Count;
                }
        }
    }
}
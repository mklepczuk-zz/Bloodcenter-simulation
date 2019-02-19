using System.Collections.Generic;
namespace Symulacja
{
    public class BloodDonationPoint
    {
        public List<Blood> BloodList;
        public List<Pacient> Queue;
        private static int _totalNumberOfBloodUnits;
        private static int _bloodCounter;
        private static int _bloodUtilized;

        public static int TotalNumberOfBloodUnits
        {
            get => _totalNumberOfBloodUnits;
            set => _totalNumberOfBloodUnits = value;
        }

        public static int BloodCounter
        {
            get => _bloodCounter;
            set => _bloodCounter = value;
        }

        public static int BloodUtilized
        {
            get => _bloodUtilized;
            set => _bloodUtilized = value;
        }

        public BloodDonationPoint(List<Blood> listOfBlood, List<Pacient> pacientQueue, int bloodUnits)
        {
            BloodList = listOfBlood;
            Queue = pacientQueue;
            TotalNumberOfBloodUnits = bloodUnits;
        }

        public enum KindOfDelivery
        {
            Emergency,
            Casual
        }

        public static void Clearcounters()
        {
            BloodDelivery.Counter = 0;
            DonorAppearance.Counter = 0;
            PacientAppearance.Counter = 0;
            SendEmergencyOrder.Counter = 0;
            SendEmergencyOrder.Ordercounter = 0;
            BloodDelivery.EmergencyOrderflag = true;
            BloodDelivery.OrderFlag = true;
            SendOrder.Ordercounter = 0;
            BloodCounter = 0;
            BloodUtilized = 0;
        }
    }
}
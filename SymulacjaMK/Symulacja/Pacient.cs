namespace Symulacja
{
    public class Pacient
    {
        private int _numberOfNeededBloodUnits;

        public int NumberOfNeededBloodUnits
        {
            get => _numberOfNeededBloodUnits;
            set => _numberOfNeededBloodUnits = value;
        }

        public Pacient(int bloodNeeded)
        {
            NumberOfNeededBloodUnits = bloodNeeded;
        }
    }
}
namespace Symulacja
{
    public class Blood
    {
        private int _numberOfBloodUnits;
        private int _simulationTime;

        public int NumberOfBloodUnits
        {
            get => _numberOfBloodUnits;
            set => _numberOfBloodUnits = value;
        }

        public int SimulationTime
        {
            get => _simulationTime;
            set => _simulationTime = value;
        }

        public Blood(int blood, int time)
        {
            NumberOfBloodUnits = blood;
            SimulationTime = time;
        }
    }
}
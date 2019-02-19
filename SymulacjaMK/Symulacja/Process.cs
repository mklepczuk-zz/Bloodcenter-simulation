using System.Collections.Generic;

namespace Symulacja
{
    public class Process
    {
        public int _simulationTime; // ta zmienna musi się tak nazywać, inaczej nie mógłbym jej przeciążać
        public virtual void Execute(List<Process> calendar) {}
    }
}
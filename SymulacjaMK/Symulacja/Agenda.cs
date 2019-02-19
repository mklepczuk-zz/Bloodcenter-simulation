using System.Collections.Generic;

namespace Symulacja
{
    public class Agenda
    {
        public List<Process> Events;

        public Agenda(List<Process> agenda)
        {
            Events = agenda;
        }
    }
}
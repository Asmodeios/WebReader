using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReader
{
    class Memento
    {
        public string State { get; set; }

        public Memento(string state)
        {
            State = state;
        }
    }
}

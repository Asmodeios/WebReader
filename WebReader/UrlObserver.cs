using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReader
{
    class UrlObserver : IUrlObserver
    {
        public string Name { get; set; }
        public UrlChecker _UrlChecker { get; set; }
        

        public UrlObserver(string name)
        {
            Name = name;
        }

        public void Update(UrlChecker checker)
        {
            Console.WriteLine(Name + " : " + checker.UrlName + " has been modified");
        }
    }
}

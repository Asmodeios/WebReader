using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReader
{
    class App
    {
        public static void Main()
        {
            UrlChecker checker = new UrlChecker("http://localhost:3000/form", "B:/Test.txt");
            checker.Attach(new UrlObserver("localhostObserver"));
            checker.State = "New";
            CareTaker careTaker = new CareTaker();
            careTaker.Memento = checker.CreateMemento();
            checker.Start();


            Console.ReadKey();

        }
    }
}

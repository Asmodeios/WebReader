using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Threading;

namespace WebReader
{
    class UrlChecker
    {
        public string UrlName { get; set; }
        public string FilePath { get; set; }
        private string _state;
        private List<IUrlObserver> observers = new List<IUrlObserver>();


        public UrlChecker(string url, string file)
        {
            UrlName = url;
            FilePath = file;
        }


        public void Start()
        {
            
            while (true)
            {
           
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string oldBody = File.ReadAllText(FilePath);
                        string newBody = client.DownloadString(UrlName);

                        if (string.IsNullOrEmpty(oldBody))
                        {
                            File.AppendAllText(FilePath, newBody);
                        } else
                        {
                            if (checkModified(newBody, oldBody))
                            {
                                File.WriteAllText(FilePath, String.Empty);
                                File.WriteAllText(FilePath, newBody);
                                State = "Modified";
                                Notify();
                            }
                        }

                        
                    }
                }
                finally
                {
                    Console.WriteLine("Checked");
                }
                Thread.Sleep(15000);
            }
           

        }

        public static bool checkModified(string newBody, string oldBody)
        {
            if (newBody.Length != oldBody.Length) return true;

            for (int i = 0; i < newBody.Length; i++)
            {
                if (newBody[i] != oldBody[i]) return true;
            }
           
            return false;
        }

       public void Attach(IUrlObserver obs)
        {
            observers.Add(obs);
        }

        public void Detach(IUrlObserver obs)
        {
            observers.Remove(obs);
        }

        public void Notify()
        {
            foreach (IUrlObserver obs in observers)
            {
                obs.Update(this);
            }
        }

        public Memento CreateMemento()
        {
            return new Memento(State);
        }

        public void SetMemento(Memento mem)
        {
            _state = mem.State;
        }

        public string State
        {
            get { return _state; }
            set
            {
                File.AppendAllText("B:\\Logs.txt", "State: " + value + " | date: " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine);
                _state = value;
                
            }
        }
    }
}

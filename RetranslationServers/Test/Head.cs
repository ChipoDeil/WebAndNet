using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RetranslationServers
{
    class Head
    {
        static List<Pipe> pipeList;
        public static void Main(string[] args)
        {
            pipeList = new List<Pipe>();
            pipeList.Add(new Pipe("192.168.1.173", "192.168.1.240", 5000, 5001));
            pipeList.Add(new Pipe("192.168.1.173", "192.168.1.240", 5001, 5000));
            foreach (Pipe currentPipe in pipeList)
            {
                Thread pipeThread = new Thread(currentPipe.Listen);
                pipeThread.Start();
            }

        }
    }
}

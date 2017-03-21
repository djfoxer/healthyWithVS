using System;

namespace MEFExample
{
    class Program
    {
        static void Main(string[] args)
        {
            HostApp hostApp = new HostApp();
            hostApp.Start();
            //just wait to see results
            Console.ReadKey();
        }
    }
}

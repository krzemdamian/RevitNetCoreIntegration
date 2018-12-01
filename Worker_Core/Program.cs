using System;

namespace Worker_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Worker starting");
            Worker worker = new Worker();
            Console.ReadKey();
        }
    }
}

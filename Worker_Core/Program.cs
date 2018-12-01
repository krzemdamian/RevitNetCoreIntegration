using System;

namespace Worker_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Worker starting");
            Worker worker = new Worker();
            Console.WriteLine("Worker started");
            Console.ReadKey();
        }
    }
}

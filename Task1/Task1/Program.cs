using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Q1 a
            Console.WriteLine("Insert numer:");
            var char1 = Console.ReadLine();
            if (int.TryParse(char1?.ToString(), out int number));
            Console.WriteLine(IsPrime(number));
            Main(args);
        }

        private static bool IsPrime(int number)
        {
            switch (number)
            {   
                case 0:
                    return false;
                case 1:
                    return false;
                case 2:
                    return true;
            }

            var limit = Math.Ceiling(Math.Sqrt(number)); 

            for (var i = 3; i <= limit; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    static class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Insert numer:");
            var char1 = Console.ReadLine();
            if (!int.TryParse(char1, out int number)) return;
          

            Func<int,bool> pintIsPrime= new Func<int, bool>(IsPrime);

            //Q1.c
            var result = Memorize(pintIsPrime).Invoke(number);
            Console.WriteLine($"{number} is prime? {result}");

            Main(args);
        }
        //Q1.a
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

        //Q1.b
        private static  Func<TParameter, TOutput> Memorize<TParameter, TOutput>(this Func<TParameter, TOutput> method)
        {
            var map = new Dictionary<TParameter, TOutput>();
            return a =>
            {
                TOutput value;
                if (map.TryGetValue(a, out value))
                    return value;
                value = method(a);
                map.Add(a, value);
                return value;
            };
        }

        private static int Search(object[] collection, object value)
        {
            for (int i = 0; i < collection.Length; i++)
            {
                if (value==collection[i])
                {
                    return 1;
                }
            }
            return -1;
        }
    }
}

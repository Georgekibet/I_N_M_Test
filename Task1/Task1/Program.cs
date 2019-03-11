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
            Console.ReadLine();
            Console.WriteLine("Question 2");
            object[] list= new object[]{3,4,9,4,6,7,8};
            Console.WriteLine("Search 7 in 3,4,9,4,6,7,8");
            Console.WriteLine("Result: "+Search(list,7));
            object[] list2= new object[]{ "r", "t", "y", "h", "j", "k", "l" };
            Console.WriteLine("Search y in r,t,y,h,j,k,l");
            Console.WriteLine("Result: "+Search(list2,"y"));
            Console.ReadLine();
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
                if (value.ToString()==collection[i].ToString())
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

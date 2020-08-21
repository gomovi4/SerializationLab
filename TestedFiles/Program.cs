using System;

namespace Cycles
{
    class Program
    {
        public static int i;
        public static int j;
        
        static void Main(string[] args)
        {
            MultiplicationWithFor();
            MultiplicationWithDoWhile();
            MultiplicationWithWhile();
            MultiplicationWithForeach();                     
        }
        //multyplication Table with "for" cycle
        public static void MultiplicationWithFor()
        {
            /*Hey, I'm test text1*/
			/*Hey, I'm test tex2, 
			text3,text4,
			text5*/
			Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Multiplication table with 'for' cycle");
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (i == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0,4} ", i * j);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("{0,4} ", i * j);
                    }
                }
                Console.WriteLine();
            }
        }

        //multiplication table with "do...while" cycle
        public static void MultiplicationWithDoWhile()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Multiplication table with 'do...while' cycle");
            Reset_ij_values();
            do
            {
                do
                {
                    if (i == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0,4} ", i * j);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("{0,4} ", i * j);
                    }
                    j++;
                }
                while (j < 11);
                j = 1;
                Console.WriteLine();
                i++;
            }
            while (i < 11);
        }

        //multiplication table with "while" cycle
        public static void MultiplicationWithWhile()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Multiplication table with 'while' cycle");
            Reset_ij_values();

            while (i < 11)
            {
                while (j < 11)
                {
                    if (i == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0,4} ", i * j);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("{0,4} ", i * j);
                    }
                    j++;

                }
                j = 1;
                i++;
                Console.WriteLine();
            }
        }

        //multiplication table with "foreach" cycle
        public static void MultiplicationWithForeach()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Multiplication table with 'foreach' cycle");
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
			            
			foreach (int i in numbers)
            {
                foreach (int j in numbers)
                {
                    if (i == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0,4} ", i * j);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("{0,4} ", i * j);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void Reset_ij_values()
        {
            i = 1;
            j = 1;
        }
/*test
    }
	
}

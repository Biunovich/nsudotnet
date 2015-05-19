using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] mat = { "Idiot ", "Stupid ", "Loshara ", "Mutherfucker ", "AI clever than ", "Fuck you " };
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;
            List<string> BigSmall = new List<string>();
            int tries = 0;
            Random rand = new System.Random();
            int RandNumber = rand.Next(101);
            string Number = null;
            int int_number;
            Console.Write("Enter your name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Guess what number I thinked between [0,100]");
            while (Number != "q")
            {
                Number = Console.ReadLine();
                if (Number != "q")
                {
                    int_number = Convert.ToInt32(Number);
                    if (int_number == RandNumber)
                    {
                        date2 = DateTime.Now;
                        break;
                    }
                    else if (int_number > RandNumber)
                    {
                        tries++;
                        Console.WriteLine("Too big");
                        BigSmall.Add(int_number.ToString());
                        BigSmall.Add("Too big");
                        if (tries % 4 == 0)
                        {
                            Console.WriteLine(mat[rand.Next(mat.Length)] + Name);
                        }
                    }
                    else
                    {
                        tries++;
                        Console.WriteLine("Too small");
                        BigSmall.Add(int_number.ToString());
                        BigSmall.Add("Too small");
                        if (tries % 4 == 0)
                        {
                            Console.WriteLine(mat[rand.Next(mat.Length)] + Name);
                        }
                    }
                }
            }
            if (Number != "q")
            {
                Console.WriteLine("Tries : {0}", tries);
                int i = 0;
                foreach (string str in BigSmall)
                {
                    if (i % 2 == 0)
                        Console.Write("Your number {0}", str);
                    else
                    {
                        Console.WriteLine(" {0}", str);
                    }
                    i++;
                }
                TimeSpan date3 = date2 - date1;
                Console.WriteLine("Time seconds : {0}", date3.Seconds);
            }
            else
            {
                Console.WriteLine("Sorry....");
            }
        }
    }
}

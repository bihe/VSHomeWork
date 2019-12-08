using System;
using System.Collections.Generic;
using System.Linq;

namespace calc
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new int[]{ 0, 2, 3, 4, 5, 7, 8, 9 };
            var smallest = 234;
            var biggest = 9875;
            var allNumbers = new List<int>();

            for(int i = smallest; i <= biggest; i++)
            {
                if (ContainsNumbers(i, numbers))
                    allNumbers.Add(i);
            }

            Console.Write("Numbers := ");
            foreach(var n in numbers)
            {
                Console.Write($"{n};");
            }
            Console.Write("\n");

            Console.WriteLine($"Got {allNumbers.Count} different possibilities!");
            Console.WriteLine($"Will sum individual numbers and return result");

            // shift the array of numbers to the right "until one turn done"
            // and add the first and second entry
            bool found = false;
            for(int a = 0; a < allNumbers.Count; a++)
            {
                for(int b = 0; b < allNumbers.Count; b++)
                {
                    if (allNumbers[a] != allNumbers[b])
                    {
                        var digitsA = new List<int>(allNumbers[a].ToString().Select(c => Int32.Parse(c.ToString())));
                        if (allNumbers[a] < 1000)
                            digitsA.Insert(0,0);
                        var digitsB = new List<int>(allNumbers[b].ToString().Select(c => Int32.Parse(c.ToString())));
                        if (allNumbers[b] < 1000)
                            digitsB.Insert(0,0);

                        bool sameDigits = false;
                        foreach(var d in digitsA)
                        {
                            if (digitsB.Contains(d))
                            {
                                sameDigits = true;
                                break;
                            }
                        }

                        // the two numbers share digits, so try again
                        if (sameDigits)
                            continue;

                        Console.WriteLine($"r := {allNumbers[a]} + {allNumbers[b]} = {Add(allNumbers[a],allNumbers[b])}");

                        found = false;
                        var result = Add(allNumbers[a], allNumbers[b]);
                        var digits = result.ToString().Select(c => Int32.Parse(c.ToString()));
                        var ds = digits.ToArray();
                        for(int j=1;j<ds.Count();j++)
                        {
                            if (ds[0] != ds[j])
                            {
                                found = false;
                                break;
                            }
                            found = true;
                        }
                        if(found)
                        {
                            var num1 = $"{allNumbers[a]}";
                            var num2 = $"{allNumbers[b]}";

                            if (allNumbers[a] < 1000)
                                num1 = "0" + num1;

                             if (allNumbers[b] < 1000)
                                num2 = "0" + num2;


                            Console.WriteLine($"\t{num1}");
                            Console.WriteLine($"+\t{num2}");
                            Console.WriteLine($"\t----");

                            Console.WriteLine($"=\t{result}");
                            Console.WriteLine($"\t====\n");
                            return;
                        }
                    }
                        
                }
            }
        }

        static bool ContainsNumbers(int number, int[] numbers)
        {
            var availableNumbers = new List<int>(numbers);

            var digits = new List<int>(number.ToString().Select(c => Int32.Parse(c.ToString())));
            if (number < 1000)
                digits.Insert(0, 0);

            foreach(var d in digits)
            {
                if (!availableNumbers.Contains(d))
                    return false;

                availableNumbers.Remove(d);
            }
            return true;
        }

        static int Add(int a, int b)
        {
            var result = a + b;
            return result;
        }
    }
}

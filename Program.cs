using System;
using System.Collections.Generic;
using System.Linq;

namespace calc
{
    class Program
    {
        // given a list of numbers / digits
        // add two numbers comprised of the given digits (two 4-digit numbers)
        // the result should be made up only similar digits (e.g. 3333)
        static void Main(string[] args)
        {
            // the available digits
            var numbers = new int[]{ 0, 2, 3, 4, 5, 7, 8, 9 };
            var smallest = 234;
            var biggest = 9875;
            var allNumbers = new List<int>();

            // get all the possible numbers from smallest to biggest
            // check that the numbers contain the available digits
            for(int i = smallest; i <= biggest; i++)
            {
                if (ContainsNumbers(i, numbers))
                    allNumbers.Add(i);
            }

            Console.Write("available numbers := ");
            var k = 0;
            foreach(var n in numbers)
            {   
                if (k > 0)
                    Console.Write(";");
                Console.Write($"{n}");
                k++;
            }
            Console.Write($"\nGot {allNumbers.Count} different number combinations! \nE.g.: ");
            k = 0;
            foreach(var num in allNumbers)
            {   
                if (k > 8)
                    break;
                if (num < 1000)
                    Console.Write($"0{num};");
                else
                    Console.Write($"0{num};");
                k++;
            }
            Console.Write("...\n");
            Console.WriteLine($"Will sum individual numbers and find result with equal digits.");

            // add every number with every other number and check if the digits of the result are all the same
            bool foundSameDigits = false;
            for(int a = 0; a < allNumbers.Count; a++)
            {
                for(int b = 0; b < allNumbers.Count; b++)
                {
                    if (allNumbers[a] != allNumbers[b])
                    {
                        var digitsA = GetDigits(allNumbers[a]);
                        var digitsB = GetDigits(allNumbers[b]);

                        // determine if the two numbers share digits
                        bool shareDigits = false;
                        foreach(var d in digitsA)
                        {
                            if (digitsB.Contains(d))
                            {
                                shareDigits = true;
                                break;
                            }
                        }

                        // the two numbers share digits, so try again
                        if (shareDigits)
                            continue;

                        // let's try a calculation
                        //Console.WriteLine($"r := {allNumbers[a]} + {allNumbers[b]} = {Add(allNumbers[a],allNumbers[b])}");
                        var result = Add(allNumbers[a], allNumbers[b]);

                        // check if the result shares all the same digits
                        foundSameDigits = false;
                        var digits = GetDigits(result);
                        var ds = digits.ToArray();
                        for(int j=1;j<ds.Count();j++)
                        {
                            if (ds[0] != ds[j])
                            {
                                foundSameDigits = false;
                                break;
                            }
                            foundSameDigits = true;
                        }

                        // the result shares the same digits so we have found the final result
                        // print and exit
                        if(foundSameDigits)
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
            var digits = GetDigits(number);
            foreach(var d in digits)
            {
                if (!availableNumbers.Contains(d))
                    return false;

                availableNumbers.Remove(d);
            }
            return true;
        }

        static List<int> GetDigits(int number)
        {
            var digits = new List<int>(number.ToString().Select(c => Int32.Parse(c.ToString())));
            if (number < 1000)
                digits.Insert(0,0);
            return digits;
        }

        static int Add(int a, int b)
        {
            var result = a + b;
            return result;
        }
    }
}

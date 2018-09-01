using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EPedrini.AdjacentMaxDistance
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                //example dataset, it must return distance: 5
                //COMMENT/DISCCOMENT THIS BLOCK FOR TEST
                var values = new List<int>() {0, 3, 3, 12, 5, 3, 7, 1};
                Console.Write("Dataset: ");
                values.ForEach(x => Console.Write($"{x}, "));


                //random dataset for performance test
                /* //COMMENT/DISCCOMENT THIS BLOCK FOR TEST
                var values = GenerateArray(40000);
                */

                var sp = new Stopwatch();
                sp.Start();
                var result = Solution.solution(values.ToArray());
                sp.Stop();
                Console.WriteLine();
                Console.WriteLine($"Distance: {result} \t Time: {sp.ElapsedMilliseconds} ms");                
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Press any key to quit");
                Console.ReadKey();
            }
        }


        private static List<int> GenerateArray(int itemsCount)
        {
            var result = new List<int>();
            var rnd = new Random();

            var filePath = Path.GetFullPath($"./Dataset_{DateTime.Now:ddMMyyyyhhmmss}.txt");

            using (StreamWriter file = new StreamWriter(filePath))
            {

                for (int i = 0; i < itemsCount; i++)
                {
                    result.Add(rnd.Next(Int32.MinValue, Int32.MaxValue));
                    file.WriteLine(result[i]);
                }
            }

            Console.WriteLine($"Dataset in: {filePath}");

            return result;
        }

    }
}

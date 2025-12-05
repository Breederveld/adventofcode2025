using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2025_05_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var strings = input.Trim().Split("\n").Select(s => s.TrimEnd()).ToArray();
            sw.Start();

            var ranges = strings.TakeWhile(s => !string.IsNullOrEmpty(s)).Select(s => s.Split('-').Select(ss => long.Parse(ss)).ToArray()).ToArray();
            var remainingRanges = new Queue<long[]>(ranges);
            var uniqueRanges = new List<long[]>();

            while (remainingRanges.Count > 0)
            {
                var range = remainingRanges.Dequeue();
                var isUnique = true;
                foreach (var remove in uniqueRanges)
                {
                    if (range[1] >= remove[0] && range[0] <= remove[1])
                    {
                        if (range[1] > remove[1])
                        {
                            remainingRanges.Enqueue([remove[1] + 1, range[1]]);
                        }
                        if (range[0] < remove[0])
                        {
                            remainingRanges.Enqueue([range[0], remove[0] - 1]);
                        }
                        isUnique = false;
                        break;
                    }
                }
                if (isUnique)
                {
                    uniqueRanges.Add(range);
                }
            }
            var result = uniqueRanges.Sum(range => range[1] - range[0] + 1);

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
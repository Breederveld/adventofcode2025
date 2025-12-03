using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_03_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var banks = input.Trim().Split("\n").Select(s => s.TrimEnd().Select(c => c - '0').ToArray()).ToArray();
            sw.Start();

            var result = 0L;
            foreach (var bank in banks)
            {
                var powered = new int[12];
                var lastIdx = -1;
                for (var num = 0; num < 12; num++)
                {
                    var highest = 0;
                    var highestIdx = -1;
                    for (var idx = lastIdx + 1; idx <= bank.Length - 12 + num; idx++)
                    {
                        if (bank[idx] > highest)
                        {
                            highest = bank[idx];
                            highestIdx = idx;
                        }
                    }
                    if (highestIdx >= 0)
                    {
                        powered[num] = highestIdx;
                        lastIdx = highestIdx;
                    }
                }
                result += powered.Aggregate(0L, (acc, next) => acc * 10 + bank[next]);
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2025_06_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var strings = input.Trim().Split("\n").Select(s => s.TrimEnd().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            sw.Start();

            var result = 0L;
            for (var i = 0; i < strings[0].Length; i++)
            {
                var op = strings.Last()[i];
                var vals = strings.Take(strings.Length - 1).Select(s => long.Parse(s[i]));
                switch (op)
                {
                    case "*":
                        result += vals.Aggregate(1L, (acc, next) => acc * next);
                        break;
                    case "+":
                        result += vals.Aggregate(0L, (acc, next) => acc + next);
                        break;
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2025_06_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var strings = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var idxes = strings.Last()
                .Select((chr, idx) => chr == ' ' ? -2 : idx)
                .Where(idx => idx != -2)
                .Concat(strings.First().Length + 1)
                .ToArray();
            var lines = strings.Select(s => Enumerable.Range(0, idxes.Length - 1).Select(idx => s.Substring(idxes[idx], idxes[idx + 1] - idxes[idx] - 1)).ToArray()).ToArray();
            sw.Start();

            var result = 0L;
            for (var i = 0; i < lines[0].Length; i++)
            {
                var op = lines.Last()[i].Trim();
                var vals = Enumerable.Range(0, lines[0][i].Length).Select(idx => long.Parse(new string(lines.Take(lines.Length - 1).Select(l => l[i][idx]).Where(c => c != ' ').ToArray()))).ToArray();
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
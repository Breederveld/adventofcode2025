using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;

namespace Puzzle_2025_05_1
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

            var result = 0;
            var ranges = strings.TakeWhile(s => !string.IsNullOrEmpty(s)).Select(s => s.Split('-').Select(ss => long.Parse(ss)).ToArray()).ToArray();
            var ids = strings.SkipUntil(s => string.IsNullOrEmpty(s)).Select(s => long.Parse(s)).ToArray();

            foreach (var id in ids)
            {
                if (ranges.Any(range => id >= range[0] && id <= range[1]))
                {
                    result++;
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}

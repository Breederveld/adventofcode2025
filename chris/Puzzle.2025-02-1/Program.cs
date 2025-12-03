using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_02_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var strings = input.Trim().Split(",").Select(s => s.TrimEnd()).Select(s => s.Split('-')).ToArray();
            sw.Start();

            var result = 0L;
            foreach (var range in strings)
            {
                var min = long.Parse(range[0]);
                var max = long.Parse(range[1]);
                for (var i = min; i <= max; i++)
                {
                    var log = (int)Math.Log10(i);
                    if (log % 2 == 0)
                        continue;
                    var div = (long)Math.Pow(10, log / 2 + 1);
                    if (i % div == i / div)
                        result += i;
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}

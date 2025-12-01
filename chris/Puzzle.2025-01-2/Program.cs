using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_01_2
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

            var result = strings
                .Select(s => (s[0] == 'L' ? -1 : 1) * int.Parse(s.Substring(1)))
                .Aggregate((100000 + 50, 0), (acc, next) =>
                {
                    var from = acc.Item1;
                    var to = acc.Item1 + next;
                    var incr = Math.Abs((to / 100) - (from / 100));
                    if (next < 0)
                    {
                        if (to % 100 == 0)
                            incr++;
                        if (from % 100 == 0)
                            incr--;
                    }
                    return (to, acc.Item2 + incr);
                })
                .Item2;

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
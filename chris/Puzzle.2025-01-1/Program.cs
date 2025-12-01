using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_01_1
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
                .Select(s => (s[0] == 'L' ? 100 - int.Parse(s.Substring(1)) : int.Parse(s.Substring(1))))
                .Aggregate((50, 0), (acc, next) =>
                {
                    var value = (acc.Item1 + next) % 100;
                    return (value, acc.Item2 + (value == 0 ? 1 : 0));
                })
                .Item2;

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
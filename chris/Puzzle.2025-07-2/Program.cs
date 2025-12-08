using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_07_2
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

            var beams = new long[strings[0].Length];
            beams[strings[0].IndexOf('S')] = 1;
            foreach (var line in strings)
            {
                for (var x = 0; x < beams.Length; x++)
                {
                    if (beams[x] > 0 && line[x] == '^')
                    {
                        var val = beams[x];
                        beams[x - 1] += val;
                        beams[x] = 0L;
                        beams[x + 1] += val;
                    }
                }
            }
            var result = beams.Sum();

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}

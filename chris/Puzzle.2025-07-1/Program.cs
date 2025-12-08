using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_07_1
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

            var beams = new bool[strings[0].Length];
            beams[strings[0].IndexOf('S')] = true;
            foreach (var line in strings)
            {
                for (var x = 0; x < beams.Length; x++)
                {
                    if (beams[x] && line[x] == '^')
                    {
                        beams[x - 1] = true;
                        beams[x] = false;
                        beams[x + 1] = true;
                        result++;
                    }
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}

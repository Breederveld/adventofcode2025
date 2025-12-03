using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_03_1
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

            var result = 0;
            foreach (var bank in banks)
            {
                var max = 0;
                for (var i = 0; i < bank.Length; i++)
                {
                    for (var j = i + 1; j < bank.Length; j++)
                    {
                        var val = bank[i] * 10 + bank[j];
                        if (val > max)
                        {
                            max = val;
                        }
                    }
                }
                result += max;
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
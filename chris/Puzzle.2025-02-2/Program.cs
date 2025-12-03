using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Puzzle_2025_02_2
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
                    var len = (int)Math.Log10(i) + 1;
                    for (var j = 2; j <= len; j++)
                    {
                        if (len % j != 0)
                            continue;
                        var div = (long)Math.Pow(10, len / j);
                        var srch = i % div;
                        var remaining = i / div;
                        while (remaining > 0)
                        {
                            if (remaining % div != srch)
                                break;
                            remaining /= div;
                        }
                        if (remaining == 0)
                        {
                            result += i;
                            break;
                        }
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

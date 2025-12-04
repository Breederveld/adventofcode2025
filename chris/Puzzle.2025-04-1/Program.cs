using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_04_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var rows = input.Trim().Split("\n").Select(s => s.TrimEnd().Select(c => c == '@').ToArray()).ToArray();
            sw.Start();
            var directions = new (int x, int y)[] { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };

            var result = 0;
            for (var y = 0; y < rows.Length; y++)
            {
                for (var x = 0; x < rows[0].Length; x++)
                {
                    if (!rows[y][x])
                    {
                        continue;
                    }
                    var sum = directions
                        .Select(dir => (x: x + dir.x, y: y + dir.y))
                        .Where(pos => pos.x >= 0 && pos.x < rows[0].Length && pos.y >= 0 && pos.y < rows.Length)
                        .Where(pos => rows[pos.y][pos.x])
                        .Count();
                    if (sum < 4)
                    {
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
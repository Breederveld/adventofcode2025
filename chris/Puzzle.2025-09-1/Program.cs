using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_09_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var tiles = input.Trim().Split("\n").Select(s => s.TrimEnd().Split(',')).Select(arr => (x: int.Parse(arr[0]), y: int.Parse(arr[1]))).ToArray();
            sw.Start();

            (int x0, int y0, int x1, int y1, long size) max = default;
            for (var i = 0; i < tiles.Length; i++)
            {
                for (var j = i + 1; j < tiles.Length; j++)
                {
                    var size = 1L * (Math.Abs(tiles[i].x - tiles[j].x) + 1) * (Math.Abs(tiles[i].y - tiles[j].y) + 1);
                    if (size > max.size)
                    {
                        max.x0 = tiles[i].x;
                        max.y0 = tiles[i].y;
                        max.x1 = tiles[j].x;
                        max.y1 = tiles[j].y;
                        max.size = size;
                    }
                }
            }
            var result = max.size;

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}
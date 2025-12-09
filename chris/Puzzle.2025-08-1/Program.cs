using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_08_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootFolder = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var input = File.ReadAllText(Path.Combine(rootFolder, "input.txt"));

            var sw = new Stopwatch();
            var boxes = input.Trim().Split("\n").Select(s => s.TrimEnd().Split(',')).Select(arr => (x: int.Parse(arr[0]), y: int.Parse(arr[1]), z: int.Parse(arr[2]))).ToArray();
            sw.Start();

            var getDistance = new Func<(int x, int y, int z), (int x, int y, int z), double>((left, right) =>
            {
                return Math.Pow(left.x - right.x, 2) + Math.Pow(left.y - right.y, 2) + Math.Pow(left.z - right.z, 2);
            });

            var distances = new List<(int left, int right, double distance)>();
            for (var i = 0; i < boxes.Length; i++)
            {
                for (var j = i + 1; j < boxes.Length; j++)
                {
                    var distance = getDistance(boxes[i], boxes[j]);
                    distances.Add((i, j, distance));
                }
            }

            var cirquits = new List<HashSet<int>>();
            var connected = 0;
            foreach (var distance in distances.OrderBy(d => d.distance))
            {
                var existing = cirquits.Where(lst => lst.Contains(distance.left) || lst.Contains(distance.right)).ToArray();
                if (existing.Length == 0)
                {
                    cirquits.Add(new HashSet<int> { distance.left, distance.right });
                    connected++;
                }
                else
                {
                    if (existing.Length == 0 && existing[0].Contains(distance.left) && existing[0].Contains(distance.right))
                    {
                        continue;
                    }
                    foreach (var set in existing)
                    {
                        cirquits.Remove(set);
                    }
                    cirquits.Add(existing.SelectMany(set => set).Append(distance.left).Append(distance.right).Distinct().ToHashSet());
                    connected++;
                }
                if (connected == 1000)
                {
                    break;
                }
            }

            var result = cirquits.Select(lst => lst.Distinct().Count()).OrderByDescending(i => i).Take(3).Aggregate(1, (acc, next) => acc * next);

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }
    }
}

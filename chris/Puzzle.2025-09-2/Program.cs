using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzle_2025_09_2
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

            // Re-index the positions.
            var xPositions = tiles.Select(tile => tile.x).Concat([0, 100000]).Distinct().OrderBy(i => i).ToArray();
            var yPositions = tiles.Select(tile => tile.y).Concat([0, 100000]).Distinct().OrderBy(i => i).ToArray();
            var tilesIndexed = tiles.Select(tile => (
                x: xPositions.Select((val, idx) => (val, idx)).TakeWhile(pos => pos.val <= tile.x).Last().idx,
                y: yPositions.Select((val, idx) => (val, idx)).TakeWhile(pos => pos.val <= tile.y).Last().idx))
                .ToArray();

            // Create a grid.
            var grid = new int[xPositions.Length * yPositions.Length];

            // Draw lines.
            var curr = tilesIndexed.Last();
            foreach (var tile in tilesIndexed)
            {
                if (curr.x == tile.x)
                {
                    var min = Math.Min(curr.y, tile.y);
                    var max = Math.Max(curr.y, tile.y);
                    for (var i = min; i < max; i++)
                    {
                        grid[curr.x + i * xPositions.Length] = 1;
                    }
                }
                else
                {
                    var min = Math.Min(curr.x, tile.x);
                    var max = Math.Max(curr.x, tile.x);
                    for (var i = min; i < max; i++)
                    {
                        grid[i + curr.y * xPositions.Length] = 1;
                    }
                }
                curr = tile;
            }

            // Flood fill.
            var todo = new Queue<long>();
            todo.Enqueue(0);
            while (todo.Count > 0)
            {
                var currPos = todo.Dequeue();
                if (grid[currPos] == 0)
                {
                    grid[currPos] = 2;
                    var x = currPos % xPositions.Length;
                    var y = currPos / xPositions.Length;
                    if (x > 0)
                    {
                        todo.Enqueue(x - 1 + y * xPositions.Length);
                    }
                    if (x + 1 < xPositions.Length)
                    {
                        todo.Enqueue(x + 1 + y * xPositions.Length);
                    }
                    if (y > 0)
                    {
                        todo.Enqueue(x + (y - 1) * xPositions.Length);
                    }
                    if (y + 1 < yPositions.Length)
                    {
                        todo.Enqueue(x + (y + 1) * xPositions.Length);
                    }
                }
            }

            var isValid = new Func<int, int, int, int, bool>((leftX, leftY, rightX, rightY) =>
            {
                var minX = Math.Min(leftX, rightX);
                var maxX = Math.Max(leftX, rightX);
                var minY = Math.Min(leftY, rightY);
                var maxY = Math.Max(leftY, rightY);
                for (var y = minY; y <= maxY; y++)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        if (grid[x + y * xPositions.Length] == 2)
                        {
                            return false;
                        }
                    }
                }
                return true;
            });

            var result = 0L;
            for (var i = 0; i < tilesIndexed.Length; i++)
            {
                var tileLeft = tilesIndexed[i];
                for (var j = i + 1; j < tilesIndexed.Length; j++)
                {
                    var tileRight = tilesIndexed[j];
                    var size = 1L * (Math.Abs(xPositions[tileLeft.x] - xPositions[tileRight.x]) + 1) * (Math.Abs(yPositions[tileLeft.y] - yPositions[tileRight.y]) + 1);
                    if (size > result)
                    {
                        if (isValid(tileLeft.x, tileLeft.y, tileRight.x, tileRight.y))
                        {
                            result = size;
                        }
                    }
                }
            }

            sw.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"Took {sw.Elapsed}");
            await Task.FromResult(0);
        }

        private class Grid<T>
        {
            public int[] XPositions { get; }
            public int[] YPositions { get; }
            public T[] Values { get; }

            public Grid(IEnumerable<int> xPositions, IEnumerable<int> yPositions)
            {
                XPositions = xPositions.OrderBy(i => i).Distinct().ToArray();
                YPositions = yPositions.OrderBy(i => i).Distinct().ToArray();
                Values = new T[XPositions.Length * YPositions.Length];
            }

            public IEnumerable<T> GetValues(long posLeft, long posRight)
            {
                var leftX = posLeft % XPositions.Length;
                var leftY = posLeft / XPositions.Length;
                var rightX = posRight % XPositions.Length;
                var rightY = posRight / XPositions.Length;
                for (var x = leftX; x <= rightX; x++)
                {
                    for (var y = leftY; y <= rightY; y++)
                    {
                        var pos = x + y * XPositions.Length;
                        yield return Values[pos];
                    }
                }
            }

            public void SetValueAt(int x, int y, T value)
            {
                Values[GetPosition(x, y)] = value;
            }

            public long GetPosition(int x, int y)
            {
                var xIdx = XPositions.Select((val, idx) => (val, idx)).TakeWhile(pos => pos.val <= x).Last().idx;
                var yIdx = YPositions.Select((val, idx) => (val, idx)).TakeWhile(pos => pos.val <= y).Last().idx;
                return xIdx + yIdx * XPositions.Length;
            }

            public long GetPositionRelative(long position, int nextX, int nextY)
            {
                var xIdx = position % XPositions.Length + nextX;
                var yIdx = position / XPositions.Length + nextY;
                if (xIdx < 0 || xIdx >= XPositions.Length)
                    throw new InvalidOperationException("Invalid position");
                if (yIdx < 0 || yIdx >= YPositions.Length)
                    throw new InvalidOperationException("Invalid position");
                return xIdx + yIdx * XPositions.Length;
            }
        }
    }
}
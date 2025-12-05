with open("day4_input") as f:
    grid = f.read().splitlines()
    grid = [list(line) for line in grid]


def get_neighbour_vals(row, col):
    neighbour_vals = []
    for x in range(-1, 2):
        for y in range(-1, 2):
            new_row = row + x
            new_col = col + y
            if (0 <= new_row < len(grid)) and (0 <= new_col < len(grid[0])) and not (x==0 and y==0):
                neighbour_vals.append(grid[new_row][new_col])
    return neighbour_vals


def is_accessible(row, col):
    neighbour_vals = get_neighbour_vals(row, col)
    return grid[row][col] == "@" and neighbour_vals.count('@') < 4


def get_accessible_coords():
    coords = []
    for row in range(len(grid)):
        for col in range(len(grid[row])):
            if is_accessible(row, col):
                coords.append((row, col))
    return coords


def part1():
    print(f"Part 1: {len(get_accessible_coords())}")


def part2():
    global grid
    total = 0
    while True:
        coords = get_accessible_coords()
        if len(coords) > 0:
            total += len(coords)
            for coord in coords:
                grid[coord[0]][coord[1]] = "."
        else:
            break
    print(f"Part 2: {total}")


part1()
part2()
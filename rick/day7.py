with open('day7_input') as f:
    input = f.read().splitlines()

input = [list(line) for line in input]
start_col = input[0].index('S')


def part1():
    splits = 0
    cols = set([start_col])
    row = 0
    while row < len(input):
        new_cols = set()
        for col in cols:
            if input[row][col] == '^':
                for new_col in [col - 1, col + 1]:
                    if 0 <= new_col <= len(input[row]):
                        new_cols.add(new_col)
                splits += 1
            else:
                new_cols.add(col)
        cols = new_cols
        row += 1
    print(f'Part 1: {splits}')


def find_paths(row, col, path_count_memo: dict) -> int:
    coord_str = f'{row}_{col}'
    if coord_str in path_count_memo:
        return path_count_memo[coord_str]
    elif row == (len(input) - 1):
        return 1
    elif input[row][col] == '^':
        paths_left = find_paths(row, col - 1, path_count_memo)
        paths_right = find_paths(row, col + 1, path_count_memo)
        total = paths_left + paths_right
        path_count_memo[coord_str] = total
        return total
    else:
        return find_paths(row + 1, col, path_count_memo)


def part2():
    path_count_memo = {}
    print(f'Part 2: {find_paths(0, start_col, path_count_memo)}')


part1()
part2()

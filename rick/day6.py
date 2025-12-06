with open('day6_input') as f:
    input = f.read().splitlines()

nums = input[0:-1]
nums_int = [[int(num) for num in line.split()] for line in nums]
ops = input[-1].split()


def calc_col(col_num):
    col_tot = None
    if ops[col_num] == '+':
        col_tot = 0
        for line in nums_int:
            col_tot += line[col_num]
    elif ops[col_num] == '*':
        col_tot = 1
        for line in nums_int:
            col_tot *= line[col_num]
    return col_tot


def part1():
    total = 0
    for col_num in range(len(nums_int[0])):
        total += calc_col(col_num)
    print(f'Part 1: {total}')


def part2():
    new_nums = []
    num_lst = []
    for char_num in range(len(nums[0])):
        num_str = ''.join([line[char_num] for line in nums if line[char_num] != ' '])
        if len(num_str) > 0:
            num_lst.append(int(num_str))
        if num_str == '' or char_num == (len(nums[0]) - 1):
            new_nums.append(tuple(num_lst))
            num_lst.clear()

    total = 0
    for problem, op in enumerate(ops):
        problem_tot = None
        if op == '+':
            problem_tot = 0
            for num in new_nums[problem]:
                problem_tot += num
        elif op == '*':
            problem_tot = 1
            for num in new_nums[problem]:
                problem_tot *= num
        total += problem_tot
    print(f'Part 2: {total}')

# part1()
part2() 

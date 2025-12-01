with open('day1_input') as f:
    input = f.read().splitlines()


def part1():
    zero_count = 0
    position = 50
    for line in input:
        direction = line[0]
        distance = int(line[1:])

        if direction == 'L':
            position -= distance
        elif direction == 'R':
            position += distance
        position = position % 100
        if position == 0:
            zero_count += 1
    print(f'Password 1: {zero_count}')

def part2():
    zero_count = 0
    position = 50
    for line in input:
        direction = line[0]
        distance = int(line[1:])

        if direction == 'L':
            for times in range(distance):
                position -= 1
                position = position % 100
                if position == 0:
                    zero_count += 1
        elif direction == 'R':
            for times in range(distance):
                position += 1
                position = position % 100
                if position == 0:
                    zero_count += 1

    print(f'Password 2: {zero_count}')

part1()
part2()
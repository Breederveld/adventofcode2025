with open('day3_input') as f:
    input = f.read().splitlines()


def get_joltage(bank: str) -> int:
    bank = [int(val) for val in list(bank)]
    digit1 = max(bank[0:-1])
    index1 = bank.index(digit1)
    digit2 = max(bank[index1+1:])
    return digit1 * 10 + digit2


def get_joltage_gen(bank: str) -> int:
    bank = [int(val) for val in list(bank)]
    digits_togo = 12
    min_index = 0
    digits = []
    while digits_togo > 0:
        digit = max(bank[min_index:len(bank)-digits_togo + 1])
        digits.append(digit)
        digit_index = bank[min_index:len(bank)-digits_togo + 1].index(digit) + min_index
        min_index = digit_index + 1
        digits_togo -= 1
    return sum([v*(10**(12-i-1)) for i, v in enumerate(digits)])


def part1():
    total = sum([get_joltage(bank) for bank in input])
    print(f'Total part 1: {total}')


def part2():
    total = sum([get_joltage_gen(bank) for bank in input])
    print(f'Total part 2: {total}')


#part1()
part2()
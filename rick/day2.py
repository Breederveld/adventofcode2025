with open('day2_input') as f:
    input = f.read()
    input = [rng.split("-") for rng in input.split(",")]


def is_valid_pt1(id):
    id_str = str(id)
    id_str_len = len(id_str)

    if id_str_len % 2 != 0:
        return False
    else:
        mid_index = id_str_len // 2
        id_str_lst = list(id_str)
        p1 = ''.join(id_str_lst[0:mid_index])
        p2 = ''.join(id_str_lst[mid_index:])
        return p1 == p2


def is_valid_pt2(id):
    id_str = str(id)
    id_str_lst = list(id_str)

    for i in range(1, (len(id_str_lst) // 2) + 1):
        pat = ''.join(id_str_lst[0:i])
        remaining = ''.join(id_str_lst[i:])
        if len(remaining) % len(pat) != 0:
            continue
        else:
            if remaining == pat * (len(remaining) // len(pat)):
                return True
    return False


def main():
    total1 = 0
    total2 = 0
    for rng in input:
        for id in range(int(rng[0]), int(rng[1]) + 1):
            if is_valid_pt1(id):
                total1 += id
            if is_valid_pt2(id):
                total2 += id

    print(f'Part 1: {total1} | Part 2: {total2}')


if __name__ == '__main__':
    main()
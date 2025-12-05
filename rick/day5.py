with open('day5_input') as f:
    input = f.read()

ranges, ids = input.split('\n\n')
ranges = ranges.splitlines()
ranges = [tuple(map(int, rng.split('-'))) for rng in ranges]

ids = ids.splitlines()
ids = [int(id) for id in ids]

def is_fresh(id):
    for rng in ranges:
        if rng[0] <= id <= rng[1]:
            return True
    return False


def part1():
    fresh_count = 0
    for id in ids:
        if is_fresh(id):
            fresh_count += 1
    print(f'Part 1: {fresh_count}')


def part2():
    global ranges
    ranges = sorted(ranges, key=lambda rng: rng[0])
    ranges_nw = []
    rng_new_cur = None
    for i in range(len(ranges)):
        rng = list(ranges[i])
        if i == 0:
            rng_new_cur = rng
            continue
        elif rng[0] > (rng_new_cur[1] + 1): # Not contig
            ranges_nw.append(rng_new_cur)
            rng_new_cur = rng
        else:
            rng_new_cur[1] = max(rng[1], rng_new_cur[1])
    ranges_nw.append(rng_new_cur)

    ranges_nw_lens = [rng[1] - rng[0] + 1 for rng in ranges_nw]
    print(f'Part 2: {sum(ranges_nw_lens)}')



    """
    |----------|
        |-----------|
            |---|
                   |-------|
    
    :return: 
    """

#part1()
part2()

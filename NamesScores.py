import sys

with open("p022_names.txt") as f:
    words = f.read().replace('\"', '').split(',')

words.sort()

sum = 0
for i, word in enumerate(words):
    value = 0
    for char in word:
        value += 1 + (ord(char) - ord('A'))

    sum += value * (i + 1)

print sum


# Banker's Algorithm - Test Cases

## Test Case 1: Safe State

Processes: 5
Resources: 3

Total Resources:
10 5 7

Allocation Matrix:
0 1 0
2 0 0
3 0 2
2 1 1
0 0 2

Maximum Matrix:
7 5 3
3 2 2
9 0 2
2 2 2
4 3 3

Expected Result:
System is in a SAFE state.

One Safe Sequence:
P1 P3 P4 P0 P2

## Test Case 2: Unsafe State

Processes: 2
Resources: 1

Total Resources:
3

Allocation Matrix:
1
1

Maximum Matrix:
3
3

Expected Result:
System is in an UNSAFE state.
No safe execution sequence exists.

# Let's explore Mars with Microsoft .Net 4.6 -> C# CLI !

## To launch the app

- you need to be on Windows

- run the MarsApp.exe in MarsApp/binary with a CLI with the following CL: MarsApp.exe Direction.txt

- to run with another dataset, use the following CL, where the parameter is the 'path' of your file (.txt): MarsApp.exe [path]

- the main file is MarsApp/Program.cs

## Example of data needed:

5 5

1 2 N

LMLMLMLMM

3 3 E

MMRMMRMRRM


## Explanation:

5 5 : size of your map : x y

L = left, R = right, M = move (forward), N = north, E = east, S = south, W = west

1 2 N : position of the first rover

LMLMLMLMM : movement of the first rover

3 3 E : position of the second rover

MMRMMRMRRM : movement of the second rover

Result:

1 3 N

5 1 E


using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("==================================");
        Console.WriteLine("     BANKER'S ALGORITHM SIMULATOR");
        Console.WriteLine("==================================");

        int processes = ReadPositiveNumber("Enter number of processes: ");
        int resources = ReadPositiveNumber("Enter number of resources: ");

        int[] total = new int[resources];

        Console.WriteLine("\nEnter total resources:");
        for (int j = 0; j < resources; j++)
        {
            total[j] = ReadNonNegativeNumber("Resource " + j + ": ");
        }

        int[,] allocation = new int[processes, resources];
        int[,] maximum = new int[processes, resources];

        Console.WriteLine("\nEnter Allocation Matrix:");
        ReadMatrix(allocation, processes, resources);

        Console.WriteLine("\nEnter Maximum Matrix:");
        ReadMatrix(maximum, processes, resources);

        bool valid = true;

        for (int i = 0; i < processes; i++)
        {
            for (int j = 0; j < resources; j++)
            {
                if (allocation[i, j] > maximum[i, j])
                    valid = false;
            }
        }

        int[] available = BankerAlgorithm.CalculateAvailable(total, allocation);

        for (int j = 0; j < resources; j++)
        {
            if (available[j] < 0)
                valid = false;
        }

        if (!valid)
        {
            Console.WriteLine("\nERROR: Invalid input data.");
            Console.WriteLine("Allocation cannot exceed Maximum or Total resources.");
            return;
        }

        int[,] need = BankerAlgorithm.CalculateNeed(maximum, allocation);

        Console.WriteLine("\n========== RESULTS ==========");

        PrintMatrix("Allocation Matrix", allocation, processes, resources);
        PrintMatrix("Maximum Matrix", maximum, processes, resources);
        PrintMatrix("Need Matrix", need, processes, resources);

        Console.WriteLine("\nAvailable Vector:");
        PrintVector(available);

        List<int> safeSequence;
        List<int> unfinished;

        bool safe = BankerAlgorithm.FindSafeSequence(
            allocation,
            need,
            available,
            out safeSequence,
            out unfinished
        );

        Console.WriteLine("\n========== SYSTEM STATUS ==========");

        if (safe)
        {
            Console.WriteLine("System is in a SAFE state.");

            Console.WriteLine("Safe Execution Sequence:");

            foreach (int p in safeSequence)
            {
                Console.Write("P" + p + " ");
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("System is in an UNSAFE state.");

            Console.WriteLine("No safe execution sequence was found.");

            Console.WriteLine("Unfinished processes:");

            foreach (int p in unfinished)
            {
                Console.Write("P" + p + " ");
            }

            Console.WriteLine();

            Console.WriteLine("These processes cannot all finish");
            Console.WriteLine("in a safe sequence with the available resources.");
            Console.WriteLine("This does not necessarily mean a deadlock has occurred.");
        }
    }

    static int ReadPositiveNumber(string message)
    {
        int value;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                return value;

            Console.WriteLine("Please enter a positive integer.");
        }
    }

    static int ReadNonNegativeNumber(string message)
    {
        int value;

        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out value) && value >= 0)
                return value;

            Console.WriteLine("Please enter a non-negative integer.");
        }
    }

    static void ReadMatrix(int[,] matrix, int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = ReadNonNegativeNumber(
                    "P" + i + ", Resource " + j + ": "
                );
            }
        }
    }

    static void PrintMatrix(string title, int[,] matrix, int rows, int columns)
    {
        Console.WriteLine("\n" + title + ":");

        for (int i = 0; i < rows; i++)
        {
            Console.Write("P" + i + " | ");

            for (int j = 0; j < columns; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }

            Console.WriteLine();
        }
    }

    static void PrintVector(int[] vector)
    {
        foreach (int value in vector)
        {
            Console.Write(value + " ");
        }

        Console.WriteLine();
    }
}

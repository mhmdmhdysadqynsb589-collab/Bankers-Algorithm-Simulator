
using System;
using System.Collections.Generic;

public class BankerAlgorithm
{
    public static int[,] CalculateNeed(
        int[,] maximum,
        int[,] allocation)
    {
        int processes = maximum.GetLength(0);
        int resources = maximum.GetLength(1);

        int[,] need = new int[processes, resources];

        for (int i = 0; i < processes; i++)
        {
            for (int j = 0; j < resources; j++)
            {
                need[i, j] = maximum[i, j] - allocation[i, j];
            }
        }

        return need;
    }

    public static int[] CalculateAvailable(
        int[] total,
        int[,] allocation)
    {
        int processes = allocation.GetLength(0);
        int resources = allocation.GetLength(1);

        int[] available = new int[resources];

        for (int j = 0; j < resources; j++)
        {
            int allocated = 0;

            for (int i = 0; i < processes; i++)
            {
                allocated += allocation[i, j];
            }

            available[j] = total[j] - allocated;
        }

        return available;
    }

    public static bool FindSafeSequence(
        int[,] allocation,
        int[,] need,
        int[] available,
        out List<int> safeSequence,
        out List<int> unfinishedProcesses)
    {
        int processes = allocation.GetLength(0);
        int resources = allocation.GetLength(1);

        int[] work = (int[])available.Clone();
        bool[] finish = new bool[processes];

        safeSequence = new List<int>();

        bool found;

        do
        {
            found = false;

            for (int i = 0; i < processes; i++)
            {
                if (finish[i])
                    continue;

                bool canExecute = true;

                for (int j = 0; j < resources; j++)
                {
                    if (need[i, j] > work[j])
                    {
                        canExecute = false;
                        break;
                    }
                }

                if (canExecute)
                {
                    for (int j = 0; j < resources; j++)
                    {
                        work[j] += allocation[i, j];
                    }

                    finish[i] = true;
                    safeSequence.Add(i);
                    found = true;
                }
            }

        } while (found);

        unfinishedProcesses = new List<int>();

        for (int i = 0; i < processes; i++)
        {
            if (!finish[i])
            {
                unfinishedProcesses.Add(i);
            }
        }

        return safeSequence.Count == processes;
    }
}

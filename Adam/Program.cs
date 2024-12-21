using NinePlanFailedLibrary;
using static System.Console;

namespace Adam;

internal class Program
{
    private static void Main()
    {
        RunAll();
    }

    private static void RunAll()
    {
        var maxIteration = 0;

        var iterationsFound = new List<int>();

        for (var i = 1000; i <= 9999; i++)
        {
            if (Kaprekar.AllCharactersSame(i.ToString()))
            {
                continue;
            }

            _ = Kaprekar.TestTerm(i, out var iterations);

            if (!iterationsFound.Contains(iterations))
            {
                WriteLine($"Found iteration of size: {iterations} - {i}");
                iterationsFound.Add(iterations);
            }

            if (iterations <= maxIteration)
            {
                continue;
            }

            maxIteration = iterations;
            WriteLine($"Best so far: {i}, required {maxIteration} iterations");
        }
    }

    // ReSharper disable once UnusedMember.Local
    private static void RunOnce()
    {
        const int Seed = 1459;

        var res = Kaprekar.TestTerm(Seed, out _);

        WriteLine(res); // 9
    }
}
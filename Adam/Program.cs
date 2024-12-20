using NinePlanFailedLibrary;

namespace Adam;

internal class Program
{
    private static void Main()
    {
        const int Seed = 1459;

        var res = Kaprekar.TestTerm(Seed);

        Console.WriteLine(res); // 9
    }
}
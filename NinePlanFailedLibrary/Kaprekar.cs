namespace NinePlanFailedLibrary;

public static class Kaprekar
{
    public static int TestTerm(int term)
    {
        const int Target = 6174;

        ValidateTerm(term);

        var res = term;

        while (res != Target)
        {
            var parts = SortDigits(res);

            var lhs = parts.Item1;
            var rhs = parts.Item2;

            if (lhs < 1000)
            {
                lhs *= 10;
            }

            res = lhs - rhs;

        }

        return SumDigits(SumDigits(res));
    }

    public static int SumDigits(int number)
    {
        var sum = 0;

        while (number != 0)
        {
            sum += number % 10;
            number /= 10;
        }

        return sum;
    }

    public static (int, int) SortDigits(int number)
    {
        var numString = number.ToString();
        var highToLow = new string(numString.OrderByDescending(c => c).ToArray());
        var lowToHigh = new string(numString.OrderBy(c => c).ToArray());
        var highToLowNumber = int.Parse(highToLow);
        var lowToHighNumber = int.Parse(lowToHigh);

        return (highToLowNumber, lowToHighNumber);
    }

    public static bool AllCharactersSame(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        var firstChar = input[0];

        return input.All(c => c == firstChar);
    }

    private static void ValidateTerm(int term)
    {
        if (TermIsInvalid(term))
        {
            throw new InvalidOperationException("Invalid term");
        }

        if (AllCharactersSame(term.ToString()))
        {
            throw new ArgumentException("Nine plan failed");
        }
    }

    private static bool TermIsInvalid(int term)
    {
        const int BottomEnd = 1000;
        const int TopEnd = 9999;

        return (term is < BottomEnd or > TopEnd);
    }

    
}
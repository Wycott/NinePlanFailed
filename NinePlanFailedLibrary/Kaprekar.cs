namespace NinePlanFailedLibrary;

/// <summary>
/// Implements Kaprekar's routine for 4-digit numbers.
/// Repeatedly arranging a number's digits in descending then ascending order
/// and subtracting the two converges on Kaprekar's constant, 6174, in at most
/// seven iterations for any 4-digit number whose digits are not all identical.
/// </summary>
public static class Kaprekar
{
    private const int Target = 6174;
    private const int BottomEnd = 1000;
    private const int TopEnd = 9999;

    // Kaprekar's routine for 4-digit numbers converges to 6174 in at most 7 steps.
    // The cap turns a hypothetical non-converging input into an observable error
    // rather than an infinite loop.
    private const int MaxIterations = 7;

    /// <summary>
    /// Runs Kaprekar's routine on <paramref name="term"/> until it reaches 6174.
    /// </summary>
    /// <param name="term">A 4-digit number (1000-9999) whose digits are not all identical.</param>
    /// <returns>The number of iterations taken and the repeated digit root of 6174 (always 9).</returns>
    /// <exception cref="InvalidOperationException">The term is outside 1000-9999, or the routine failed to converge.</exception>
    /// <exception cref="ArgumentException">The term is a repdigit (all digits identical).</exception>
    public static KaprekarResult Run(int term)
    {
        ValidateTerm(term);

        var res = term;
        var iteration = 0;

        while (res != Target)
        {
            if (++iteration > MaxIterations)
            {
                throw new InvalidOperationException($"Kaprekar's routine did not converge for term {term}");
            }

            var (high, low) = SortDigits(res);

            // Sorting digits descending can drop a leading zero (e.g. 1000 -> "1000" = 1000,
            // but 2000 -> descending "2000" = 2000 while numbers like 1110 -> "1110").
            // When the high value lost its thousands digit, re-pad it so the subtraction
            // operates on the intended 4-digit magnitude.
            if (high < BottomEnd)
            {
                high *= 10;
            }

            res = high - low;
        }

        return new KaprekarResult(SumDigits(SumDigits(res)), iteration);
    }

    /// <summary>
    /// Backwards-compatible wrapper around <see cref="Run"/> that exposes the iteration
    /// count through an out parameter and returns the digit root.
    /// </summary>
    public static int TestTerm(int term, out int iteration)
    {
        var result = Run(term);

        iteration = result.Iterations;

        return result.DigitRoot;
    }

    /// <summary>
    /// Sums the decimal digits of <paramref name="number"/>.
    /// </summary>
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

    /// <summary>
    /// Returns the value formed by arranging the digits of <paramref name="number"/>
    /// in descending order (High) and ascending order (Low).
    /// </summary>
    public static (int High, int Low) SortDigits(int number)
    {
        // Extract exactly four digits. Inputs are constrained to 1000-9999 at the
        // public entry points, but we read four positions so leading zeros that
        // appear mid-routine (e.g. 0999) are preserved in the ordering.
        Span<int> digits = stackalloc int[4];

        for (var i = 0; i < digits.Length; i++)
        {
            digits[i] = number % 10;
            number /= 10;
        }

        digits.Sort();

        // Ascending: digits[0..3] low-to-high. Descending: the reverse.
        var low = 0;
        var high = 0;

        for (var i = 0; i < digits.Length; i++)
        {
            low = (low * 10) + digits[i];
            high = (high * 10) + digits[digits.Length - 1 - i];
        }

        return (high, low);
    }

    /// <summary>
    /// Returns true when every character in <paramref name="input"/> is identical.
    /// An empty or null string returns false (treated as "not a repdigit").
    /// </summary>
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
        return term is < BottomEnd or > TopEnd;
    }
}

/// <summary>
/// The outcome of running Kaprekar's routine: the repeated digit root of the
/// result (always 9 for 6174) and the number of iterations required to converge.
/// </summary>
public readonly record struct KaprekarResult(int DigitRoot, int Iterations);

namespace NinePlanFailedLibrary.Test;

public class KaprekarTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(999)]
    [InlineData(10000)]
    public void OutOfRangeTermThrowsException(int term)
    {
        var message = Assert.Throws<InvalidOperationException>(() => Kaprekar.TestTerm(term, out _));

        Assert.Equal("Invalid term", message.Message);
    }

    [Theory]
    [InlineData(1111)]
    [InlineData(2222)]
    [InlineData(3333)]
    [InlineData(4444)]
    [InlineData(5555)]
    [InlineData(6666)]
    [InlineData(7777)]
    [InlineData(8888)]
    [InlineData(9999)]
    public void InvalidTermsThrowException(int term)
    {
        var message = Assert.Throws<ArgumentException>(() => Kaprekar.TestTerm(term, out _));

        Assert.Equal("Nine plan failed", message.Message);
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void TestAllValidTerms(int term)
    {
        const int ExpectedValue = 9;

        var ret = Kaprekar.TestTerm(term, out _);

        Assert.Equal(ExpectedValue, ret);
    }

    public static IEnumerable<object[]> GetTestData()
    {
        for (var i = 1000; i <= 9999; i++)
        {
            if (!Kaprekar.AllCharactersSame(i.ToString()))
            {
                yield return new object[]
                    { i };
            }
        }
    }
}
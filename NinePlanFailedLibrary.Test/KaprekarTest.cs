using NinePlanFailedLibrary;

namespace NinePlanFailed.Test;

public class KaprekarTest
{
    [Fact]
    public void LowTermThrowsException()
    {
        var message = Assert.Throws<InvalidOperationException>(() => Kaprekar.TestTerm(999));

        Assert.Equal("Invalid term", message.Message);
    }

    [Fact]
    public void HighTermThrowsException()
    {
        var message = Assert.Throws<InvalidOperationException>(() => Kaprekar.TestTerm(10000));

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
        var message = Assert.Throws<ArgumentException>(() => Kaprekar.TestTerm(term));

        Assert.Equal("Nine plan failed", message.Message);
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void TestAllValidTerms(int term)
    {
        const int ExpectedValue = 9;

        var ret = Kaprekar.TestTerm(term);

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
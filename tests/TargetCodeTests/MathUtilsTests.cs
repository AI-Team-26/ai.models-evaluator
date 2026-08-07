using NUnit.Framework;
using TargetCode;

[TestFixture]
public sealed class MathUtilsTests
{
    [Test]
    public void SumRange_single_element_returns_itself()
    {
        var result = MathUtils.SumRange(5, 5);
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void SumRange_basic_range_includes_end()
    {
        var result = MathUtils.SumRange(1, 5);
        Assert.That(result, Is.EqualTo(1 + 2 + 3 + 4 + 5)); // 15
    }

    [Test]
    public void SumRange_negative_to_positive()
    {
        var result = MathUtils.SumRange(-2, 2);
        Assert.That(result, Is.EqualTo(-2 + -1 + 0 + 1 + 2)); // 0
    }
}

using NUnit.Framework;
using TargetCode;

[TestFixture]
public sealed class SafeProductTests
{
    [Test]
    public void SafeProduct_small_numbers_correct_result()
    {
        var result = MathUtils.SafeProduct(2, 3, 4);
        Assert.That(result, Is.EqualTo(24L));
    }

    [Test]
    public void SafeProduct_large_values_no_overflow()
    {
        // 10_000 * 10_000 * 10_000 = 1_000_000_000_000 — fits in long
        var result = MathUtils.SafeProduct(10_000, 10_000, 10_000);
        Assert.That(result, Is.EqualTo(1_000_000_000_000L));
    }

    [Test]
    public void SafeProduct_exceeds_int_max_still_accurate()
    {
        // 50_000^3 = 125_000_000_000_000 which exceeds Int32.MaxValue (~2.1B)
        // Must use long arithmetic internally or overflow wraps around
        var result = MathUtils.SafeProduct(50_000, 50_000, 50_000);
        Assert.That(result, Is.EqualTo(125_000_000_000_000L));
    }

    [Test]
    public void SafeProduct_single_number_returns_itself()
    {
        var result = MathUtils.SafeProduct(7);
        Assert.That(result, Is.EqualTo(7L));
    }

    [Test]
    public void SafeProduct_zero_includes_zero_product()
    {
        var result = MathUtils.SafeProduct(5, 0, 9);
        Assert.That(result, Is.EqualTo(0L));
    }
}

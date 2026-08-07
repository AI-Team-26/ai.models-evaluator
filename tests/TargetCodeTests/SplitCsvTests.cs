using NUnit.Framework;
using TargetCode;

[TestFixture]
public sealed class SplitCsvTests
{
    [Test]
    public void SplitCsv_simple_fields_returns_array()
    {
        var result = MathUtils.SplitCsv("a,b,c");
        Assert.That(result, Is.EqualTo(new[] { "a", "b", "c" }));
    }

    [Test]
    public void SplitCsv_quoted_field_with_comma_keeps_intact()
    {
        var result = MathUtils.SplitCsv("\"hello,world\",foo,\"bar\"");
        Assert.That(result, Is.EqualTo(new[] { "\"hello,world\"", "foo", "\"bar\"" }));
    }

    [Test]
    public void SplitCsv_single_field_returns_one_element()
    {
        var result = MathUtils.SplitCsv("onlyone");
        Assert.That(result, Is.EqualTo(new[] { "onlyone" }));
    }

    [Test]
    public void SplitCsv_empty_string_returns_empty_array()
    {
        var result = MathUtils.SplitCsv("");
        Assert.That(result.Length, Is.Zero);
    }

    [Test]
    public void SplitCsv_whitespace_trimmed_outside_quotes()
    {
        var result = MathUtils.SplitCsv(" x , y ");
        Assert.That(result, Is.EqualTo(new[] { "x", "y" }));
    }
}

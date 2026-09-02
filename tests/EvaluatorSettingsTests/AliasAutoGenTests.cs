using Evaluator.Settings;
using NUnit.Framework;

namespace EvaluatorSettingsTests;

[TestFixture]
public sealed class AliasAutoGenTests
{
    [Test]
    public void AutoAliasFromGguf_StripsExtension()
    {
        var alias = SettingsView.AutoAliasFromGguf("qwen2.5-7b-instruct-q4_k_m.gguf");
        Assert.That(alias, Is.EqualTo("qwen2.5-7b-instruct-q4_k_m"));
    }

    [Test]
    public void AutoAliasFromGguf_HandlesNoExtension()
    {
        var alias = SettingsView.AutoAliasFromGguf("llama-3");
        Assert.That(alias, Is.EqualTo("llama-3"));
    }

    [Test]
    public void AutoAliasFromGguf_HandlesPath()
    {
        // Path.GetFileNameWithoutExtension extracts the last path segment
        var alias = SettingsView.AutoAliasFromGguf("/data/models/qwen-7b.gguf");
        Assert.That(alias, Is.EqualTo("qwen-7b"));
    }

    [Test]
    public void AutoAliasFromGguf_EmptyInputReturnsEmpty()
    {
        Assert.That(SettingsView.AutoAliasFromGguf(""), Is.EqualTo(""));
        Assert.That(SettingsView.AutoAliasFromGguf("   "), Is.EqualTo(""));
        Assert.That(SettingsView.AutoAliasFromGguf(null!), Is.EqualTo(""));
    }
}

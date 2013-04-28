#region Usings

using NUnit.Framework;

#endregion


[TestFixture]
public class TestInPluginsPrimer
{
    // NOTE: intentionally erroneous test
    [Test]
    public void CanCombineTestsWithAndOperator()
    {
        // NOTE: forced failure result
        Assert.That(42, Is.GreaterThan(40) & Is.LessThan(40));
    }
}

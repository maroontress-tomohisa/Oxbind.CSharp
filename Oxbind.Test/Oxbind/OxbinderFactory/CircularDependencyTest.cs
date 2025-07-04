namespace Maroontress.Oxbind.Test.Oxbind.OxbinderFactory;

using Maroontress.Oxbind;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public sealed class CircularDependencyTest
{
    [TestMethod]
    public void RootTest()
    {
        const string message = """
            Root has a circular dependency: Root -> Alpha -> Beta -> Root
            """;
        var factory = new OxbinderFactory();
        Checks.ThrowBindException(
            () => _ = factory.Of<Root>(),
            "Of<Root>()",
            message);
    }

    [ForElement("root")]
    public record class Root(
        [Required] Alpha FirstChild);

    [ForElement("alpha")]
    public record class Alpha(
        [Required] Beta FirstChild);

    [ForElement("beta")]
    public record class Beta(
        [Required] /*!?*/ Root FirstChild);
}

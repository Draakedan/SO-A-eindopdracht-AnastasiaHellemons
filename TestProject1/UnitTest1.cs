using avansDevOps;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var result = Class1.HelloWorld();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("Hello, World!"));
        }
    }
}
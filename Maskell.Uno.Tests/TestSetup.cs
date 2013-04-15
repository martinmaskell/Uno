using Maskell.Uno.Helpers;
using NUnit.Framework;

namespace Maskell.Uno.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
        [SetUp]
        public void Setup()
        {
            AutofacResolver.Init();
        }

    }
}

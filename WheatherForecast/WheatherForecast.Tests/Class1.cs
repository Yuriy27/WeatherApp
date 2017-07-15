using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WheatherForecast.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestTest()
        {
            Assert.That(4, Is.EqualTo(2 + 2));            
        }
    }
}

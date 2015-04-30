using System.Threading.Tasks;
using Mvc.Extras;
using NUnit.Framework;

namespace Mvc.Tests
{
    [TestFixture]
    public class TestableAsyncEventsTests
    {
        [Test]
        public async void MyButtonActionAsyncDoesNotCrash()
        {
            //arrange
            var buttonActionTask = new TestableAsyncEvents().MyButtonActionAsync();

            //act
            await buttonActionTask;

            //assert
            Assert.That(buttonActionTask.Status, Is.EqualTo(TaskStatus.RanToCompletion));
        }
    }
}

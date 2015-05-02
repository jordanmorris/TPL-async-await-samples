using System.Threading.Tasks;
using Mvc.Extras;
using Xunit;

namespace Tests
{
    public class TestableAsyncEventsXUnitTests
    {
        [Fact]
        public async void MyButtonActionAsyncDoesNotCrashWithXUnit()
        {
            //arrange
            var buttonActionTask = new TestableAsyncEvents().MyButtonActionAsync();

            //act
            await buttonActionTask;

            //assert
            Assert.Equal(buttonActionTask.Status, TaskStatus.RanToCompletion);
        }
    }
}

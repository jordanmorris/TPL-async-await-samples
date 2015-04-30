using System.Threading.Tasks;

namespace Mvc.Extras
{
    public class TestableAsyncEvents
    {
        public async void MyButtonClickAsync()
        {
            await MyButtonActionAsync();
        }

        public async Task MyButtonActionAsync()
        {
            //do logic ..
            await Task.Delay(1000)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
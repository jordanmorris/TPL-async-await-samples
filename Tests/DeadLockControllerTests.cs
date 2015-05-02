using System.Web.Mvc;
using Mvc.Controllers;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DeadLockControllerTests
    {
        [Test]
        public void DeadLockControllerIndexActionRedirectsWithExpectedOutput()
        {
            //arrange
            var controller = new DeadLockController();

            //act
            var actionResult = (RedirectToRouteResult)controller.Index();

            //assert
            Assert.That(actionResult.RouteValues["Output"], Is.EqualTo("Success"));
        }
    }
}

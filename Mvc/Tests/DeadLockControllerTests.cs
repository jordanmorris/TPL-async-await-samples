using System.Web.Mvc;
using Mvc.Controllers;
using NUnit.Framework;

namespace Mvc.Tests
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
            var actionResult = controller.Index() as RedirectToRouteResult;

            //assert
            // ReSharper disable once PossibleNullReferenceException
            // justification: a null reference is a legit way for this test to fail
            Assert.That(actionResult.RouteValues["Output"], Is.EqualTo("Success"));
        }
    }
}

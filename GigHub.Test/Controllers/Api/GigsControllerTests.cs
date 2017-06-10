
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GigHub.Test.Controllers.Api
{
    /// <summary>
    /// Summary description for GigsControllerTests
    /// </summary>
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _gigsController;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _gigsController = new GigsController(mockUow.Object);
            _userId = "1";
            _gigsController.MockCurrnetUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
           var result =  _gigsController.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig{ArtistId = _userId+"-"};

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _gigsController.Cancel(1);

            result.Should().BeOfType<OkResult>();
        }
    }
}

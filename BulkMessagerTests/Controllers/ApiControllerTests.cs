using AutoMapper;
using BulkMessager.Controllers;
using BulkMessager.Data.Entities;
using BulkMessager.Dtos;
using BulkMessager.Services;
using BulkMessager.Utils;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BulkMessagerTests.Controllers {

    public class ApiControllerTests {

        private readonly IMapper _mapper;
        private readonly IApplicationLogger<MessagingController> _logger;
        private readonly IMessageService _service; 
        private readonly MessagingController _controller;

        public ApiControllerTests() {
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<IApplicationLogger<MessagingController>>();
            _service = A.Fake<IMessageService>();

            //..create a controller instance
            _controller = new MessagingController(_logger, _mapper, _service);
        }

        [Fact]
        public void MessagingController_Welcome_ReturnsMessage() {

            #region Arrange

            A.CallTo(()=>_service.SayHello()).Returns("Hello send from Messaging Service");

            #endregion

            #region Act

            IActionResult result = _controller.Welcome(); 
            var okResult = (OkObjectResult)result;
            var content = okResult.Value;

            #endregion

            #region Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>(); 
            content.Should().Be("Hello send from Messaging Service");

            #endregion
        }

        [Theory]
        [InlineData(1)]
        public void MessagingController_GetMessageById_Returns_OK(long id) {

            #region Arrange

            A.CallTo(()=>_service.ExistsAsync(id)).Returns(Task.FromResult(true));
            A.CallTo(()=>_service.FindMessageAsync(id)).Returns(Task.FromResult(new Message { Id = id }));

            #endregion

            #region Act
            var result = _controller.GetMessageById(id).Result;
            var statusObj = (OkObjectResult)result;
            var content = statusObj.StatusCode;
            #endregion

            #region Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            content.Should().Be(200);
            #endregion
        }

        [Theory]
        [InlineData(1)]
        public void MessagingController_GetMessageById_Returns_NOTFOUND(long id) {

            #region Arrange

             A.CallTo(()=>_service.ExistsAsync(id)).Returns(Task.FromResult(false));
             A.CallTo(()=>_service.FindMessageAsync(id)).Returns(Task.FromResult(new Message { Id = 2 }));

            #endregion

            #region Act
            var result =  _controller.GetMessageById(id).Result;
            var statusObj = (NotFoundResult)result;
            var content = statusObj.StatusCode;
            #endregion

            #region Assert
            
            result.Should().BeOfType<NotFoundResult>();
            content.Should().Be(404);

            #endregion
        }

        [Fact]
        public void MessagingController_GetMessages_Returns_OK() { 

            #region Arrange
             var redords = A.Fake<IList<Message>>();
             A.CallTo(()=>_service.GetAllMessagesAsync()).Returns(Task.FromResult(redords));

            #endregion

            #region Act

            var result = _controller.GetMessages().Result;
            var statusObj = (OkObjectResult)result;
            var content = statusObj.StatusCode;

            #endregion

            #region Assert
            
            result.Should().BeOfType<OkObjectResult>();
            content.Should().Be(200);

            #endregion

        }

        [Fact]
        public void MessagingController_CreateMessage_Returns_OK() { 
            
            #region Arrange
            var messageDto = new MessageDto(){
                Id = 1,
                Message = "Fake SMS message",
                StartSending = new DateTime(2024, 04, 23),
                StopSending = new DateTime(2024, 04, 25),
                MessageInterval = "Monthly",
                Status = "New",
                IntervalStatus = "None",
                LastSent = DateTime.UtcNow,
                AddedBy = "Marc",
                MessageApproved = "YES",
                MessageDeleted = "NO"
            };
            
            var message = new Message();

            //fake mapper
            A.CallTo(() => _mapper.Map<Message>(messageDto));

            //..create object
            A.CallTo(()=>_service.CreatMessageAsync(message)).Returns(Task.FromResult(true));

            #endregion

            #region Act

            var result = _controller.CreateMessage(messageDto).Result;
            var statusObj = (OkObjectResult)result;
            var content = statusObj.StatusCode;

            #endregion

            #region Assert
            
            result.Should().BeOfType<OkObjectResult>();
            content.Should().Be(200);

            #endregion
        }

    }
}
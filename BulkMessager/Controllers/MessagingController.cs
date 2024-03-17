using AutoMapper;
using BulkMessager.Data.Entities;
using BulkMessager.Dtos;
using BulkMessager.Services;
using BulkMessager.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BulkMessager.Controllers {

    [ApiController]
    [Route("sms")]    
    public class MessagingController : Controller {

        private readonly IMapper _mapper;
        private readonly IApplicationLogger<MessagingController> _logger;
        private readonly IMessageService _service;

        public MessagingController(IApplicationLogger<MessagingController> logger, IMapper mapper, IMessageService service) {
            _logger = logger;
            _logger.Channel = "API_CONTROLLER";
            _mapper = mapper;
            _service = service;
        }

        [HttpGet("/")]
        public IActionResult Welcome() {
            _logger.LogInfo("Calling the Welcome endpoint..");
            var msg = _service.SayHello();
            return Ok(msg);
        }

        [HttpGet]
        public IList<MessageDto> GetMessages() {
            _logger.LogInfo("Retrieving a list of messages");
            var messages = new List<Message>{ 
                new() {
                    Id=1, 
                    Text = "Your daily Balance statement as seen by us",
                    Duration = MessageDuration.Running,
                    Interval = Interval.Daily,
                    IntervalStatus = IntervalStatus.Sent,
                    RunFrom = new DateTime(2024,01,15),
                    AddedOn = new DateTime(2025,01,01),
                    IsApproved = true,
                    IsDeleted = false,
                },
                new() {
                    Id=1, 
                    Text = "Daily manifestation",
                    Duration = MessageDuration.Running,
                    Interval = Interval.Daily,
                    IntervalStatus = IntervalStatus.Sent,
                    RunFrom = new DateTime(2024,01,15),
                    AddedOn = new DateTime(2025,01,01),
                    IsApproved = true,
                    IsDeleted = false,
                },
                new() {
                    Id=1, 
                    Text = "Nutrition suggestions for a day",
                    Duration = MessageDuration.Running,
                    Interval = Interval.Daily,
                    IntervalStatus = IntervalStatus.Sent,
                    RunFrom = new DateTime(2024,01,15),
                    AddedOn = new DateTime(2025,01,01),
                    IsApproved = true,
                    IsDeleted = false,
                }
            };

            _logger.LogInfo("Show all retrieved messages");
            return messages.Select(_mapper.Map<MessageDto>).ToList();
        }
    }
}

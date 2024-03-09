using AutoMapper;
using BulkMessager.Data.Entities;
using BulkMessager.Dtos;
using BulkMessager.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BulkMessager.Controllers {

    [ApiController]
    [Route("sms")]    
    public class MessagingController : Controller {

        private readonly IMapper _mapper;
        private readonly ApplicationLogger<MessagingController> _appLogger;

        public MessagingController(ILogger<MessagingController> logger, IMapper mapper) {
            _appLogger = new(logger);
            _mapper = mapper;
        }

        [HttpGet("/")]
        public IActionResult Welcome() {
            return Ok("Hello World");
        }

        [HttpGet]
        public IList<MessageDto> GetMessages() {
            _appLogger.LogInfo("Retrieving a list of messages");
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

            _appLogger.LogInfo("Show all retrieved messages");
            return messages.Select(_mapper.Map<MessageDto>).ToList();
        }
    }
}

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

        [HttpGet("message/{id}")]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMessageById(long id) {

            if (!await _service.ExistsAsync(id)) {
                 _logger.LogInfo($"Message with id {id} not found");
                return NotFound();
            }

            var record = _mapper.Map<MessageDto>(await _service.FindMessageAsync(id));
            if(!ModelState.IsValid) {
                _logger.LogInfo("Invalid data receieved. Message was not successfully retrieved");
                return BadRequest(ModelState);
            } else {
                _logger.LogInfo("Message retrieved and returned successfully");
            }
            return Ok(record);
        }

        [HttpGet("messages")][HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<MessageDto>))]
        public async Task<IActionResult> GetMessages() {

            _logger.LogInfo("Retrieving a list of messages from database");
            
            var messages = await _service.GetAllMessagesAsync();
            if(messages!= null && messages.Any()) {

                //var result = messages.Select(_mapper.Map<MessageDto>).ToList();
                var result = _mapper.Map<List<MessageDto>>(messages);

                if(!ModelState.IsValid) {
                      _logger.LogInfo("Invalid data receieved. Messages where not successfully retrieved");
                     return BadRequest(ModelState);
                } else {
                   _logger.LogInfo("Messages retrieved and returned successfully");
                }

                return Ok(result);
            } 
            
            //..return an empty list
            _logger.LogInfo("No messages found in the database");
            return Ok(new List<MessageDto>());
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDto message) {

            await _service.CreatMessageAsync(new Message());
            return Ok();
        }

        [HttpGet("/")]
        public IActionResult Welcome() {
            _logger.LogInfo("Calling the Welcome endpoint..");
            var msg = _service.SayHello();
            return Ok(msg);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketsService;
        public TicketsController(ITicketService ticketsService)
        {
            _ticketsService = ticketsService;
        }
        // POST: api/Tickets
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] TicketModel model)
        {
            var userId = GetAuthorizedUserId();
            model.UserId = userId;
            _ticketsService.CreateTicket(model);
            return Ok();
        }


        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userId))
            {
                throw new Exception("Name identifier claim does not exist.");
            }
            return userId;
        }
    }
}

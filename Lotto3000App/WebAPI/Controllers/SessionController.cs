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
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        // POST: api/Session
        [AllowAnonymous]
        [HttpPost("create")]
        public ActionResult CreateSession([FromBody] SessionModel model)
        {
            int userRole = GetAuthorizedUserRole();
            if (userRole != 1)
                return Unauthorized();

            _sessionService.CreateSession(model);
            return Ok("New session created!");
        }

        // POST: api/Session
        [AllowAnonymous]
        [HttpPost("close")]
        public ActionResult CloseSession([FromBody] SessionModel model)
        {
            int userRole = GetAuthorizedUserRole();
            if (userRole != 1)
                return Unauthorized();

            var currentSessionId = _sessionService.GetCurrentSession();
            _sessionService.CloseSession(currentSessionId);

            _sessionService.AddWinnersByThisSession(currentSessionId);

            return Ok("Current session closed!");
        }

        private int GetAuthorizedUserRole()
        {
            if (!int.TryParse(User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value,
                out var userRole))
            {
                throw new Exception("Name identifier claim does not exist.");
            }
            return userRole;
        }
    }
}

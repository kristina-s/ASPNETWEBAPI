using System;
using System.Collections.Generic;
using System.Linq;
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
    public class WinnersController : ControllerBase
    {
        private readonly IWinnersService _winnersService;
        public WinnersController(IWinnersService winnersService)
        {
            _winnersService = winnersService;
        }

        [AllowAnonymous]
        // GET: api/Winners
        [HttpGet("{id}")]
        public IEnumerable<WinnerModel> Get(int id)
        {
            
            return _winnersService.GetWinners(id);
        }

        
    }
}

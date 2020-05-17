using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaServices.Models;
using MediaServices.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaServices.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/shows")]
    public class ShowsController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepository;

        public ShowsController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        // GET: api/Shows
        [HttpGet]
        public IActionResult Get(int pageNumber = 1, int pageSize = 50)
        {
            return Ok(_mediaRepository.Get(pageNumber, pageSize));
        }

        // GET: api/Shows/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var response = _mediaRepository.Get(id);
            if (response == null)
                return BadRequest("No record found with provided id: " + id);
            return Ok(response);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _log; 
        private IConfiguration _configuration { get; }

        public DefaultController(ILogger<DefaultController> log, IConfiguration configuration)
        {
            this._log = log;
            this._configuration = configuration;
        }

        // GET: api/<DefaultController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<DefaultController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DefaultController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DefaultController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

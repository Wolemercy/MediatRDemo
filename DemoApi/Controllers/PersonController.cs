using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DemoLibrary.Models;
using DemoLibrary.Queries;
using DemoLibrary.Commands;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<List<PersonModel>> Get()
        {
            return await _mediator.Send(new GetPersonListQuery());
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<PersonModel> Get(int id)
        {
            return await _mediator.Send(new GetPersonByIdQuery(id));
        }

        // POST: api/Person
        [HttpPost]
        public async Task<PersonModel> Post([FromBody] PersonModel value)
        {
            var model = new InsertPersonCommand(value.FirstName, value.LastName);
            return await _mediator.Send(model);
        }
    }
}

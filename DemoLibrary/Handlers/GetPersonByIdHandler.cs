using System;
using MediatR;
using DemoLibrary.Models;
using DemoLibrary.Queries;

namespace DemoLibrary.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonModel>
    {
        private IMediator _mediator;

        public GetPersonByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PersonModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var results = await _mediator.Send(new GetPersonListQuery());

            var output = results.FirstOrDefault(x => x.Id == request.Id);

            return output;
        }
    }
}


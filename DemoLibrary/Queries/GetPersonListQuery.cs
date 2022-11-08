using System;
using MediatR;
using DemoLibrary.Models;

namespace DemoLibrary.Queries
{
    public record GetPersonListQuery() : IRequest<List<PersonModel>>;
}


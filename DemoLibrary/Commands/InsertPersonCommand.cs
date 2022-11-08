using System;
using MediatR;
using DemoLibrary.Models;

namespace DemoLibrary.Commands
{
    public record InsertPersonCommand(string FirstName, string LastName) : IRequest<PersonModel>;

    //public class InsertPersonCommandClass : IRequest<PersonModel>
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }

    //    public InsertPersonCommandClass(string firstName, string lastName)
    //    {
    //        FirstName = firstName;
    //        LastName = lastName;
    //    }
    //}
}


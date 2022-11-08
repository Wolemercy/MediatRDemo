using DemoLibrary.DataAccess;
using DemoLibrary.Handlers;
using DemoLibrary.Queries;
using DemoLibrary.Commands;
using DemoLibrary.Models;
using Moq;
using MediatR;
using DemoLibrary.Commands;

namespace DemoTest;

public class HandlerTest
{
    private readonly Mock<IDataAccess> _dataAccessMock;
    private readonly Mock<IMediator> _mediatorMock;

    public HandlerTest()
    {
        _dataAccessMock = new();
        _mediatorMock = new();
    }

    [Fact]
    public async Task GetPersonListHandler_Should_ReturnListOfPersons()
    {
        // Arrange
        var query = new GetPersonListQuery();
        var handler = new GetPersonListHandler(_dataAccessMock.Object);

        List<PersonModel> expectedResult = new();
        expectedResult.Add(new PersonModel { Id = 1, FirstName = "Wole", LastName = "Mercy" });
        expectedResult.Add(new PersonModel { Id = 2, FirstName = "John", LastName = "Gboyega" });

        _dataAccessMock.Setup(opt => opt.GetPeople()).Returns(expectedResult);

        // Act
        List<PersonModel> result = await handler.Handle(query, default);

        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(result[0].FirstName, expectedResult[0].FirstName);
        Assert.Equal(result[1].LastName, expectedResult[1].LastName);
    }

    [Fact]
    public async Task GetPersonByIdHandler_Should_ReturnAPerson()
    {
        // Arrange
        var query = new GetPersonByIdQuery(1);
        var handler = new GetPersonByIdHandler(_mediatorMock.Object);

        List<PersonModel> expectedResult = new();
        expectedResult.Add(new PersonModel { Id = 1, FirstName = "Wole", LastName = "Mercy" });
        expectedResult.Add(new PersonModel { Id = 2, FirstName = "John", LastName = "Gboyega" });

        _mediatorMock.Setup(opt => opt.Send(It.IsAny<IRequest<List<PersonModel>>>(), default)).ReturnsAsync(expectedResult);

        _dataAccessMock.Setup(opt => opt.GetPeople()).Returns(expectedResult);

        // Act
        PersonModel result = await handler.Handle(query, default);

        //Assert
        Assert.Equal(result.FirstName, expectedResult[0].FirstName);
        Assert.Equal(result.LastName, expectedResult[0].LastName);
    }

    [Fact]
    public async Task InsertPersonHandler_Should_ReturnInsertedPerson()
    {
        // Arrange
        var query = new InsertPersonCommand("John", "Jonathan");
        var handler = new InsertPersonHandler(_dataAccessMock.Object);

        PersonModel expectedResult = new() { Id = 4, FirstName = "John", LastName = "Jonathan" };

        _dataAccessMock.Setup(opt => opt.InsertPerson(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResult);

        // Act
        PersonModel result = await handler.Handle(query, default);

        //Assert
        Assert.Equal(result.FirstName, expectedResult.FirstName);
        Assert.Equal(result.LastName, expectedResult.LastName);
    }
}

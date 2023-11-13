using BookingAp.ArchitectureTests;
using BookingAP.Domain.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;

namespace BookingAp.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void DomainEvent_Should_Have_DomainEventPostfix()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should().HaveNameEndingWith("DomainEvent")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
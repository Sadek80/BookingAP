using BookingAP.Application.Abstractions.Messaging;
using BookingAP.Domain.Abstractions;
using BookingAP.Infrastructure;
using System.Reflection;

namespace BookingAp.ArchitectureTests;

public class BaseTest
{
    protected static Assembly ApplicationAssembly => typeof(IAssymblyMarker).Assembly;

    protected static Assembly DomainAssembly => typeof(IEntity).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(ApplicationDbContext).Assembly;
}
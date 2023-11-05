using ErrorOr;
using MediatR;

namespace BookingAP.Application.Abstractions.Messaging
{
    public interface IMessagingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : IErrorOr
    {
    }
}

using ErrorOr;
using MediatR;

namespace BookingAP.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse : IErrorOr
    {
    }
}

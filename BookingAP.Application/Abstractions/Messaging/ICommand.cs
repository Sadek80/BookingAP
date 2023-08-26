using ErrorOr;
using MediatR;

namespace BookingAP.Application.Abstractions.Messaging
{
    public interface ICommand<TResponse> : IRequest<TResponse> where TResponse : IErrorOr
    {
    }
}

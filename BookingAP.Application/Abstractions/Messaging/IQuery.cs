using ErrorOr;
using MediatR;

namespace BookingAP.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : IErrorOr
    {
    }
}

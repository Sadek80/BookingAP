using ErrorOr;
using MediatR;

namespace BookingAP.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest: IQuery<TResponse>
        where TResponse : IErrorOr
    {
    }
}

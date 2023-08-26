using BookingAP.Application.Abstractions.Messaging;
using ErrorOr;
using MediatR;
using Serilog;

namespace BookingAP.Application.Abstractions.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse: IErrorOr
    {
        private readonly ILogger _logger;

        public LoggingPipelineBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                _logger.Information("Executing command {Command} with {@request}", name, request);

                var result = await next();

                _logger.Information("Command {Command} processed successfully", name);

                return result;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Command {Command} processing failed", name);
                throw;
            }
        }
    }
}

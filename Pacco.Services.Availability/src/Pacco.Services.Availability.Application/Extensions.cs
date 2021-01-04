using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
 
namespace Pacco.Services.Availability.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder)
            => builder
              .AddCommandHandlers()
              .AddInMemoryCommandDispatcher();
    }
}
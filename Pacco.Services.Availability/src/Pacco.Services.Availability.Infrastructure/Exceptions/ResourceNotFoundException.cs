namespace Pacco.Services.Availability.Infrastructure.Exceptions
{
    public class ResourceNotFoundException : InfrastructureException
    {
        public ResourceNotFoundException() : base($"Resource not found.")
        {
        }
    }
}
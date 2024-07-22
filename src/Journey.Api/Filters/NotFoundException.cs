using Journey.Exception.ExceptionsBase;

namespace Journey.Api.Filters;

public class NotFoundException(string message) : JourneyException(message);

using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.Delete;

public class DeleteActivityForTripUseCase
{
    public void Execute(Guid tripId, Guid activityId)
    {
        var dbContext = new JourneyDbContext();

        var activity = dbContext
            .Activities
            .FirstOrDefault(act => act.Id == activityId && act.TripId == tripId);

        if (activity is null)
        {
            throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);
        }

        dbContext.Activities.Remove(activity);
        dbContext.SaveChanges();
    }
}
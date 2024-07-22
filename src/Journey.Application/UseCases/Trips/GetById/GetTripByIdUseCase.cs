using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById;

public class GetTripByIdUseCase
{
    public ResponseTripJson Execute(Guid id)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext.Trips
            .Include(t => t.Activities)
                .FirstOrDefault(trip => trip.Id == id);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        return new ResponseTripJson
        {
            Name = trip.Name,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Activities = trip.Activities.Select(act => new ResponseActivityJson
            {
                Id = act.Id,
                Name = act.Name,
                Date = act.Date,
                Status = (Communication.Enums.ActivityStatus)act.Status
            }).ToList()
        };
    }
}
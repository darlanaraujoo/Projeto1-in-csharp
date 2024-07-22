using FluentValidation.Results;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Save;

public class RegisterActivityForTripUseCase
{
     public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
     {
          var dbContext = new JourneyDbContext();

          var trip = dbContext
               .Trips
               .Include(trip => trip.Activities)
               .FirstOrDefault(trip => trip.Id == tripId);

          if (trip is null)
          {
               throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
          }
          
          Validate(trip, request);

          var entity = new Activity
          {
               Name = request.Name,
               Date = request.Date,
               TripId = tripId
          };

          dbContext.Activities.Add(entity);
          dbContext.SaveChanges();
          
          return new ResponseActivityJson
          {
               Date = entity.Date,
               Id = entity.Id,
               Name = entity.Name,
               Status = (ActivityStatus)entity.Status
          };
     }

     private static void Validate(Trip? trip ,RequestRegisterActivityJson request)
     {
          var validator = new RegisterActivityValidator();

          var result = validator.Validate(request);

          if ((request.Date >= trip!.StartDate && request.Date <= trip.EndDate) == false)
          {
               result.Errors.Add(new ValidationFailure());
          }

          if (result.IsValid == false)
          {
               var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

               throw new ErrorOnValidationException(errorMessages);
          }
     }
     
}
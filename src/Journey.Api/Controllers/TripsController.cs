using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Save;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetDelete;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
     {
         var useCase = new RegisterTripUseCase();
         var response =  useCase.Execute(request);
         return Created(string.Empty, response);
     }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var userCase = new GetAllTripUseCase();
        var response = userCase.Execute();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute]Guid id)
    {
        var useCase = new GetAllTripUseCase();

        var response = useCase.Execute();
        return Ok(response);
        
    }
    
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType( StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute]Guid id)
    {
        var useCase = new DeleteTripByIdUseCase();

        useCase.Execute(id);
        
        return NoContent();
        
    }
    
    [HttpPost]
    [Route("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult RegisterActivity([FromRoute]Guid tripId, [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterActivityForTripUseCase();

        var response = useCase.Execute(tripId, request);
        
        return Created(string.Empty, response);
        
    }
    
    [HttpPut]
    [Route("{tripId}/activity/{activityId}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult CompeteActivity([FromRoute]Guid tripId, [FromRoute]Guid activityId)
    {
        var useCase = new CompleteActivityForTripUseCase();
        
        useCase.Execute(tripId, activityId);
        
        return NoContent();
        
    }
    
    [HttpDelete]
    [Route("{tripId}/activity/{activityId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteActivity([FromRoute]Guid tripId, [FromRoute]Guid activityId)
    {
        var useCase = new DeleteActivityForTripUseCase();
        
        useCase.Execute(tripId, activityId);
        
        return NoContent();
        
    }
}
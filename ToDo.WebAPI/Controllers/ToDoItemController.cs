using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Commands.ToDoItem;
using ToDo.Application.Commands.ToDoList;
using ToDo.Application.DTO;
using ToDo.Application.Query;
using ToDo.Application.Validators;


namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ToDoDataAccess _dataAccess;
        public ToDoItemController(IMediator mediator, ToDoDataAccess dataAccess)
        {
            _mediator = mediator;
            _dataAccess = dataAccess;
        }





        [HttpGet("Items")]
        public async Task<ActionResult<GetStatus>> GetAllToDoItems()
        {
            try
            {
                //var Employees = await _mediator.Send(new GetAllEmployeesQuery());
                var ToDoItems = await _dataAccess.GetStatusOfItems();
                return Ok(ToDoItems);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}/Item")]
        public async Task<ActionResult<GetItemStatus>> GetToDoItemById(Guid Id)
        {
            try
            {
                //var Employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
                var ToDoItem = await _dataAccess.GetItemPropertyById(Id);
                return Ok(ToDoItem);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }






        [HttpPost("Item")]
        public async Task<ActionResult<CreateToDoItem>> CreateToDoItem(CreateToDoItem inputDto)
        {
            //var validationResults = await _createToDoItemValidator.ValidateAsync(new CreateToDoItemCommand(inputDto));

            var createCommand = new CreateToDoItemCommand(inputDto);

            var validator = new CreateToDoItemCommandValidator();
            var validationResult = await validator.ValidateAsync(createCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var ToDoItem = await _mediator.Send(new CreateToDoItemCommand(inputDto));
                return ToDoItem;
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}/ItemProperty")]
        public async Task<ActionResult> UpdateToDoList(Guid Id, CreateToDoItem inputToDoItem)
        {
            var updateCommand = new UpdateToDoItemPropertyCommand(Id, inputToDoItem);

            var validator = new UpdateToDoItemPropertyCommandValidator();
            var validationResult = await validator.ValidateAsync(updateCommand);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                await _mediator.Send(new UpdateToDoItemPropertyCommand(Id, inputToDoItem));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}/ItemProgress")]
        public async Task<ActionResult<GetItemStatus>> UpdateItemProgress(Guid Id, UpdateItemProgress inputToItem)
        {
            var updateCommand = new UpdateToDoItemProgressCommand(Id, inputToItem);

            var validator = new UpdateToDoItemProgressCommandValidator();
            var validationResult = await validator.ValidateAsync(updateCommand);


            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var updated = await _mediator.Send(new UpdateToDoItemProgressCommand(Id, inputToItem));
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[HttpPost("Item")]
        //public async Task<ActionResult<CreateToDoItem>> CreateToDoItem(CreateToDoItem inputDto)
        //{
        //    try
        //    {
        //        //var employeePosition = await _mediator.Send(new CreatePositionCommand(inputDto));
        //        var ToDoItem = await _mediator.Send(new CreateToDoItemCommand(inputDto));
        //        //return CreatedAtAction(nameof(GetEmployeeId), new { id = employeePosition.Id }, employeePosition);
        //        return ToDoItem;
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpPut("{Id}/ItemProperty")]
        //public async Task<ActionResult> UpdateToDoList(Guid Id, UpdateItemProperty inputToDoItem)
        //{
        //    try
        //    {
        //        //var updatedEmployee = await _mediator.Send(new UpdatePositionCommand(id, inputDto));
        //        await _mediator.Send(new UpdateToDoItemPropertyCommand(Id, inputToDoItem));
        //        return NoContent();
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpPut("{Id}/ItemProgress")]
        //public async Task<ActionResult<GetItemStatus>> UpdateItemProgress(Guid Id, UpdateItemProgress inputToItem)
        //{
        //    try
        //    {
        //        //var updatedEmployee = await _mediator.Send(new UpdatePositionCommand(id, inputDto));
        //        var updated = await _mediator.Send(new UpdateToDoItemProgressCommand(Id, inputToItem));
        //        return Ok(updated);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpDelete("{Id}/ToDoItem")]
        public async Task<ActionResult> DeleteToDoItem(Guid Id)
        {
            try
            {
                await _mediator.Send(new DeleteToDoItemCommand(Id));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }








    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Commands.ToDoList;
using ToDo.Application.DTO;
using ToDo.Application.Query;
using ToDo.Application.Validators;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ToDoDataAccess _dataAccess;
        public ToDoListController(IMediator mediator, ToDoDataAccess dataAccess)
        {
            _mediator = mediator;
            _dataAccess = dataAccess;
        }





        [HttpGet]
        public async Task<ActionResult<GetStatus>> GetAllToDoLists()
        {
            try
            {
                var ToDoLists = await _dataAccess.GetStatusOfList();
                return Ok(ToDoLists);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<GetStatus>> GetToDoListById(Guid Id)
        {
            try
            {
                var ToDoList = await _dataAccess.GetStatusOfListById(Id);
                return Ok(ToDoList);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{Id}/ItemsOfToDoList")]
        public async Task<ActionResult<IEnumerable<GetItemStatus>>> GetItemsOfToDoList(Guid Id)
        {
            try
            {
                var itemsOfList = await _dataAccess.GetItemsOfToDoListById(Id);
                return Ok(itemsOfList);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }




        [HttpPost]
        public async Task<ActionResult<CreateToDoList>> CreateToDoList(CreateToDoList inputDto)
        {
            try
            {
                var createCommand = new CreateToDoListCommand(inputDto);

                var validator = new CreateToDoListCommandValidator();
                var validationResult = await validator.ValidateAsync(createCommand);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var ToDoList = await _mediator.Send(createCommand);
                //var ToDoList = await _service.CreateToDoList(inputDto);
                return ToDoList;
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<GetStatus>> UpdateToDoList(Guid Id, CreateToDoList inputToDo)
        {
            try
            {
                var updateCommand = new UpdateToDoListCommand(Id, inputToDo);

                var validator = new UpdateToDoListCommandValidator();
                var validationResult = await validator.ValidateAsync(updateCommand);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var updatedList = await _mediator.Send(updateCommand);
                //var updatedList = await _service.UpdateToDoList(Id, inputToDo);
                return Ok(updatedList);
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




        //[HttpPost]
        //public async Task<ActionResult<CreateToDoList>> CreateToDoList(CreateToDoList inputDto)
        //{
        //    try
        //    {
        //        var ToDoList = await _mediator.Send(new CreateToDoListCommand(inputDto));
        //        //var ToDoList = await _service.CreateToDoList(inputDto);
        //        return ToDoList;
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{Id}")]
        //public async Task<ActionResult<GetStatus>> UpdateToDoList(Guid Id, CreateToDoList inputToDo)
        //{
        //    try
        //    {
        //        var updatedList = await _mediator.Send(new UpdateToDoListCommand(Id, inputToDo));
        //        //var updatedList = await _service.UpdateToDoList(Id, inputToDo);
        //        return Ok(updatedList);
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

        [HttpDelete("{Id}/ToDoList")]
        public async Task<ActionResult> DeleteToDoList(Guid Id)
        {
            try
            {
                await _mediator.Send(new DeleteToDoListCommand(Id));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }








    }
}
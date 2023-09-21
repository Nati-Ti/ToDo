using MediatR;
using ToDo.Application.Commands.ToDoList;
using ToDo.Application.DTO;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;

namespace ToDo.Application.Handlers.ToDoListHandler
{
    public class UpdateToDoListHandler : IRequestHandler<UpdateToDoListCommand, GetStatus>
    {
        private readonly ToDoListRepository _repository;
        public UpdateToDoListHandler(ToDoListRepository repository)
        {
            _repository = repository;
        }


        public async Task<GetStatus> Handle(UpdateToDoListCommand command, CancellationToken cancellationToken)
        {
            var toDo = command.Id;
            var toDoInput = command.inputToDoList;

            var updateToDoList = await _repository.GetToDoList(toDo);

            if (updateToDoList == null)
            {
                throw new KeyNotFoundException("The specified ToDo List was not found.");
            }

            var allLists = await _repository.titleCheck(toDoInput.Title);

            if (allLists == true)
            {
                throw new InvalidOperationException("Invalid Title. The specified ToDo List already exists.");
            }

          
            updateToDoList.Title = toDoInput.Title;
            updateToDoList.Value = toDoInput.Value;

            
            await _repository.UpdateToDoList(updateToDoList);

            var updatedReturn = new GetStatus
            {
                Id = updateToDoList.Id,
                Title = updateToDoList.Title,
                Value = updateToDoList.Value,
                Percentage = updateToDoList.Percentage
            };

            return updatedReturn;
        }

    }
}

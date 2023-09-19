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

            var allLists = await _repository.GetAllToDoLists();

            if (updateToDoList.Title != toDoInput.Title && allLists.Any(p => p.Title == toDoInput.Title))
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




        //public async Task<GetStatus> Handle(UpdateToDoListCommand command, CancellationToken cancellationToken)
        //{
        //    var toDo = command.Id;
        //    var toDoInput = command.inputToDoList;

        //    var allLists = await _repository.GetAllToDoLists();
        //    var toDoList = allLists.FirstOrDefault(p => p.Id == toDo);

        //    var updateToDoList = new ToDoList
        //    {
        //        Id = toDo,
        //        Title = toDoInput.Title
        //    };

        //    if (toDoList == null)
        //    {
        //        throw new KeyNotFoundException("The specified ToDo List was not found.");
        //    }

        //    if (allLists.Any(p => p.Title == toDoInput.Title))
        //    {
        //        throw new InvalidOperationException("Invalid Title. The specified ToDo List already exists.");
        //    }

        //    await _repository.UpdateToDoList(updateToDoList);
        //    var updatedReturn = new GetStatus
        //    {
        //        Id = updateToDoList.Id,
        //        Title = updateToDoList.Title,
        //        Value = toDoList.Value,
        //        Percentage = toDoList.Percentage
        //    };
        //    //inputToDoList.Value = toDoList.Value;
        //    //inputToDoList.Percentage = toDoList.Percentage;

        //    return updatedReturn;
        //}




    }
}

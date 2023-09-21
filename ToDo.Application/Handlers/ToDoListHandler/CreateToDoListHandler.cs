using MediatR;
using ToDo.Application.Commands.ToDoList;
using ToDo.Application.DTO;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;

namespace ToDo.Application.Handlers.ToDoListHandler
{
    public class CreateToDoListHandler : IRequestHandler<CreateToDoListCommand, CreateToDoList>
    {
        private readonly ToDoListRepository _repository;

        public CreateToDoListHandler(ToDoListRepository repository)
        {
            _repository = repository;
        }


        public async Task<CreateToDoList> Handle(CreateToDoListCommand command, CancellationToken cancellationToken)
        {
            var toDoList = command.toDoList;
            var inputToDoList = new ToDoList
            {
                Id = Guid.NewGuid(),
                Title = toDoList.Title,
                Value = toDoList.Value
            };

            var existList = await _repository.titleCheck(toDoList.Title);

            if (existList == true)
            {
                throw new InvalidOperationException("ToDo List already exists.");
            }

            await _repository.CreateToDoList(inputToDoList);

            toDoList.Id = inputToDoList.Id;

            return toDoList;
        }





    }
}

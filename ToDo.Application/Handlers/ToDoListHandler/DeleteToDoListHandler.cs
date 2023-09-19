using MediatR;
using ToDo.Application.Commands.ToDoList;
using ToDo.Persistence.Repositories;

namespace ToDo.Application.Handlers.ToDoListHandler
{
    public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand>
    {
        private readonly ToDoListRepository _repository;
        public DeleteToDoListHandler(ToDoListRepository repository)
        {
            _repository = repository;
        }



        public async Task Handle(DeleteToDoListCommand command, CancellationToken cancellationToken)
        {
            var toDoId = command.Id;

            var toDoList = await _repository.GetToDoList(toDoId);

            if (toDoList == null)
            {
                throw new KeyNotFoundException("The specified ToDo List was not found.");
            }

            await _repository.DeleteToDoList(toDoList);
        }



    }
}

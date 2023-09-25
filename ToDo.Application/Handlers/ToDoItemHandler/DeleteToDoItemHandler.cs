using MediatR;
using ToDo.Application.Commands.ToDoItem;
using ToDo.Application.DTO;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;
using ToDo.Application.Handlers.ToDoItemHandler;

namespace ToDo.Application.Handlers.ToDoItemHandler
{
    public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItemCommand>
    {
        private readonly ToDoItemRepository _repository;
        private readonly ToDoListRepository _repositoryList;

        public DeleteToDoItemHandler(ToDoItemRepository repository, ToDoListRepository repositoryList)
        {
            _repository = repository;
            _repositoryList = repositoryList;
        }


        public async Task Handle(DeleteToDoItemCommand command, CancellationToken cancellationToken)
        {
            var inputId = command.Id;
            var item = await _repository.GetToDoItem(inputId);

            if (item == null)
            {
                throw new KeyNotFoundException("The specified ToDo Item was not found.");
            }

            var toDoList = await _repositoryList.GetToDoList(item.ToDoId);
            // var emptyList = await _repositoryList.GetItemsOfToDoList(item.ToDoId);

            await _repository.DeleteToDoItem(item);

            if (toDoList == null)
            {
                throw new InvalidOperationException("Specified toDoList was not found.");
            }

            if (toDoList.Items == null)
            {
                toDoList.Percentage = 0;
            }
            else
            {
                await UpdateToDoList(toDoList.Id);
            }

            await _repositoryList.UpdateToDoList(toDoList);
        }


        public async Task UpdateToDoList(Guid Id)
        {
            // Checks if the ToDo List is found
            // Calculates the Total Value and Total Progress from all the items within the List
            // Update the ToDo List


            var toDoList = await _repositoryList.GetToDoList(Id); ;
            if (toDoList == null)
            {
                throw new InvalidOperationException("Specified toDoList was not found.");
            }

            var totalProgress = await _repository.sumOfProgress(Id);
            var totalValue = await _repository.sumOfValues(Id);

            toDoList.Percentage = (totalProgress / totalValue) * 100;

            await _repositoryList.UpdateToDoList(toDoList);
        }


    }

}

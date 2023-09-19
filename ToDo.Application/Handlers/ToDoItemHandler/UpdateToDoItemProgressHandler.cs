using MediatR;
using ToDo.Application.Commands.ToDoItem;
using ToDo.Application.DTO;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;

namespace ToDo.Application.Handlers.ToDoItemHandler
{
    public class UpdateToDoItemProgressHandler : IRequestHandler<UpdateToDoItemProgressCommand, GetItemStatus>
    {
        private readonly ToDoItemRepository _repository;
        private readonly ToDoListRepository _repositoryList;

        public UpdateToDoItemProgressHandler(ToDoItemRepository repository, ToDoListRepository repositoryList)
        {
            _repository = repository;
            _repositoryList = repositoryList;
        }


        public async Task<GetItemStatus> Handle(UpdateToDoItemProgressCommand command, CancellationToken cancellationToken)
        {
            var input = command.inputToItem;
            var inputId = command.Id;

            //Checks if the item exists
            //Checks if the input Progress is less or equal to the Value
            //Updates the Item and the ToDo List


            var item = await _repository.GetToDoItem(inputId);

            if (item == null)
            {
                throw new KeyNotFoundException("The specified ToDo Item was not found.");
            }

            if (input.Progress > item.Value)
            {
                throw new InvalidOperationException("Invalid Progress. The Progress cannot exceed the Value.");
            }

            var updateToDoItem = await _repository.GetToDoItem(inputId);

            if (updateToDoItem == null)
            {
                throw new InvalidOperationException("The specified ToDo Item was not found.");
            }

            updateToDoItem.Title = item.Title;
            updateToDoItem.Value = item.Value;
            updateToDoItem.Progress = input.Progress;
            updateToDoItem.Percentage = (input.Progress / item.Value) * 100;

            await _repository.UpdateToDoItem(updateToDoItem);

            await UpdateToDoList(inputId);

            var returnItem = new GetItemStatus
            {
                Id = inputId,
                Title = item.Title,
                Value = item.Value,
                Progress = input.Progress,
                Percentage = (input.Progress / item.Value) * 100
            };

            return returnItem;
        }

        public async Task UpdateToDoList(Guid Id)
        {
            //Checks if the Item is found
            //Checks if the ToDo List is found
            //Calculates the Total Value and Total Progress from all the items within the List
            //Update the ToDo List

            var item = await _repository.GetToDoItem(Id);
            if (item == null)
            {
                throw new InvalidOperationException("Specified item was not found.");
            }

            var toDoList = await _repositoryList.GetToDoList(item.ToDoId);
            if (toDoList == null)
            {
                throw new InvalidOperationException("Specified toDoList was not found.");
            }

            var allItems = await _repository.GetAllToDoItems();
            var listItems = allItems.Where(p => p.ToDoId == item.ToDoId).ToList();  

            var totalProgress = listItems.Sum(i => i.Progress);
            var totalValue = listItems.Sum(i => i.Value);


            var updateToDoList = await _repositoryList.GetToDoList(item.ToDoId);

            if (updateToDoList == null)
            {
                throw new InvalidOperationException("Specified toDoList was not found.");
            }

            updateToDoList.Title = toDoList.Title;
            updateToDoList.Value = toDoList.Value;
            updateToDoList.Percentage = (totalProgress / totalValue) * 100;

            await _repositoryList.UpdateToDoList(updateToDoList);
        }
    }
}
using MediatR;
using ToDo.Application.Commands.ToDoItem;
using ToDo.Application.DTO;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;

namespace ToDo.Application.Handlers.ToDoItemHandler
{
    public class CreateToDoItemHandler : IRequestHandler<CreateToDoItemCommand, CreateToDoItem>
    {
        private readonly ToDoItemRepository _repository;
        private readonly ToDoListRepository _repositoryList;

        public CreateToDoItemHandler(ToDoItemRepository repository, ToDoListRepository repositoryList)
        {
            _repository = repository;
            _repositoryList = repositoryList;
        }


        public async Task<CreateToDoItem> Handle(CreateToDoItemCommand command, CancellationToken cancellationToken)
        {
            var toDoItem = command.inputToDoItem;
            var inputToDoItem = new ToDoItem
            {
                Id = Guid.NewGuid(),
                Title = toDoItem.Title,
                ToDoId = toDoItem.ToDoId,
                Value = toDoItem.Value
            };

            //Check if the ToDo List already exists
            //Check if the input Value is not greater than the sum of the item's value
            //Check if the input Title already exists


            var toDoList = await _repositoryList.GetToDoList(toDoItem.ToDoId);
            if (toDoList == null)
            {
                throw new InvalidOperationException("Invalid ToDoId. The specified ToDoId or ToDoList doesn't exist.");
            }

            var allItems = await _repository.GetAllToDoItems();

            var itemsOfList = allItems.Where(p => p.ToDoId ==  inputToDoItem.ToDoId);
            
            var sumOfValue = itemsOfList.Sum(p => p.Value);
            
            if (toDoItem.Value > (toDoList.Value - sumOfValue))
            {
                throw new InvalidOperationException("Invalid ToDoItem Value. The sum of all Items value within" +
                    " the ToDo List cannot exceed the ToDoList Value");
            }

            var existItem = allItems.Any(p => p.Title == inputToDoItem.Title);

            if (existItem)
            {
                throw new InvalidOperationException("ToDo Item already exists.");
            }

            var createToDoItem = await _repository.CreateToDoItem(inputToDoItem);

            await UpdateToDoList(createToDoItem.Id);

            return new CreateToDoItem
            {
                Id = inputToDoItem.Id,
                Title = inputToDoItem.Title,
                ToDoId = inputToDoItem.ToDoId,
                Value = inputToDoItem.Value
            };
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

            //var toDoList = await _repositoryList.GetToDoList(item.ToDoId);
            var toDoList = item.ToDo;

            if (toDoList == null)
            {
                throw new InvalidOperationException("Specified toDoList was not found.");
            }

            var allItems = await _repository.GetAllToDoItems();
            var listItems = allItems.Where(p => p.ToDoId == item.ToDoId).ToList();

            var totalProgress = listItems.Sum(i => i.Progress);
            var totalValue = listItems.Sum(i => i.Value);


            //var updateToDoList = await _repositoryList.GetToDoList(item.ToDoId);
            var updateToDoList = item.ToDo;

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
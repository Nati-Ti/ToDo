using MediatR;
using ToDo.Application.Commands.ToDoItem;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;

namespace ToDo.Application.Handlers.ToDoItemHandler
{

    public class UpdateToDoItemPropertyHandler : IRequestHandler<UpdateToDoItemPropertyCommand>
    {
        private readonly ToDoItemRepository _repository;
        private readonly ToDoListRepository _repositoryList;

        public UpdateToDoItemPropertyHandler(ToDoItemRepository repository, ToDoListRepository repositoryList)
        {
            _repository = repository;
            _repositoryList = repositoryList;
        }

        public async Task Handle(UpdateToDoItemPropertyCommand command, CancellationToken cancellationToken)
        {
            var inputId = command.Id;
            var inputUpdate = command.inputToDoItem;

            //Check if the Item is found
            //Check if the smae Title is found within the same ToDo List
            //Check if the ToDoId exists
            //Call the item again to avoid tracking and Update


            var toDoItem = await _repository.GetToDoItem(inputId);
            if (toDoItem == null)
            {
                throw new KeyNotFoundException("The specified ToDo Item was not found.");
            }


            var toDoList = await _repositoryList.GetToDoList(inputUpdate.ToDoId);

            if (toDoList == null)
            {
                throw new InvalidOperationException("Invalid ToDoId. The specified ToDoId or ToDoList doesn't exist.");
            }

            var newTitle = await _repository.titleCheckItem(inputUpdate.ToDoId, inputUpdate.Title);

            if (newTitle == true)
            {
                throw new InvalidOperationException("Invalid Title. The specified ToDo Item already exists.");
            }


            var sumOfValue = await _repository.sumOfValues(inputUpdate.ToDoId);

            if (inputUpdate.Value > (toDoList.Value - sumOfValue + toDoItem.Value))
            {
                throw new InvalidOperationException("Invalid ToDoItem Value. The sum of all Items value within" +
                    " the ToDo List cannot exceed the ToDoList Value");
            }

            toDoItem.Title = inputUpdate.Title;
            toDoItem.Value = inputUpdate.Value;
            toDoItem.ToDoId = inputUpdate.ToDoId;


            await _repository.UpdateToDoItem(toDoItem);




        }
    }
}


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

            //var itemsInToDoList = toDoItem.ToDo.Items;

            var itemsInToDoList = await _repositoryList.GetItemsOfToDoList(inputUpdate.ToDoId);
            itemsInToDoList.RemoveAll(p => p.Id == inputId);

            if (itemsInToDoList.Any(p => p.Title == inputUpdate.Title))
            {
                throw new InvalidOperationException("Invalid Title. The specified ToDo Item already exists.");
            }

            var toDoList = await _repositoryList.GetToDoList(inputUpdate.ToDoId);

            if (toDoList == null)
            {
                throw new InvalidOperationException("Invalid ToDoId. The specified ToDoId or ToDoList doesn't exist.");
            }

            var updateToDoItem = await _repository.GetToDoItem(inputId);

            if (updateToDoItem == null)
            {
                throw new InvalidOperationException("The specified ToDo Item was not found.");
            }

            updateToDoItem.Title = inputUpdate.Title;
            updateToDoItem.Value = inputUpdate.Value;
            updateToDoItem.ToDoId = inputUpdate.ToDoId;
            

            await _repository.UpdateToDoItem(updateToDoItem);



            
        }


    }
}







//{
//    "id": "e3a93cd8-0618-4b8c-9aad-a3119353cb5d",
//  "title": "Mathematics",
//  "toDoId": "8f14606b-dc7c-429e-aea4-3938ae13f890",
//  "value": 45
//}


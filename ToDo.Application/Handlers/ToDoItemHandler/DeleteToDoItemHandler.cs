using MediatR;
using ToDo.Application.Commands.ToDoItem;
using ToDo.Application.DTO;
using ToDo.Domain.Models;
using ToDo.Persistence.Repositories;

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

            await _repository.DeleteToDoItem(item);

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





        //public async Task Handle(DeleteToDoItemCommand command, CancellationToken cancellationToken)
        //{
        //    var inputId = command.Id;
        //    var item = await _repository.GetToDoItem(inputId);
        //    var toDoList = await _repositoryList.GetToDoList(item.ToDoId);

        //    if (item == null)
        //    {
        //        throw new KeyNotFoundException("The specified ToDo List was not found.");
        //    }

        //    await _repository.DeleteToDoItem(item);

        //    var allItems = await _repository.GetAllToDoItems();

        //    var listItems = allItems.Where(p => p.ToDoId == item.ToDoId);

        //    var totalProgress = listItems.Sum(i => i.Progress);
        //    var totalValue = listItems.Sum(i => i.Value);


        //    var updateToDoList = new ToDoList
        //    {
        //        Id = item.ToDoId,
        //        Title = toDoList.Title,
        //        Value = totalValue,
        //        Percentage = (totalProgress / totalValue) * 100
        //    };

        //    await _repositoryList.UpdateToDoList(updateToDoList);
        //}



    }

}

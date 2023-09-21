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

            await UpdateToDoList(item.ToDoId);

            var returnItem = new GetItemStatus
            {
                Id = inputId,
                Title = item.Title,
                Value = item.Value,
                Progress = input.Progress,
                Percentage = updateToDoItem.Percentage
            };

            return returnItem;
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

            toDoList.Title = toDoList.Title;
            toDoList.Value = toDoList.Value;
            toDoList.Percentage = (totalProgress / totalValue) * 100;

            await _repositoryList.UpdateToDoList(toDoList);
        }

        //public async task updatetodolist(guid id)
        //{
        //    //checks if the item is found
        //    //checks if the todo list is found
        //    //calculates the total value and total progress from all the items within the list
        //    //update the todo list

        //    var item = await _repository.gettodoitem(id);
        //    if (item == null)
        //    {
        //        throw new invalidoperationexception("specified item was not found.");
        //    }

        //    var todolist = await _repositorylist.gettodolist(item.todoid);
        //    if (todolist == null)
        //    {
        //        throw new invalidoperationexception("specified todolist was not found.");
        //    }

        //    var allitems = await _repository.getalltodoitems();
        //    var listitems = allitems.where(p => p.todoid == item.todoid).tolist();

        //    var totalprogress = listitems.sum(i => i.progress);
        //    var totalvalue = listitems.sum(i => i.value);


        //    var updatetodolist = await _repositorylist.gettodolist(item.todoid);

        //    if (updatetodolist == null)
        //    {
        //        throw new invalidoperationexception("specified todolist was not found.");
        //    }

        //    updatetodolist.title = todolist.title;
        //    updatetodolist.value = todolist.value;
        //    updatetodolist.percentage = (totalprogress / totalvalue) * 100;

        //    await _repositorylist.updatetodolist(updatetodolist);
        //}
    }
}
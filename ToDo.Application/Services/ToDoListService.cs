//using Dapper;
//using Newtonsoft.Json.Linq;
//using System.ComponentModel.DataAnnotations;
//using ToDo.Application.DTO;
//using ToDo.Application.Models;
//using ToDo.Application.Query;
//using ToDo.Application.Repositories;

//namespace ToDo.Application.Services
//{
//    public class ToDoListService
//    {

//        private readonly ToDoDataAccess _dataAccess;
//        private readonly ToDoListRepository _repository;
//        private readonly ToDoItemRepository _repositoryI;
//        public ToDoListService(ToDoListRepository repository, ToDoDataAccess dataAccess,
//             ToDoItemRepository repositoryI)
//        {
//            _dataAccess = dataAccess;
//            _repository = repository;
//            _repositoryI = repositoryI;
//        }

//        // TODO LIST Services

//        public async Task<IEnumerable<GetStatus>> GetAllToDoLists()
//        {
//            var toDoLists = await _dataAccess.GetStatusOfList();
//            return toDoLists;
//        }

//        public async Task<GetStatus> GetToDoListById(Guid Id)
//        {
//            var toDoList = await _dataAccess.GetStatusOfListById(Id);

//            if (toDoList == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo List was not found.");
//            }
//            return toDoList;
//        }

//        public async Task<CreateToDoList> CreateToDoList(CreateToDoList toDoList)
//        {
//            var inputToDoList = new ToDoList
//            {
//                Id = Guid.NewGuid(),
//                Title = toDoList.Title
//            };

//            var allLists = await _dataAccess.GetListProperty();
//            var existList = allLists.Any(p => p.Title == inputToDoList.Title);

//            if (existList == true)
//            {
//                throw new InvalidOperationException("ToDo List already exists.");
//            }

//            var createToDoList = await _repository.CreateToDoList(inputToDoList);

//            toDoList.Id = inputToDoList.Id;

//            return toDoList;
//        }

//        public async Task<GetStatus> UpdateToDoList(Guid Id, CreateToDoList inputToDoList)
//        {
//            var allLists = await _dataAccess.GetStatusOfList();
//            var toDoList = allLists.FirstOrDefault(p => p.Id == Id);

//            var updateToDoList = new ToDoList
//            {
//                Id = Id,
//                Title = inputToDoList.Title
//            };

//            if (toDoList == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo List was not found.");
//            }

//            if (allLists.Any(p => p.Title == inputToDoList.Title))
//            {
//                throw new InvalidOperationException("Invalid Title. The specified ToDo List already exists.");
//            }

//            await _repository.UpdateToDoList(updateToDoList);
//            var updatedReturn = new GetStatus
//            {
//                Id = updateToDoList.Id,
//                Title = updateToDoList.Title,
//                Value = toDoList.Value,
//                Percentage = toDoList.Percentage
//            };
//            //inputToDoList.Value = toDoList.Value;
//            //inputToDoList.Percentage = toDoList.Percentage;

//            return updatedReturn;
//        }

//        public async Task<IEnumerable<GetItemStatus>> GetItemsOfToDoList(Guid Id)
//        {
//            var allItems = await _dataAccess.GetAllToDoItems();
//            var itemsOfList = allItems.Where(p => p.ToDoId == Id);

//            if (itemsOfList == null)
//            {
//                throw new KeyNotFoundException(" No Items found from the specified ToDo List.");
//            }

//            var selectedItems = itemsOfList.Select(item => new GetItemStatus
//            {
//                Id = item.Id,
//                Title = item.Title,
//                Value = item.Value,
//                Progress = item.Progress,
//                Percentage = item.Percentage
//            });

//            return selectedItems;
//        }

//        public async Task DeleteToDoList(Guid Id)
//        {
//            var toDoList = await _dataAccess.GetToDoListById(Id);

//            if (toDoList == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo List was not found.");
//            }

//            await _repository.DeleteToDoList(toDoList);
//        }


//        //TODO ITEM Services


//        public async Task<IEnumerable<GetStatus>> GetAllToDoItems()
//        {
//            var toDoItems = await _dataAccess.GetStatusOfItems();
//            return toDoItems;
//        }

//        public async Task<GetItemStatus> GetToDoItemById(Guid Id)
//        {
//            var toDoItem = await _dataAccess.GetItemPropertyById(Id);

//            if (toDoItem == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo List was not found.");
//            }
//            return toDoItem;
//        }

//        public async Task<CreateToDoItem> CreateToDoItem(CreateToDoItem toDoItem)
//        {
//            var inputToDoItem = new ToDoItem
//            {
//                Id = Guid.NewGuid(),
//                Title = toDoItem.Title,
//                ToDoId = toDoItem.ToDoId,
//                Value = toDoItem.Value
//            };

//            var allLists = await _dataAccess.GetListProperty();
//            var listExists = allLists.Any(p => p.Id == inputToDoItem.ToDoId);

//            var toDoList = await _dataAccess.GetToDoListById(toDoItem.ToDoId);
       

//            if (listExists == false)
//            {
//                throw new InvalidOperationException("Invalid ToDoId. The specified ToDoId or ToDoList doesn't exist.");
//            }

//            if (toDoItem.Value == 0)
//            {
//                throw new InvalidOperationException("Invalid Value, the Value property cannot be 0.");
//            }

//            var allItems = await _dataAccess.GetItemProperty();
//            var existItem = allItems.Any(p => p.Title == inputToDoItem.Title);

//            if (existItem == true)
//            {
//                throw new InvalidOperationException("ToDo Item already exists.");
//            }
           
//            var createToDoItem = await _repositoryI.CreateToDoItem(inputToDoItem);

//            toDoItem.Id = inputToDoItem.Id;
//            await UpdateToDoList(toDoItem.Id);
            

//            return toDoItem;
//        }

//        public async Task UpdateToDoItemProperty(Guid Id, UpdateItemProperty inputToDoItem)
//        {
//            var allItems = await _dataAccess.GetItemProperty();
//            var toDoItem = allItems.Where(p => p.Id == Id);

//            var updateToDoItem = new ToDoItem
//            {
//                Id = Id,
//                Title = inputToDoItem.Title,
//                ToDoId = inputToDoItem.ToDoId
//            };

//            if (toDoItem == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo Item was not found.");
//            }

//            if (allItems.Any(p => p.Title == inputToDoItem.Title))
//            {
//                throw new InvalidOperationException("Invalid Title. The specified ToDo Item already exists.");
//            }

//            var allLists = await _dataAccess.GetListProperty();
//            var listExists = allLists.Any(p => p.Id == inputToDoItem.ToDoId);

//            if (listExists == false)
//            {
//                throw new InvalidOperationException("Invalid ToDoId. The specified ToDoId or ToDoList doesn't exist.");
//            }

//            await _repositoryI.UpdateToDoItem(updateToDoItem);
//        }

//        public async Task<GetItemStatus> UpdateItemProgress(Guid Id, UpdateItemProgress inputToItem)
//        {
//            var item = await _dataAccess.GetToDoItemById(Id);

//            if (item == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo Item was not found.");
//            }

//            if (inputToItem.Progress > item.Value)
//            {
//                throw new InvalidOperationException("Invalid Progress. The Progress cannot exceed the Value.");
//            }

//            var updateToDoItem = new ToDoItem
//            {
//                Id = Id,
//                Title = item.Title,
//                Value = item.Value,
//                Progress = inputToItem.Progress,
//                ToDoId = item.ToDoId,
//                Percentage = (inputToItem.Progress / item.Value) * 100
//            };

//            await _repositoryI.UpdateToDoItem(updateToDoItem);

//            await UpdateToDoList(Id);

//            var returnItem = new GetItemStatus
//            {
//                Id = Id,
//                Title = item.Title,
//                Value = item.Value,
//                Progress = inputToItem.Progress,
//                Percentage = (inputToItem.Progress / item.Value) * 100
//            };

//            return returnItem;
//        }

//        public async Task DeleteToDoItem(Guid Id)
//        {
//            var item = await _dataAccess.GetToDoItemById(Id);
//            var toDoList = await _dataAccess.GetToDoListById(item.ToDoId);

//            if (item == null)
//            {
//                throw new KeyNotFoundException("The specified ToDo List was not found.");
//            }

//            await _repositoryI.DeleteToDoItem(item);

//            var allItems = await _dataAccess.GetAllToDoItems();

//            var listItems = allItems.Where(p => p.ToDoId == item.ToDoId);

//            var totalProgress = listItems.Sum(i => i.Progress);
//            var totalValue = listItems.Sum(i => i.Value);


//            var updateToDoList = new ToDoList
//            {
//                Id = item.ToDoId,
//                Title = toDoList.Title,
//                Value = totalValue,
//                Percentage = (totalProgress / totalValue) * 100
//            };

//            await _repository.UpdateToDoList(updateToDoList);

//        }



//        public async Task UpdateToDoList(Guid Id)
//        {
//            var item = await _dataAccess.GetToDoItemById(Id);
//            var toDoList = await _dataAccess.GetToDoListById(item.ToDoId);
//            var allItems = await _dataAccess.GetAllToDoItems();

//            var listItems = allItems.Where(p => p.ToDoId == item.ToDoId);

//            var totalProgress = listItems.Sum(i => i.Progress);
//            var totalValue = listItems.Sum(i => i.Value);


//            var updateToDoList = new ToDoList
//            {
//                Id = item.ToDoId,
//                Title = toDoList.Title,
//                Value = totalValue,
//                Percentage = (totalProgress / totalValue) * 100
//            };

//            await _repository.UpdateToDoList(updateToDoList);
//        }

//    }
//}

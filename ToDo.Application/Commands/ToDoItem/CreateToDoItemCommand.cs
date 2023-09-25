using MediatR;
using ToDo.Application.DTO;

namespace ToDo.Application.Commands.ToDoItem
{
    public record CreateToDoItemCommand(CreateToDoItem inputToDoItem) : IRequest<CreateToDoItem>;

}

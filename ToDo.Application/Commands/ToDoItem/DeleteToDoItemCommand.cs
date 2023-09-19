
using MediatR;
using ToDo.Application.DTO;

namespace ToDo.Application.Commands.ToDoItem
{
    public record DeleteToDoItemCommand(Guid Id) : IRequest;

}

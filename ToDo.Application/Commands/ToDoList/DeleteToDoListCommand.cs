using MediatR;

namespace ToDo.Application.Commands.ToDoList
{
    public record DeleteToDoListCommand(Guid Id) : IRequest;


}

using MediatR;
using ToDo.Application.DTO;

namespace ToDo.Application.Commands.ToDoList
{
    public record UpdateToDoListCommand(Guid Id, CreateToDoList inputToDoList) : IRequest<GetStatus>;


}

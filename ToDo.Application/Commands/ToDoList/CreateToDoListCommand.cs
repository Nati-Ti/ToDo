using MediatR;
using ToDo.Application.DTO;

namespace ToDo.Application.Commands.ToDoList
{
    public record CreateToDoListCommand(CreateToDoList toDoList) : IRequest<CreateToDoList>;


}

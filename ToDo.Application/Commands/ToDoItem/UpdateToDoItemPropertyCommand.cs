using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTO;

namespace ToDo.Application.Commands.ToDoItem
{
    public record UpdateToDoItemPropertyCommand(Guid Id, CreateToDoItem inputToDoItem) : IRequest;
}

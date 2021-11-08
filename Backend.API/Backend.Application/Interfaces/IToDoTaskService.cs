using Backend.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces
{
    public interface IToDoTaskService
    {
        Task<List<ToDoTaskDTO>> GetAllTodo();
        Task<List<ToDoTaskDTO>> GetIncomingTodo(int type);
        Task<(bool Added, string Message)> CreateToDo(ToDoTaskDTO itemDTO);
        Task<(bool Updated, string Message)> EditToDo(ToDoTaskDTO itemDTO);
        Task<(bool Marked, string Message)> MarkToDo(ToDoTaskMarkDoneDTO itemDTO);
        Task<(bool Set, string Message)> SetPercentToDo(ToDoTaskSetPercentDTO itemDTO);
    }
}

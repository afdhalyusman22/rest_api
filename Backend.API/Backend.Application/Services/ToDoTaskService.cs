using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Core.Repositories.Base;
using Backend.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System;
namespace Backend.Application.Services
{
    public class ToDoTaskService : IToDoTaskService
    {

        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public ToDoTaskService(IRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<List<ToDoTaskDTO>> GetAllTodo()
        {
            var items = await _repository.ListAsync<ToDoTask>();

            var result = _mapper.Map<List<ToDoTaskDTO>>(items);

            return result;
        }

        public async Task<List<ToDoTaskDTO>> GetIncomingTodo(int type)
        {
            var items = new List<ToDoTask>();
            var date = DateTime.Today;
            //type 1 = today, 2 = next day, 3 = next week
            if (type == 1)
                items = await _repository.ListAsyncWithWhere<ToDoTask>(x => x.IsDeleted == false && x. IsDone == false && x.StartDate >= date && x.StartDate < date.AddDays(1) && x.EndDate > date);
            else if (type == 2)
                items = await _repository.ListAsyncWithWhere<ToDoTask>(x => x.IsDeleted == false && x.IsDone == false && x.StartDate >= date.AddDays(1) && x.StartDate < date.AddDays(2) && x.EndDate >= date.AddDays(1));
            else if (type == 3)
                items = await _repository.ListAsyncWithWhere<ToDoTask>(x => x.IsDeleted == false && x.IsDone == false && x.StartDate <= date.AddDays(7));

            var result = _mapper.Map<List<ToDoTaskDTO>>(items);

            return result;
        }

        public async Task<(bool Added, string Message)> CreateToDo(ToDoTaskDTO itemDTO)
        {
            var item = _mapper.Map<ToDoTask>(itemDTO);
            item.Id = 0;
            return await _repository.AddAsync<ToDoTask>(item);
        }

        public async Task<(bool Updated, string Message)> EditToDo(ToDoTaskDTO itemDTO)
        {
            var existingItems = await _repository.GetByIdAsync<ToDoTask>(itemDTO.Id);
            if (existingItems == null)
            {
                return (false, "Data Not Found");
            }

            var item = _mapper.Map<ToDoTaskDTO, ToDoTask>(itemDTO, existingItems);

            return await _repository.UpdateAsync<ToDoTask>(item);
        }

        public async Task<(bool Marked, string Message)> MarkToDo(ToDoTaskMarkDoneDTO itemDTO)
        {
            var existingItems = await _repository.GetByIdAsync<ToDoTask>(itemDTO.Id);
            if (existingItems == null)
            {
                return (false, "Data Not Found");
            }
            existingItems.IsDone = true;
            existingItems.Completed = 100;

            return await _repository.UpdateAsync<ToDoTask>(existingItems);
        }

        public async Task<(bool Set, string Message)> SetPercentToDo(ToDoTaskSetPercentDTO itemDTO)
        {
            var existingItems = await _repository.GetByIdAsync<ToDoTask>(itemDTO.Id);
            if (existingItems == null)
            {
                return (false, "Data Not Found");
            }
            existingItems.Completed = itemDTO.Completed;

            return await _repository.UpdateAsync<ToDoTask>(existingItems);
        }
    }
}

using AutoMapper;
using Backend.API.Error;
using Backend.Application.Dto;
using Backend.Application.Interfaces;
using Backend.Core.Repositories.Base;
using Backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTaskService _toDoTask;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ToDoTaskController(IToDoTaskService toDoTask, IRepository repository, IMapper mapper)
        {
            _toDoTask = toDoTask;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get All ToDo
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllToDo()
        {
            try
            {
                var result = await _toDoTask.GetAllTodo();

                return Requests.Response(this, new ApiStatus(200), result, "");

            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// Get Specific ToDo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("specific/{id:int}")]
        public async Task<IActionResult> GetSpecificToDo(long id)
        {
            try
            {
                var items = await _repository.GetByIdAsync<ToDoTask>(id);

                var result = _mapper.Map<ToDoTaskDTO>(items);
                if (result == null)
                {
                    return Requests.Response(this, new ApiStatus(404), result, "Data Not Found");
                }
                return Requests.Response(this, new ApiStatus(200), result, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
        /// <summary>
        /// Get Incoming ToDo (today/next day/ current week)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("incoming/{type:int}")]
        public async Task<IActionResult> GetIncomingToDo(int type)
        {
            try
            {
                var result = await _toDoTask.GetIncomingTodo(type);
                if (result == null)
                {
                    return Requests.Response(this, new ApiStatus(404), result, "Data Not Found");
                }
                return Requests.Response(this, new ApiStatus(200), result, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// Create ToDo
        /// </summary>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateToDo([FromBody] ToDoTaskDTO itemDTO)
        {
            try
            {
                var (Added, Message) = await _toDoTask.CreateToDo(itemDTO);
                return !Added ? Requests.Response(this, new ApiStatus(500), null, Message) : Requests.Response(this, new ApiStatus(200), null, Message);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// Edit ToDo
        /// </summary>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        [HttpPatch("update/{id:int}")]
        public async Task<IActionResult> EditToDo([FromBody] ToDoTaskDTO itemDTO)
        {
            try
            {
                var existingItems = await _repository.GetByIdAsync<ToDoTask>(itemDTO.Id);
                if (existingItems == null)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
                }

                var item = _mapper.Map<ToDoTaskDTO, ToDoTask>(itemDTO, existingItems);
                if (ModelState.IsValid)
                {
                    var (Updated, Message) = await _repository.UpdateAsync<ToDoTask>(item);
                    return !Updated ? Requests.Response(this, new ApiStatus(500), null, Message) : Requests.Response(this, new ApiStatus(200), null, Message);
                }
                else
                {
                    return Requests.Response(this, new ApiStatus(500), ModelState, "");
                }
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// Deleted ToDo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteToDo(long id)
        {
            try
            {
                var existingItems = await _repository.GetByIdAsync<ToDoTask>(id);
                if (existingItems == null)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "Data Not Found");
                }
                var (Deleted, Message) = await _repository.DeleteAsync<ToDoTask>(id);
                return !Deleted ? Requests.Response(this, new ApiStatus(500), null, Message) : Requests.Response(this, new ApiStatus(200), null, Message);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        /// <summary>
        /// Mark ToDo
        /// </summary>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        [HttpPost("mark")]
        public async Task<IActionResult> MarkToDo([FromBody] ToDoTaskMarkDoneDTO itemDTO)
        {
            try
            {
                var (Marked, Message) = await _toDoTask.MarkToDo(itemDTO);
                return !Marked ? Requests.Response(this, new ApiStatus(500), null, Message) : Requests.Response(this, new ApiStatus(200), null, "Success Mark ToDo");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }


        /// <summary>
        /// Set Percent ToDo
        /// </summary>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        [HttpPost("setpercent")]
        public async Task<IActionResult> SetPercentToDo([FromBody] ToDoTaskSetPercentDTO itemDTO)
        {
            try
            {
                var (Set, Message) = await _toDoTask.SetPercentToDo(itemDTO);
                return !Set ? Requests.Response(this, new ApiStatus(500), null, Message) : Requests.Response(this, new ApiStatus(200), null, "Success Set ToDo Percent Completed");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

    }
}

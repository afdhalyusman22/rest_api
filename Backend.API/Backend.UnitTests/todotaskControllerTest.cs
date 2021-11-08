using Backend.Core.Repositories.Base;
using Backend.Infrastructure.Data;
using Backend.Application.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Backend.API.Controllers;
using Backend.Application.Mappers;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Backend.Application.Dto;
using Backend.Application.Services;

namespace Backend.UnitTests
{
    public class todotaskControllerTest
    {
        private readonly IList<ToDoTask> ToDoTask;
        private readonly Mock<IRepository> mockRepo;
        private readonly Mock<IToDoTaskService> mockToDoTaskService;
        private readonly IMapper mockMapper;
        private readonly ToDoTaskController controller;
        private readonly ToDoTaskService service;

        public todotaskControllerTest()
        {

            this.mockRepo = new Mock<IRepository>();
            this.mockToDoTaskService = new Mock<IToDoTaskService>();
            var mappingProfile = new MappingProfile();
            var conifg = new MapperConfiguration(mappingProfile);
            mockMapper = new Mapper(conifg);
            this.service = new ToDoTaskService(this.mockRepo.Object, this.mockMapper);

            this.controller = new ToDoTaskController(this.mockToDoTaskService.Object, this.mockRepo.Object, this.mockMapper);

            ToDoTask = new List<ToDoTask>
            {
                SeedData.ToDoListTask1,
                SeedData.ToDoListTask2,
                SeedData.ToDoListTask3
            };

            mockRepo.Setup(repo => repo.GetByIdAsync<ToDoTask>(1)).ReturnsAsync(ToDoTask.Where(i => i.Id == 1).FirstOrDefault()).Verifiable();
            mockRepo.Setup(repo => repo.DeleteAsync<ToDoTask>(1)).ReturnsAsync((true, "Data successfully deleted")).Verifiable();
            mockRepo.Setup(repo => repo.GetQueryable<ToDoTask>()).Verifiable();

            mockRepo.Setup(repo => repo.ListAsync<ToDoTask>(1000)).ReturnsAsync(ToDoTask.ToList()).Verifiable();
        }

        [Test]
        public async Task GetAllToDo_ActionExecutes_ReturnsNotFound()
        {
            var result = await controller.GetAllToDo();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllToDo_ActionExecutes_Returns500()
        {
            mockRepo.Setup(repo => repo.ListAsync<ToDoTask>()).Throws<Exception>().Verifiable();
            var result = await controller.GetAllToDo();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllToDo_ActionExecutes_ReturnsNumbersOfToDoTaskAsync()
        {
            var result = await controller.GetAllToDo();
            var oor = result as ObjectResult;
            Assert.IsNotNull(oor);
            Assert.AreEqual(200, oor.StatusCode);
        }

        [Test]
        public async Task GetSpecificToDo_ActionExecutes_ReturnsCorrectValue()
        {
            var result = await controller.GetSpecificToDo(1);
            var oor = result as ObjectResult;

            Assert.IsNotNull(oor);
            Assert.AreEqual(200, oor.StatusCode);
        }

        [Test]
        public async Task GetSpecificToDo_ActionExecutes_ReturnsNotFound()
        {
            var result = await controller.GetSpecificToDo(-1);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetSpecificToDo_ActionExecutes_ReturnsError()
        {
            mockRepo.Setup(repo => repo.GetByIdAsync<ToDoTask>(-1)).Throws<Exception>().Verifiable();
            var result = await controller.GetSpecificToDo(-1);

            var oor = result as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(500, oor.StatusCode);
        }

        [Test]
        public async Task Add_ActionExecutes_ReturnsToDoTaskData()
        {
            mockRepo.Setup(repo => repo.AddAsync(SeedData.ToDoListTaskForNew)).ReturnsAsync((true, "Data successfully added")).Verifiable();
            var ToDoListTaskForNew = SeedData.ToDoListTaskForNew;
            var dto = mockMapper.Map<ToDoTaskDTO>(ToDoListTaskForNew);
            var result = await controller.CreateToDo(dto);
            var oor = result as ObjectResult;
            Assert.IsNotNull(oor);
        }

        [Test]
        public async Task Add_ActionExecutes_ReturnsMissingReq()
        {
            var ToDoListTask2 = SeedData.ToDoListTask2;
            ToDoListTask2.Title = null;
            var dto = mockMapper.Map<ToDoTaskDTO>(ToDoListTask2);
            var result = await controller.CreateToDo(dto);
            var oor = result as ObjectResult;

            Assert.IsNotNull(oor);
        }

        [Test]
        public async Task Add_ActionExecutes_ReturnsBadRequest()
        {
            var result = await controller.CreateToDo(null);
            var oor = result as ObjectResult;

            Assert.AreEqual(500, oor.StatusCode);
        }

        [Test]
        public async Task Add_ActionExecutes_ReturnsBadRequest2()
        {

            var item = new ToDoTask
            {
                Id = 0
            };
            var dto = mockMapper.Map<ToDoTaskDTO>(item);
            var result = await controller.CreateToDo(dto);
            var oor = result as ObjectResult;

            Assert.AreEqual(500, oor.StatusCode);
        }

        [Test]
        public async Task Add_ActionExecutes_ReturnsModelIsNotValid()
        {
            controller.ModelState.AddModelError("key", "error message");

            var result = await controller.CreateToDo(null);
            var oor = result as ObjectResult;
            Assert.AreEqual(500, oor.StatusCode);
        }

        [Test]
        public async Task Edit_ActionExecutes_ReturnsError()
        {
            var result = await controller.EditToDo(null);
            var oor = result as ObjectResult;

            Assert.AreEqual(500, oor.StatusCode);
        }

        [Test]
        public async Task Edit_ActionExecutes_ReturnsModelIsNotValid()
        {
            controller.ModelState.AddModelError("key", "error message");
            var ToDoListTask1 = SeedData.ToDoListTask1;
            ToDoListTask1.Title = "Edited";

            var dto = mockMapper.Map<ToDoTaskDTO>(ToDoListTask1);
            var result = await controller.EditToDo(dto);
            var oor = result as ObjectResult;
            Assert.AreEqual(500, oor.StatusCode);
        }

        [Test]
        public async Task Edit_ActionExecutes_ReturnsEditedToDoTaskData()
        {
            var editedToDoListTask = SeedData.ToDoListTask1;
            editedToDoListTask.Title = "Edited";
            mockRepo.Setup(repo => repo.UpdateAsync(editedToDoListTask)).ReturnsAsync((true, "Data successfully edited")).Verifiable();

            var dto = mockMapper.Map<ToDoTaskDTO>(editedToDoListTask);

            var result = await controller.EditToDo(dto);
            var oor = result as ObjectResult;
            Assert.AreEqual(200, oor.StatusCode);
        }

        [Test]
        public async Task Delete_ActionExecutes_ReturnSuccess()
        {
            var result = await controller.DeleteToDo(1);
            var oor = result as ObjectResult;

            Assert.AreEqual(200, oor.StatusCode);
        }

        [Test]
        public async Task Delete_ActionExecutes_Return404()
        {
            var result = await controller.DeleteToDo(-1);
            var oor = result as ObjectResult;

            Assert.AreEqual(404, oor.StatusCode);
        }
    }
}

using AutoMapper.Configuration;
using Backend.Application.Dto;
using Backend.Infrastructure.Data;

namespace Backend.Application.Mappers
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<ToDoTask, ToDoTaskDTO>();
            CreateMap<ToDoTaskDTO, ToDoTask>();
        }
    }
}

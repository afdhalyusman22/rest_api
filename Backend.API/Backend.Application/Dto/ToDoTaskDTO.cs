using System;

namespace Backend.Application.Dto
{
    public class ToDoTaskDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? Completed { get; set; }
        public bool? IsDone { get; set; }
    }

    public class ToDoTaskMarkDoneDTO
    {
        public long Id { get; set; }
        public bool? IsDone { get; set; }
    }

    public class ToDoTaskSetPercentDTO
    {
        public long Id { get; set; }
        public decimal? Completed { get; set; }
    }
}

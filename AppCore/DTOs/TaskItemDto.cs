namespace AppCore.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using AppCore.Enums;

    public class TaskItemDto
    {
        public int Id { get; set; }

        [ Required ] public string Details { get; set; }

        [ Required ] public Status CurrentStatus { get; set; }
    }
}
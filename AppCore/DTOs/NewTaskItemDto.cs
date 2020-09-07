namespace AppCore.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class NewTaskItemDto
    {
        [ Required ] public string Details { get; set; }
    }
}
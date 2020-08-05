namespace AppCore.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AppCore.Enums;

    public class TaskItem
    {
        public int Id { get; set; }

        [ Required ] public string Details { get; set; }

        public Status CurrentStatus { get; set; }

        public DateTime Created     { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
namespace AppCore.Entities
{
    using System;
    using System.Runtime.InteropServices.ComTypes;
    using AppCore.Enums;

    public class TaskItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
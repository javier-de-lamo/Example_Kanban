namespace AppCore.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppCore.DTOs;
    using AppCore.Enums;

    public interface ITaskService
    {
        Task<TaskItemDto> CreateTaskAsync( TaskItemDto itemDto );
        Task<List<TaskItemDto>> GetAllTaskByStatusAsync( Status status );
        Task<TaskItemDto> GetTaskByIdAsync( int id );
        Task<int> RemoveTaskAsync( TaskItemDto itemDto );
        Task<int> UpdateTaskAsync( TaskItemDto itemDto );
    }
}
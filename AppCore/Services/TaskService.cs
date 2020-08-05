namespace AppCore.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppCore.DTOs;
    using AppCore.Enums;
    using AppCore.Interfaces;

    public class TaskService : ITaskService
    {
        private readonly ITaskItemRepo _repository;
        public TaskService( ITaskItemRepo repository ) => _repository = repository;

        public async Task<TaskItemDto> CreateTaskAsync( TaskItemDto itemDto )
        {
            return await _repository.CreateTaskAsync( itemDto );
        }

        public async Task<List<TaskItemDto>> GetAllTaskByStatusAsync( Status status )
        {
            return await _repository.GetAllTaskByStatusAsync( status );
        }

        public async Task<TaskItemDto> GetTaskByIdAsync( int id )
        {
            return await _repository.GetTaskByIdAsync( id );
        }

        public async Task<int> RemoveTaskAsync( TaskItemDto itemDto )
        {
            return await _repository.RemoveTaskAsync( itemDto );
        }

        public Task<int> UpdateTaskAsync( TaskItemDto itemDto )
        {
            return _repository.UpdateTaskAsync( itemDto );
        }
    }
}
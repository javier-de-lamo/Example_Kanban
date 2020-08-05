namespace AppCore.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppCore.DTOs;
    using AppCore.Enums;
    using AppCore.Interfaces;

    /// <summary>
    ///     TODO Bussiness Rules
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskItemRepo _repository;
        public TaskService( ITaskItemRepo repository ) => _repository = repository;

        public async Task< TaskItemDto > CreateTaskAsync
            ( TaskItemDto itemDto ) => await _repository.CreateTaskAsync( itemDto );

        public async Task< List< TaskItemDto > > GetAllTaskByStatusAsync
            ( Status status ) => await _repository.GetAllTaskByStatusAsync( status );

        public async Task< TaskItemDto > GetTaskByIdAsync( int id ) => await _repository.GetTaskByIdAsync( id );

        public async Task< int > RemoveTaskAsync( int id ) => await _repository.RemoveTaskAsync( id );

        public Task< int > UpdateTaskAsync( TaskItemDto itemDto ) => _repository.UpdateTaskAsync( itemDto );
    }
}
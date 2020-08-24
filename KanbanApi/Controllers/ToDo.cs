namespace KanbanApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppCore.DTOs;
    using AppCore.Enums;
    using AppCore.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     /api/todo endpoint
    /// </summary>
    [ ApiController, Route( "/api/[controller]" ) ]
    public class ToDo : ControllerBase
    {
        private readonly ILogger< ToDo > _logger;

        private readonly ITaskService _service;

        /// <summary>
        ///     Needed services are injected in the constructor
        /// </summary>
        public ToDo( ILogger< ToDo > logger, ITaskService service )
        {
            _logger  = logger;
            _service = service;
        }

        /// <summary>
        ///     Get all To-Do Tasks
        /// </summary>
        /// <returns>List of To-Do Task</returns>
        [ HttpGet( Name = "GetAllToDoTask" ) ]
        public async Task< ActionResult< List< TaskItemDto > > > Get( )
        {
            _logger.LogInformation( "Get All ToDo Items" );
            return await _service.GetAllTaskByStatusAsync( Status.ToDo );
        }

        /// <summary>
        ///     Get a Task by its Id
        /// </summary>
        /// <param name="id">id of Task</param>
        /// <returns>Task Item with the provided Id, if it exist</returns>
        [ HttpGet( "{id}", Name = "GetTaskById" ) ]
        public async Task< ActionResult< TaskItemDto > > Get( int id )
        {
            _logger.LogInformation( "Get a single Task by its ID = {id}" );
            return await _service.GetTaskByIdAsync( id );
        }

        /// <summary>
        ///     Delete a Task by its Id
        /// </summary>
        /// <param name="id">Id of the Task to be deleted</param>
        /// <returns>Result of Deletion</returns>
        [ HttpDelete( "{id}", Name = "DeleteTaskById" ) ]
        public async Task< ActionResult > Delete( int id )
        {
            _logger.LogInformation( "Delete Task by ID = {id}" );

            var deletedSucessfully = await _service.RemoveTaskAsync( id );

            if( deletedSucessfully ) { return this.NoContent( ); }

            return this.NotFound( );
        }

        /// <summary>
        ///     Create a new To-Do Task
        /// </summary>
        /// <param name="taskDto">Only Task Detail is neccesary, all other fields are ignored</param>
        /// <returns>The task just created</returns>
        [ HttpPost( Name = "CreateNewTask" ) ]
        public async Task< ActionResult > Post( [ FromBody ] TaskItemDto taskDto )
        {
            _logger.LogInformation( "Create New Task" );

            var result = await _service.CreateTaskAsync( taskDto );

            return new CreatedAtRouteResult( "GetTaskById", new { id = result.Id }, result );
        }

        /// <summary>
        ///     Update a To-Do Task
        /// </summary>
        /// <param name="id">Id of task is provided by endpoint</param>
        /// <param name="taskDto">Details and Status of a Task can be updated. Id cannot be updated.</param>
        /// <returns>Result of Update</returns>
        [ HttpPut( "{id}", Name = "UpdateTask" ) ]
        public async Task< ActionResult > Put( int id, [ FromBody ] TaskItemDto taskDto )
        {
            _logger.LogInformation( "Update Task by ID = {id}" );

            taskDto.Id = id;

            var updatedSucessfully = await _service.UpdateTaskAsync( taskDto );

            if( updatedSucessfully ) { return this.Ok( ); }

            return this.NotFound( );
        }
    }
}
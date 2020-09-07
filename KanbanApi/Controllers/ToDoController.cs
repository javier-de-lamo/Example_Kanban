namespace KanbanApi.Controllers
{
    using System;
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
    public class ToDoController : ControllerBase
    {
        private readonly ILogger< ToDoController > _logger;

        private readonly IToDoService _service;

        /// <summary>
        ///     Needed services are injected in the constructor
        /// </summary>
        public ToDoController( ILogger< ToDoController > logger, IToDoService service )
        {
            _logger  = logger;
            _service = service;
        }

        /// <summary>
        ///     Get all To-Do Tasks
        /// </summary>
        /// <returns>List of To-Do Items</returns>
        [ HttpGet( Name = "GetAllToDoTask" ) ]
        public async Task< ActionResult< List< TaskItemDto > > > Get( )
        {
            _logger.LogInformation( "Get All ToDoController Items" );

            try { return this.Ok( await _service.GetAllTaskByStatusAsync( Status.ToDo ) ); }
            catch( Exception ex )
            {
                _logger.LogError( $"Error retrieving data from the database. Error Code = {ex.Message}" );
                return this.StatusCode( 500, "Error retrieving data from the database." );
            }
        }

        /// <summary>
        ///     Get a Task by its Id
        /// </summary>
        /// <param name="id">id of Task</param>
        /// <returns>Task Item with the provided Id, if it exist</returns>
        [ HttpGet( "{id:int:min(1)}", Name = "GetTaskById" ) ]
        public async Task< ActionResult< TaskItemDto > > Get( int id )
        {
            _logger.LogInformation( $"Get a single Task by its ID = {id}" );

            try
            {
                var taskItemDto = await _service.GetTaskByIdAsync( id );
                if( taskItemDto != null ) { return this.Ok( taskItemDto ); }

                return this.NotFound( new { Message = $"Task with id {id} do not exist in the Database." } );
            }
            catch( Exception ex )
            {
                _logger.LogError( $"Error retrieving data from the database. Error Code = {ex.Message}" );
                return this.StatusCode( 500, "Error retrieving data from the database." );
            }
        }

        /// <summary>
        ///     Delete a Task by its Id
        /// </summary>
        /// <param name="id">Id of the Task to be deleted</param>
        /// <returns>Result of Deletion</returns>
        [ HttpDelete( "{id:int:min(1)}", Name = "DeleteTaskById" ) ]
        public async Task< ActionResult > Delete( int id )
        {
            _logger.LogInformation( $"Delete Task by ID = {id}" );

            try
            {
                var deletedSucessfully = await _service.RemoveTaskAsync( id );
                if( deletedSucessfully ) { return this.NoContent( ); }

                return this.NotFound( new { Message = $"Task with id {id} do not exist in the Database." } );
            }
            catch( Exception ex )
            {
                _logger.LogError( $"Error retrieving data from the database. Error Code = {ex.Message}" );
                return this.StatusCode( 500, "Error retrieving data from the database." );
            }
        }

        /// <summary>
        ///     Create a new To-Do Task
        /// </summary>
        /// <param name="newTaskDto">Only Task Detail is neccesary, all other fields are ignored</param>
        /// <returns>The task just created</returns>
        [ HttpPost( Name = "CreateNewTask" ) ]
        public async Task< ActionResult > Post( NewTaskItemDto newTaskDto )
        {
            _logger.LogInformation( "Create New Task" );

            if( newTaskDto == null ) { return this.BadRequest( ); }

            try
            {
                var result = await _service.CreateTaskAsync( newTaskDto );
                return new CreatedAtRouteResult( "GetTaskById", new { id = result.Id }, result );
            }
            catch( Exception ex )
            {
                _logger.LogError( $"Error retrieving data from the database. Error Code = {ex.Message}" );
                return this.StatusCode( 500, "Error retrieving data from the database." );
            }
        }

        /// <summary>
        ///     Update a To-Do Task
        /// </summary>
        /// <param name="id">Id of task is provided by endpoint</param>
        /// <param name="taskDto">Details and Status of a Task can be updated. Id cannot be updated.</param>
        /// <returns>Result of Update</returns>
        [ HttpPut( "{id:int:min(1)}", Name = "UpdateTask" ) ]
        public async Task< ActionResult > Put( int id, TaskItemDto taskDto )
        {
            _logger.LogInformation( $"Update Task by ID = {id}" );

            if( taskDto == null ) { return this.BadRequest( ); }

            try
            {
                taskDto.Id = id;
                var updatedSucessfully = await _service.UpdateTaskAsync( taskDto );
                if( updatedSucessfully ) { return this.Ok( ); }

                return this.NotFound( new { Message = $"Task with id {id} do not exist in the Database." } );
            }
            catch( Exception ex )
            {
                _logger.LogError( $"Error retrieving data from the database. Error Code = {ex.Message}" );
                return this.StatusCode( 500, "Error retrieving data from the database." );
            }
        }
    }
}
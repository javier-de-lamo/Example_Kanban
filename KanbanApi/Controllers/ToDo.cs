namespace KanbanApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AppCore.DTOs;
    using AppCore.Enums;
    using AppCore.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ ApiController, Route( "/api/[controller]" ) ]
    public class ToDo : ControllerBase
    {
        private readonly ILogger< ToDo > _logger;

        private readonly ITaskService _service;

        public ToDo( ILogger< ToDo > logger, ITaskService service )
        {
            _logger  = logger;
            _service = service;
        }


        [ HttpGet( Name = "GetAllToDoTask" ) ]
        public async Task< ActionResult< List< TaskItemDto > > > Get( )
        {
            _logger.LogInformation( "Get All ToDo Items" );
            return await _service.GetAllTaskByStatusAsync( Status.ToDo );
        }

        [ HttpGet( "{id}", Name = "GetTaskById" ) ]
        public async Task< ActionResult< TaskItemDto > > Get( int id )
        {
            _logger.LogInformation( "Get a single Task by its ID = {id}" );
            return await _service.GetTaskByIdAsync( id );
        }

        [ HttpDelete( "{id}", Name = "DeleteTaskById" ) ]
        public async Task< ActionResult > Delete( int id )
        {
            _logger.LogInformation( "Delete Task by ID = {id}" );

            int result = await _service.RemoveTaskAsync( id );

            if( result == 1 ) { return this.NoContent( ); }

            return this.NotFound( );
        }

        [ HttpPost( Name = "CreateNewTask" ) ]
        public async Task< ActionResult > Post( [ FromBody ] TaskItemDto taskDto )
        {
            _logger.LogInformation( "Create New Task" );

            var result = await _service.CreateTaskAsync( taskDto );

            return new CreatedAtRouteResult( "GetTaskById", new { id = result.Id }, result );
        }

        [ HttpPut( "{id}", Name = "UpdateTask" ) ]
        public async Task< ActionResult > Put( int id, [ FromBody ] TaskItemDto taskDto )
        {
            _logger.LogInformation( "Update Task by ID = {id}" );

            taskDto.Id = id;

            var result = await _service.UpdateTaskAsync( taskDto );

            if( result == 1 ) { return this.Ok( ); }

            return this.NotFound( );
        }
    }
}
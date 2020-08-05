﻿namespace Infraestructure.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AppCore.DTOs;
    using AppCore.Entities;
    using AppCore.Enums;
    using AppCore.Interfaces;
    using global::AutoMapper;
    using Infraestructure.Data.Context;
    using Microsoft.EntityFrameworkCore;

    public class TaskItemRepo : ITaskItemRepo
    {
        private readonly KanbanDbContext _context;
        private readonly IMapper         _mapper;

        public TaskItemRepo( KanbanDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper  = mapper;
        }

        /// <summary>
        ///     Returns a concrete Task from the database
        /// </summary>
        /// <param name="id">id of Task to return</param>
        /// <returns>Returns a Task DTO</returns>
        public async Task< TaskItemDto > GetTaskByIdAsync( int id ) => _mapper.Map< TaskItemDto >
            ( await this.GetTaskEntityByIdAsync( id ) );

        /// <summary>
        ///     Updates an item in the database
        /// </summary>
        /// <param name="itemDto">Updated item DTO to be saved</param>
        /// <returns>Number of updated records. 1 means it was sucessfull, 0 means nothing got updated</returns>
        public async Task< int > UpdateTaskAsync( TaskItemDto itemDto )
        {
            TaskItem item = await this.GetTaskEntityByIdAsync( itemDto.Id );

            if( item != null )
            {
                item.LastUpdated             = DateTime.Now;
                _context.Entry( item ).State = EntityState.Modified;
                return await this.SaveAsync( );
            }

            return 0;
        }

        /// <summary>
        ///     Create a new task in the database
        /// </summary>
        /// <param name="itemDto">item DTO without id, because the id will be autogenerated</param>
        /// <returns>Updated task DTO with new id value</returns>
        public async Task< TaskItemDto > CreateTaskAsync( TaskItemDto itemDto )
        {
            TaskItem item = _mapper.Map< TaskItem >( itemDto );
            item.Created     = DateTime.Now;
            item.LastUpdated = DateTime.Now;
            await _context.TaskItems.AddAsync( item );
            await this.SaveAsync( );
            // the database auto-generated id is added to item automatically by Entity Framework
            return _mapper.Map< TaskItemDto >( item );
        }

        /// <summary>
        ///     Remove a Task from the database
        /// </summary>
        /// <param name="itemDto">Item DTO with id</param>
        /// <returns>Number of deleted records. 1 means it was deleted sucessfully. 0 means nothing got deleted.</returns>
        public async Task< int > RemoveTaskAsync( TaskItemDto itemDto )
        {
            var item = _mapper.Map< TaskItem >( itemDto );
            _context.TaskItems.Remove( item );
            return await this.SaveAsync( );
        }

        /// <summary>
        ///     Get a list of task with have a concrete Status
        /// </summary>
        /// <param name="status">Status of the task you want a list of</param>
        /// <returns>List of Task with the required status</returns>
        public async Task< List< TaskItemDto > > GetAllTaskByStatusAsync( Status status )
        {
            var items = await _context.TaskItems.Where( t => t.CurrentStatus == status ).ToListAsync( );

            return _mapper.Map< List< TaskItemDto > >( items );
        }

        private async Task< TaskItem > GetTaskEntityByIdAsync( int id )
        {
            TaskItem item = await _context.TaskItems.FindAsync( id );

            return item;
        }

        private async Task< int > SaveAsync( ) => await _context.SaveChangesAsync( );
    }
}
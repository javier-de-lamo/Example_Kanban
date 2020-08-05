namespace Infraestructure.AutoMapper
{
    using AppCore.DTOs;
    using AppCore.Entities;
    using global::AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile( ) { this.CreateMap< TaskItem, TaskItemDto >( ).ReverseMap( ); }
    }
}
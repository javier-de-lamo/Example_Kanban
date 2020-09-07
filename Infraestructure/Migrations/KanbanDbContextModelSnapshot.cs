namespace Infraestructure.Migrations
{
    using System;
    using Infraestructure.Data.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    [ DbContext( typeof( KanbanDbContext ) ) ]
    internal class KanbanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel( ModelBuilder modelBuilder )
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation
                    ( "ProductVersion", "3.1.6" )
               .HasAnnotation( "Relational:MaxIdentifierLength", 128 )
               .HasAnnotation( "SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn );

            modelBuilder.Entity
            (
                "AppCore.Entities.TaskItem", b =>
                {
                    b.Property< int >( "Id" )
                       .ValueGeneratedOnAdd( )
                       .HasColumnType( "int" )
                       .HasAnnotation
                            ( "SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn );

                    b.Property< DateTime >( "Created" ).HasColumnType( "datetime2" );

                    b.Property< int >( "CurrentStatus" ).HasColumnType( "int" );

                    b.Property< string >( "Details" ).IsRequired( ).HasColumnType( "nvarchar(max)" );

                    b.Property< DateTime >( "LastUpdated" ).HasColumnType( "datetime2" );

                    b.HasKey( "Id" );

                    b.ToTable( "TaskItems" );
                }
            );
#pragma warning restore 612, 618
        }
    }
}
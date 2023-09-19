using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Models;


namespace ToDo.Persistence.EntityConfigurations
{
    public class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.ToTable("ToDoLists");

            builder.HasKey(l => l.Id); 

            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(150);

            //builder.Property(l => l.Value)
            //    .IsRequired();

            builder.Property(l => l.Percentage)
                //.IsRequired()
                .HasColumnType("decimal(18,2)"); 


            // Configure the relationship with ToDoItem
            builder.HasMany(l => l.Items)
                .WithOne(t => t.ToDo)
                .HasForeignKey(t => t.ToDoId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

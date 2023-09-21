using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Models;

namespace ToDo.Persistence.EntityConfigurations
{
    public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.ToTable("ToDoItems"); 

            builder.HasKey(t => t.Id); 

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(p => p.Title)
                .IsUnique();

            builder.Property(t => t.Value)
                .IsRequired(); 

            builder.Property(t => t.Progress)
                //.IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Percentage)
                //.IsRequired()
                .HasColumnType("decimal(18,2)"); 

            builder.Property(t => t.ToDoId)
                .IsRequired(); 

            builder.HasOne(t => t.ToDo)
                .WithMany(l => l.Items)
                .HasForeignKey(t => t.ToDoId); 
        }
    }
}
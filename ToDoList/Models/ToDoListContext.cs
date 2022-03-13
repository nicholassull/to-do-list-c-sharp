using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  //This is the reference object for our database
  public class ToDoListContext : DbContext
  {
    //The DbSets represent the tables within out database
    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryItem> CategoryItem { get; set; }
    public ToDoListContext(DbContextOptions options) : base(options) { }
    
    //Enables lazy loading
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}
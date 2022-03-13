using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Category
  {
    public Category()
    {
      this.JoinEntities = new HashSet<CategoryItem>();
    }

    public int CategoryId { get; set; }
    public string Name { get; set; }
    //A collection of all items that belong to the category
    public virtual ICollection<CategoryItem> JoinEntities { get; set; }
  }
}
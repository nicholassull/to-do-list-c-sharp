using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Item
  {
    public Item()
    {
      this.JoinEntities = new HashSet<CategoryItem>();
    }
    public int ItemId { get; set; }
    public string Description { get; set; }
    
    public virtual ApplicationUser User { get; set; }
    //A collection navigation property, which holds the list of relationships this Item is a part of. This is how we will find its related categories. A CategoryItem is simply a reference to a relationship, with each one including the id of an Item as well as an id of a Category.
    public virtual ICollection<CategoryItem> JoinEntities { get; }
  }
}
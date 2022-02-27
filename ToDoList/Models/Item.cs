using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; set; }

    public Item(string description)
    {
      Description = description;
    }
    public Item(string description, int id)
    {
      Description = description;
      Id = id;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.Id == newItem.Id);
        bool descriptionEquality = (this.Description == newItem.Description);
        return (descriptionEquality && idEquality);
      }
    }
    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM items;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = "INSERT INTO items (description) VALUES(@ItemDescription);";
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ItemDescription";
      param.Value = this.Description;
      cmd.Parameters.Add(param);
      cmd.ExecuteNonQuery();
      //Makes sure that the associated Id is the same between application and database and converts the long LastInsertId into an int.
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Item Find(int searchId)
    {
      //Open a connection
      MySqlConnection conn = DB.Connection();
      conn.Open();
      //Create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM items WHERE id = @ThisId;";
      // We have to use parameter placeholders @ThisId and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = searchId;
      cmd.Parameters.Add(param);
      // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      while (rdr.Read())
      {
        //The 0 and 1 in this context refers to which column the reader should store data from. 0 being IDs and 1 being descriptions.
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      Item foundItem = new Item(itemDescription, itemId);
      //Close connection
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }
  }
}

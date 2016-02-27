using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SalonNamespace
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylist_id;

  public Client(string name, int stylistId, int id = 0)
  {
    _id = id;
    _name = name;
    _stylist_id = stylistId;
  }

  public override bool Equals(System.Object otherClient)
  {
    if(!(otherClient is Client))
    {
      return false;
    }
    else
    {
      Client newClient = (Client) otherClient;
      bool idEquality = this.getID() == newClient.getID();
      bool nameEquality = this.getName() == newClient.getName();
      bool stylistIdEquality = this.getStylistId() == newClient.getStylistId();

      return (idEquality && nameEquality && stylistIdEquality);
    }
  }

  public void Save()
  {

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @StylistId);", conn);

    SqlParameter nameParameter = new SqlParameter();
    nameParameter.ParameterName = "@ClientName";
    nameParameter.Value = this.getName();

    SqlParameter stylistParameter = new SqlParameter();
    stylistParameter.ParameterName = "@StylistId";
    stylistParameter.Value = this.getStylistId();

    cmd.Parameters.Add(nameParameter);
    cmd.Parameters.Add(stylistParameter);

    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
  }

  public static void DeleteAll()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
    cmd.ExecuteNonQuery();
  }

  public int getID()
  {
    return _id;
  }

  public string getName()
  {
    return _name;
  }

  private int getStylistId()
  {
    return _stylist_id;
  }

  public static List<Client> GetAll()
  {
    List<Client> allClients = new List<Client>{};

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();
    SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      string name = rdr.GetString(0);
      int Id = rdr.GetInt32(1);
      int stylistId = rdr.GetInt32(2);
      Client newClient = new Client(name, stylistId, Id);
      allClients.Add(newClient);
    }

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }

    return allClients;
    }


    public static Client Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @ClientId;", conn);
      SqlParameter clientsIdParameter = new SqlParameter();
      clientsIdParameter.ParameterName = "@ClientId";
      clientsIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientsIdParameter);
      rdr = cmd.ExecuteReader();

      string foundClientName = null;
      int foundId = 0;
      int foundStylistId = 0;

      while(rdr.Read())
      {
        foundClientName = rdr.GetString(0);
        foundId = rdr.GetInt32(1);
        foundStylistId = rdr.GetInt32(2);

      }
      Client newClient = new Client(foundClientName, foundStylistId, foundId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return newClient;

    }

    public void UpdateName(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName OUTPUT INSERTED.name WHERE id = @clientsId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;

      cmd.Parameters.Add(newNameParameter);

      SqlParameter clientsIdParameter = new SqlParameter();
      clientsIdParameter.ParameterName = "@clientsId";
      clientsIdParameter.Value = this.getID();
      cmd.Parameters.Add(clientsIdParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @clientsId;", conn);

      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@clientsId";
      clientIdParameter.Value = this.getID();

      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}

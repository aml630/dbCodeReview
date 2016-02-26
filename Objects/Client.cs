using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SalonNamespace
{

  public class Client
  {

    private string _name;
    private int _stylist_id;
    private int _id;

    public Client(string Name, int StylistId, int Id = 0)
    {
      _id = Id;
      _stylist_id = StylistId;
      _name = Name;
    }

    public override bool Equals(System.Object otherClient)
  {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetName() == newClient.GetName();
        bool StylistIdEquality = this.GetStylistId() == newClient.GetStylistId();

        return (idEquality && nameEquality && StylistIdEquality);
      }
  }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public int GetStylistId()
    {
      return _stylist_id;
    }

    public void SetName(string newName)
    {
      _name = newName;
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
        string clientsName = rdr.GetString(0);

        int Id = rdr.GetInt32(1);

        int stylistsId = rdr.GetInt32(2);


        Client newClient = new Client(clientsName, stylistsId, Id);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@ClientName, @StylistId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@ClientName";
      nameParameter.Value = this.GetName();

      SqlParameter IdParameter = new SqlParameter();
      IdParameter.ParameterName = "@StylistId";
      IdParameter.Value = this.GetId();


      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(IdParameter);



      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
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
      Client foundClient = new Client(foundClientName, foundStylistId, foundId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClient;
    }


    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName OUTPUT INSERTED.name WHERE id = @ClientId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter clientsIdParameter = new SqlParameter();
      clientsIdParameter.ParameterName = "@ClientId";
      clientsIdParameter.Value = this.GetId();
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

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id = @ClientId", conn);

      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@ClientId";
      categoryIdParameter.Value = this.GetId();

      cmd.Parameters.Add(categoryIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}

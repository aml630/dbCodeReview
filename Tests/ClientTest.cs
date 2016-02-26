using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SalonNamespace
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Client firstClient = new Client("Red Lobster", 3);
      Client secondClient = new Client("Red Lobster", 3);

      //Assert
      Assert.Equal(firstClient, secondClient);
    }
    [Fact]
    public void Test_To_Find_if_Client_is_saving()
    {
      //Arrange, Act
      Client firstClient = new Client("Red Lobster", 3);

      firstClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{firstClient};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_To_Assign_ID()
    {
      //Arrange, Act
      Client client = new Client("Red Lobster", 3);

      client.Save();
      Client savedClient = Client.GetAll()[0];

      int testID = client.getID();
      int result = savedClient.getID();

      //Assert
      Assert.Equal(testID, result);
    }
    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      //Arrange
      Client testClient = new Client("Red Lobster", 3);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.getID());

      //Assert
      Assert.Equal(testClient, foundClient);
    }

    [Fact]

    public void Test_Update_UpdatesStylistInDatabase()
    {
      string name1 = "Thai";
      Stylist testStylist1 = new Stylist(name1);
      testStylist1.Save();
      //Arrange
      string name = "RedLobster";
      Client testClient = new Client(name, testStylist1.GetId());
      testClient.Save();
      string newName = "GreenLobster";

      //Act
      testClient.UpdateName(newName);

      string result = testClient.getName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesClientFromDatabase()
    {
      //Arrange
      string name1 = "Thai";
      Stylist testStylist1 = new Stylist(name1);
      testStylist1.Save();

      string name2 = "Italian";
      Stylist testStylist2 = new Stylist(name2);
      testStylist2.Save();

      Client testClients1 = new Client("RedLobster", testStylist1.GetId());
      testClients1.Save();
      Client testClients2 = new Client("GreenLobster", testStylist2.GetId());
      testClients2.Save();

      //Act
      testClients1.Delete();
      // List<Stylist> resultStylists = Stylist.GetAll();
      // List<Stylist> testStylistList = new List<Stylist> {testStylist2};

      List<Client> resultClients = Client.GetAll();
      List<Client> testClientsList = new List<Client> {testClients2};

      //Assert
      // Assert.Equal(testStylistList, resultStylists);
      Assert.Equal(testClientsList, resultClients);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}

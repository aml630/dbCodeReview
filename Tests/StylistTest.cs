using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SalonNamespace
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";

    }

    [Fact]
    public void Test_Stylist_Empty()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("John");
      Stylist secondStylist = new Stylist("John");

      //Assert
      Assert.Equal(firstStylist, secondStylist);
    }

    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("John"); testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_AssignsIdToStylistObject()
    {
      //Arrange
      Stylist testStylist = new Stylist("John");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("John"); testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_Update_UpdatesStylistInDatabase()
    {
      //Arrange
      string name = "Bob";
      Stylist testStylist = new Stylist(name);
      testStylist.Save();
      string newName = "John";
      //Act
      testStylist.Update(newName);

      string result = testStylist.GetName();

      //Assert
      Assert.Equal(newName, result);
    }
    //
    // [Fact]
    // public void Test_Delete_DeletesStylistFromDatabase()
    // {
    //   //Arrange
    //   string name1 = "Sandy";
    //   Stylist testStylist1 = new Stylist(name1);
    //   testStylist1.Save();
    //
    //   string name2 = "John";
    //   Stylist testStylist2 = new Stylist(name2);
    //   testStylist2.Save();
    //
    //   Restaurant testRestaurants1 = new Restaurant("RedLobster", testStylist1.GetId());
    //   testRestaurants1.Save();
    //   Restaurant testRestaurants2 = new Restaurant("GreenLobster", "happyStreet", "333444343", testStylist2.GetId());
    //   testRestaurants2.Save();
    //
    //   //Act
    //   testStylist1.Delete();
    //   List<Stylist> resultStylists = Stylist.GetAll();
    //   List<Stylist> testStylistList = new List<Stylist> {testStylist2};
    //
    //   List<Restaurant> resultRestaurantss = Restaurant.GetAll();
    //   List<Restaurant> testRestaurantsList = new List<Restaurant> {testRestaurants2};
    //
    //   //Assert
    //   Assert.Equal(testStylistList, resultStylists);
    //   Assert.Equal(testRestaurantsList, resultRestaurantss);
    // }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }
  }
}

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

    public void Dispose()
    {
      Stylist.DeleteAll();
    }
  }
}

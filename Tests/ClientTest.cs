// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace SalonNamespace
// {
//   public class ClientTest : IDisposable
//   {
//     public ClientTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
//
//     }
//
//     [Fact]
//     public void Test_Client_Empty()
//     {
//       //Arrange, Act
//       int result = Stylist.GetAll().Count;
//
//       //Assert
//       Assert.Equal(0, result);
//     }
//
//   }
//
// }

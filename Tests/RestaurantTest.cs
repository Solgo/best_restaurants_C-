using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_EmptyAtFirst()
    {
      int result = Restaurant.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_SaveToDataBase()
    {
      Restaurant testRestaurant = new Restaurant("Pete's pies", 1);
      testRestaurant.Save();

      int result = Restaurant.GetAll().Count;

      Assert.Equal(1, result);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
    [Fact]
    public void Test_ReturnsTrueForSame()
    {
      Restaurant firstRestaurant = new Restaurant("Fire on the Mountain", 1);
      Restaurant secondRestaurant = new Restaurant("Fire on the Mountain", 1);
      Assert.Equal(firstRestaurant, secondRestaurant);
    }
    [Fact]
    public void Test_IdPassesCorrectly()
    {
      Restaurant testRestaurant = new Restaurant("Sushi", 1);
      int result = testRestaurant.GetId();
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Find()
    {
      Restaurant newRestaurant = new Restaurant("Italian", 2);
      newRestaurant.Save();
      Restaurant testRestaurant = Restaurant.Find(newRestaurant.GetId());

      Assert.Equal(testRestaurant, newRestaurant);
    }
    [Fact]
    public void Test_Update_RestaurantInDataBase()
    {
      string originalRestaurantName = "bob's mexican";
      int originalRestaurantCuisine = 1;
      Restaurant originalRestaurant = new Restaurant(originalRestaurantName, originalRestaurantCuisine);
      originalRestaurant.Save();
      string newRestaurantName = "Bob's sushi";
      int newRestaurantCuisine = 2;
      Restaurant updatedRestaurant = new Restaurant(newRestaurantName, newRestaurantCuisine);
      originalRestaurant.Update(newRestaurantName, newRestaurantCuisine);
      Console.WriteLine(originalRestaurant.GetId());
      Console.WriteLine(originalRestaurant.GetName());
      Console.WriteLine(originalRestaurant.GetCuisineId());

      Restaurant foundRestaurant = Restaurant.Find(originalRestaurant.GetId());
      Console.WriteLine(foundRestaurant.GetId());
      Console.WriteLine(foundRestaurant.GetName());
      Console.WriteLine(foundRestaurant.GetCuisineId());
      Assert.Equal(foundRestaurant, originalRestaurant);
    }
  }
}

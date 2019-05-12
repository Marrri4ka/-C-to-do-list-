using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using MySql.Data.MySqlClient;

namespace ToDoList.Tests
{
[TestClass]
public class ItemTest : IDisposable
{
public void Dispose()
{
	Item.DeleteAll();
}

public ItemTest()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=to_do_list_test;";
}

[TestMethod]
public void ItemConstructor_CreatesInstanceOfItem_Item()
{
	Item newItem = new Item("test", new DateTime(), 1);
	Assert.AreEqual(typeof(Item), newItem.GetType());
}

[TestMethod]
public void GetDescription_ReturnDescription_String()
{
	//Arrange
	string description = "Walk the dog.";
	DateTime duedate = new DateTime (1991,07,10);
	Item newItem = new Item(description,duedate);

	string result = newItem.GetDescription();
	DateTime duedate1 = newItem.GetDueDate();
	Assert.AreEqual(description,result);
	Assert.AreEqual(duedate,duedate1);
}
[TestMethod]
public void SetDescription_SetDescription_String()
{
	//Arrange
	string description = "Walk the dog.";
	Item newItem = new Item(description, new DateTime(), 1);

	//Act
	string updatedDescription = "Do the dishes";
	newItem.SetDescription(updatedDescription);
	string result = newItem.GetDescription();

	//Assert
	Assert.AreEqual(updatedDescription, result);
}

[TestMethod]
public void GetAll_ReturnsEmptyList_ItemList()
{
	//Arrange
	List<Item> newList = new List<Item> {
	};

	//Act
	List<Item> result = Item.GetAll();

	//Assert
	CollectionAssert.AreEqual(newList, result);
}
[TestMethod]
public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
{
	DateTime date = new DateTime();
	Item firstItem = new Item("Mow the lawn", date, 1);
	Item secondItem = new Item("Mow the lawn", date, 1);
	Assert.AreEqual(firstItem, secondItem);
}

[TestMethod]
public void Save_SavesToDatabase_ItemList()
{
	DateTime date = new DateTime();

	Item testItem = new Item("Mow the lawn", date, 1);
	testItem.Save();
	List<Item> result = Item.GetAll();
	List<Item> testList = new List<Item> {
		testItem
	};

	CollectionAssert.AreEqual(testList, result);
}



[TestMethod]
public void GetAll_ReturnsItems_ItemList()
{
	DateTime date = new DateTime();

	//Arrange
	string description01 = "Walk the dog";
	string description02 = "Wash the dishes";
	Item newItem1 = new Item(description01, date, 1);
	newItem1.Save();
	Item newItem2 = new Item(description02, date, 1);
	newItem2.Save();
	List<Item> newList = new List<Item> {
		newItem1, newItem2
	};

	//Act
	List<Item> result = Item.GetAll();

	//Assert
	CollectionAssert.AreEqual(newList, result);
}

[TestMethod]
public void AddCategory_AddCategoryToItem_CategoryList()
{
	Item testItem = new Item ("Mow the lawn",new DateTime());
	testItem.Save();
	Category testCategory = new Category("Home stuff");
	testCategory.Save();
	testItem.AddCategory(testCategory);

	List<Category> result = testItem.GetCategories();
	List<Category> testList = new List<Category> {
		testCategory
	};
	CollectionAssert.AreEqual (testList,result);
}

[TestMethod]
public void Edit_UpdatesItemInDatabase_String()
{
	Category testCategory = new Category("Home stuff");
	testCategory.Save();
	string testDescription = "Mow the lawn";
	Item testItem = new Item (testDescription,new DateTime());
	testItem.Save();

	testItem.AddCategory(testCategory);
	testItem.Delete();
	List<Item> resultCategoryItems = testCategory.GetItems();
	List<Item> testCategoryItems = new List<Item> {
	};
	CollectionAssert.AreEqual (testCategoryItems,resultCategoryItems);
}



[TestMethod]
public void Find_ReturnsCorrectItemFromDatabase_Item()
{
	DateTime date = new DateTime();

	Item testItem = new Item("Mow the lawn", date, 1);
	testItem.Save();

	Item foundItem = Item.Find(testItem.GetId());

	Assert.AreEqual(testItem, foundItem);
}


}
}

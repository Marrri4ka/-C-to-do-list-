using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
[TestClass]
public class CategoryTest : IDisposable
{



public CategoryTest()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=to_do_list_test;";
}


[TestMethod]
public void CategoryConstructor_CreatesInstanceOfCategory_Category()
{
	Category newCategory = new Category("test category");
	Assert.AreEqual(typeof(Category), newCategory.GetType());
}


public void CategoryTests()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=to_do_list_test;";
}
[TestMethod]
public void Delete_DeletesCategoryAssociationsFromDatabase_CategoryList()
{
	DateTime duedate = new DateTime (1991,07,10);
	Item testItem = new Item("Walk the dog",duedate );
	testItem.Save();
	string testName = "Homework";
	Category testCategory = new Category(testName);
	testCategory.Save();

	testCategory.AddItem (testItem);
	testCategory.Delete();
	List<Category> resultItemCategories = testItem.GetCategories();
	List<Category> testItemCategories = new List<Category> {
	};
	CollectionAssert.AreEqual(testItemCategories,resultItemCategories);
}
[TestMethod]
public void Test_AddItem_AddsItemToCategory()
{
	Category testCategory = new Category("Books to read");
	testCategory.Save();
	DateTime duedate = new DateTime (1991,07,10);
	Item testItem = new Item("LOTR",duedate);
	testItem.Save();
	Item testItem2 = new Item ("Im Westen nichts Neues",duedate);
	testItem2.Save();

	testCategory.AddItem(testItem);
	testCategory.AddItem(testItem2);
	List<Item> result = testCategory.GetItems();
	List<Item> testList = new List<Item> {
		testItem,testItem2
	};
	CollectionAssert.AreEqual(testList,result);
}

[TestMethod]
public void GetItems_ReturnAllCategoryItems_ItemList()
{
	Category testCategory = new Category("Books to read");
	testCategory.Save();
	Item testItem1 = new Item ("Lotr",new DateTime (1991,07,10));
	testItem1.Save();
	Item testItem2 = new Item("Im Westen Nichts Neues",new DateTime (1991,07,10));
	testItem2.Save();

	testCategory.AddItem(testItem1);
	List<Item> savedItems = testCategory.GetItems();
	List<Item> testList = new List<Item> {
		testItem1
	};
	CollectionAssert.AreEqual(testList,savedItems);
}


[TestMethod]
public void GetItems_ReturnsAllCategoryItems_ItemList()
{
	//Arrange
	Category testCategory = new Category("Household chores");
	testCategory.Save();
	Item testItem1 = new Item("Mow the lawn",new DateTime (1991,07,10));
	testItem1.Save();
	Item testItem2 = new Item("Buy plane ticket",new DateTime (1991,07,10));
	testItem2.Save();

	//Act
	testCategory.AddItem(testItem1);
	List<Item> savedItems = testCategory.GetItems();
	List<Item> testList = new List<Item> {
		testItem1
	};

	//Assert
	CollectionAssert.AreEqual(testList, savedItems);
}

public void Dispose()
{
	Item.DeleteAll();
	Category.DeleteAll();
}

[TestMethod]
public void GetAll_CategoriesEmptyAtFirst_List()
{
	int result = Category.GetAll().Count;
	Assert.AreEqual(0, result);
}

}
}

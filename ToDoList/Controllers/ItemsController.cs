using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Controllers
{
public class ItemsController : Controller
{
[HttpGet("/items")]
public ActionResult Index()
{
	List<Item> allItems = Item.GetAll();
	return View(allItems);
}

[HttpPost("/items")]
public ActionResult Create(string description, DateTime dueDate)
{
	Item newItem = new Item(description,dueDate);
	newItem.Save();
	List<Item> allItems = Item.GetAll();
	return View("Index", allItems);
}

[HttpGet("/items/new")]
public ActionResult New()
{

	return View();
}

[HttpGet("/items/{id}")]
public ActionResult Show(int id)
{
	Dictionary<string, object> model = new Dictionary<string, object>();
	Item selectedItem = Item.Find(id);
	List<Category> itemCategories = selectedItem.GetCategories();
	List<Category> allCategories = Category.GetAll();
	model.Add("selectedItem", selectedItem);
	model.Add("itemCategories", itemCategories);
	model.Add("allCategories", allCategories);
	return View(model);
}

[HttpPost("/items/delete")]
public ActionResult DeleteAll()
{
	Item.DeleteAll();
	return View();
}

[HttpGet("/categories/{categoryId}/items/{itemId}/edit")]
public ActionResult Edit(int categoryId, int itemId)
{

	Dictionary<string, object> model = new Dictionary<string, object>();
	Category category = Category.Find(categoryId);

	model.Add("category", category);
	Item item = Item.Find(itemId);
	model.Add("item", item);
	return View(model);
}

[HttpPost("/items/{itemId}/categories/new")]
public ActionResult AddCategory(int itemId, int categoryId)
{
	Item item = Item.Find(itemId);
	Category category = Category.Find(categoryId);
	item.AddCategory(category);
	return RedirectToAction("Show",  new { id = itemId });
}
}
}

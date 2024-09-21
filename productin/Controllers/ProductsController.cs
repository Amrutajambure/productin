using Microsoft.AspNetCore.Mvc;
using productin.DATA;
using productin.Models;
using System.Collections.Generic;

public class ProductsController : Controller
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var products = _repository.GetAllProducts();
        return View(products);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _repository.AddProduct(product);
            TempData["Message"] = "Product added successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        var product = _repository.GetProductById(id);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            _repository.UpdateProduct(product);
            TempData["Message"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public IActionResult Delete(int id)
    {
        var product = _repository.GetProductById(id);
        return View(product);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _repository.DeleteProduct(id);
        TempData["Message"] = "Product deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}

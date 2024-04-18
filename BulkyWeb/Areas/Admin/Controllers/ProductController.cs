﻿using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;
namespace BulkyBookWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<Product> objProductsList=_unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
        return View(objProductsList);
    }
    public IActionResult Upsert(int? id)
    {       
        ProductVM productVM = new()
        {
                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product=new Product()
        };
        if(id==null||id==0)
        {
            return View(productVM);
        }
        else
        {
            productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
            return View(productVM);     
        }
    }
    [HttpPost]
    public IActionResult Upsert(ProductVM productVM,IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath=_webHostEnvironment.WebRootPath;
            if (file != null) 
            {
                string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                string productPath=Path.Combine(wwwRootPath,@"images\product");

                if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImagePath)) 
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                productVM.Product.ImageUrl=@"\images\product\"+fileName;
            }
            if(productVM.Product.ProductId == 0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }
           
            _unitOfWork.Save();
            TempData["success"] = "Product created successfylly";
            return RedirectToAction("Index");
        }
        else
        {
            productVM.CategoryList=_unitOfWork.Category.GetAll().Select(u=>new SelectListItem()
            {
                Text=u.Name,
                Value = u.CategoryId.ToString()
            });
            return View(productVM);
        }
    }
    
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = objProductList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Product.Get(u=>u.ProductId == id);
        if (productToBeDeleted == null)
        {
            return Json(new {success=false, message="Error while deleting"});
        }
        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
       if(System.IO.File.Exists(oldImagePath)) 
        {
            System.IO.File.Delete(oldImagePath);
        }
        _unitOfWork.Product.Remove(productToBeDeleted);
        _unitOfWork.Save();
        return Json(new { success=true,message="Delete Successfull" });
    }

    #endregion
}
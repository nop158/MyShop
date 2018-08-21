using MyShop.Core.Contract;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryManagerController(IRepository<ProductCategory> contexts)
        {
            context = contexts;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();

            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory product = new ProductCategory();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productEdit = context.Find(Id);
            if (productEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productEdit);
            }

        }
        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productEdit = context.Find(Id);
            if (productEdit == null)
            {
                return HttpNotFound();
            }
            else
               if (!ModelState.IsValid)
            {
                return View(product);
            }
            productEdit.Category = product.Category;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productDel = context.Find(Id);
            if (productDel == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productDel);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(ProductCategory product, string Id)
        {
            ProductCategory productDel = context.Find(Id);
            if (productDel == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}
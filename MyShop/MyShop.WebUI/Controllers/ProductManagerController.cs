using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(Product product)
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
            Product productEdit = context.Find(Id);
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
        public ActionResult Edit(Product product, string Id)
        {
            Product productEdit = context.Find(Id);
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
            productEdit.Price = product.Price;
            productEdit.Description = product.Description;
            productEdit.Name = product.Name;
            productEdit.Image = product.Image;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            Product productDel = context.Find(Id);
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
        public ActionResult ConfirmDelete(Product product, string Id)
        {
            Product productDel = context.Find(Id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.ViewModels;
using MyShop.Core.Contract;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel ViewModel = new ProductManagerViewModel();
            ViewModel.Product = new Product();
            ViewModel.ProductCategory = productCategories.Collection();
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
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
                ProductManagerViewModel ViewModel = new ProductManagerViewModel();
                ViewModel.Product = productEdit;
                ViewModel.ProductCategory = productCategories.Collection();
                return View(ViewModel);

            }

        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
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
            if (file != null)
            {
                productEdit.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + productEdit.Image);
            }
            productEdit.Category = product.Category;
            productEdit.Price = product.Price;
            productEdit.Description = product.Description;
            productEdit.Name = product.Name;
            //productEdit.Image = "e05ddcd9-532e-4cdb-90f8-71a54937ac28.jpg";
            context.Update(productEdit);
            context.Commit();

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
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository  //เก็บ ID Category
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory product)
        {
            ProductCategory productUpdate = productCategories.Find(p => p.Id == product.Id);
            if (productUpdate != null)
            {
                productUpdate = product;
            }
            else
            {
                throw new Exception("Product Category not founded");
            }
        }
        public ProductCategory Find(string id)  //Return เป็น object Product
        {
            ProductCategory productFind = productCategories.Find(p => p.Id == id);
            if (productFind != null)
            {
                return productFind;
            }
            else
            {
                throw new Exception("Product Category not founded");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productDel = productCategories.Find(p => p.Id == id);
            if (productDel != null)
            {
                productCategories.Remove(productDel);
            }
            else
            {
                throw new Exception("Product Category not founded");
            }
        }
    }
}

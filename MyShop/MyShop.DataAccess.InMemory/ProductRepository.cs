using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productUpdate = products.Find(p => p.Id == product.Id);
            if (productUpdate != null)
            {
                productUpdate = product;
            }
            else
            {
                throw new Exception("Product not founded");
            }
        }
        public Product Find(string id)  //Return เป็น object Product
        {
            Product productFind = products.Find(p => p.Id == id);
            if (productFind != null)
            {
                return productFind;
            }
            else
            {
                throw new Exception("Product not founded");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productDel = products.Find(p => p.Id == id);
            if (productDel != null)
            {
                products.Remove(productDel);
            }
            else
            {
                throw new Exception("Product not founded");
            }
        }
    }

}



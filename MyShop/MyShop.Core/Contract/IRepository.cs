using System.Linq;
using MyShop.Core.Models;

namespace MyShop.Core.Contract
{
    public interface IRepository<T> where T : BaseIntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Bloger.DataAccess.Abstract;
using Bloger.DataAccess.Concrete;

namespace Bloger.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        
        //Generic Repository Pattern, “generic” kelimesinden de anlaşılacağı gibi bu“genel” yapıyı oluşturmamızı sağlıyor. Yani, ortak işlemlerimiz için genel bir yapı kurup, her bir modelin bu genel yapı üzerinden o işlemi gerçekleştirmesini sağlayacağız.
        public void Insert(T item)
        {
            using var context = new Context();
            context.Add(item);
            context.SaveChanges();
        }

        public void Delete(T item)
        {
            using var context = new Context();
            context.Remove(item);
            context.SaveChanges();
        }

        public void Update(T item)
        {
            using var context = new Context();
            context.Update(item);
            context.SaveChanges();
        }

        public List<T> GetListAll()
        {
            using var context = new Context();
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            using var context = new Context();
            return context.Set<T>().Find(id)!;
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using var context = new Context();
            return context.Set<T>().Where(filter).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.Business.Abstract
{
    public interface IGenericService<T>
    {
        void Add(T item);
        void Delete(T item);
        void Update(T item);
        List<T> GetList();
        T TGetById(int id);
    }
}

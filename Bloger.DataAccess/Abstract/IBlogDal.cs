using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete;

namespace Bloger.DataAccess.Abstract
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        // blog sayfasında category listememiz için gerekli sayfa

        // kategori ile listeleme
        List<Blog> GetListWithCategory();

        // kullanıcıya göre katagori listeleme
        List<Blog> GetListWithCategoryByUser(int id);
    }
}

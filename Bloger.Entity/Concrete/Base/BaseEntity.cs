using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.Entity.Concrete.Base
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

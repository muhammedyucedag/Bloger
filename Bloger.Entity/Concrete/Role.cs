using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloger.Entity.Concrete.Base;
using Microsoft.AspNetCore.Identity;

namespace Bloger.Entity.Concrete
{
    public class Role : IdentityRole<int>
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}

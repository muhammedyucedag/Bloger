﻿using Bloger.Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloger.Business.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori Adı");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Kategori Açıklaması");
            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Kategori Adı En Fazla 50 Karakter içermelidir");
            RuleFor(x => x.CategoryName).MinimumLength(2).WithMessage("Kategori Adı En Az 2 Karakter içermelidir");
        }
    }
}


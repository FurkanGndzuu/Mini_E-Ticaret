using ETicaretAPI.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.name).NotNull().NotEmpty()
                .WithMessage("Please Don't Forget Write Your Name")
                .MinimumLength(2).MaximumLength(50)
                .WithMessage("Please Write Your Name Between 2 And 50 Characters");

            RuleFor(x => x.price).NotNull().NotEmpty()
                .WithMessage("Please Don't Forget Write Product Price").
                GreaterThan(0).WithMessage("Product Price Should be Greater Than 1 TL");

            RuleFor(x => x.stock).NotNull().NotEmpty()
               .WithMessage("Please Don't Forget Write Product Stock").
               GreaterThan(0).WithMessage("Product Price Should be Greater Than 0");

        }
    }
}

using FluentValidation;
using Products.Api.ModelsDto;

namespace Products_Api.ModelsDto;

public class ProductCreateDtoValidate : AbstractValidator<ProductCreateDto>
{
  public ProductCreateDtoValidate()
  {
    RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
    RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
    RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
  }
}

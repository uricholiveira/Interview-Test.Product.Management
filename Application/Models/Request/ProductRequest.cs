using System.ComponentModel.DataAnnotations;
using Application.Models.Common;

namespace Application.Models.Request;

public class GetProductRequest : PaginationRequest
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(1, ErrorMessage = "Precisa contar pelo menos {0} caracteres")]
    [MaxLength(50, ErrorMessage = "Precisa contar no máximo {0} caracteres")]
    public string? Name { get; set; }
}

public class CreateProductRequest
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(1, ErrorMessage = "Precisa contar pelo menos {0} caracteres")]
    [MaxLength(50, ErrorMessage = "Precisa contar no máximo {0} caracteres")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    public required int CategoryId { get; set; }
}

public class UpdateProductRequest
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [MinLength(1, ErrorMessage = "Precisa contar pelo menos {0} caracteres")]
    [MaxLength(50, ErrorMessage = "Precisa contar no máximo {0} caracteres")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Campo obrigatório")]
    public required int CategoryId { get; set; }
}
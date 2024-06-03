using System.ComponentModel.DataAnnotations;
using Application.Models.Common;

namespace Application.Models.Request;

public class GetCategoryRequest : PaginationRequest
{
    [MinLength(1, ErrorMessage = "Precisa contar pelo menos {0} caracteres")]
    [MaxLength(50, ErrorMessage = "Precisa contar no máximo {0} caracteres")]
    public string? Name { get; set; }
}

public class CreateCategoryRequest
{
    [MinLength(1, ErrorMessage = "Precisa contar pelo menos {0} caracteres")]
    [MaxLength(50, ErrorMessage = "Precisa contar no máximo {0} caracteres")]
    public required string Name { get; set; }
}

public class UpdateCategoryRequest
{
    [MinLength(1, ErrorMessage = "Precisa contar pelo menos {0} caracteres")]
    [MaxLength(50, ErrorMessage = "Precisa contar no máximo {0} caracteres")]
    public required string Name { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request;

public class UpdateInventoryRequest
{
    [Required(ErrorMessage = "Campo obrigatório")]
    public required int Quantity { get; set; }
}
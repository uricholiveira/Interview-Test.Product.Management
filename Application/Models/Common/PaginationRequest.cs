using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Common;

/// <summary>
///     Represents the pagination information used for retrieving a specific page of data.
/// </summary>
public class PaginationRequest
{
    /// <summary>
    ///     Represents the pagination information used for retrieving a specific page of data.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Deve ser maior que zero")]
    [DefaultValue(1)]
    public int Page { get; set; }

    /// <summary>
    ///     Represents the number of items to be displayed per page in a paginated collection.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Deve ser maior que zero")]
    [DefaultValue(10)]
    public int PageSize { get; set; }
}
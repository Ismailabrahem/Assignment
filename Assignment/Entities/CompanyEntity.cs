using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Entities;

internal class CompanyEntity
{
    [Key]
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string CompanyDescription { get; set; } = null!;

    public int CompanyValueId { get; set; }
    public CompanyValueEntity CompanyValue { get; set; } = null!;


    public int CompanyBranchId { get; set; }
    public CompanyBranchEntity CompanyBranch { get; set; } = null!;
}

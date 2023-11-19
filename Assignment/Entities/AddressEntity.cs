using System.ComponentModel.DataAnnotations;

namespace Assignment.Entities;

internal class AddressEntity
{
    [Key]
    public int Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string StreetNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string City { get; set; } = null!;
}

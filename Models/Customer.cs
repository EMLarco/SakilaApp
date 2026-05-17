using System.ComponentModel.DataAnnotations;

namespace SakilaApp.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime LastUpdate { get; set; } = DateTime.Now;

    // Propiedad para eliminación lógica (soft delete)
    public bool IsDeleted { get; set; } = false;
}
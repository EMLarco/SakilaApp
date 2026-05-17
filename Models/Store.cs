using System.ComponentModel.DataAnnotations;

namespace SakilaApp.Models;

public class Store
{
    public byte StoreId { get; set; }
    public byte ManagerStaffId { get; set; }
    public int AddressId { get; set; }
    public DateTime LastUpdate { get; set; }

    // Propiedad para eliminación lógica (soft delete)
    public bool IsDeleted { get; set; } = false;
}
using System.ComponentModel.DataAnnotations;

namespace SakilaApp.Models;

public class FilmActor
{
    public int ActorId { get; set; }
    public int FilmId { get; set; }
    public DateTime LastUpdate { get; set; }

    // Propiedades de navegación
    public virtual Actor Actor { get; set; } = null!;
    public virtual Film Film { get; set; } = null!;

    // Propiedad para eliminación lógica (soft delete)
    public bool IsDeleted { get; set; } = false;
}
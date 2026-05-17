using System.ComponentModel.DataAnnotations;

namespace SakilaApp.Models;

public class Actor
{
    public int ActorId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime LastUpdate { get; set; }

    // Propiedad para eliminación lógica (soft delete)
    public bool IsDeleted { get; set; } = false;

    // Relación muchos a muchos con Film a través de FilmActor
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
}
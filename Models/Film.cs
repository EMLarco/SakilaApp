using System.ComponentModel.DataAnnotations;

namespace SakilaApp.Models;

public class Film
{
    public int FilmId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReleaseYear { get; set; } = string.Empty;
    public byte RentalDuration { get; set; }
    public decimal RentalRate { get; set; }
    public short? Length { get; set; }
    public decimal ReplacementCost { get; set; }
    public string Rating { get; set; } = string.Empty;
    public byte LanguageId { get; set; }
    public byte? OriginalLanguageId { get; set; }
    public DateTime LastUpdate { get; set; }

    // Relación muchos a muchos con Actor a través de FilmActor
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    // Propiedad para eliminación lógica (soft delete)
    public bool IsDeleted { get; set; } = false;
}
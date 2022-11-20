using System.ComponentModel.DataAnnotations;

namespace Astromedia.Models;

public class Like 
{
    public int Id { get; set; }
    public Comentario Comentario { get; set; }
    public Postagem Postagem { get; set; }

    [Required]
    public Usuario Usuario { get; set; }
}
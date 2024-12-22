using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;
[ExcludeFromCodeCoverage]
public class Categorias
{
    [Key]
    public int CategoriaId { get; set; }
   
    public string? Descripcion { get; set; }
}

using System.ComponentModel.DataAnnotations;
namespace practica1.Models
{
    public class equipos
    {
        [Key] 
        public int id_equipos { get; set; }

        public string nombre { get; set; }
        public decimal costo { get; set; }
    }

}

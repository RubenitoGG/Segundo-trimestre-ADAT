using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCine.MODEL
{
    [Table("Tarifa")]
    public class Tarifa : PropertyValidateModel
    {
        public Tarifa()
        {
            Entradas = new HashSet<Entrada>();
        }

        [Key]
        [Required(ErrorMessage = "Necesita un codigo de tarifa.")]
        public string CodigoTarifa { get; set; }
        [Required(ErrorMessage = "Necesita un formato de tarifa.")]
        public string formatoSala { get; set; }
		[Required(ErrorMessage = "Necesita un formato de tarifa.")]
		public string formatoFuncion { get; set; }
		[Required(ErrorMessage = "Necesita un formato de tarifa.")]
		public string formatoCliente { get; set; }
		public bool diaEspectador { get; set; }
		[Required(ErrorMessage = "Necesita un precio de tarifa.")]
        public double precio { get; set; }


        public virtual ICollection<Entrada> Entradas { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using ControleCelulasWebMvc.Models.Enums;

namespace ControleCelulasWebMvc.Models
{
    public class Area
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O tamanho do nome deve ser entre {2} e {1}")]
        public string Nome { get; set; }
        public StatusCadastro Status { get; set; }
        
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Display(Name = "Coordenador")]
        public int CoordenadorId { get; set; }
        public Coordenador Coordenador { get; set; }
    }
}
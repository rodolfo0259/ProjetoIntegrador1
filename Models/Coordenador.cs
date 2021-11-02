using System.ComponentModel.DataAnnotations;
using ControleCelulasWebMvc.Models.Enums;

namespace ControleCelulasWebMvc.Models
{
    public class Coordenador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O tamanho do nome deve ser entre {2} e {1}")]
        public string Nome { get; set; }
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")] 
        public StatusCadastro Status { get; set; }
    }
}
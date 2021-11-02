using System.ComponentModel.DataAnnotations;
using ControleCelulasWebMvc.Models.Enums;

namespace ControleCelulasWebMvc.Models
{
    public class Celula
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O tamanho do nome deve ser entre {2} e {1}")]
        public string Nome { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }

        [Required(ErrorMessage = "O campo responsável é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O tamanho do nome do responsável deve ser entre {2} e {1}")]
        [Display(Name = "Responsável")]
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo reunião é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O tamanho do campo deve ser entre {2} e {1}")]
        [Display(Name = "Reunião")]        
        public string DiaHoraReuniao { get; set; }
        public StatusCadastro Status { get; set; }

        [Display(Name = "Área")]
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
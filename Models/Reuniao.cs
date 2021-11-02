using System;
using System.ComponentModel.DataAnnotations;

namespace ControleCelulasWebMvc.Models
{
    public class Reuniao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo data da reunião é obrigatório")]        
        [Display(Name = "Data da Reunião")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataHoraReuniao { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")] 
        [Display(Name = "Célula")]
        public int CelulaId { get; set; }
        public Celula Celula { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")] 
        [Display(Name = "Membro")]
        public int PessoaId { get; set; }      
        public Pessoa Pessoa { get; set; }        
    }
}
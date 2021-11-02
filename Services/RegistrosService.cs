using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControleCelulasWebMvc.Data;
using ControleCelulasWebMvc.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ControleCelulasWebMvc.Services
{
    public class RegistrosService
    {
        private readonly WebDbContext _context;

        public RegistrosService(WebDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reuniao>> FindByDateAsync(DateTime? data, int celula)
        {
            var result = from obj in _context.Reuniao 
                select obj;
           
            result = result            
                .Where(d => d.DataHoraReuniao == data.Value)
                .Where(d => d.CelulaId == celula);                

            return await result
                .Include(x => x.Pessoa)
                .Include(x => x.Pessoa.Celula)
                .OrderByDescending(x => x.DataHoraReuniao)
                .ToListAsync();
        }
    }
}
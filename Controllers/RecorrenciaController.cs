using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI.Models;

namespace ProjetoMidasAPI_Final.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")] // Define a rota sem precisar colocar API: /Recorrencia
    public class RecorrenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Construtor
        public RecorrenciaController(AppDbContext context) => _context = context;

        private int UserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private IQueryable<Recorrencia> QueryUsuario()
        {
            return _context.Recorrencias
                .Where(r => r.IdUsuario == UserId);
        }
        
        // READ - Lista todas as recorrências
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Recorrencia>>> GetAll() =>
            await QueryUsuario()
            .Include(r => r.TipoRecorrencia)
            .ToListAsync();

        // READ - Busca por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Recorrencia>> GetById(int id)
        {
            var recorrencia = await QueryUsuario()
            .Include(r => r.TipoRecorrencia)
            .FirstOrDefaultAsync(r => r.idRecorrente == id);
        
            if (recorrencia == null)
                return NotFound();
            
            return recorrencia;
        }

        // CREATE - Adiciona nova recorrência
        [HttpPost("New")]
        public async Task<ActionResult<Recorrencia>> Post(Recorrencia recorrencia)
        {
            recorrencia.IdUsuario = UserId; 
            recorrencia.momentoCriacao = DateTime.UtcNow; 
            
            _context.Recorrencias.Add(recorrencia); 
            
            await _context.SaveChangesAsync(); 
            
            return CreatedAtAction(nameof(GetById), new { id = recorrencia.idRecorrente }, recorrencia); 
        }

        // UPDATE - Atualiza recorrência existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Recorrencia recorrencia)
        {
            var existente = await QueryUsuario().FirstOrDefaultAsync(r => r.idRecorrente == id);
            
            if (existente == null) return NotFound();

            existente.Valor = recorrencia.Valor;
            existente.TipoRecorrenciaId = recorrencia.TipoRecorrenciaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE - Remove recorrência
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existente = await QueryUsuario().FirstOrDefaultAsync(r => r.idRecorrente == id);
            
            if (existente == null) return NotFound();

            _context.Recorrencias.Remove(existente);
            await _context.SaveChangesAsync();
            return NoContent();   
        }
        [HttpGet("Data/{data}")]
        public async Task<ActionResult<IEnumerable<Recorrencia>>> GetByData(DateTime data)
        {
            return await QueryUsuario()
                .Where(r => r.momentoCriacao.Date == data.Date)
                .ToListAsync();
        }
        [HttpGet("Tipo/{tipoId}")]
        public async Task<ActionResult<IEnumerable<Recorrencia>>> GetByTipo(int tipoId)
        {
            return await QueryUsuario()
                .Where(r => r.TipoRecorrenciaId == tipoId)
                .ToListAsync();
        }
        [HttpGet("Valor/{valor}")]
        public async Task<ActionResult<IEnumerable<Recorrencia>>> GetByValor(decimal valor)
        {
            return await QueryUsuario()
                .Where(r => r.Valor == valor)
                .ToListAsync();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using ProjetoMidasAPI.Data;

namespace ProjetoMidasAPI_Final.Controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota sem precisar colocar API: /Recorrencia
    public class RecorrenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Construtor
        public RecorrenciaController(AppDbContext context) => _context = context;

        // READ - Lista todas as recorrências
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Recorrencia>>> GetAll() =>
            await _context.Recorrencias
            .Include(r => r.TipoRecorrencia)
            .ToListAsync();

        // READ - Busca por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Recorrencia>> GetById(int id)
        {
            var recorrencia = await _context.Recorrencias
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
            recorrencia.momentoCriacao = DateTime.UtcNow; // Define data/hora atual
            _context.Recorrencias.Add(recorrencia); // Adiciona a recorrência
            await _context.SaveChangesAsync(); // Salva no banco
            return CreatedAtAction(nameof(GetById), new { id = recorrencia.idRecorrente }, recorrencia); // Retorna 201
        }

        // UPDATE - Atualiza recorrência existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Recorrencia recorrencia)
        {
            if (id != recorrencia.idRecorrente) return BadRequest();
            _context.Entry(recorrencia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE - Remove recorrência
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var recorrencia = await _context.Recorrencias.FindAsync(id);
            if (recorrencia == null) return NotFound();
            _context.Recorrencias.Remove(recorrencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
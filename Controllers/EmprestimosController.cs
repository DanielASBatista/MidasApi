using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;

namespace ProjetoMidasAPI.Controllers
{

    [ApiController]
    [Route("[controller]")] // Define a rota Emprestimos
    public class EmprestimosController : ControllerBase
    {
        private readonly AppDbContext _context;

        //Construtor
        public EmprestimosController(AppDbContext context) => _context = context;


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetAll() =>
            await _context.Emprestimos.ToListAsync();

        // Retorna uma simulação específica
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetById(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
                return NotFound();

            return emprestimo;
        }
        // Cria uma nova simulação
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> Create(Emprestimo emprestimo)
        {
            // Calcula valores da simulação
            emprestimo.CalcularValores();

            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = emprestimo.IdSimEmprestimo },
                emprestimo
            );
        }
        // Edita uma simulação existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Emprestimo emprestimoAtualizado)
        {
            if (id != emprestimoAtualizado.IdSimEmprestimo)
                return BadRequest();

            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
                return NotFound();

            // Atualiza campos editáveis
            emprestimo.nomeEmprestimo = emprestimoAtualizado.nomeEmprestimo;
            emprestimo.descricaoEmprestimo = emprestimoAtualizado.descricaoEmprestimo;
            emprestimo.provedorEmprestimo = emprestimoAtualizado.provedorEmprestimo;
            emprestimo.valorEmprestimo = emprestimoAtualizado.valorEmprestimo;
            emprestimo.parcelasEmprestimo = emprestimoAtualizado.parcelasEmprestimo;
            emprestimo.IOFemprestimo = emprestimoAtualizado.IOFemprestimo;
            emprestimo.despesasEmprestimo = emprestimoAtualizado.despesasEmprestimo;
            emprestimo.tarifasEmprestimo = emprestimoAtualizado.tarifasEmprestimo;
            emprestimo.Data = emprestimoAtualizado.Data;

            // Recalcula a simulação
            emprestimo.CalcularValores();

            await _context.SaveChangesAsync();
            return NoContent();
        }
        // Remove uma simulação
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(id);
            if (emprestimo == null)
                return NotFound();

            _context.Emprestimos.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
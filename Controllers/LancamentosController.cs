using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI.Models;

namespace ProjetoMidasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] // Define a rota sem precisar colocar API: /Lancamentos
    public class LancamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Construtor
        public LancamentosController(AppDbContext context) => _context = context;

        // READ - Lista todos os lançamentos
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetAll() =>
            await _context.Lancamentos.ToListAsync();

        // READ - Busca por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Lancamento>> GetById(int id)
        {
            var lancamento = await _context.Lancamentos.FindAsync(id);
            return lancamento == null ? NotFound() : lancamento;
        }

        // CREATE - Adiciona novo lançamento
        [HttpPost("New")]
        public async Task<ActionResult<Lancamento>> Post(Lancamento lancamento)
        {
            lancamento.DataCriacao = DateTime.UtcNow; // Define data/hora atual
            _context.Lancamentos.Add(lancamento); // Adiciona ao lançamento
            await _context.SaveChangesAsync(); // Salva no banco
            return CreatedAtAction(nameof(GetById), new { id = lancamento.IdLancamento }, lancamento); // Retorna 201
        }

        // UPDATE - Atualiza lançamento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Lancamento lancamento)
        {
            if (id != lancamento.IdLancamento) return BadRequest();
            _context.Entry(lancamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE - Remove lançamento
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento == null) return NotFound();
            _context.Lancamentos.Remove(lancamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Busca por Data
        [HttpGet("DataReferencia/{data}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByData(DateTime data) =>
            await _context.Lancamentos.Where(l => l.Data.Date == data.Date).ToListAsync();

        // Busca por DataCriacao
        [HttpGet("DataCriacao/{data}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByDataCriacao(DateTime data) =>
            await _context.Lancamentos.Where(l => l.DataCriacao.Date == data.Date).ToListAsync();

        // Busca por Valor
        [HttpGet("valor/{valor}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByValor(decimal valor) =>
            await _context.Lancamentos.Where(l => l.Valor == valor).ToListAsync();

        // Busca por Ano
        [HttpGet("ano/{ano}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByAno(int ano) =>
            await _context.Lancamentos.Where(l => l.Data.Year == ano).ToListAsync();

        // Busca por Ano/Mês    
        [HttpGet("mes/{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByMes(int ano, int mes) =>
            await _context.Lancamentos.Where(l => l.Data.Year == ano && l.Data.Month == mes).ToListAsync();

        // Busca por Ano/Mês/Dia    
        [HttpGet("dia/{ano}/{mes}/{dia}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByDia(int ano, int mes, int dia) =>
            await _context.Lancamentos.Where(l => l.Data.Year == ano && l.Data.Month == mes && l.Data.Day == dia).ToListAsync();

        // Somatória de valores
        [HttpGet("somatoria")]
        public async Task<ActionResult<decimal>> GetSomatoria() =>
            await _context.Lancamentos.SumAsync(l => l.Valor);

        // Comparação (maior que valor informado)
        [HttpGet("comparacao/{valor}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetComparacao(decimal valor) =>
            await _context.Lancamentos.Where(l => l.Valor > valor).ToListAsync();
    }
}

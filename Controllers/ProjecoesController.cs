using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI.Models;

namespace ProjetoMidasAPI.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")] // Rota: /Projecoes
    public class ProjecoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjecoesController(AppDbContext context) => _context = context;

        // READ - Lista todas as projeções
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetAll() =>
            await _context.Projecoes.ToListAsync();

        // READ - Busca por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Projecao>> GetById(int id)
        {
            var projecao = await _context.Projecoes.FindAsync(id);
            return projecao == null ? NotFound() : projecao;
        }

        // CREATE
        [HttpPost("New")]
        public async Task<ActionResult<Projecao>> Post(Projecao projecao)
        {
            projecao.DataCriacao = DateTime.UtcNow; // Define data/hora atual
            _context.Projecoes.Add(projecao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = projecao.IdProjecao }, projecao);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Projecao projecao)
        {
            if (id != projecao.IdProjecao) return BadRequest();
            _context.Entry(projecao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projecao = await _context.Projecoes.FindAsync(id);
            if (projecao == null) return NotFound();
            _context.Projecoes.Remove(projecao);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Busca por DataReferencia
        [HttpGet("DataReferencia/{data}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByDataReferencia(DateTime data) =>
            await _context.Projecoes.Where(p => p.DataReferencia.Date == data.Date).ToListAsync();

        // Busca por DataCriacao
        [HttpGet("DataCriacao/{data}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByDataCriacao(DateTime data) =>
            await _context.Projecoes.Where(p => p.DataCriacao.Date == data.Date).ToListAsync();

        // Busca por ValorPrevisto
        [HttpGet("valor/{valor}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByValor(decimal valor) =>
            await _context.Projecoes.Where(p => p.ValorPrevisto == valor).ToListAsync();

        // Busca por Ano
        [HttpGet("ano/{ano}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByAno(int ano) =>
            await _context.Projecoes.Where(p => p.DataReferencia.Year == ano).ToListAsync();

        // Busca por Ano/Mês    
        [HttpGet("mes/{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByMes(int ano, int mes) =>
            await _context.Projecoes.Where(p => p.DataReferencia.Year == ano && p.DataReferencia.Month == mes).ToListAsync();

        // Busca por Ano/Mês/Dia    
        [HttpGet("dia/{ano}/{mes}/{dia}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByDia(int ano, int mes, int dia) =>
            await _context.Projecoes.Where(p => p.DataReferencia.Year == ano && p.DataReferencia.Month == mes && p.DataReferencia.Day == dia).ToListAsync();

        // Somatória de valores
        [HttpGet("somatoria")]
        public async Task<ActionResult<decimal>> GetSomatoria() =>
            await _context.Projecoes.SumAsync(p => p.ValorPrevisto);

        // Comparação (maior que valor informado)
        [HttpGet("comparacao/{valor}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetComparacao(decimal valor) =>
            await _context.Projecoes.Where(p => p.ValorPrevisto > valor).ToListAsync();
    }
}

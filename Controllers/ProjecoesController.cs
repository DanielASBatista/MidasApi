using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI.Models;

namespace ProjetoMidasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProjecoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjecoesController(AppDbContext context) => _context = context;

        private int UserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private IQueryable<Projecao> QueryUsuario()
        {
            return _context.Projecoes
                .Where(p => p.IdUsuario == UserId);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetAll()
        {
            return await QueryUsuario().ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        public async Task<ActionResult<Projecao>> GetById(int id)
        {
            var projecao = await QueryUsuario()
                .FirstOrDefaultAsync(p => p.IdProjecao == id);

            if (projecao == null)
                return NotFound();

            return projecao;
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost("New")]
        public async Task<ActionResult<Projecao>> Post(Projecao projecao)
        {
            projecao.IdUsuario = UserId;
            projecao.DataCriacao = DateTime.UtcNow;

            _context.Projecoes.Add(projecao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = projecao.IdProjecao },
                projecao);
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Projecao projecao)
        {
            if (id != projecao.IdProjecao) return BadRequest();

            var existente = await QueryUsuario()
                .FirstOrDefaultAsync(p => p.IdProjecao == id);

            if (existente == null) return NotFound();

            existente.ValorPrevisto = projecao.ValorPrevisto;
            existente.DataReferencia = projecao.DataReferencia;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projecao = await QueryUsuario()
                .FirstOrDefaultAsync(p => p.IdProjecao == id);

            if (projecao == null)
                return NotFound();

            _context.Projecoes.Remove(projecao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================
        // FILTROS
        // =========================

        [HttpGet("DataReferencia/{data}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByDataReferencia(DateTime data)
        {
            return await QueryUsuario()
                .Where(p => p.DataReferencia.Date == data.Date)
                .ToListAsync();
        }

        [HttpGet("DataCriacao/{data}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByDataCriacao(DateTime data)
        {
            return await QueryUsuario()
                .Where(p => p.DataCriacao.Date == data.Date)
                .ToListAsync();
        }

        [HttpGet("valor/{valor}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByValor(decimal valor)
        {
            return await QueryUsuario()
                .Where(p => p.ValorPrevisto == valor)
                .ToListAsync();
        }

        [HttpGet("ano/{ano}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByAno(int ano)
        {
            return await QueryUsuario()
                .Where(p => p.DataReferencia.Year == ano)
                .ToListAsync();
        }

        [HttpGet("mes/{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByMes(int ano, int mes)
        {
            return await QueryUsuario()
                .Where(p => p.DataReferencia.Year == ano &&
                            p.DataReferencia.Month == mes)
                .ToListAsync();
        }

        [HttpGet("dia/{ano}/{mes}/{dia}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetByDia(int ano, int mes, int dia)
        {
            return await QueryUsuario()
                .Where(p => p.DataReferencia.Year == ano &&
                            p.DataReferencia.Month == mes &&
                            p.DataReferencia.Day == dia)
                .ToListAsync();
        }

        [HttpGet("somatoria")]
        public async Task<ActionResult<decimal>> GetSomatoria()
        {
            return await QueryUsuario()
                .SumAsync(p => p.ValorPrevisto);
        }

        [HttpGet("comparacao/{valor}")]
        public async Task<ActionResult<IEnumerable<Projecao>>> GetComparacao(decimal valor)
        {
            return await QueryUsuario()
                .Where(p => p.ValorPrevisto > valor)
                .ToListAsync();
        }
    }
}
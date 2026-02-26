using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using System.Security.Claims;

namespace ProjetoMidasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LancamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LancamentosController(AppDbContext context) => _context = context;

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // READ - Lista todos os lançamentos
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetAll()
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario)
                .ToListAsync();
        }

        // READ - Busca por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Lancamento>> GetById(int id)
        {
            var idUsuario = GetUserId();

            var lancamento = await _context.Lancamentos
                .FirstOrDefaultAsync(l => l.IdLancamento == id 
                                       && l.IdUsuario == idUsuario);

            return lancamento == null ? NotFound() : lancamento;
        }

        // CREATE - Adiciona novo lançamento
        [HttpPost("New")]
        public async Task<ActionResult<Lancamento>> Post(Lancamento lancamento)
        {
            var idUsuario = GetUserId();

            // Força o usuário logado
            lancamento.IdUsuario = idUsuario;
            lancamento.DataCriacao = DateTime.UtcNow;

            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = lancamento.IdLancamento }, lancamento);
        }

        // UPDATE - Atualiza lançamento existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Lancamento lancamento)
        {
            var idUsuario = GetUserId();

            if (id != lancamento.IdLancamento)
                return BadRequest();

            var lancamentoExistente = await _context.Lancamentos
                .FirstOrDefaultAsync(l => l.IdLancamento == id 
                                       && l.IdUsuario == idUsuario);

            if (lancamentoExistente == null)
                return NotFound();

            // Atualiza apenas os campos permitidos
            lancamentoExistente.Valor = lancamento.Valor;
            lancamentoExistente.Data = lancamento.Data;
            lancamentoExistente.DescricaoLancamento = lancamento.DescricaoLancamento;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE - Remove lançamento
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var idUsuario = GetUserId();

            var lancamento = await _context.Lancamentos
                .FirstOrDefaultAsync(l => l.IdLancamento == id 
                                       && l.IdUsuario == idUsuario);

            if (lancamento == null)
                return NotFound();

            _context.Lancamentos.Remove(lancamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Busca por Data
        [HttpGet("DataReferencia/{data}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByData(DateTime data)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.Data.Date == data.Date)
                .ToListAsync();
        }

        // Busca por DataCriacao
        [HttpGet("DataCriacao/{data}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByDataCriacao(DateTime data)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.DataCriacao.Date == data.Date)
                .ToListAsync();
        }

        // Busca por Valor
        [HttpGet("valor/{valor}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByValor(decimal valor)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.Valor == valor)
                .ToListAsync();
        }

        // Busca por Ano
        [HttpGet("ano/{ano}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByAno(int ano)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.Data.Year == ano)
                .ToListAsync();
        }

        // Busca por Ano/Mês    
        [HttpGet("mes/{ano}/{mes}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByMes(int ano, int mes)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.Data.Year == ano &&
                            l.Data.Month == mes)
                .ToListAsync();
        }

        // Busca por Ano/Mês/Dia    
        [HttpGet("dia/{ano}/{mes}/{dia}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetByDia(int ano, int mes, int dia)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.Data.Year == ano &&
                            l.Data.Month == mes &&
                            l.Data.Day == dia)
                .ToListAsync();
        }

        // Somatória de valores
        [HttpGet("somatoria")]
        public async Task<ActionResult<decimal>> GetSomatoria()
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario)
                .SumAsync(l => l.Valor);
        }

        // Comparação (maior que valor informado)
        [HttpGet("comparacao/{valor}")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetComparacao(decimal valor)
        {
            var idUsuario = GetUserId();

            return await _context.Lancamentos
                .Where(l => l.IdUsuario == idUsuario &&
                            l.Valor > valor)
                .ToListAsync();
        }
    }
}
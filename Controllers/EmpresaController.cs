using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;

namespace ProjetoMidasAPI.Controllers
{

    [ApiController]
    [Route("[controller]")] // Define a rota Empresa
    public class EmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;

        //Construtor
        public EmpresaController(AppDbContext context) => _context = context;

        // Retorna todas as empresas
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetAll() =>
            await _context.Empresas.ToListAsync();

        // Retorna uma empresa específica
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetById(int id)
        {
            var Empresa = await _context.Empresas.FindAsync(id);

            if (Empresa == null)
                return NotFound();

            return Empresa;
        }
        // Cria uma nova empresa
        [HttpPost("New")]
        public async Task<ActionResult<Empresa>> Post(Empresa empresa)
        {
            /*empresa.DataCriacao = DateTime.UtcNow; // Define data/hora atual da criação da empresa. Não sei se vou colocar pq tenho que ver se faz sentido.*/
            _context.Empresas.Add(empresa); // Adiciona à empresa
            await _context.SaveChangesAsync(); // Salva no banco
            return CreatedAtAction(nameof(GetById), new { id = empresa.IdEmpresa }, empresa); // Retorna 201
        }
        // Edita uma empresa existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Empresa EmpresaAtualizado)
        {
            if (id != EmpresaAtualizado.IdEmpresa)
                return BadRequest();

            var Empresa = await _context.Empresas.FindAsync(id);
            if (Empresa == null)
                return NotFound();

            // Atualiza campos editáveis
            Empresa.idResponsavel = EmpresaAtualizado.idResponsavel;
            Empresa.razaoSocial = EmpresaAtualizado.razaoSocial;
            Empresa.nomeFantasia = EmpresaAtualizado.nomeFantasia;
            Empresa.telefoneEmp = EmpresaAtualizado.telefoneEmp;
            Empresa.cnpjEmpresa = EmpresaAtualizado.cnpjEmpresa;
            Empresa.emailEmpresa = EmpresaAtualizado.emailEmpresa;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // Remove uma simulação
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Empresa = await _context.Empresas.FindAsync(id);
            if (Empresa == null)
                return NotFound();

            _context.Empresas.Remove(Empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
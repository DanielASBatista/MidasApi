using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoMidasAPI.Data;
using ProjetoMidasAPI_Final.Utils;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoMidasAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/[controller]")]
    
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        public UsuarioController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        private async Task<bool> UsuarioExistente(string nomeUsuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.nomeUsuario.ToLower() == nomeUsuario.ToLower()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string CriarToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.nomeUsuario)
            };

            //Pra não virar bagunça e ficar citando a instancia a toda hora nós só definimos uma variável pra ela e chamamos como sendo uma nova instancia.
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("ConfiguracaoToken:Chave").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(29),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario (Usuario usuario)
        {
            try
            {
                if (await UsuarioExistente(usuario.nomeUsuario))
                throw new System.Exception("Nome de usuário já existe!");

                Criptografia.CriarPasswordHash(usuario.PasswordString, out byte[] hash, out byte[] salt);
                usuario.PasswordString = string.Empty;
                usuario.PasswordHash = hash;
                usuario.PasswordSalt = salt;
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return Ok("Usuário registrado com sucesso!");  
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao registrar usuário: {ex.Message}");
            }
        }
        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            { 
                Usuario? usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.nomeUsuario.ToLower().Equals(credenciais.nomeUsuario.ToLower()));
            
                if (usuario == null)
                {
                    throw new System.Exception("Usuário não encontrado.");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash!, usuario.PasswordSalt!))
                {
                    throw new System.Exception("Senha incorreta.");
                }
                else
                {
                    usuario.PasswordHash = null;
                    usuario.PasswordSalt = null;
                    usuario.Token = CriarToken(usuario);

                    var token = CriarToken(usuario);

                    return Ok(new 
                    {
                        usuario = usuario.nomeUsuario,
                        token = token
                    });
            };
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao autenticar usuário: {ex.Message}");
            }
        }
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenhaUsuario(Usuario credenciais)
        {
            try
            {
                Usuario? usuario = await _context.Usuarios //Busca o usuário no banco através do login
                .FirstOrDefaultAsync(U => U.nomeUsuario.ToLower().Equals(credenciais.nomeUsuario.ToLower()));

                if (usuario == null) // Se o usuário não for encontrado, lança uma exceção
                    throw new System.Exception("Usuário não encontrado.");
                    Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordHash = hash; //Se o usuário existir, executa a criptografia
                    usuario.PasswordSalt = salt; //Atualiza o hash e o salt no banco
                        
                    _context.Usuarios.Update(usuario);
                    int linhasAfetadas = await _context.SaveChangesAsync(); // Cponfirma a alteração no banco
                    return Ok(linhasAfetadas); //Retorna o número de linhas afetadas (Geralmente sempre tem 1 linha msm)
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao alterar a senha do usuário: {ex.Message}");
            }
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
            .Include(u => u.Lancamentos)
            .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }
        [HttpPost("New")]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Set<Usuario>().Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Set<Usuario>().FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Set<Usuario>().Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
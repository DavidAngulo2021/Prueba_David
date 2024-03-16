using API_PROYECTO.Models;
using Cliente.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API_PROYECTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        public readonly DB_PdavidContext _dbcontext;

        public PersonasController (DB_PdavidContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("lista")]

        public IActionResult Lista()
        {
            List<Persona> lista = new List<Persona>();


            try
            {

                lista = _dbcontext.Personas.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }

        [HttpGet]
        [Route("verCliente")]
        public async Task<IActionResult> GetCliente(int id)
        {

            Persona client = await _dbcontext.Personas.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }


        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Persona objeto)
        {
            try
            {
                _dbcontext.Personas.Add(objeto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "La persona se ha guardado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Ocurrió un error al intentar guardar la persona.", error = ex.Message });
            }
        }



        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> UpdateProduct(int id, Persona objeto)
        {

            Persona user = _dbcontext.Personas.Find(objeto.Id);

            if (user == null)
            {
                return BadRequest("no encontrado");
            }


            try

            {
                user = await _dbcontext.Personas.FindAsync(id);
                user.PrimerNombre = objeto.PrimerNombre is null ? user.PrimerNombre : objeto.PrimerNombre;
                user.SegundoNombre = objeto.SegundoNombre is null ? user.SegundoNombre : objeto.SegundoNombre;
                user.PrimerApellido = objeto.PrimerApellido is null ? user.PrimerApellido : objeto.PrimerApellido;
                user.SegundoApellido = objeto.SegundoApellido is null ? user.SegundoApellido : objeto.SegundoApellido;
                user.FechaNacimiento = objeto.FechaNacimiento;
                user.Sueldo = objeto.Sueldo;
                user.FechaModificacion = objeto.FechaModificacion ?? DateTime.Now;

                _dbcontext.Personas.Update(user);
                _dbcontext.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }


            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Ocurrió un error al intentar editar.", error = ex.Message });
            }
        }

    
        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var clienteBorrado = await _dbcontext.Personas.FindAsync(id);
            _dbcontext.Personas.Remove(clienteBorrado!);

            await _dbcontext.SaveChangesAsync();

            return Ok();
        }
    }
}

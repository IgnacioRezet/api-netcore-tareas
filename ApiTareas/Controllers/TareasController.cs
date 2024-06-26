using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTareas.Models;

namespace ApiTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        public readonly TESTDBAPIContext _dbcontext;

        public TareasController(TESTDBAPIContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Models.Task> lsttask = new List<Models.Task>();
            try
            {
                lsttask = _dbcontext.Tasks.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lsttask });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lsttask });
            }
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public IActionResult Obtener(int idTask)
        {
            Models.Task oTask = _dbcontext.Tasks.Find(idTask);

            if (oTask == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                oTask = _dbcontext.Tasks.Where(p => p.Id == idTask).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oTask });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oTask });
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Models.Task objeto)
        {
            try
            {
                _dbcontext.Tasks.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Models.Task objeto)
        {

            Models.Task oTask = _dbcontext.Tasks.Find(objeto.Id);

            if (oTask == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                oTask.Titulo = objeto.Titulo is null ? oTask.Titulo : objeto.Titulo;
                oTask.Descripcion = objeto.Descripcion is null ? oTask.Descripcion : objeto.Descripcion;
                oTask.FechaCreacion = objeto.FechaCreacion;
                oTask.Estado = objeto.Estado;
               

                _dbcontext.Tasks.Update(oTask);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public IActionResult Eliminar(int idTask)
        {
            Models.Task oTask = _dbcontext.Tasks.Find(idTask);

            if (oTask == null)
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                _dbcontext.Tasks.Remove(oTask);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }

        }


    }
}

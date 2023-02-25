using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using practica1.Models;
using Microsoft.EntityFrameworkCore;

namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)

        {
            _equiposContexto = equiposContexto;
        }
        ///<sumary>
        ///EndPoint que retorna el lisado  de todo los equipos existentes
        ///</sumary>
        ///<returns></sumary>
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
                                           select e).ToList();
            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
        ///<sumary>
        ///EndPoint que retorna los registros de una tabla filtrados  por su ID
        ///</sumary>
        ///<returns></sumary>

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        ///<sumary>
        ///EndPoint que retorna los registros de una tabla filtrados por descripcion
        ///</sumary>
        ///<returns></sumary>
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.nombre.Contains(filtro)
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        [HttpGet]
        [Route("add")]
        public IActionResult GuardarEquipo([FromBody] equipos equipo)
        {

            try
            {
                _equiposContexto.equipos.Add(equipo);
                _equiposContexto.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("update/{id}")]
        public IActionResult actualizarEquipo(int id, [FromBody] equipos updateDevice)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                                     where e.id_equipos==id
                                     select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            equipo.nombre = updateDevice.nombre;
            equipo.costo = updateDevice.costo;

            _equiposContexto.Entry(equipo).State = EntityState.Modified;
            _equiposContexto.SaveChanges(); 
            return Ok(equipo);
        }

        //metodos de eliminar ID
        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }

            _equiposContexto.equipos.Attach(equipo);
            _equiposContexto.equipos.Remove(equipo);
            _equiposContexto.SaveChanges();
            return Ok(equipo);
        }



    }
}

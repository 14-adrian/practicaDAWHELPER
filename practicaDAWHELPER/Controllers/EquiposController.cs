using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using practicaDAWHELPER.Models;

namespace practicaDAWHELPER.Controllers
{
    public class EquiposController : Controller
    {
        private readonly equiposContext _equiposContext;

        public EquiposController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        public IActionResult Index()
        {
            var listaTipoEquipo = (from m in _equiposContext.tipo_equipo
                                   select m).ToList();

            var listaDeMarcas = (from m in _equiposContext.marcas
                                 select m).ToList();

            var listaEstado = (from m in _equiposContext.estados_equipo
                               select m).ToList();

            var listadoDeEquipos = (from e in _equiposContext.equipos
                                    join m in _equiposContext.marcas on e.marca_id equals m.id_marcas
                                    select new
                                    {
                                        nombre = e.nombre,
                                        descripcion = e.descripcion,
                                        marca_id = e.marca_id,
                                        marca_nombre = m.nombre_marca
                                    }).ToList();

            ViewData["listadoEquipo"] = listadoDeEquipos;
            ViewData["listadoEstado"] = new SelectList(listaEstado, "id_estados_equipo", "descripcion");
            ViewData["listadoTipo"] = new SelectList(listaTipoEquipo,"id_tipo_equipo", "descripcion");
            ViewData["listadoDeMarcas"] = new SelectList(listaDeMarcas, "id_marcas", "nombre_marca");
            return View();
        }

        public IActionResult CrearEquipos(equipos nuevoEquipo) 
        {
            _equiposContext.Add(nuevoEquipo);
            _equiposContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

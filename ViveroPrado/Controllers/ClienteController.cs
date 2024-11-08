using ContenedorDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ViveroPrado.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DBContextVivero ContextoDB;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public ClienteController(DBContextVivero context, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            ContextoDB = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        ////////////////////////////////////////////////////////////////////
        //// Control de Inicio de Sesion
        public bool SesionIniciada()
        {
            var Usuario = ContextoDB.Usuarios.Find(1);
            return Usuario.Loggeado;
        }
        public ActionResult Redirigir()
        {
            return RedirectToAction("Login", "Home");
        }
        ////////////////////////////////////////////////////////////////////
        public ActionResult DetalleCliente()
        {
            if (SesionIniciada())
            {
                var listaclientes = ContextoDB.Clientes.ToList();
                ViewBag.Clientes = listaclientes;

                return View();
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return Redirigir();
            }
        }

        /// Guarda los clientes en la base de datos
        [HttpPost]
        public ActionResult GuardarCliente(Cliente NuevoRegistro)
        {
            ContextoDB.Clientes.Add(NuevoRegistro);
            ContextoDB.SaveChanges();

            TempData["CreacionExito"] = "Si";
            TempData["Mensaje"] = "Cliente Guardado";

            return RedirectToAction("DetalleCliente");
        }


        //Trae la lista de clientes y las muestra
        public ActionResult EditarCliente()
        {
            if (SesionIniciada())
            {
                var listaclientes = ContextoDB.Clientes.ToList();
                ViewBag.Clientes = listaclientes;

                return View();
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return Redirigir();
            }
        }

        //Para ver el detalle en especifico de cada cliente
        [HttpGet]
        public IActionResult DetalleRegistroCliente(int Id)
        {
            if (SesionIniciada())
            {
                var Detalle = ContextoDB.Clientes.Where(x => x.IdCliente == Id).SingleOrDefault();
                ViewBag.Detalle = Detalle;

                TempData["CreacionExito"] = "Si";
                TempData["Mensaje"] = "Cliente Guardado";

                return View();
            }
            else
            {
                return Redirigir();
            }
        }

        [HttpPost]
        public ActionResult EditarRegistroCliente(Cliente NuevoRegistro)
        {
            var Detalle = ContextoDB.Clientes.Find(NuevoRegistro.IdCliente);

            Detalle.NombreCompleto = NuevoRegistro.NombreCompleto;
            Detalle.Telefono = NuevoRegistro.Telefono;

            ContextoDB.SaveChanges();
            
            return RedirectToAction("EditarCliente");
        }
        
        /// Elimina los clientes de la base de datos
        [HttpPost]
        public ActionResult EliminarCliente(int id)
        {
            try
            {
                var Registro = ContextoDB.Clientes.Find(id);

                ContextoDB.Clientes.Remove(Registro);
                ContextoDB.SaveChanges();

                TempData["CreacionExito"] = "Si";
                TempData["Mensaje"] = "Cliente Eliminado";

                return RedirectToAction("EditarCliente");
            }
            catch
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return View();
            }
        }
    }
}

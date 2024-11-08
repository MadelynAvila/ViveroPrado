using ContenedorDB;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ViveroPrado.Controllers
{
    public class ViveroController : Controller
    {
        private readonly DBContextVivero ContextoDB;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public ViveroController(DBContextVivero context, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            ContextoDB = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        ////////////////////////////////////////////////////////////////////
        //Control de Inicio de Sesion
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
        
        public IActionResult Productos()
        {
            //Listar nuestros productos
            if (SesionIniciada())
            {
                var ListaProductos = ContextoDB.Productos.ToList();
                ViewBag.Mensajero = ListaProductos;

                return View();
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return Redirigir();
            }
        }
        //Guarda los Productos en la Base de datos 
        [HttpPost]
        public IActionResult GuardarProducto(Producto NuevoRegistro)
        {
            ContextoDB.Productos.Add(NuevoRegistro);
            ContextoDB.SaveChanges();

            TempData["CreacionExito"] = "Si";
            TempData["Mensaje"] = "Producto Guardado";

            return RedirectToAction("Productos");
        }

        public IActionResult EdicionProducto()
        {
            //Listar nuestros productos
            if (SesionIniciada())
            {
                var ListaProductos = ContextoDB.Productos.ToList();
                ViewBag.Mensajero = ListaProductos;

                return View();
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return Redirigir();
            }
        }

        [HttpGet]
        public IActionResult DetalleProducto(int Id)
        {
            if (SesionIniciada())
            {
                var Detalle = ContextoDB.Productos.Where(x => x.IdProducto == Id).SingleOrDefault();
                ViewBag.Detalle = Detalle;

                return View();
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return Redirigir();
            }
        }

        [HttpPost]
        public IActionResult EditarProducto(Producto NuevoRegistro)
        {
            var Detalle = ContextoDB.Productos.Find(NuevoRegistro.IdProducto);

            Detalle.Nombre = NuevoRegistro.Nombre;
            Detalle.Descripcion = NuevoRegistro.Descripcion;
            Detalle.Existencia = NuevoRegistro.Existencia;
            Detalle.Precio = NuevoRegistro.Precio;

            ContextoDB.SaveChanges();

            TempData["CreacionExito"] = "Si";
            TempData["Mensaje"] = "Producto Guardado";

            return RedirectToAction("EdicionProducto");
        }

        ///Elimina los Productoos
        [HttpPost]
        public ActionResult EliminarProducto(int id)
        {
            try
            {
                var Registro = ContextoDB.Productos.Find(id);

                ContextoDB.Productos.Remove(Registro);
                ContextoDB.SaveChanges();

                TempData["CreacionExito"] = "Si";
                TempData["Mensaje"] = "Producto Eliminado";

                return RedirectToAction("EdicionProducto");
            }
            catch
            {
                return View();
            }
        }
    }
}
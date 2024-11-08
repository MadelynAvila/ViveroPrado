using ContenedorDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ViveroPrado.Controllers
{
    public class FacturaController : Controller
    {
        private readonly DBContextVivero ContextoDB;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public FacturaController(DBContextVivero context, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
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
        
        public ActionResult Venta()
        {
            if (SesionIniciada())
            {
                var listaclientes = ContextoDB.Clientes.ToList();
                ViewBag.Clientes = listaclientes;

                var ListaProductos = ContextoDB.Productos.ToList();
                ViewBag.Productos = ListaProductos;
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
        public ActionResult GuardarVenta(Factura RegistroRecibido)
        {
            try
            {
                var DetalleProducto = ContextoDB.Productos.Find(RegistroRecibido.IdProducto);

                //Calculo del total
                RegistroRecibido.Total = DetalleProducto.Precio * RegistroRecibido.Cantidad;

                //Actualizacion de la existencia del producto
                DetalleProducto.Existencia = DetalleProducto.Existencia - RegistroRecibido.Cantidad;

                if (DetalleProducto.Existencia < 0)
                {
                    TempData["Error"] = "Si";
                    TempData["Mensaje"] = "Ya no hay existencias";
                    return RedirectToAction("Venta");
                }

                ContextoDB.Facturas.Add(RegistroRecibido);
                ContextoDB.SaveChanges();

                TempData["CreacionExito"] = "Si";
                TempData["Mensaje"] = "Factura creada";
                return RedirectToAction("Venta");
            }
            catch
            {
                return RedirectToAction("Venta");
            }
        }
        public ActionResult ListaFactura()
        {
            if (SesionIniciada())
            {
                //Lo que tenia la funcion

                var ListaFactura = ContextoDB.Facturas.ToList();
                ViewBag.Facturas = ListaFactura;
                var listaclientes = ContextoDB.Clientes.ToList();
                ViewBag.Clientes = listaclientes;
                var ListaProductos = ContextoDB.Productos.ToList();
                ViewBag.Productos = ListaProductos;

                return View();
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Debe iniciar Sesion";
                return Redirigir();
            }
        }
    }
}

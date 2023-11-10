using ContenedorDB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ViveroPrado.Models;

namespace ViveroPrado.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DBContextVivero ContextoDB;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(DBContextVivero context, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            ContextoDB = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        ////////////////////////////////////////////////////////////////////
        //Control de inicio de sesion
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

        public IActionResult Index()
        {
            if (SesionIniciada())
            {
                return View();
            }
            else
            {
                return Redirigir();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(string NombreUsuario, string Contraseña)
        {
            if (NombreUsuario == "Madelyn" && Contraseña == "1234")
            {
                var Usuario = ContextoDB.Usuarios.Find(1);
                Usuario.Loggeado = true;
                ContextoDB.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Usuario Incorrecto tonto";
                return RedirectToAction("Index");
            }
        }

        public IActionResult CerrarSesion()
        {
            var Usuario = ContextoDB.Usuarios.Find(1);
            Usuario.Loggeado = false;
            ContextoDB.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            //Cliente InstaciaCliente = new Cliente();

            //InstaciaCliente.NombreCompleto = "Madelyn";
            //InstaciaCliente.Telefono = 37291383;
            //ContextoDB.Clientes.Add(InstaciaCliente);
            //ContextoDB.SaveChanges();

            //Producto InstanciaProducto = new Producto();

            //InstanciaProducto.Nombre = "Hortencia";
            //InstanciaProducto.Descripcion = "Azules";
            //InstanciaProducto.Existencia = 15;
            //InstanciaProducto.Precio = 10;

            //ContextoDB.Productos.Add(InstanciaProducto);
            //ContextoDB.SaveChanges();

            //Factura InstaciaFactura = new Factura();

            //InstaciaFactura.IdCliente = 1;
            //InstaciaFactura.IdProducto = 1;
            //InstaciaFactura.Cantidad = 25;
            //InstaciaFactura.Total = 10.5;

            //ContextoDB.Facturas.Add(InstaciaFactura);
            //ContextoDB.SaveChanges();


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
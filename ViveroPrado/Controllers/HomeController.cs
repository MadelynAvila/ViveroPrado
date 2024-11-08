using ContenedorDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        ///////////////////////////////////////////////////////////////////////

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
        public async Task<IActionResult> IniciarSesion(string NombreUsuario, string Contraseña)
        {
            var captchaResponse = Request.Form["g-recaptcha-response"];
            var secretKey = "6Le3H10qAAAAAKHQ87OtVPlN3ouPvceZ0M8OWeMz";

            // Validar el token de reCAPTCHA con la API de Google
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={captchaResponse}", null);
            var jsonResult = await response.Content.ReadAsStringAsync();
            dynamic result = JObject.Parse(jsonResult);

            // Verificar si el captcha fue exitoso
            if (result.success != true)
            {
                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Verificacion de CAPTCHA fallida.";
                return RedirectToAction("Login");
            }

            try
            {
               var UsuarioEncontrado =  ContextoDB.Usuarios.Any(user => user.Nombre == NombreUsuario && user.Contraseña == Contraseña);

                if (UsuarioEncontrado)
                {

                    var Usuario = ContextoDB.Usuarios.Find(1);
                    Usuario.Loggeado = true;
                    ContextoDB.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Si";
                    TempData["Mensaje"] = "Usuario Incorrecto";
                    return RedirectToAction("Login");
                }


            }
            catch 
            {

                TempData["Error"] = "Si";
                TempData["Mensaje"] = "Usuario Incorrecto";
                return RedirectToAction("Login");

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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
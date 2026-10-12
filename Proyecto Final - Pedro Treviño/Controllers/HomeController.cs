using Proyecto_Final___Pedro_Treviño.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Final___Pedro_Treviño.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        Conexion db = new Conexion();

        public ActionResult Acceso()
        {
            Login login = new Login();
            login.usuario = Request.Form["txtUsuario"];
            login.password = Request.Form["txtPassword"];

            string consulta = $"SELECT id_empleado, rol FROM Tbl_Empleados WHERE usuario='{login.usuario}' and contrasena='{login.password}'";

            DataTable resultado = db.selvalTabla(consulta);

            if (resultado.Rows.Count > 0)
            {
                int id = Convert.ToInt32(resultado.Rows[0]["id_empleado"]);
                string rol = resultado.Rows[0]["rol"].ToString();

                Session["id_empleado"] = id;
                Session["rol"] = rol;

                return View("Acceso", login);
            }
            else
            {
                ViewBag.Mensaje = "Usuario o contraseña incorrectos";
                return View("Index");
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear(); 
            return RedirectToAction("Index");
        }

        public ActionResult ListaEmpleados()
        {
            if (Session["id_empleado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = new DataTable();
            dt = db.seltable("SELECT * FROM Tbl_Empleados");

            Empleados empleado;

            List<Empleados> lst = new List<Empleados>();

            foreach (DataRow dr in dt.Rows)
            {
                empleado = new Empleados();
                empleado.id_empleado = Convert.ToInt32(dr[0].ToString());
                empleado.apellido = dr[1].ToString();
                empleado.nombre = dr[2].ToString();
                empleado.correo = dr[3].ToString();
                empleado.usuario = dr[4].ToString();
                empleado.contrasena = dr[5].ToString();
                empleado.rol = Convert.ToInt32(dr[6].ToString());
                empleado.categoria = Convert.ToInt32(dr[7].ToString());
                empleado.sueldo = Convert.ToDecimal(dr[8].ToString());
                lst.Add(empleado);
            }

            return View("ListaEmpleados", lst);
        }

        public ActionResult Edit(int id = 0)
        {

            if (Session["id_empleado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = new DataTable();
            dt = db.seltable("SELECT * FROM Tbl_Empleados WHERE id_empleado='" + id + "'");

            Empleados empleado = new Empleados();

            foreach (DataRow dr in dt.Rows)
            {
                empleado.id_empleado = Convert.ToInt32(dr[0].ToString());
                empleado.apellido = dr[1].ToString();
                empleado.nombre = dr[2].ToString();
                empleado.correo = dr[3].ToString();
                empleado.usuario = dr[4].ToString();
                empleado.contrasena = dr[5].ToString();
                empleado.rol = Convert.ToInt32(dr[6].ToString());
                empleado.categoria = Convert.ToInt32(dr[7].ToString());
                empleado.sueldo = Convert.ToDecimal(dr[8].ToString());

            }

            return View("Edit", empleado);
        }

        public ActionResult Actualizar(Empleados emp)
        {
            if (Session["id_empleado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string query = "UPDATE Tbl_Empleados SET " +
                           "apellido = '" + emp.apellido + "', " +
                           "nombre = '" + emp.nombre + "', " +
                           "correo = '" + emp.correo + "', " +
                           "usuario = '" + emp.usuario + "', " +
                           "contrasena = '" + emp.contrasena + "', " +
                           "rol = '" + emp.rol + "', " +
                           "categoria = '" + emp.categoria + "', " +
                           "sueldo = '" + emp.sueldo + "' " +
                           "WHERE id_empleado = " + emp.id_empleado;

            int valor = db.actualizar(query);

            return ListaEmpleados();
        }

        public ActionResult Delete(int id = 0)
        {

            if (Session["id_empleado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = new DataTable();
            dt = db.seltable("SELECT * FROM Tbl_Empleados WHERE id_empleado='" + id + "'");

            Empleados empleado = new Empleados();

            foreach (DataRow dr in dt.Rows)
            {
                empleado.id_empleado = Convert.ToInt32(dr[0].ToString());
                empleado.apellido = dr[1].ToString();
                empleado.nombre = dr[2].ToString();
                empleado.correo = dr[3].ToString();
                empleado.usuario = dr[4].ToString();
                empleado.contrasena = dr[5].ToString();
                empleado.rol = Convert.ToInt32(dr[6].ToString());
                empleado.categoria = Convert.ToInt32(dr[7].ToString());
                empleado.sueldo = Convert.ToDecimal(dr[8].ToString());
            }

            return View("Delete", empleado);
        }

        public ActionResult Eliminar(Empleados emp2)
        {
            int valor = db.Eliminar("DELETE FROM Tbl_Empleados WHERE id_empleado = '" + emp2.id_empleado + "'");
            return ListaEmpleados();
        }

        public ActionResult Details(int id = 0)
        {
            if (Session["id_empleado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = new DataTable();
            dt = db.seltable("SELECT * FROM Tbl_Empleados WHERE id_empleado='" + id + "'");

            Empleados empleado = new Empleados();

            foreach (DataRow dr in dt.Rows)
            {
                empleado.id_empleado = Convert.ToInt32(dr[0].ToString());
                empleado.apellido = dr[1].ToString();
                empleado.nombre = dr[2].ToString();
                empleado.correo = dr[3].ToString();
                empleado.usuario = dr[4].ToString();
                empleado.contrasena = dr[5].ToString();
                empleado.rol = Convert.ToInt32(dr[6].ToString());
                empleado.categoria = Convert.ToInt32(dr[7].ToString());
                empleado.sueldo = Convert.ToDecimal(dr[8].ToString());
            }

            return View("Details", empleado);
        }

        public JsonResult scomboRoles()
        {
            DataTable dt = new DataTable();
            dt = db.seltable("SELECT id_rol, descripcion FROM Tbl_Rol");

            Roles rol;

            List<Roles> Lst = new List<Roles>();

            foreach (DataRow dtRow in dt.Rows)
            {
                rol = new Roles();
                rol.id_rol = Convert.ToInt32(dtRow[0].ToString());
                rol.descripcion = dtRow[1].ToString();
                Lst.Add(rol);
                rol = null;
            }

            return Json(new { data = Lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult scomboCategorias()
        {
            DataTable dt = new DataTable();
            dt = db.seltable("SELECT id_categoria, descripcion FROM Tbl_Categoria");

            Categorias categoria;

            List<Categorias> Lst = new List<Categorias>();

            foreach (DataRow dtRow in dt.Rows)
            {
                categoria = new Categorias();
                categoria.id_categoria = Convert.ToInt32(dtRow[0].ToString());
                categoria.descripcion = dtRow[1].ToString();
                Lst.Add(categoria);
                categoria = null;
            }

            return Json(new { data = Lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarEmpleadoJs(string nombre, string apellido, string idrol, string correo, string usuario, string contrasena, string idcategoria, string sueldo)
        {
            if (nombre == "" || apellido == "" || correo == "" || usuario == "" || contrasena == "" || idrol == "" || idcategoria == "")
            {
                return Json("3", JsonRequestBehavior.AllowGet);
            }
            else
            {

                int valor = db.Insertar("INSERT INTO Tbl_Empleados(Nombre,Apellido,rol,correo,usuario,contrasena, categoria, sueldo) VALUES " +
                    " ('" + nombre + "', '" + apellido + "', '" + idrol + "', '" + correo + "', '" + usuario + "', '" + contrasena + "', '" + idcategoria + "', '" + sueldo + "')");

                if (valor == 1)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("2", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult CalcularNomina(Nomina model)
        {

            if (Session["id_empleado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid || model.FechaEntrada > model.FechaSalida)
            {
                model.Mensaje = "Fechas inválidas. Revisa la hora de entrada y salida.";
                return View(model);
            }

            string consulta = $"SELECT nombre, apellido, categoria, sueldo FROM Tbl_Empleados WHERE id_empleado = {model.id_empleado}";
            DataTable dt = db.seltable(consulta);

            if (dt.Rows.Count == 0)
            {
                model.Mensaje = "Empleado no encontrado.";
                return View(model);
            }

            model.Nombre = dt.Rows[0]["nombre"].ToString();
            model.Apellido = dt.Rows[0]["apellido"].ToString();
            int categoria = Convert.ToInt32(dt.Rows[0]["categoria"]);
            decimal sueldo = Convert.ToDecimal(dt.Rows[0]["sueldo"]);
            model.SueldoBase = sueldo;

            TimeSpan tiempo = model.FechaSalida - model.FechaEntrada;
            model.HorasTrabajadas = tiempo.TotalHours;

            decimal resultado = 0;

            if (categoria == 1 || categoria == 3)
            {
                resultado = sueldo;
                model.TipoSueldo = "Fijo";
            }
            else if (categoria == 2)
            {
                resultado = sueldo * (decimal)model.HorasTrabajadas;
                model.TipoSueldo = "Por horas";
            }
            else if (categoria == 3)
            {
                resultado = sueldo * (decimal)model.HorasTrabajadas;
                model.TipoSueldo = "Contratista";
            }

            model.SueldoCalculado = Math.Round(resultado, 2);
            model.Mensaje = "Cálculo realizado correctamente.";

            return View(model);
        }

    }
}
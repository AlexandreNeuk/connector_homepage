using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectorSite.Controllers
{
    public class HomeController : Controller
    {
        //DB_A4925A_connectorEntities

        DB_A4925A_connectorEntities db = new DB_A4925A_connectorEntities();

        public ActionResult Index()
        {
            Site_Acesso sa = new Site_Acesso();
            //
            sa.IP = GetIPAddress();
            sa.DataHora = getData();
            db.Site_Acesso.Add(sa);
            db.SaveChanges();
            //
            return View();
        }

        public JsonResult Newsletter(string email)
        {
            string sRet = string.Empty;
            //
            try
            {
                Site_NewsLetter sn = new Site_NewsLetter();
                //
                sn.Email = email;
                sn.DataHora = getData();
                //
                db.Site_NewsLetter.Add(sn);
                db.SaveChanges();
                //
                sRet = "ok";
            }
            catch (Exception exc)
            {
                sRet = exc.Message;
            }
            //
            return new JsonResult()
            {
                Data = new { data = sRet },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public JsonResult Contact(string nome, string email, string assunto, string mensagem)
        {
            string sRet = string.Empty;
            //
            try
            {
                Site_Mensagem sm = new Site_Mensagem();
                //
                sm.Assunto = assunto;
                sm.Descricao = mensagem;
                sm.Nome = nome;
                sm.Email = email;
                sm.DataHora = getData();
                //
                db.Site_Mensagem.Add(sm);
                db.SaveChanges();
                //
                sRet = "ok";
            }
            catch (Exception exc)
            {
                sRet = exc.Message;
            }
            //
            return new JsonResult()
            {
                Data = new { data = sRet },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public JsonResult Proposta(string nome, string email, string empresa, string telefone, string plano)
        {
            string sRet = string.Empty;
            //
            try
            {
                Site_Proposta sp = new Site_Proposta();
                //                
                sp.Email = email;
                sp.Empresa = empresa;
                sp.Nome = nome;
                sp.Telefone = telefone;
                sp.Proposta = plano;
                sp.DataHora = getData();
                //
                db.Site_Proposta.Add(sp);
                db.SaveChanges();
                //
                sRet = "ok";
            }
            catch (Exception exc)
            {
                sRet = exc.Message;
            }
            //
            return new JsonResult()
            {
                Data = new { data = sRet },
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };
        }

        public DateTime getData()
        {
            DateTime timeUtc = DateTime.UtcNow;
            var brasilia = TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time");
            //
            int ano = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia).Year;
            int mes = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia).Month;
            int dia = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia).Day;
            int hora = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia).Hour;
            int min = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia).Minute;
            int seg = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, brasilia).Second;
            //
            return new DateTime(ano, mes, dia, hora, min, seg).AddHours(1);
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
﻿using TchotchoLoto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace TchotchoLoto.Controllers
{
    public class PointDeVentesController : Controller
    {
        // GET: PointDeVentes

        Entities db = new Entities();

        [AjaxOnly]
        public ActionResult Index()
        {

            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];


            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }

            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/Index", "Button Point of Sale [Point of Sale]");


            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }

            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "index")))
            {

                List<StatutEntite> currentStatutPointDeVente = (List<StatutEntite>)Session["statutEntiteData"];
                if (currentStatutPointDeVente != null && currentStatutPointDeVente.Count() > 0)
                {
                    ViewBag.StatutId = new SelectList(currentStatutPointDeVente, "Id", "Description");
                }
                else
                {
                    currentStatutPointDeVente.Insert(0, new StatutEntite { Description = "All Status", Id = -1 });
                    currentStatutPointDeVente.Insert(1, new StatutEntite { Description = "Active", Id = 1 });
                    currentStatutPointDeVente.Insert(2, new StatutEntite { Description = "Inactive", Id = 0 });

                    ViewBag.StatutId = new SelectList(currentStatutPointDeVente, "Id", "Description");

                }


                //ViewBag.PointDeVentes = new List<PointDeVente>();

                ViewBag.pointDeVentes = db.PointDeVentes.Where(p=>p.Statut).ToList();

                return View();
            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }



        [AjaxOnly]
        public ActionResult __Index(int? id)
        {

            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];



            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }

            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/__Index", "Button Select Status [Point of Sale]");


            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }

            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "index")))
            {


                string message = null;

                List<StatutEntite> currentStatutPointDeVente = (List<StatutEntite>)Session["statutEntiteData"];
                if (currentStatutPointDeVente != null && currentStatutPointDeVente.Count() > 0)
                {
                    ViewBag.StatutId = new SelectList(currentStatutPointDeVente, "Id", "Description");
                }
                else
                {
                    currentStatutPointDeVente.Insert(0, new StatutEntite { Description = "All Status", Id = -1 });
                    currentStatutPointDeVente.Insert(1, new StatutEntite { Description = "Active", Id = 1 });
                    currentStatutPointDeVente.Insert(2, new StatutEntite { Description = "Inactive", Id = 0 });

                    ViewBag.StatutId = new SelectList(currentStatutPointDeVente, "Id", "Description");

                }

                if (id == null)
                {
                    message = "Please Select a Transport Line!";
                    return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);

                }

                var pointDeVentes = new List<PointDeVente>();

                if (id == 0)
                {
                    ViewBag.PintDeVentes = db.PointDeVentes.Where(p => !p.Statut).ToList();
                }
                else if (id == 1)
                {
                    ViewBag.pointDeVentes = db.PointDeVentes.Where(p => p.Statut).ToList();
                }
                else
                {
                    ViewBag.pointDeVentes = db.PointDeVentes.ToList();
                }




                return PartialView();

            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }






        [AjaxOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Index([Bind(Include = "PointDeVenteId, CodePointDeVente, NomPointDeVente, Adresse, Ville, CodePostal,Latitude,Longitude, Statut")] PointDeVente pointDeVente)
        {
            

            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];

            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }



            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/_Index", "Button Edit or New Point of sale => Save [Point of Sale]");



            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }

            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "index")))
            {

                string message = null;

                int pointDeVenteNameExist = db.PointDeVentes.Where(p => p.PointDeVenteId != pointDeVente.PointDeVenteId && p.NomPointDeVente.Trim().ToLower() == pointDeVente.NomPointDeVente.Trim().ToLower()).Count();

                int pointDeVenteCodeExist = db.PointDeVentes.Where(p => p.PointDeVenteId != pointDeVente.PointDeVenteId && p.CodePointDeVente.Trim().ToLower() == pointDeVente.CodePointDeVente.Trim().ToLower()).Count();



                if (pointDeVenteNameExist == 0 && pointDeVenteCodeExist == 0 && !string.IsNullOrWhiteSpace(pointDeVente.CodePointDeVente) && !string.IsNullOrWhiteSpace(pointDeVente.NomPointDeVente) && !string.IsNullOrWhiteSpace(pointDeVente.Adresse) && !string.IsNullOrWhiteSpace(pointDeVente.Ville) && !string.IsNullOrWhiteSpace(pointDeVente.CodePostal))
                {

                    if (pointDeVente.PointDeVenteId != 0)
                    {


                        PointDeVente pointDeVenteEdit = db.PointDeVentes.FirstOrDefault(p => p.PointDeVenteId == pointDeVente.PointDeVenteId);
                        if (pointDeVenteEdit == null)
                        {
                            message = "Point Of Sale not Found!";
                            return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);
                        }

                        pointDeVenteEdit.CodePointDeVente = pointDeVente.CodePointDeVente;
                        pointDeVenteEdit.NomPointDeVente = pointDeVente.NomPointDeVente;
                        pointDeVenteEdit.Adresse = pointDeVente.Adresse;
                        pointDeVenteEdit.Ville = pointDeVente.Ville;
                        pointDeVenteEdit.CodePostal = pointDeVente.CodePostal;
                        pointDeVenteEdit.Latitude = pointDeVente.Latitude;
                        pointDeVenteEdit.Longitude = pointDeVente.Longitude;
                        pointDeVenteEdit.ModifiePar = currentUser.FirstName + " " + currentUser.LastName;
                        pointDeVenteEdit.ModifieDate = DateTime.Now;
                        db.Entry(pointDeVenteEdit).State = EntityState.Modified;

                    }
                    else
                    {

                        
                        pointDeVente.DateCreation = DateTime.Now;
                        pointDeVente.Statut = true;
                        pointDeVente.ModifiePar = currentUser.LastName + " " + currentUser.FirstName;
                        pointDeVente.ModifieDate = DateTime.Now;
                        db.PointDeVentes.Add(pointDeVente);
                    }

                    try
                    {

                        db.SaveChanges();

                        message = "Saved successfully!";
                        return Json(new { saved = true, message }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.InnerException);
                        message = "Operation Failed!";
                        return Json(new { dbError = true, message }, JsonRequestBehavior.AllowGet);

                    }


                }

                else if (string.IsNullOrWhiteSpace(pointDeVente.CodePointDeVente))
                {
                    message = "Please enter the Code!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);
                }
                else if (pointDeVenteCodeExist > 0)
                {
                    message = "This Code is already exist!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(pointDeVente.NomPointDeVente))
                {
                    message = "Please enter the Name!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);

                }
                else if (pointDeVenteNameExist > 0)
                {
                    message = "This Name is already exist!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(pointDeVente.Adresse))
                {
                    message = "Please enter the Adresse!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);

                }
                else if (string.IsNullOrWhiteSpace(pointDeVente.Ville))
                {
                    message = "Please enter the City!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);

                }
                else if (string.IsNullOrWhiteSpace(pointDeVente.CodePostal))
                {
                    message = "Please enter the Postal Code!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);

                }
                else if (string.IsNullOrWhiteSpace(pointDeVente.CodePostal))
                {
                    message = "Please enter the Postal Code!";
                    return Json(new { validationError = true, message }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return null;
                }


            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }
        }







        [AjaxOnly]
        public ActionResult _Edit(int? id)
        {
            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];

            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }



            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/_Edit", "Button Edit [Point of Sale]");




            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }

            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "edit")))
            {

                string message = null;
                if (id == null)
                {
                    message = "Id Point of sale not Found!";
                    return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);

                }

                PointDeVente pointDeVente = db.PointDeVentes.FirstOrDefault(p => p.PointDeVenteId == id);

                if (pointDeVente == null)
                {
                    message = "Point of sale not Found!";
                    return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);
                }

                return PartialView(pointDeVente);

            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }






        [AjaxOnly]
        public ActionResult Delete(int? id)
        {
            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];

            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }

            

            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/Delete", "Button Delete [Point of Sale]");



            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }


            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "delete")))
            {
                string message = null;
                try
                {
                    PointDeVente pointDeVente = db.PointDeVentes.FirstOrDefault(p => p.PointDeVenteId == id);

                    if (pointDeVente == null)
                    {
                        message = "Point of sale not Found!";
                        return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);
                    }
                    message = "Point of sale " + pointDeVente.NomPointDeVente + " successfully deleted!";
                    db.PointDeVentes.Remove(pointDeVente);
                    db.SaveChanges();
                    return Json(new { saved = true, message, ctrlName = "PointDeVentes" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception a)
                {
                    message = "Deletion not performed. This Point of sale is used for other entities!";
                    return Json(new { dbEx = true, message }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }




        [AjaxOnly]
        public ActionResult StatutToggle(int? id)
        {
            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];

            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }


            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/StatutToggle", "Button Active or Inactive [Point of Sale]");





            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }


            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "edit")))
            {
                string message = null;

                PointDeVente pointDeVente = db.PointDeVentes.FirstOrDefault(p => p.PointDeVenteId == id);

                if (pointDeVente == null)
                {
                    message = "Point of sale Not Found!";
                    return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);
                }
                try
                {

                    pointDeVente.Statut = !pointDeVente.Statut;
                    db.Entry(pointDeVente).State = EntityState.Modified;
                    db.SaveChanges();

                }
                catch (Exception u)
                {
                    message = "Operation Failed!";
                    return Json(new { dbEx = true, message }, JsonRequestBehavior.AllowGet);
                }

                if (pointDeVente.Statut)
                {
                    message = "The Point of sale " + pointDeVente.NomPointDeVente + "  is successfully Active!";
                }
                else
                {
                    message = "The Point of sale " + pointDeVente.NomPointDeVente + "  is successfully Inactive!";

                }
                return Json(new { saved = true, message }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }






        [AjaxOnly]
        public ActionResult Maps()
        {

            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];

            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }


            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/Maps", "Button Map [Point of Sale]");



            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }

            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "index")))
            {

                string message = null;



                var pointDeVente = db.PointDeVentes.ToList();

                if (pointDeVente.Count() == 0)
                {
                    message = "No Point of Sale Found!";
                    return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);
                }

                return View();

            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }





        [AjaxOnly]
        public ActionResult MapIndex()
        {

            User currentUser = (User)Session["userData"];
            Compagnie currentCompagnie = (Compagnie)Session["compagnieData"];



            if (currentUser == null || currentCompagnie == null)
            {
                return Json(new { returnToLogin = true }, JsonRequestBehavior.AllowGet);
            }


            new AccountController().AddUserActionLog(currentUser, currentCompagnie, "PointDeVentes/MapIndex", "Button Maps [Point of Sale]");



            int sessionIdExist = db.Users.Where(u => u.SessionId == HttpContext.Session.SessionID).Count();

            if (sessionIdExist == 0)
            {
                HttpContext.Session.Abandon();
                string message1 = "You have lost this connection because a new one has been detected!";
                return Json(new { newSession = true, message1 }, JsonRequestBehavior.AllowGet);

            }

            if (currentUser.Role.RolePermissions.ToList().Exists(u => u.Permission.ParentName.Trim().ToLower() == "pointdeventes" && (u.FullPermission || u.Permission.ObjectName.Trim().ToLower() == "index")))
            {

                string message = null;

                var pointDeVentes = db.PointDeVentes.AsEnumerable().Where(p => !string.IsNullOrWhiteSpace(p.Latitude) && !string.IsNullOrWhiteSpace(p.Longitude)).Select(p => new { Juridiction = p.NomPointDeVente, Name = p.CodePointDeVente, Coords = new { p.Latitude, Longitude = p.Longitude }, Departement = p.Ville, Capacite = 4 }).ToList();

                if (pointDeVentes.Count() == 0)
                {
                    message = "No point of Salñe!";
                    return Json(new { notFound = true, message }, JsonRequestBehavior.AllowGet);
                }
                ViewBag.mapDatas = new JavaScriptSerializer().Serialize(pointDeVentes);

                return View();


            }
            else
            {
                return Json(new { noPermission = true }, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
﻿
using MySql.Data.MySqlClient;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Item> list = new List<Item>();
            return View(list);
        }

    }
}
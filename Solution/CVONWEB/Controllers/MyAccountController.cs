﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace CVOnWeb.Controllers
{
    public class MyAccountController : RenderMvcController
    {
        // GET: MyAccount
        public override ActionResult Index(ContentModel model)
        {
            return View(model);
        }
    }
}
﻿using CRUD.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CRUD
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {
           var createTableDB = new CreateTableDB();
            createTableDB.CriarTabelasSeNaoExistirem().GetAwaiter().GetResult();

        }
    }
}
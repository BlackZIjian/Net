using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestWeb.Models;

namespace TestWeb.Service
{
    public class ServiceBase
    {
        private static ServiceBase mInstance;
        public static ServiceBase Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ServiceBase();
                    mInstance.db = new dotnetEntities2();
                }
                return mInstance;
            }
        }

        protected dotnetEntities2 db;
    }
}
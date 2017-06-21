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
using TestWeb.Enumeration;

namespace TestWeb.Service
{
    public class LoginService : ServiceBase
    {
        public new static LoginService Instance
        {
            get { return ServiceBase.Instance as LoginService; }
        }

        public muser CheckLogin(string username,string password,out ServiceMessage message)
        {
            message = ServiceMessage.UserNotFound;
            for(int i=0;i<db.muser.Count();i++)
            {
                muser user = db.muser.ElementAt(i);
                if(user.userName == username)
                {
                    if(user.password == password)
                    {
                        message = ServiceMessage.LoginSuccess;
                        return user;
                    }
                    else
                    {
                        message = ServiceMessage.PasswordError;
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
﻿using System;
using System.Web.Security;
using SportsStore.WebUI.Infrastructure.Abstract;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool results = FormsAuthentication.Authenticate(username, password);
            if (results)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return results;

        }
    }
}
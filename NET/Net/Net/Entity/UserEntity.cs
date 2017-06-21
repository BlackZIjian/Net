using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net.Entity
{
    public class UserEntity : Entity
    {
        public string UserName
        {
            get
            {
                if(mAttrs.ContainsKey("UserName")) {
                    return (string)mAttrs["UserName"];
                }
                return "";
            }
            set
            {
                mAttrs["UserName"] = value;
            }
        }

        public string Password
        {
            get
            {
                if (mAttrs.ContainsKey("Password"))
                {
                    return (string)mAttrs["Password"];
                }
                return "";
            }
            set
            {
                mAttrs["Password"] = value;
            }
        }

        public string Telephone
        {
            get
            {
                if (mAttrs.ContainsKey("Telephone"))
                {
                    return (string)mAttrs["Telephone"];
                }
                return "";
            }
            set
            {
                mAttrs["Telephone"] = value;
            }
        }

        public string Description
        {
            get
            {
                if (mAttrs.ContainsKey("Description"))
                {
                    return (string)mAttrs["Description"];
                }
                return "";
            }
            set
            {
                mAttrs["Description"] = value;
            }
        }
    }
}
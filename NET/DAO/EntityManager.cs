using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net.MEntity;
using MysqlOp;
using MySql.Data.MySqlClient;


namespace Net.DAO
{
    public class EntityManager
    {
        private static EntityManager mInstance;
        public EntityManager Instance
        {
            get
            {
                if (mInstance != null)
                {
                    return mInstance;
                }
                else
                {
                    mInstance = new EntityManager();
                    return mInstance;
                }
            }
        }


        public virtual Entity GetById(int id)
        {
            return null;
        }

        public virtual Entity Create(Dictionary<string,object> attr)
        {
            return null;
        }

        public virtual void Del(int id)
        {
            
        }

        public virtual Entity Update(int id,Dictionary<string,object> attr)
        {
            return null;
        }
    }
}
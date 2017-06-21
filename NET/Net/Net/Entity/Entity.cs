using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net.Tools;


//实体对象基类
namespace Net.Entity
{
    public class Entity
    {
        protected int mId;
        public int Id
        {
            get { return mId; }
            set { mId = value; }
        }
        protected Dictionary<string, object> mAttrs;

        public T GetAttr<T>(string key)
        {
            if(mAttrs.ContainsKey(key))
            {
                return (T)mAttrs[key];
            }
            else
            {
                return default(T);
            }
        }

        public bool UpdateAttr<T>(string key,T newValue)
        {
            if (mAttrs.ContainsKey(key))
            {
                mAttrs[key] = newValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAttr<T>(string key,T defaultValue)
        {
            if (mAttrs.ContainsKey(key))
            {
                return false;
            }
            else
            {
                mAttrs.Add(key, defaultValue);
                return true;
            }
        }

        public bool RemoveAttr<T>(string key)
        {
            if (mAttrs.ContainsKey(key))
            {
                mAttrs.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Entity Create(int id)
        {
            Entity entity = new Entity();
            entity.Id = id;
            entity.mAttrs = new Dictionary<string, object>();
            return entity;
        }

        public static Entity ParseJson(string json)
        {
            try
            {
                Entity entity = new Entity();
                Dictionary<string, object> dic = JsonTool.JsonToDictionary(json);
                entity.Id = (int)dic["Id"];
                dic.Remove("Id");
                entity.mAttrs = dic;
                return entity;
            }
            catch
            {
                return null;
            }
        }
        public string ToJson()
        {
            Dictionary<string, object> json = mAttrs;
            json["Id"] = mId;
            return JsonTool.DictionaryToJson(json);
        }
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Core;

using System.Reflection;

namespace DataAccess
{
    public class BaseFunctions<T> where T : class, new()
    {
        private string GetTypeT()
        {
            return typeof(T).Name;
        }

        /// <summary>
        /// H�m l?y danh s�ch c�c gi� tr? c�c thu?c t�nh c?a m?t ??i t??ng
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<object> GetInsertUpdateValues(T item)
        {
            //L�y danh s�ch c�c thu?c t�nh c?a m?t ?t
            object[] props = item.GetType().GetProperties();

            //Danh s�ch gi� tr? c�c thu?c t�nh
            List<object> objvalue = new List<object>();

            //Duy?t danh s�ch c�c thu?c t�nh
            foreach (PropertyInfo prop in props)
            {
                //L?y gi� tr? c?a m?t thu?c t�nh v� add v� danh s�ch gi� tr? c�c thu?c t�nh
                objvalue.Add(prop.GetValue(item, null));
            }

            return objvalue;
        }

        public List<T> SelectAll()
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader("GetAll"+ this.GetTypeT()));
        }

        public List<T> SelectPage(object From, object To)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader(this.GetTypeT() + "_SelectPage", From, To));
        }

        public int Add(T obj)
        {
            return DataProvider.Instance.ExecuteNonQuery("Add"+ this.GetTypeT(), this.GetInsertUpdateValues(obj).ToArray());
        }

        public int Update(T obj)
        {

            return DataProvider.Instance.ExecuteNonQuery("Update"+ this.GetTypeT(), this.GetInsertUpdateValues(obj).ToArray());
        }

        public int Delete(object ID)
        {

            return DataProvider.Instance.ExecuteNonQuery("Delete"+ this.GetTypeT(), ID);
        }

        public List<T> SelectBy(object item, string Name)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader( "GetBy" + Name + this.GetTypeT(), item));
        }

        public List<T> SelectByID(object ID)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader("GetByID"+ this.GetTypeT(), ID));
        }

        public List<T> FindKeyWord(object item)
        {
            return CBO.FillCollection<T>(DataProvider.Instance.ExecuteReader("FindKeyWord"+ this.GetTypeT(), item));
        }

        public DataTable SelectTables(string SPName, params object[] parameterValues)
        {
            return null;
        }

    }
}

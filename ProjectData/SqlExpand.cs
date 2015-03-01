using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
    public static class SqlExpand
    {
        public static void DeleteAllOnSubmit<T>(this System.Data.Objects.ObjectQuery<T> obj, IQueryable<T> query) where T : class
        {
            foreach(T t in query)
            {
                obj.Context.DeleteObject(t);
            }
        }
    }
 
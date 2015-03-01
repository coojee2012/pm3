using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.ComponentModel;

namespace Tools
{
    public static  class CopyTable
    {
 
        /// <summary>
        /// 静态方法，将一个实体的值复制到另一个实体
        /// </summary>
        /// <typeparam name="From"></typeparam>
        /// <typeparam name="To"></typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static To Copy<From, To>(this From from, To to) where From : INotifyPropertyChanging, INotifyPropertyChanged where To: INotifyPropertyChanging, INotifyPropertyChanged
        {
            Type type = typeof(From);
            
            if (from != null)
            {
                foreach (PropertyInfo property in from.GetType().GetProperties())
                {
                    object value = property.GetValue(from, null);
                    if (value != null)
                    {
                        //foreach (PropertyInfo p in k.GetType().GetProperties())
                        //{
                        //    if (property.Name == p.Name)
                        //    {
                        Type typeTo = typeof(To);
                        PropertyInfo proper = typeTo.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (proper != null    )
                        {
                            try
                            {
                                proper.SetValue(to, value, null);
                            }
                            catch (Exception ex)
                            { }
                        }
                        //    }
                        //}
                    }
                }
            }
            return to;
        }

 
    }
}

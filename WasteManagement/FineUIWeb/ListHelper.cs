using System;
using System.Collections.Generic;
using System.Web;

namespace WasteManagement
{
    public static class ListHelper
    {
        //得到a有,B没有的
        public static List<int> ExceptList(List<int> a, List<int> b)
        {
            List<int> c = new List<int>();
            foreach (int i in a)
            {
                if (!b.Contains(i))
                {
                    c.Add(i);
                }
            }
            return c;
        }

        //得到a和B都有的
        public static List<int> SameList(List<int> a, List<int> b)
        {
            List<int> c = new List<int>();
            foreach (int i in a)
            {
                if (b.Contains(i))
                {
                    c.Add(i);
                }
            }
            return c;
        }
    }
}
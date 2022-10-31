using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ClassFolder
{
    public partial class EmplyeeClass
    {
        public string GetHour
        {
            get
            {
                string name = "Доброй ночи!";
                if(DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12)
                {
                    name = "Доброе утро!";
                }
                if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                {
                    name = "Доброе день!";
                }
                if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 23)
                {
                    name = "Доброе вечер!";
                }
            }
        }
}

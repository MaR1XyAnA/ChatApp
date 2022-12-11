using System;

namespace ChatApp.ClassFolder
{
    public partial class EmplyeeClass
    {
        public string GetHour
        {
            get
            {
                string name = "Доброй ночи!";
                if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12)
                {
                    name = "Доброе утро!";
                }
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                {
                    name = "Доброе день!";
                }
                else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 23)
                {
                    name = "Доброе вечер!";
                }
                return name;
            }
        }
        //public string GetWeather
        //{
        //    get
        //    {

        //    }
        //}
        public string Hello
        {
            get
            {
                string HelloName = $"{GetHour} {NameUser}";
                return HelloName;
            }
        }
    }
}

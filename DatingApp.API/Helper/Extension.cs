using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helper
{
    public static class Extension
    {
        // For Gloabl Error
         public static void AddApplicationError (this HttpResponse response, string message){
                response.Headers.Add("Application-Error",message);
                response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
                response.Headers.Add("Access-Control-All-Origin","*");
        }
        public static int CalculateAge (this DateTime _dateTime){
            var age= DateTime.Today.Year - _dateTime.Year;
            if(_dateTime.AddYears(age) > DateTime.Today)
            age --;

            return age;
        }
    }
}
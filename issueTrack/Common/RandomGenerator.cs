using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace issueTrack.Common
{
    public class RandomGenerator
    {
        /// <summary>
        /// return A-Z and 0-9 mix string
        /// </summary>
        /// <param name="stringLength"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int stringLength)
        {
            if (stringLength <= 1)
                return "";
            Random rnd = new Random(DateTime.Now.Millisecond);
            StringBuilder bd = new StringBuilder();
            
            for(int i = 0; i < stringLength; i ++)
            {
                int tmp = rnd.Next(65, 101);
                if(tmp <= 90)
                {
                    bd.Append((char)tmp);
                }
                else
                {
                    bd.Append(((tmp - 90) % 10).ToString());
                }
            }
            return bd.ToString();
        }
    }
}
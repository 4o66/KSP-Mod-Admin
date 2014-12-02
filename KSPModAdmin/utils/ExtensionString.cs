using System;
using System.Collections.Generic;

namespace KSPModAdmin.Utils
{
    public static class ExtensionsString
    {
        public static string[] Split(this string str, string seperator)
        {
            if (string.IsNullOrEmpty(seperator))
                return null;

            int sepLength = seperator.Length;
            if (sepLength == 1) 
                return str.Split(seperator[0]);

            int myLength = str.Length;
            List<string> list = new List<string>();
            string temp = string.Empty;
            for (int startIndex = 0; startIndex < myLength; ++startIndex)
            {
                if (sepLength <= myLength - startIndex)
                {
                    temp = str.Substring(startIndex, sepLength);
                    if (temp == seperator)
                    {
                        list.Add(str.Substring(0, startIndex));
                        str = str.Substring(startIndex + sepLength);
                        myLength = str.Length;
                        startIndex = -1;
                    }
                }
            }

            return list.ToArray();
        }
    }  
}

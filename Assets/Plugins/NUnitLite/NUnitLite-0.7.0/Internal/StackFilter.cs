// *****************************************************
// Copyright 2007, Charlie Poole
//
// Licensed under the Open Software License version 3.0
// *****************************************************

using System;
using System.IO;

namespace NUnit.Framework.Internal
{
    public class StackFilter
    {
        public static string Filter(string rawTrace)
        {
            if (rawTrace == null) return null;

            StringReader sr = new StringReader(rawTrace);
            StringWriter sw = new StringWriter();

            try
            {
                string line;
                while ((line = sr.ReadLine()) != null && line.IndexOf("NUnit.Framework.Assert") >= 0)
                    /*Skip*/
                    ;

                while (line != null)
                {
                    sw.WriteLine(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception)
            {
                return rawTrace;
            }

            return sw.ToString();
        }
    }
}

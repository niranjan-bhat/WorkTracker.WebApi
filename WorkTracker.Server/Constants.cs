using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WorkTracker.Server
{
    public static class Constants
    {
        public static string EncryptionSecretKey = "WorkTrackerSecretKey";

        #region ErrorCodes
        public static string InvalidEmail = "http://localhost:60365";
        

        #endregion
       

    }
}

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
        public static string OWNER_NOT_FOUND = "OWNER_NOT_FOUND";
        public static string WORKER_NOT_FOUND = "WORKER_NOT_FOUND";
        public static string JOB_NOT_FOUND = "JOB_NOT_FOUND";
        public static string ASSIGNMENT_NOT_FOUND = "ASSIGNMENT_NOT_FOUND";
        public static string INVALID_TOKEN = "INVALID_TOKEN";
        public static string USERNAMEPASSWORD_WRONG = "USERNAMEPASSWORD_WRONG";
        public static string DUPLICATE_JOBNAME = "DUPLICATE_JOBNAME";
        public static string DUPLICATE_MOBILE_NUMBER = "DUPLICATE_MOBILE_NUMBER";
        public static string DUPLICATE_WORKERNAME = "DUPLICATE_WORKERNAME";
        public static string INVALID_EMAIL = "INVALID_EMAIL";
        #endregion

        #region DberrorStrings

        public static string DbErrorStringDuplicateJobName = "Violation of UNIQUE KEY constraint 'AK_Job_Name_OwnerId";

        #endregion


    }
}

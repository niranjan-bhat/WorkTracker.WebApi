﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkTracker.Server {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WorkTracker.Server.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to authenticate the user..
        /// </summary>
        public static string AuthenticationFailure {
            get {
                return ResourceManager.GetString("AuthenticationFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Violation of UNIQUE KEY constraint &apos;AK_Job_Name_OwnerId&apos;.
        /// </summary>
        public static string DBErrorDuplicateJobName {
            get {
                return ResourceManager.GetString("DBErrorDuplicateJobName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot insert duplicate key row in object &apos;dbo.Worker&apos; with unique index &apos;IX_Worker_Mobile&apos;.
        /// </summary>
        public static string DBErrorDuplicateWorkerMobile {
            get {
                return ResourceManager.GetString("DBErrorDuplicateWorkerMobile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Violation of UNIQUE KEY constraint &apos;AK_Worker_Name_OwnerId&apos;.
        /// </summary>
        public static string DBErrorDuplicateWorkerName {
            get {
                return ResourceManager.GetString("DBErrorDuplicateWorkerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Job with this name is already present.
        /// </summary>
        public static string DuplicateJobName {
            get {
                return ResourceManager.GetString("DuplicateJobName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mobile number belongs to another worker..
        /// </summary>
        public static string DuplicateMobileNumber {
            get {
                return ResourceManager.GetString("DuplicateMobileNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to process the request, duplicate worker name..
        /// </summary>
        public static string DuplicateWorkerName {
            get {
                return ResourceManager.GetString("DuplicateWorkerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Assignment not found.
        /// </summary>
        public static string ErrorAssignmentNotFound {
            get {
                return ResourceManager.GetString("ErrorAssignmentNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid comment.
        /// </summary>
        public static string ErrorComment {
            get {
                return ResourceManager.GetString("ErrorComment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid date.
        /// </summary>
        public static string ErrorInvalidDate {
            get {
                return ResourceManager.GetString("ErrorInvalidDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid email address.
        /// </summary>
        public static string ErrorInvalidEmail {
            get {
                return ResourceManager.GetString("ErrorInvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid value for job.
        /// </summary>
        public static string ErrorInvalidJob {
            get {
                return ResourceManager.GetString("ErrorInvalidJob", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid name.
        /// </summary>
        public static string ErrorInvalidName {
            get {
                return ResourceManager.GetString("ErrorInvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid worker name.
        /// </summary>
        public static string ErrorInvalidWorkerName {
            get {
                return ResourceManager.GetString("ErrorInvalidWorkerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date range can be maximum of 1000 days.
        /// </summary>
        public static string ErrorOverflowDateRange1000Days {
            get {
                return ResourceManager.GetString("ErrorOverflowDateRange1000Days", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date range can be maximum of 31 days.
        /// </summary>
        public static string ErrorOverflowDateRange31 {
            get {
                return ResourceManager.GetString("ErrorOverflowDateRange31", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Worker not found.
        /// </summary>
        public static string ErrorWorkerNotFound {
            get {
                return ResourceManager.GetString("ErrorWorkerNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid value for date.
        /// </summary>
        public static string InvalidDate {
            get {
                return ResourceManager.GetString("InvalidDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid worker mobile number.
        /// </summary>
        public static string InvalidWorkerMobileNumber {
            get {
                return ResourceManager.GetString("InvalidWorkerMobileNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Job not found.
        /// </summary>
        public static string JobNotFound {
            get {
                return ResourceManager.GetString("JobNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Owner not found.
        /// </summary>
        public static string OwnerNotFound {
            get {
                return ResourceManager.GetString("OwnerNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User already registered with this email.
        /// </summary>
        public static string OwnerPresent {
            get {
                return ResourceManager.GetString("OwnerPresent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to add worker.
        /// </summary>
        public static string UnableToAddWorker {
            get {
                return ResourceManager.GetString("UnableToAddWorker", resourceCulture);
            }
        }
    }
}

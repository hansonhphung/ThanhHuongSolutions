﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThanhHuongSolution.Common.LocResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CustomerManagementResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CustomerManagementResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ThanhHuongSolution.Common.LocResources.CustomerManagementResources", typeof(CustomerManagementResources).Assembly);
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
        ///   Looks up a localized string similar to Khách hàng này đã có lịch sử mua bán, không thể xoá được..
        /// </summary>
        public static string CUSTOMER_CAN_NOT_DELETE {
            get {
                return ResourceManager.GetString("CUSTOMER_CAN_NOT_DELETE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã khách hàng này đã tồn tại..
        /// </summary>
        public static string CUSTOMER_EXIST {
            get {
                return ResourceManager.GetString("CUSTOMER_EXIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã khách hàng không được rỗng..
        /// </summary>
        public static string CUSTOMER_ID_REQUIRED {
            get {
                return ResourceManager.GetString("CUSTOMER_ID_REQUIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Khách hàng này không tồn tại..
        /// </summary>
        public static string CUSTOMER_NOT_EXIST {
            get {
                return ResourceManager.GetString("CUSTOMER_NOT_EXIST", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không tìm thấy khách hàng..
        /// </summary>
        public static string CUSTOMER_NOT_FOUND {
            get {
                return ResourceManager.GetString("CUSTOMER_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã khách hàng không hợp lệ..
        /// </summary>
        public static string ID_INVALID {
            get {
                return ResourceManager.GetString("ID_INVALID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hiện tại chưa có khách hàng..
        /// </summary>
        public static string NO_CUSTOMER {
            get {
                return ResourceManager.GetString("NO_CUSTOMER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không thể xoá thông tin khách hàng bán lẻ..
        /// </summary>
        public static string RETAIL_CUSTOMER_CAN_NOT_DELETED {
            get {
                return ResourceManager.GetString("RETAIL_CUSTOMER_CAN_NOT_DELETED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không thể thay đổi thông tin của khách hàng bán lẻ..
        /// </summary>
        public static string RETAIL_CUSTOMER_CAN_NOT_UPDATED {
            get {
                return ResourceManager.GetString("RETAIL_CUSTOMER_CAN_NOT_UPDATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã khách hàng đã tồn tại..
        /// </summary>
        public static string TRACKING_NUMBER_EXIST {
            get {
                return ResourceManager.GetString("TRACKING_NUMBER_EXIST", resourceCulture);
            }
        }
    }
}

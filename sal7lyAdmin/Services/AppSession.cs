using Model;

namespace sal7lyAdmin.Services
{
    public partial class AppSession
    {
        public static string AppURL = "http://13.57.196.134/";
        //public static string AppURL { get; internal set; }
        #region Upload Folders
        public static string OfferImagesUploads = "Uploads/OfferImages";
        public static string CategoriesUploads = "Uploads/Categories";
        public static string ItemsUploads = "Uploads/Items";
        public static string ServicesImagesUploads = "Uploads/ServicesImages";
        public static string CustomerUploads = "Uploads/Customer";
        public static string UserUploads = "Uploads/Users";
        public static string OrderServiceUploads = "Uploads/OrderService";
        public static string CustomerReviewUploads = "Uploads/CustomerReview";
        
        #endregion

        #region Default Image
        public static string CustomerDefaultImage = "default.jpg";
        public static string UserDefaultImage = "default.jpg";
        public static string ServiceDefaultImage = "default.jpg";
        public static string OfferDefaultImage = "default.jpg";
        #endregion
        public static string CompanyEmail = "";
        public static string CompanyEmailDisplayName = "SMS BackEnd Email Service";
        public static string CompanyEmailPassword = "";

        public static Users CurrentUser
        {
            get
            {
                var _user = WebHelpers.HttpContext.Session.Get<Users>("CurrentUser");
                if (_user != null)
                {
                    try
                    { return _user; }
                    catch { }
                }
                return null;
            }
            set
            {
                WebHelpers.HttpContext.Session.Set("CurrentUser", value);
            }
        }


    }
}

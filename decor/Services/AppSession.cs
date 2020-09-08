using Microsoft.AspNetCore.Hosting;
using Model;

namespace Decor.Services
{
    public partial class AppSession
    {
        private readonly IHostingEnvironment env;
        
        public static string ServicesUploads = "Uploads/ServicesImages";
        public static string CustomerUploads = "Uploads/Customer";
        public static string ImagesHostvalue = "http://13.57.196.134";
        public AppSession()
        {
        }
        public AppSession(IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                ImagesHostvalue = "http://localhost:45451/";
            }
            else
            {
                ImagesHostvalue = "http://13.57.196.134";
            }
        }

        public static string ImagesHost = "http://13.57.196.134";
        public static string SliderImagesUploads = "Uploads/SliderImages";
        public static string JobApplyUploads = "Uploads/JobApplyUploads";
        public static string CustomerReviewUploads = "Uploads/CustomerReview";
        public static string CategoriesUploads = "Uploads/Categories";
        public static string ItemsUploads = "Uploads/Items";
        public static string UploadsFolder = "/Uploads";
        public static string CompanyEmail = "esraa.makhlof999@gmail.com";
        public static string CompanyEmailDisplayName = "Ecommerce Email Service";
        public static string CompanyEmailPassword = "15119956";

        
        public static bool IsArabic { get; set; } = true;
    }
}

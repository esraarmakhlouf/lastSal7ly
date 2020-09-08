using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.APIModels
{
    public class FIleUploadAPI
    {
        public IFormFile files
        {
            get;
            set;
        }
    }
}

using Microsoft.AspNetCore.Http;
using sal7lyAdmin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sal7lyAdmin
{
    public class Images
    {
        public async static Task<string> SaveFile(string filePath, IFormFile file, string lastName = "", string defaultName = "")
        {
            try
            {
                var fileName = "";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //check file name exist in directory
                if (file != null && file.Length > 0)
                {
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + file.FileName;
                    if (File.Exists(Path.Combine(filePath, fileName)))
                    {
                        //choose other name     
                        fileName = Guid.NewGuid() + fileName;
                    }
                    using (var fileContentStream = new MemoryStream())
                    {
                        await file.CopyToAsync(fileContentStream);
                        await System.IO.File.WriteAllBytesAsync(Path.Combine(filePath, fileName), fileContentStream.ToArray());
                    }

                    //delete last file
                    if (!string.IsNullOrEmpty(lastName) && lastName != defaultName)
                    {
                        if (System.IO.File.Exists(Path.Combine(filePath, lastName)))
                        {
                            System.IO.File.Delete(Path.Combine(filePath, lastName));
                        }
                    }
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}

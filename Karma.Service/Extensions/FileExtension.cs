using Karma.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Karma.Service.Extensions
{
    public static class FileExtension
    {
        public static string SaveFile(this IFormFile file,string rootPath,string folder)
        {
            string RootPath = Path.Combine(rootPath, folder);
            string FileName = Guid.NewGuid().ToString() + file.FileName;
            string FullPath = Path.Combine(RootPath, FileName);
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return FileName;

        }


        public static bool IsImage(this IFormFile file)
        {

            return file.ContentType.Contains("image");

        }

        public static bool IsSizeOk(this IFormFile file,int mb)
        {
            double length = ((double)(file.Length / 1024) / 1024);
            return length > mb;
        }

    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stolarstwo.Models
{
    public class GalleryModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public string Title { get; set; } = "Gallery";
        public GalleryModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GalleryImagesPath { get; set; }

        public string[] GetImages(string type)
        {
            var provider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath);
            string dirPath = Path.Combine(provider.Root, "images", type);


            List<string> images = new();
            images.AddRange(Directory.GetFiles(dirPath, "*.jpg"));
            images.AddRange(Directory.GetFiles(dirPath, "*.jpeg"));
            images.AddRange(Directory.GetFiles(dirPath, "*.png"));
            images.AddRange(Directory.GetFiles(dirPath, "*.bmp"));

            return images.ToArray();
        }
    }
}

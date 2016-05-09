using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RabbitHouse.Models;
using RabbitHouse.ViewModels;
using System.IO;
using RabbitHouse.ExternalClasses;

namespace RabbitHouse.ExternalClasses
{
    public class UploadedFile
    {
        private HttpPostedFileBase PostedFile;
        public UploadedFile(HttpPostedFileBase file)
        {
            PostedFile = file;
        }

        public string SaveAs(string directory)
        {
            var pathAbs = Path.Combine(directory, PostedFile.FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(pathAbs));
            PostedFile.SaveAs(pathAbs);

            return PostedFile.FileName;
        }
        public string SaveAsWithGuid(string directory)
        {
            var fileNewName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(PostedFile.FileName);
            var pathAbs = Path.Combine(directory, fileNewName);

            Directory.CreateDirectory(Path.GetDirectoryName(pathAbs));
            PostedFile.SaveAs(pathAbs);

            return fileNewName;
        }

        public static string UploadedFileMoveTo(string sourcePath,string targetPath)
        {
            var file = new FileInfo(sourcePath);
            file.MoveTo(targetPath);

            Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            return file.Name;
        }
    }
}
using Core.Libraries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace Core.Models
{
    public class FileLibrary : BaseModel
    {
        [Required]
        public string ContentDisposition { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }
            
        public byte[] FileData { get; set; }

        public FileLibrary()
        {
        }

        public FileLibrary(IFormFile formFile)
        {
            this.ContentType = formFile.ContentType;
            this.ContentDisposition = formFile.ContentDisposition;
            this.FileName = formFile.FileName;            
            this.FileSize = formFile.Length;

            using(MemoryStream ms = new MemoryStream())
            {
                formFile.OpenReadStream().CopyTo(ms);
                this.FileData = ms.ToArray();
            }
        }

        public Stream GetFile()
        {
            return new MemoryStream(FileData);
        }
    }
}

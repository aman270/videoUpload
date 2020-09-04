using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace uploadVideo.Models
{
    public class Video
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Video Name")]
        public string videoName { get; set; }

        [Display(Name = "Type of Video")]
        public string ContentType { get; set; }

        public IFormFile file { set; get; }

        public string fileId { get; set; }

        public bool HasImage()
        {
            return !String.IsNullOrWhiteSpace(fileId);
        }


    }
}

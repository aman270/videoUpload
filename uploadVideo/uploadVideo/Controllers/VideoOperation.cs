using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using uploadVideo.Models;


namespace uploadVideo.Controllers
{
    public class VideoOperation : Controller
    {
       
        IGridFSBucket gridFS;   //file storage
        IMongoCollection<Video> video; // collection in database

        public VideoOperation()
        {
            //connection string
            string connectionString = "mongodb://localhost:27017/VideoDB";
            var connection = new MongoUrlBuilder(connectionString);
            // get a client to interact with the database
            MongoClient client = new MongoClient(connectionString);
            // we get access to the database itself
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            // get access to file storage
            gridFS = new GridFSBucket(database);
            video = database.GetCollection<Video>("Videos");
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload( Video p, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                await video.InsertOneAsync(p);
                ObjectId fileId = new ObjectId();
                fileId= await gridFS.UploadFromStreamAsync(uploadedFile.FileName, uploadedFile.OpenReadStream());
                p.fileId = fileId.ToString();
                return RedirectToAction("Index","HomeController");
            }
            return View(p);
        }
    }
}

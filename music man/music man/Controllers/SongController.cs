using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;
using music_man.models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace music_man.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public SongController(IConfiguration configuration , IWebHostEnvironment env)
        {
            this._configuration = configuration;
            this._env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM `albums`";
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post(Song song)
        {
            //INSERT INTO `songs`(`SNG_ID`, `SNG_NAME`, `SNG_YEAR`, `ART_ID`, `ALB_id`, `SND_URL`) VALUES ('[value-1]','[value-2]','[value-3]','[value-4]','[value-5]','[value-6]')
            string query = @"INSERT INTO `songs`(`SNG_NAME`, `SNG_YEAR`, `ART_ID`, `ALB_id`, `SND_URL`)"+
                                 "VALUES ('"+song.song_name+"','"+song.song_year+"','"+song.art_id+"','"+song.albm_id+"','"+song.song_url+"')";
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);

        }

        [HttpPut]
        public JsonResult Put(Song song)
        {
            string query = @"UPDATE `songs` SET `SNG_NAME`='"+ song .song_name+ 
                "',`SNG_YEAR`='"+song.song_year+
                "',`ART_ID`='"+song.art_id+
                "',`ALB_id`='"+song.albm_id+
                "',`SND_URL`='"+song.song_url+
                "' WHERE `SNG_ID`="+song.song_id;
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);




        }

        [HttpDelete]
        public JsonResult Delete(Song song)
        {
            string query = @" DELETE FROMDELETE FROM `songs` WHERE "+song.song_id;
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);
        }

        [Route("Music_upload")]
        [HttpPost]
        public JsonResult UploadMusic()
        {
            try
            {
                var httprequest = Request.Form;
                var postedFile = httprequest.Files[0];
                string filename = postedFile.FileName;
                var physicalpath = _env.ContentRootPath + "/Music_files/"+filename;

                

                using (var stream = new FileStream(physicalpath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception e) 
            { 

            }
            return new JsonResult("failed");
        }

    }
}

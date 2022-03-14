using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using music_man.models;

namespace music_man.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AlbumController(IConfiguration configuration)
        {
            this._configuration = configuration;
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
        public JsonResult Post(Album album)
        {          
            string query = @"INSERT INTO `albums`( `ALB_NAME`, `art_id`) 
                           VALUES ('"+album.alb_name+"','"+album.art_id+"')";
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);

        }

        [HttpPut]
        public JsonResult Put(Album album)
        {
            string query = @"UPDATE `albums` 
                 SET `ALB_NAME`='"+album.alb_name+"',`art_id`='"+album.art_id+"'" +
                 " WHERE ALB_ID`="+album.art_id;
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);




        }

        [HttpDelete]
        public JsonResult Delete(Artist artist)
        {
            string query = @" DELETE FROM `albums` WHERE 'ART_ID'=" + artist.artist_id;
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);



        }
    }
}

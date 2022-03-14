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
    public class ArtistController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ArtistController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM `artist`";
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);
        }

        [HttpPost]
        public JsonResult Post(Artist artist)
        {
            string query = @"INSERT INTO `artist`( `name`, `no_album`)
                               VALUES 
                              ('" + artist.art_name + "','" + artist.no_alb + "')";
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);

        }

        [HttpPut]
        public JsonResult Put(Artist artist)
        {
            string query = @"UPDATE `artist` SET `name`='" + artist.art_name + "'," +
                "`no_album`='" + artist.no_alb + "'" +
                "WHERE 'ART_ID'=" + artist.artist_id;
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);
         



        }

        [HttpDelete]
        public JsonResult Delete(Artist artist)
        {
            string query = @" DELETE FROM `artist` WHERE 'ART_ID'=" + artist.artist_id;
            Connection connection = new Connection();
            DataTable dataTable = connection.mysql_executor(_configuration.GetConnectionString("Music_con"), query);
            return new JsonResult(dataTable);



        }

    }
}

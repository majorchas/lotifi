using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace music_man.models
{
    public class Song
    {
        public int song_id { get; set; }
        public int art_id { get; set; }
        public int albm_id { get; set; }
        public int song_year { get; set; }
        public string song_name { get; set; }
        public string song_url { get; set; }


    }
}

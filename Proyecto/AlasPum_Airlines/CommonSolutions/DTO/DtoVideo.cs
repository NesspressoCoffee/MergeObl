using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolutions.DTO
{
    public class DtoVideo
    {
        public string urlVideo { get; set; }
        public string video { get; set; }
        public string modeloAvion { get; set; }

        public DtoVideo() { }
        public DtoVideo(string urlVideo, string video, string modeloAvion)
        {
            this.urlVideo = urlVideo;
            this.video = video;
            this.modeloAvion = modeloAvion;
        }
    }
}

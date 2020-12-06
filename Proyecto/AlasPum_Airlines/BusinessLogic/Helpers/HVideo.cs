using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using CommonSolutions.DTO;
using DataAccess.Persistencia;

namespace BusinessLogic.Helpers
{
    class HVideo : IVideo
    {
        private static HVideo instance;

        private HVideo()
        {

        }

        public static HVideo getInstance()
        {
            if (instance == null)
            {
                instance = new HVideo();
            }

            return instance;
        }

        public void addVideoRequest(DtoVideo dto)
        {
            PVideo pv = new PVideo();
            pv.addVideoRequest(dto);
        }

        public DtoVideo getVideoRequest(string modelo)
        {
            PVideo pv = new PVideo();
            return pv.getVideoRequest(modelo);
        }

        public bool existeVideoAvion(string modelo)
        {
            PVideo pv = new PVideo();
            return pv.existeVideoAvion(modelo);
        }
    }
}

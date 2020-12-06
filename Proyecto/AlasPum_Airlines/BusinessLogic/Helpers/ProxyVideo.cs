using BusinessLogic.Interfaces;
using BusinessLogic.Patrones;
using CommonSolutions.DTO;
using DataAccess.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class ProxyVideo : IVideo
    {
        HVideo hvideo;
        ClasePelotuda clasePelotuda;

        public ProxyVideo()
        {
            hvideo = HVideo.getInstance();
            clasePelotuda = ClasePelotuda.getInstance();
        }

        public DtoVideo getVideoRequest(string modelo)
        {
            if (hvideo.existeVideoAvion(modelo) == false) {

                DtoVideo dto = clasePelotuda.getVideo(modelo);
                hvideo.addVideoRequest(dto);
                return dto;
            }
            else
            {
                return hvideo.getVideoRequest(modelo);
            }
        }
    }
}

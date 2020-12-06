using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Patrones
{
    public class ClasePelotuda
    {
        private static ClasePelotuda instance;

        private ClasePelotuda()
        {

        }

        public static ClasePelotuda getInstance()
        {
            if (instance == null)
            {
                instance = new ClasePelotuda();
            }

            return instance;
        }

        public DtoVideo getVideo(string modelo)
        {
            DtoVideo dtoVideo = new DtoVideo("https://DummyServer:8800/Videos?mod=" + modelo + ".mp4", "Video de " + modelo, modelo);
            return dtoVideo;
        }
    }
}

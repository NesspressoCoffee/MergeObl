using CommonSolutions.DTO;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistencia
{
    public class PVideo
    {
        public MVideo mapper;

        public PVideo()
        {
            this.mapper = new MVideo();
        }

        public void addVideoRequest(DtoVideo dto)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                context.VideoAvion.Add(mapper.MapToObj(dto));
                context.SaveChanges();
            }
        }

        public DtoVideo getVideoRequest(string modelo)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                VideoAvion video = context.VideoAvion.Where(w => w.modeloAvion == modelo).FirstOrDefault();
                return mapper.MapToDto(video);
            }
        }

        public bool existeVideoAvion(string modelo)
        {
            using (alasdbEntities context = new alasdbEntities())
            {
                if (context.VideoAvion.Any(a => a.modeloAvion == modelo))
                {
                    return true;
                }
                return false;
            }
        }

    }
}

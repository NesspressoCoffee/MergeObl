using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    public class MVideo
    {
        public DtoVideo MapToDto(VideoAvion obj)
        {
            DtoVideo dto = new DtoVideo();
            dto.modeloAvion = obj.modeloAvion;
            dto.urlVideo = obj.urlVideo;
            dto.video = obj.video;
            return dto;
        }
        public VideoAvion MapToObj(DtoVideo dto)
        {
            VideoAvion obj = new VideoAvion();
            obj.modeloAvion = dto.modeloAvion;
            obj.urlVideo = dto.urlVideo;
            obj.video = dto.video;
            return obj;
        }

       
    }
}

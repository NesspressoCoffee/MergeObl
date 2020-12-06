using CommonSolutions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IVideo
    {
        DtoVideo getVideoRequest(string modelo);
    }
}

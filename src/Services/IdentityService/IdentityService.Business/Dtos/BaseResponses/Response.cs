using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Dtos.BaseResponses
{
    public class Response
    {
        public bool Success { get; set; }
        public Response(bool success)
        {
            Success = success;  
        }
    }
}

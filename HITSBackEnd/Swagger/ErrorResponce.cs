using HITSBackEnd.baseClasses;
using System.Net;

namespace HITSBackEnd.Swagger
{
    public class ErrorResponseModel
    {
        public HttpStatusCode status { get; set; }
        public string message { get; set; }
    }
}

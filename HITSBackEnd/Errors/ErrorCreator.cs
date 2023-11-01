using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace HITSBackEnd.Swagger
{
    public class ErrorCreator
    {
        public static string CreateError(string message)
        {
            var errorModel = new ErrorResponseModel
            {
                status = HttpStatusCode.BadRequest,
                message = message
            };

            var errorJson = JsonConvert.SerializeObject(errorModel);
            return errorJson;
        }
    }
}

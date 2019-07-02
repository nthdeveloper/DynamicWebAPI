using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using DynamicWebAPI.Operations;

namespace DynamicWebAPI.Controllers
{
    public class OperationsController : ApiController
    {
        List<IOperation> _supportedOperations;

        public OperationsController(IEnumerable<IOperation> supportedOperations)
        {
            _supportedOperations = new List<IOperation>(supportedOperations);
        }

        //Single action that gets the operation name and reads the operation paramters as JSON from request body
        [HttpPost]
        public async Task<IHttpActionResult> Process(string operationName)
        {
            //Find the operation
            IOperation operation = _supportedOperations.FirstOrDefault(x => x.Name == operationName);
            if (operation == null)
                return BadRequest($"'{operationName}' is not suported.");

            //Get the request body as string
            string jsonBody = await Request.Content.ReadAsStringAsync();

            object operationParams = null;            

            try
            {
                //Parse the JSON data in the request body t construct operation's parameter object
                if (operation.ParameterClassType != null)
                    operationParams = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonBody, operation.ParameterClassType);

                object result = operation.Execute(operationParams);

                //Return the result value as JSON to the caller
                if (result != null)
                    return Json(result);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            //Return Ok if the operation has no return value
            return Ok();
        }
    }
}

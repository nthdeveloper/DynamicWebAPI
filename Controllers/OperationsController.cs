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
                if (operation.ParameterClassType != null)
                    operationParams = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonBody, operation.ParameterClassType);

                object result = operation.Execute(operationParams);

                if (result != null)
                    return Json(result);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }
    }
}

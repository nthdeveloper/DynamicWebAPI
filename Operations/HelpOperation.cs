using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicWebAPI.Operations
{
    public class HelpOperation : OperationBase
    {
        private readonly IEnumerable<Lazy<IOperation>> allOperations;

        public HelpOperation(IEnumerable<Lazy<IOperation>> allOperations)
            : base("help", "Returns supported operations list.", null)
        {
            this.allOperations = allOperations;
        }

        public override object Execute(object prm)
        {
            var operationInfos = from op in allOperations select new { Name = op.Value.Name, Description = op.Value.Description };

            return operationInfos.ToArray();
        }        
    }
}
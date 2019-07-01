using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicWebAPI.Operations
{
    public class SumOperation : BaseOperation
    {
        public SumOperation()
            : base("sum", "Returns the sum of the given two numbers.", typeof(SumOperationParameters))
        {
        }

        public override object Execute(object prm)
        {
            if (prm == null)
                throw new ArgumentNullException();

            SumOperationParameters operationParameters = (SumOperationParameters)prm;

            int sum = operationParameters.Number1 + operationParameters.Number2;

            return new { Result=sum };
        }

        class SumOperationParameters
        {
            public int Number1 { get; set; }
            public int Number2 { get; set; }
        }
    }
}
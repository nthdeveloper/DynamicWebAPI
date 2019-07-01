using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicWebAPI.Operations
{
    public interface IOperation
    {
        string Name { get; }
        string Description { get; }
        Type ParameterClassType { get; }
        object Execute(object prm);
    }
}
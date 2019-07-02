using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicWebAPI.Operations
{
    public abstract class OperationBase : IOperation
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public Type ParameterClassType { get; private set; }

        public OperationBase(string name, string description, Type parameterClassType)
        {
            this.Name = name;
            this.Description = description;
            this.ParameterClassType = parameterClassType;
        }

        public abstract object Execute(object prm);
    }
}
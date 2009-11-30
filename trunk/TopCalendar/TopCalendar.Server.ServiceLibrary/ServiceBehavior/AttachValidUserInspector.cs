#region

using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace TopCalendar.Server.ServiceLibrary.ServiceBehavior
{
    public class AttachValidUserInspector : Attribute, IOperationBehavior
    {
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            ValidUserParameterInspector inspector = ServiceLocator.Current.GetInstance<ValidUserParameterInspector>();
            dispatchOperation.ParameterInspectors.Add(inspector);
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void AddBindingParameters(OperationDescription operationDescription,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(OperationDescription operationDescription)
        {
        }
    }
}
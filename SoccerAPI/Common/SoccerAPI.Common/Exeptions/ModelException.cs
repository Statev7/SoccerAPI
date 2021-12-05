namespace SoccerAPI.Common.Exeptions
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ModelException : Exception
    {
        public IEnumerable<ModelError> ErrorsMessage { get; }

        public ModelException(IEnumerable<ModelError> errorsMessage)
            :base(null)
        {
            this.ErrorsMessage = errorsMessage;
        }

        public ModelException(IEnumerable<ModelError> errorsMessage, Exception inner)
            :base(null, inner)
        {
            this.ErrorsMessage = errorsMessage;
        }
    }
}

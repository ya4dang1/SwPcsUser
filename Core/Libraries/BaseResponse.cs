using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Libraries
{
    public class BaseResponse
    { 

        public HashSet<string> Errors { get; private set; }
        public BaseResponse()
        {
            Errors = new HashSet<string>();
        }

        public bool IsError
        {
            get { return Errors.Count > 0; }
        }

        public void ClearError()
        {
            Errors.Clear();
        }

        public void AddError(Exception exception)
        {
            Errors.Add(exception.Message);
        }

        public void AddError(String message)
        {
            Errors.Add(message);
        }
    }
}

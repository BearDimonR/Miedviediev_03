using System;

namespace Miedviediev_03.Exceptions
{
    public class AgeException : ApplicationException
    {
        protected AgeException(string message): 
                base(message)
            {}
    }
}
using System;

namespace Miedviediev_03.Exceptions
{
    public class NotBornPersonException : AgeException
    {
        public NotBornPersonException(DateTime date) :
            base($"Person have not born yet: {date} \n Now: {DateTime.Today}")
        {}
    }
}
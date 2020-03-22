﻿using System;

namespace Miedviediev_03.Exceptions
{
    public class OldPersonException: AgeException
    {
        public OldPersonException(DateTime date): 
            base($"Person is too old(max 135 years): {date} \n Now: {DateTime.Today}")
        {}
    }
}
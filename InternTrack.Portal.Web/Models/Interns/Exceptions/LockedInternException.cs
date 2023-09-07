﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace InternTrack.Portal.Web.Models.Interns.Exceptions
{
    public class LockedInternException : Xeption
    {
        public LockedInternException(Exception innerException)
            : base(message: "Locked Intern error occurred, please try again later.",
                  innerException)
        { }

        public LockedInternException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

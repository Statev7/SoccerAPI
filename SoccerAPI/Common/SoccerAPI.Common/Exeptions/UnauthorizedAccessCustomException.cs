namespace SoccerAPI.Common.Exeptions
{
    using System;

    public class UnauthorizedAccessCustomException : Exception
    {
        public UnauthorizedAccessCustomException(string message)
            : base(message)
        {

        }
    }
}

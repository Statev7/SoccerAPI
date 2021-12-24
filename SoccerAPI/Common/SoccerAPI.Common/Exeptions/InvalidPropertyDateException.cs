namespace SoccerAPI.Common.Exeptions
{
    using System;

    public class InvalidPropertyDateException : Exception
    {
        public InvalidPropertyDateException(string message)
            :base(message)
        {

        }
    }
}

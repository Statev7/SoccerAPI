namespace SoccerAPI.Common.Exeptions
{
    using System;

    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException(string message)
            :base(message)
        {

        }

        public EntityDoesNotExistException(string message, Exception inner)
            :base(message, inner)
        {

        }
    }
}

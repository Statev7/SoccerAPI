namespace SoccerAPI.Common.Exeptions
{
    using System;

    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException(string message)
            :base(message)
        {

        }
    }
}

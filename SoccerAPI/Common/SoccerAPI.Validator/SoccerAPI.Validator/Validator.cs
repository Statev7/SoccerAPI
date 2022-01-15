namespace SoccerAPI.Validator
{
    using System;

    public static class Validator
    {
        public static bool IsNullOrDefault<T>(T argument)
        {
            // deal with normal scenarios
            if (argument == null)
            {
                return true;
            }
            if (object.Equals(argument, default(T)))
            {
                return true;
            }

            // deal with non-null nullables
            Type methodType = typeof(T);
            if (Nullable.GetUnderlyingType(methodType) != null)
            {
                return false;
            }

            // deal with boxed value types
            Type argumentType = argument.GetType();
            if (argumentType.IsValueType && argumentType != methodType)
            {
                object obj = Activator.CreateInstance(argument.GetType());
                return obj.Equals(argument);
            }

            return false;
        }

        public static bool IsDateValid(DateTime dateTime)
        {
            if (DateTime.Compare(dateTime, DateTime.UtcNow) > 0)
            {
                return false;
            }

            return true;
        }

        public static int CalculateAge(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }
    }
}

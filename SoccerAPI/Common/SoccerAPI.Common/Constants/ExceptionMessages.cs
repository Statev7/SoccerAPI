﻿namespace SoccerAPI.Common.Constants
{
    public static class ExceptionMessages
    {
        public const string SOMETHING_WENT_WRONG_MESSAGE = "Something went wrong!";

        public const string FOOTBALLER_NOT_EXIST_ERROR_MESSAGE = "Such a footballer does not exist!";

        public const string FOOTBALLER_IS_ALREADY_IN_THE_TEAM_ERROR_MESSAGE = "{0} is already in the team!";

        public const string A_FOOTBALLER_CANNOT_HAVE_MORE_TEAMS_ERROR_MESSAGE = "The {0} has reached the maximum number of teams!";

        public const string TEAM_PLAYERS_COUNT_ERROR_MESSAGE = "The team cannot have more than {0} players!";

        public const string TEAM_NOT_EXIST_ERROR_MESSAGE = "Such a team does not exist!";

        public const string TEAM_IS_ALREADY_IN_THIS_CHAMPIONSHIP_ERROR_MESSAGE = "The {0} is already in {1}!";

        public const string CANNOT_ADD_NEW_TEAM_ERROR_MESSAGE = "The {0} has reached the maximum number of teams!";

        public const string COACH_NOT_EXIST_ERROR_MESSAGE = "Such a coach does not exist!";

        public const string THE_COACH_IS_ALREADY_IN_THE_TEAM = "{0} is already a coach!";

        public const string THE_COACH_IS_ALREADY_THE_COACH_OF_ANOTHER_TEAM = "{0} is the coach of another team";

        public const string CANNOT_ADD_NEW_COACH_TO_TEAM_ERROR_MESSAGE = "The {0} has reached the maximum number of coaches!";

        public const string TEAM_FOOTBOOLER_MAPPING_DOES_NOT_EXIST_ERROR_MESSAGE = "There is no relation between this team and footballer!";
    }
}

namespace DefaultNamespace
{
    public static class Constnts
    {
        public static class Stats
        {
            public const string HealthPoints = "HealthPoints";
            public const string ActionPoints = "ActionPoints";
            public const string MaxActionPoints = "MaxActionPoints";
            public const string Damage = "Damage";
            public const string Distance = "Distance";
        }
        
        public static class Ability
        {
            public const string Move = "Move";
            public const string Attack = "Attack";
        }
        
        public static class Events
        {
            public static class Game
            {
                public const string StartGame = "StartGame";
                
                public const string SelectMember = "SelectMember";
                public const string DeselectMember = "DeselectMember";
                public const string SelectLocation = "SelectLocation";
                public const string DeselectLocation = "DeselectLocation";
                
                public const string BoxClick = "BoxClick";
                public const string MemberClick = "MemberClick";
                public const string ChangeGameState = "ChangeGameState";
                
                public const string NextTurn = "NextTurn";
                public const string HUDMemberClick = "HUDMemberClick";
                public const string MemberUpdate = "MemberUpdate";

                public const string ActionClick = "ActionClick";

            }
            
            public static class GameField
            {
                public const string NewLevelCreated = "NewLevelCreated";
                public const string CreateTeams = "CreateTeams";
                public const string UpdateBoxes = "UpdateBoxes";
                public const string ResetState = "ResetState";
            }
        }
    }
}
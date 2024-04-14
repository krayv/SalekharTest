using System;

namespace Scorewarrior.Test
{
    public class EventBus
    {
        public Action StartBattle;
        public Action<uint> NoAliveCharactersInTeam;
        public Action EndBattle;
        public Action RestartBattle;
    } 
}

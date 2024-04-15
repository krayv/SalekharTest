using Scorewarrior.Test.Models;
using System;
using System.Collections.Generic;

namespace Scorewarrior.Test
{
    public class EventBus
    {
        public Action StartBattle;
        public Action<uint> NoAliveCharactersInTeam;
        public Action EndBattle;
        public Action RestartBattle;
        public Action<List<Character>> SpawnCharacters;
    } 
}

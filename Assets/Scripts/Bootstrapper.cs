using Scorewarrior.Test.Models;
using Scorewarrior.Test.Views;
using System.Collections.Generic;
using UnityEngine;

namespace Scorewarrior.Test
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField]
        private CharacterPrefab[] _characters;
        [SerializeField]
        private SpawnPoint[] _spawns;
        [SerializeField]
        private UIController _uiController;

        private Battlefield _battlefield;

        private readonly EventBus _eventBus = new EventBus();

        private GameState _gameState;

        public void Start()
        {
            _eventBus.StartBattle += () => InitAndStartBattle();
            _eventBus.RestartBattle += () => StartBattle();
            _uiController.Init(_eventBus);
        }

        public void InitAndStartBattle()
        {
            Dictionary<uint, List<Vector3>> spawnPositionsByTeam = new Dictionary<uint, List<Vector3>>();
            foreach (SpawnPoint spawn in _spawns)
            {
                uint team = spawn.Team;
                if (spawnPositionsByTeam.TryGetValue(team, out List<Vector3> spawnPoints))
                {
                    spawnPoints.Add(spawn.transform.position);
                }
                else
                {
                    spawnPositionsByTeam.Add(team, new List<Vector3> { spawn.transform.position });
                }
                Destroy(spawn.gameObject);
            }
            _battlefield = new Battlefield(spawnPositionsByTeam, _eventBus);
            StartBattle();
        }

        private void StartBattle()
        {
            _battlefield.Start(_characters);
            _gameState = GameState.Running;
        }

        public void Update()
        {
            switch (_gameState)
            {
                case GameState.None:
                    break;
                case GameState.Running:
                    _battlefield.Update(Time.deltaTime);
                    break;
            }
        }
    }
}

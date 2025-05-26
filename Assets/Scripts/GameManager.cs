using System;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; } = false;
        public static GameManager Instance { get; private set; } = null;
        public static Action OnInitialized = null;

        private float _gameTimeDelay = 0f;
        private float _gameTime = 0f;
        public float GameTime
        {
            get => _gameTime;
            set
            {
                _gameTime = value;
                OnGameTimeUpdated?.Invoke(_gameTime);
            }
        }
        public event Action<float> OnGameTimeUpdated = null;

        private TimeManager _timeManager = null;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _timeManager = ManagersController.Instance.GetManager<TimeManager>();

            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        private void Start()
        {
            _gameTimeDelay = 0f;
            _gameTime = 0f;
        }

        private void Update()
        {
            CountGameTime();
        }

        private void CountGameTime()
        {
            _gameTimeDelay += Time.deltaTime;
            if (_gameTimeDelay >= 1f)
            {
                _gameTimeDelay--;
                GameTime++;
            }
        }
    }
}
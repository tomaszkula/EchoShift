using System;
using System.Collections;
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

        private BaseManager[] managers = new BaseManager[0];

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            managers = GetComponentsInChildren<BaseManager>();

            IsInitialized = true;
            OnInitialized?.Invoke();
        }

        private IEnumerator Start()
        {
            _gameTimeDelay = 0f;
            _gameTime = 0f;

            for (int i = 0; i < managers.Length; i++)
            {
                managers[i].Initialize();
            }

            bool allManagersReady = true;
            do
            {
                allManagersReady = AreAllManagersInitialized();

                if (!allManagersReady)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            } while (!allManagersReady);
        }

        private void Update()
        {
            CountGameTime();
        }

        public bool AreAllManagersInitialized()
        {
            foreach (var manager in managers)
            {
                if (!manager.isInitialized)
                {
                    return false;
                }
            }
            return true;
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

        public T GetManager<T>() where T : BaseManager
        {
            foreach (var manager in managers)
            {
                if (manager is T)
                {
                    return manager as T;
                }
            }
            return null;
        }
    }
}
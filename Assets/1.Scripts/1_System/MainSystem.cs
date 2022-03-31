namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Managers.SceneManager;
    using Managers.GameManager;
    public partial class MainSystem : GenericSingleton<MainSystem> //Data
    {
        public SceneManager SceneManager { get; private set; } = default;
        public GameManager GameManager { get; private set; } = default;
    }
    public partial class MainSystem : GenericSingleton<MainSystem> //Main
    {
        private void Allocate()
        {
            SceneManager = gameObject.AddComponent<SceneManager>();
            GameManager = gameObject.AddComponent<GameManager>();
        }
        private void Initialize()
        {
            Allocate();
            SceneManager.Initialize();
            GameManager.Initialize();
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
    public partial class MainSystem : GenericSingleton<MainSystem> //Prop
    {
        public void MainSystemStart(System.Action receiveFuntion)
        {
            Initialize();
            receiveFuntion?.Invoke();
        }
    }
}
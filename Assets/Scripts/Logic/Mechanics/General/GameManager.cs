using System;
using Game.GridSystem;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private MapGridData[] levelList;
        [SerializeField] private Grid gridManager;
        [SerializeField] private UI.InGameMenu managerUI;
        private static int lastCompletedLevel = 0;
        private static int currentLevel = 0;
        private static GameManager instance;

        public static Action<bool> LevelEndEvent;

        #endregion

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        /// <summary>
        /// The method that freezes game time and causes the opening of the ESC menu
        /// </summary>
        public static void PauseGameButton()
        {
            if (instance.managerUI.OnClickInGameMenuButton()) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

        /// <summary>
        /// The method restarts the same level.
        /// </summary>
        public static void RestartLevel()
        {
            UploadLevel(currentLevel);
        }

        /// <summary>
        /// The method loads the level.
        /// </summary>
        /// <param name="number">The ordinal number of the level.</param>
        public static void UploadLevel(int number)
        {
            instance.gridManager.UploadLevel(instance.levelList[number]);
            Time.timeScale = 1;
        }

        /// <summary>
        /// Called if the level has been completed.
        /// </summary>
        /// <param name="isWin">The result of the level. False - loss. True - winning.</param>
        public static void LevelEnd(bool isWin)
        {
            Time.timeScale = 0;
            LevelEndEvent?.Invoke(isWin);
        }
    }
}
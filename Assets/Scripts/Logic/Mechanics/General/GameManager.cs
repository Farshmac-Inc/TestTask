using System;
using System.Collections;
using System.Collections.Generic;
using Game.GridSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Grid = Game.GridSystem.Grid;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private MapGridData[] levelList;
        [SerializeField] private Grid gridManager;
        private static int lastСompletedLevel = 0;
        private static int currentLevel;
        private static GameManager instance;
        private static bool isOpenMainMenu;

        #endregion

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        /// <summary>
        /// The method restarts the same level.
        /// </summary>
        public static void RestartLevel()
        {
            ;
            UploadLevel(currentLevel);
        }

        /// <summary>
        /// The method loads the level.
        /// </summary>
        /// <param name="number">The ordinal number of the level.</param>
        public static void UploadLevel(int number)
        {
            if (isOpenMainMenu) SceneManager.LoadScene("Level");
            instance.gridManager.UploadLevel(instance.levelList[number]);
        }

        /// <summary>
        /// Called if the level has been completed.
        /// </summary>
        /// <param name="result">The result of the level. False - loss. True - winning.</param>
        public static void LevelEnd(bool result)
        {
        }

        /// <summary>
        /// The method loads the main menu scene.
        /// </summary>
        public static void UploadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
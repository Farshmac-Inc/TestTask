using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class InGameMenu : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private Canvas canvas;
        [SerializeField] private Slider soundVolume;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Button restartButton;

        private static bool isMenuOpen;
        private static InGameMenu instance;

        #endregion

        private void Start()
        {
            instance = this;
            soundVolume.onValueChanged.AddListener(ChangeSoundVolume);
            musicVolume.onValueChanged.AddListener(ChangeMusicVolume);
            restartButton.onClick.AddListener(OnClickButtonRestart);
            
        }

        private void OnDestroy()
        {
            soundVolume.onValueChanged.RemoveListener(ChangeSoundVolume);
            musicVolume.onValueChanged.RemoveListener(ChangeMusicVolume);
            restartButton.onClick.RemoveListener(OnClickButtonRestart);
        }

        private void ChangeSoundVolume(float value)
        {
            
        }

        private void ChangeMusicVolume(float value)
        {
            
        }

        private void OnClickButtonRestart()
        {
            GameManager.RestartLevel();
        }

        /// <summary>
        /// The method that enables or disables the menu.
        /// </summary>
        /// <returns>Whether the menu is currently open or not.</returns>
        public bool OnClickInGameMenuButton()
        {
            if(isMenuOpen) instance.MenuClose();
            else instance.MenuOpen();
            isMenuOpen = !isMenuOpen;
            return isMenuOpen;
        }

        private void MenuOpen()
        {
            canvas.gameObject.SetActive(true);
        }

        private void MenuClose()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
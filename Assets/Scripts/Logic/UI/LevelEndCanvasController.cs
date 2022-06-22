using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    public class LevelEndCanvasController : MonoBehaviour
    {
        #region MyRegion

        [SerializeField] private Canvas levelEndCanvas;
        [SerializeField] private Text resultText;
        [SerializeField] private Text hintText;
        [SerializeField] private Button restartButton;
        #endregion
        

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            restartButton.onClick.AddListener(OnClickRestartButton);
            GameManager.LevelEndEvent += DisplayLevelEndCanvas;
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(OnClickRestartButton);
            GameManager.LevelEndEvent -= DisplayLevelEndCanvas;
        }

        private void OnClickRestartButton()
        {
            GameManager.RestartLevel();
            levelEndCanvas.gameObject.SetActive(false);
        }

        private void DisplayLevelEndCanvas(bool isWin)
        {
            Cursor.lockState = CursorLockMode.None;
            if (isWin)
            {
                resultText.text = "You won!";
                hintText.text = "Do you want to try to improve your result?";
            }
            else
            {
                resultText.text = "You lose!";
                hintText.text = "Try again and everything will work out! \n Do you want to try again?";
            }            
            levelEndCanvas.gameObject.SetActive(true);
        }
    }
}
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

        private UnityAction OnClickButton;

        #endregion
        

        private void Start()
        {
            restartButton.onClick.AddListener(OnClickButton);
            GameManager.LevelEndEvent += DisplayLevelEndCanvas;
            OnClickButton += () =>
            {
                GameManager.RestartLevel();
                levelEndCanvas.gameObject.SetActive(false);
            };
        }

        private void DisplayLevelEndCanvas(bool isWin)
        {
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
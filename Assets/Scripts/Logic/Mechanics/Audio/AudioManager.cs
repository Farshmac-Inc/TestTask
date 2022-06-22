using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        #region Fields

        private protected AudioSource source;

        #endregion
        

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private protected AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }
    }
}
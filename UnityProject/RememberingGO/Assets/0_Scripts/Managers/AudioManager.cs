using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace RememberingManagers
{

    public class AudioManager : MonoBehaviour
    {

        public void SetSnapShot(AudioMixerSnapshot snapShot, float transitionTime)
        {

            snapShot.TransitionTo(transitionTime);

        }
    }
}

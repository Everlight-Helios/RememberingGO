using UnityEngine;
using UnityEngine.Audio;
using RememberingManagers;

public class HospitalStartAudioSnapshot : MonoBehaviour {

    [SerializeField]
    private AudioMixerSnapshot snapShot;

    [SerializeField]
    private AudioManager manager;

    void Awake ()
    {

        manager.SetSnapShot(snapShot, 0);

	}

}

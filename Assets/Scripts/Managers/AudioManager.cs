using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource scareSound;

    public void PlayScareSound()
    {
        AudioSource aS = Instantiate(scareSound, gameObject.transform);
        aS.Play();
        Destroy(aS.gameObject, scareSound.clip.length);
    }
}

using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Settings")] 
    [Tooltip("Set the volume of the music."), Range(0, 1)]
    public float volumeMusic = 1.0f;
    [Tooltip("Set the volume of the SFX."), Range(0,1)]
    public float volumeSFX = 1.0f;
    
    [SerializeField] private AudioSource scareSound;

    public void PlayScareSound()
    {
        AudioSource aS = Instantiate(scareSound, gameObject.transform);
        aS.Play();
        Destroy(aS.gameObject, scareSound.clip.length);
    }
}
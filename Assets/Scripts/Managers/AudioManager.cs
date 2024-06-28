using System;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Settings")] 
    [Tooltip("Set the volume of the music."), Range(0, 1)]
    public float volumeMusic = 1.0f;
    [Tooltip("Set the volume of the SFX."), Range(0,1)]
    public float volumeSFX = 1.0f;
    
    [SerializeField] private AudioSource scareSound;

    private VCA VcaMusicController;
    private VCA VcaSFXController;
    [SerializeField, BankRef] private string nameMusic = "Music";
    [SerializeField, BankRef] private string nameSFX = "SFX";

    private void Start()
    {
        // VcaMusicController = RuntimeManager.GetVCA($"vca:/{nameMusic}");
        // VcaSFXController = RuntimeManager.GetVCA($"vca:/{nameSFX}");
    }

    public void PlayScareSound()
    {
        AudioSource aS = Instantiate(scareSound, gameObject.transform);
        aS.Play();
        Destroy(aS.gameObject, scareSound.clip.length);
    }
}
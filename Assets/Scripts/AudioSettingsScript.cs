using Unity.VRTemplate;
using UnityEngine;

[RequireComponent(typeof(XRKnob))]
public class AudioSettingsScript : MonoBehaviour
{
    [SerializeField, Tooltip("Set to true if the music volume should be changed instead of the SFX volume.")]
    private bool changeMusicVolume = false;

    private XRKnob _xrKnob;
    
    private void Start()
    {
        _xrKnob = GetComponent<XRKnob>();
        if (changeMusicVolume)
            _xrKnob.value = AudioManager.Instance.volumeMusic;
        else
            _xrKnob.value = AudioManager.Instance.volumeSFX;
    }

    private void Update()
    {
        if (changeMusicVolume)
            AudioManager.Instance.volumeMusic = _xrKnob.value;
        else
            AudioManager.Instance.volumeSFX = _xrKnob.value;
    }
}
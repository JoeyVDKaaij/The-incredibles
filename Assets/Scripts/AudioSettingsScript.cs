using Unity.VRTemplate;
using UnityEngine;

[RequireComponent(typeof(XRKnob))]
public class AudioSettingsScript : MonoBehaviour
{
    [SerializeField, Tooltip("Set to true if the music volume should be changed instead of the SFX volume.")]
    private bool changeMusicVolume = false;

    private XRKnob _xrKnob;
    private float _oldValue;
    
    private void Start()
    {
        _xrKnob = GetComponent<XRKnob>();
        if (changeMusicVolume)
            _xrKnob.value = AudioManager.Instance.volumeMusic;
        else
            _xrKnob.value = AudioManager.Instance.volumeSFX;

        _oldValue = _xrKnob.value;
    }

    private void Update()
    {
        if (_xrKnob.value != _oldValue)
            AudioManager.Instance.SetVolume(_xrKnob.value, changeMusicVolume);
    }
}
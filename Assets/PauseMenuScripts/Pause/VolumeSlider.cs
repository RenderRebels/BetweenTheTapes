using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private const string VolumeKey = "GameVolume";
    void Start()
    {
        Slider slider = GetComponent<Slider>();
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        slider.value = savedVolume;
        SetVolume(savedVolume);

        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; 
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;

    private const string VOL_MUSIC = "VolMusic";
    private const string VOL_EFFECTS = "VolEffects";

    void Start()
    {
        pausePanel.gameObject.SetActive(false);
        pausePanel.alpha = 0;

        Cursor.lockState = CursorLockMode.Locked;


        float musicVol = PlayerPrefs.GetFloat(VOL_MUSIC, 1);
        float effectsVol = PlayerPrefs.GetFloat(VOL_EFFECTS, 1);

        musicSlider.value = musicVol;
        effectSlider.value = effectsVol;

        SetMusicVolume(musicVol);
        SetEffectsVolume(effectsVol);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.gameObject.activeSelf)
            {
                Time.timeScale = 1;

                pausePanel.DOFade(0, 0.5f).SetUpdate(true);
                pausePanel.gameObject.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;

                pausePanel.gameObject.SetActive(true);
                pausePanel.DOFade(1, 0.5f).SetUpdate(true);

                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void SetMusicVolume(float newVolume)
    {
        float currentVolume = Mathf.Log10(newVolume) * 20;

        audioMixer.SetFloat(VOL_MUSIC, currentVolume);
        PlayerPrefs.SetFloat(VOL_MUSIC, newVolume);
    }

    public void SetEffectsVolume(float newVolume)
    {
        float currentVolume = Mathf.Log10(newVolume) * 20;

        audioMixer.SetFloat(VOL_EFFECTS, currentVolume);
        PlayerPrefs.SetFloat(VOL_EFFECTS, newVolume);
    }
}

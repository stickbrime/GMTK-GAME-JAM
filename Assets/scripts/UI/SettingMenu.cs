using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public static SettingMenu Instance;

    public Slider soundSlider;
    public Slider musicSlider;

    public Button exitButton;
    public Button continueButton;
    public GameObject settingsPanel;
    public GameObject backgroundMask;

    public AudioSource soundSource;
    public AudioSource musicSource;

    public AudioMixer gameAudioMixer;

    private void Awake()
    {
        //����ֱ��ѡ�ű�Ȼ��ѡshowsetting��������
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

            
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
        backgroundMask.SetActive(false);

        musicSlider.onValueChanged.AddListener(AdjustMusicVolume);
        soundSlider.onValueChanged.AddListener(AdjustSoundVolume);
        exitButton.onClick.AddListener(ExitGame);
        continueButton.onClick.AddListener(HideSettings);

        float savedMusicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSoundVol = PlayerPrefs.GetFloat("SoundVolume", 1f);

        musicSlider.value = savedMusicVol;
        soundSlider.value = savedSoundVol;

        AdjustMusicVolume(savedMusicVol);
        AdjustSoundVolume(savedSoundVol);
    }

    void AdjustMusicVolume(float value)
    {
        float volume = Mathf.Lerp(-80f, 0f, value);
        gameAudioMixer.SetFloat("MusicVolume", volume);

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    void AdjustSoundVolume(float value)
    {
        soundSource.volume = value;
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        backgroundMask.SetActive(true);
        Time.timeScale = 0;
    }
    void HideSettings()
    {
        settingsPanel.SetActive(false);
        backgroundMask.SetActive(false);
        Time.timeScale = 1;
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
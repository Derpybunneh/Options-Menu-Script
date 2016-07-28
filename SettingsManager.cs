using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour {
    public Toggle fullscreenToggle;

    public Dropdown resDD;
    public Dropdown textureQualityDD;
    public Dropdown aaDD;
    public Dropdown vSyncDD;

    public Slider musicVolumeSlider;

    public Resolution[] resolutions;
    public GameSettings gameSettings;

    public GameControl gameController;

    void Start() {
        gameController.musicVolume = musicVolumeSlider.value;

        resDD.options.Clear();

        resolutions = Screen.resolutions;

        resDD.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resDD.value].width, resolutions[resDD.value].height, gameSettings.fullscreen); });

        for (int i = 0; i < resolutions.Length; i++) {
            resDD.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
        }
    }

    string ResToString(Resolution res) {
        return res.width + "x" + res.height;
    }

    void OnEnable() {
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        textureQualityDD.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        aaDD.onValueChanged.AddListener(delegate { OnAAChange(); });
        vSyncDD.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVol(); });
    }

    public void OnFullscreenToggle() {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange() {
        Screen.SetResolution(resolutions[resDD.value].width, resolutions[resDD.value].height, gameSettings.fullscreen);
    }

    public void OnTextureQualityChange() {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = textureQualityDD.value;
    }

    public void OnAAChange() {
        if (aaDD.value == 0) {
            QualitySettings.antiAliasing = 0;
        }
        else if (aaDD.value == 1) {
            QualitySettings.antiAliasing = 2;
        }
        else if (aaDD.value == 2) {
            QualitySettings.antiAliasing = 4;
        }
        else if (aaDD.value == 3) {
            QualitySettings.antiAliasing = 8;
        }
    }

    public void OnVSyncChange() {
        QualitySettings.vSyncCount = vSyncDD.value;
        print(QualitySettings.vSyncCount);
    }

    public void OnMusicVol() {
        gameController.musicVolume = musicVolumeSlider.value;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    /// <summary>
    /// フルスクリーンかどうか
    /// </summary>
    private bool isFullScreen;
    /// <summary>
    /// フルスクリーンかどうかのUI要素
    /// </summary>
    [SerializeField]
    private Toggle fullScreenToggle = default;
    /// <summary>
    /// 解像度選択
    /// </summary>
    [SerializeField]
    private Dropdown resolutionDropdown = default;
    /// <summary>
    /// 解像度
    /// </summary>
    private string resolution;


    /// <summary>
    /// 音量調整用スライダー
    /// </summary>
    [SerializeField]
    private Slider volumeSlider = default;
    /// <summary>
    /// 音量
    /// </summary>
    private float volume;

    /// <summary>
    /// タイトル類をまとめているオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject title = default;

    /// <summary>
    /// 現在の設定をUIに反映させる
    /// </summary>
    void Start()
    {
        isFullScreen = Screen.fullScreen;
        fullScreenToggle.isOn = isFullScreen;
        if (!isFullScreen) {
            var currentResolution = $"{Screen.width}x{Screen.height}";
            resolutionDropdown.value = resolutionDropdown.options
                .IndexOf(resolutionDropdown.options.Find(r => r.text == currentResolution));
        }

        volumeSlider.value = AudioListener.volume;
    }

    /// <summary>
    /// フルスクリーンを切り替える
    /// </summary>
    public void ToggleFullScreen()
    {
        isFullScreen = fullScreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            resolutionDropdown.interactable = false;
        }
        else
        {
            resolutionDropdown.interactable = true;
            Screen.SetResolution(1280, 720, false);
        }
    }

    /// <summary>
    /// フルスクリーンじゃないときの解像度を切り替える
    /// </summary>
    public void ChangeResolution()
    {
        resolution = resolutionDropdown.options[resolutionDropdown.value].text;
        int x = int.Parse(resolution.Split('x')[0]);
        int y = int.Parse(resolution.Split('x')[1]);
        Screen.SetResolution(x, y, false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void CloseOption()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        //PlayerPrefs.SetString("isFullScreen", isFullScreen.ToString());
        //PlayerPrefs.SetString("resolution", resolution);
        PlayerPrefs.SetString("screen", isFullScreen ? isFullScreen.ToString() : resolution);

        this.gameObject.SetActive(false);
        title.SetActive(true);
    }
}

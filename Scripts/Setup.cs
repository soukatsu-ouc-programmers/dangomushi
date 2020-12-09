using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム起動時に保存されてる設定とかを反映させる
/// </summary>
public class Setup : MonoBehaviour
{
    void Awake()
    {
        // スクリーン設定
        if (PlayerPrefs.HasKey("screen"))
        {
            string screenSetting = PlayerPrefs.GetString("screen");
            if (screenSetting == "True")
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
                int x = int.Parse(screenSetting.Split('x')[0]);
                int y = int.Parse(screenSetting.Split('x')[1]);
                Screen.SetResolution(x, y, false);
            }
        }
        else
        {
            // 何も設定されてなかったとき（デフォルトの設定）
            Screen.fullScreen = false;
            Screen.SetResolution(1280, 720, false);
        }
        // 音量設定
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        }
        else
        {
            // デフォルト設定
            AudioListener.volume = 1;
        }
    }
}

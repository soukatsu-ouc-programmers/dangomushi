using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 操作説明
/// </summary>
public class HowToPlay : MonoBehaviour
{
    /// <summary>
    /// 再生先
    /// </summary>
    [SerializeField]
    private VideoPlayer videoPlayer = default;

    /// <summary>
    /// 再生するビデオクリップ
    /// </summary>
    [SerializeField]
    private VideoClip[] videoClips = default;

    /// <summary>
    /// 再生するビデオの番号
    /// </summary>
    private int clipNumber = 0;

    /// <summary>
    /// 説明
    /// </summary>
    [Multiline(3)]
    [SerializeField]
    private string[] description = default;
    /// <summary>
    /// 説明を表示する要素
    /// </summary>
    [SerializeField]
    private Text descriptionText = default;

    /// <summary>
    /// タイトルのオブジェクト色々
    /// </summary>
    [SerializeField]
    private GameObject title = default;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.clip = videoClips[clipNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextVideo()
    {
        clipNumber++;
        // 最後のビデオなら終了
        if(clipNumber == videoClips.Length)
        {
            Close();
        }
        videoPlayer.clip = videoClips[clipNumber];
        descriptionText.text = description[clipNumber];
        //videoPlayer.Play();
    }

    public void PreviousVideo()
    {
        if(clipNumber == 0)
        {
            return;
        }
        clipNumber--;
        videoPlayer.clip = videoClips[clipNumber];
        descriptionText.text = description[clipNumber];
        //videoPlayer.Play();
    }

    public void Close()
    {
        title.SetActive(true);
        clipNumber = 0;
        videoPlayer.clip = videoClips[clipNumber];
        this.gameObject.SetActive(false);
    }
}

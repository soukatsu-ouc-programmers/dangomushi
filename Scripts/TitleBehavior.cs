using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面での挙動
/// </summary>
public class TitleBehavior : MonoBehaviour
{
    private bool isGameStart;
    /// <summary>
    /// フェードに使用するオブジェクト
    /// </summary>
    [SerializeField]
    private Image fadeObject = default;

    private float alpha;
    private float tempTime;

    /// <summary>
    /// HowToPlayとかのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject howToPlay = default;

    /// <summary>
    /// オプションとかのオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject option = default;

    // Update is called once per frame
    void Update()
    {
        if (!isGameStart)
        {
            return;
        }
        else
        {
            //tempTime += Time.deltaTime;
            if (alpha >= 1)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                alpha += 1 * Time.deltaTime;
                fadeObject.color = new Color(0, 0, 0, alpha);
            }
        }
    }

    /// <summary>
    /// GAME STARTボタンを押したとき
    /// </summary>
    public void PushGameStart()
    {
        fadeObject.gameObject.SetActive(true);
        //SceneManager.LoadScene(1);
        isGameStart = true;
    }

    /// <summary>
    /// オプションボタン押したとき
    /// </summary>
    public void PushOption()
    {
        option.SetActive(true);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// how to play ボタンを押したとき
    /// </summary>
    public void PushHowToPlay()
    {
        howToPlay.SetActive(true);
        this.gameObject.SetActive(false);
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1);
    }
}

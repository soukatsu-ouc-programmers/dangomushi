using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    /// <summary>
    /// 1ゲームの制限時間
    /// </summary>
    [SerializeField]
    private int limitTime = 180;
    /// <summary>
    /// 現在の残り時間
    /// </summary>
    private float currentTime;
    /// <summary>
    /// 時間を表示させるテキスト
    /// </summary>
    [SerializeField]
    private Text timeText = default;

    /// <summary>
    /// スコア(沈めただんごむしの数)
    /// </summary>
    private int currentScore = 0;
    /// <summary>
    /// スコアを表示させるテキスト
    /// </summary>
    [SerializeField]
    private Text scoreText = default;

    /// <summary>
    /// 終了時に表示するテキストオブジェクト
    /// </summary>
    [SerializeField]
    private GameObject endTextObject = default;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = limitTime;
        showTime(currentTime);
        //StartCoroutine("countDown");
    }

    // Update is called once per frame
    void Update()
    {
        countDown();
    }

    //IEnumerator countDown()
    //{
    //    for (int i = 0; i < limitTime*100; i++)
    //    {
    //        yield return new WaitForSecondsRealtime(0.01f);
    //        currentTime -= 0.01f;
    //        showTime(currentTime);
    //    }
    //    showTime(0);
    //    yield return new WaitForSeconds(1);

    //    StartCoroutine("GameEnd");
    //}

    private void countDown()
    {
        currentTime -= 1 * Time.deltaTime;
        showTime(currentTime);
        if(currentTime <= 0)
        {
            showTime(0);
            StartCoroutine("GameEnd");
        }
    }

    /// <summary>
    /// 残り時間をUIに出す
    /// </summary>
    /// <param name="time">秒数</param>
    private void showTime(float time)
    {
        int minutes = 0;
        int seconds = (int) time;
        float milliseconds = time - seconds;

        if (time >= 60)
        {
            minutes = seconds / 60;
            seconds = seconds - minutes * 60;
            milliseconds = time - seconds - minutes * 60;
        }
        else
        {
            minutes = 0;
            seconds = (int) time;
            milliseconds = time - seconds;
        }

        timeText.text = minutes.ToString("D1") + "." + seconds.ToString("D2") + "." + ((int) (milliseconds * 1000)).ToString("D3");
    }

    public void ScoreUp()
    {
        currentScore += 1;
        scoreText.text = currentScore.ToString();
    }

    private void GameStart()
    {

    }

    IEnumerator GameEnd()
    {
        // 敵と自分の動きを止める
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().enabled = false;
        }
        UserPlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<UserPlayerController>();
        playerController.enabled = false;

        // 終了テキスト出す
        endTextObject.SetActive(true);
        yield return new WaitForSeconds(3);

        // スコアを記録
        ScoreData.Score = currentScore;

        // リザルトシーンに移動
        SceneManager.LoadScene(2);
    }

}

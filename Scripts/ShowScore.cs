using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルトでスコア表示させる
/// </summary>
public class ShowScore : MonoBehaviour
{
    /// <summary>
    /// スコアを表示するテキスト
    /// </summary>
    private Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = this.GetComponent<Text>();
        scoreText.text = ScoreData.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

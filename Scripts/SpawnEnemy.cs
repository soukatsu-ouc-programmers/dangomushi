using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    /// <summary>
    /// だんごむしのプレハブ
    /// </summary>
    [SerializeField]
    private GameObject dangoMushi = default;

    private GameMaster gameMaster;

    /// <summary>
    /// 次の敵がでてくるまでのインターバル
    /// </summary>
    [SerializeField]
    private float interval = 10;

    /// <summary>
    /// 前の敵が出てきてからの経過時間
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// スポーン位置の候補
    /// </summary>
    [SerializeField]
    private Transform[] spawnPosition = default;

    /// <summary>
    /// 同時に存在する敵の数
    /// </summary>
    [SerializeField]
    private int spawnLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(elapsedTime >= interval)
        {
            int currentDangomushi = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if(currentDangomushi == spawnLimit)
            {
                return;
            }
            GameObject tempEnemy = Instantiate(dangoMushi, spawnPosition[Random.Range(0, 4)].position, Quaternion.identity);
            elapsedTime = 0;
        }
        else
        {
            elapsedTime += 1 * Time.deltaTime;
        }
    }
}

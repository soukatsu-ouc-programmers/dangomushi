using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    /// <summary>
    /// 回復アイテム
    /// </summary>
    [SerializeField]
    private GameObject tarako = default;

    private GameMaster gameMaster;

    /// <summary>
    /// アイテムが出てくるインターバル
    /// </summary>
    [SerializeField]
    private float interval = 10;

    /// <summary>
    /// 前のアイテムが出てからの経過時間
    /// </summary>
    private float elapsedTime;

    /// <summary>
    /// ドロップ位置の候補
    /// </summary>
    [SerializeField]
    private Transform[] dropPoint = default;

    /// <summary>
    /// 同時に存在するアイテムの数
    /// </summary>
    [SerializeField]
    private int dropLimit = 5;

    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime >= interval)
        {
            int currentItem = GameObject.FindGameObjectsWithTag("RecoveryItem").Length;
            if (currentItem == dropLimit)
            {
                return;
            }
            Instantiate(tarako, dropPoint[Random.Range(0, 6)].position, Quaternion.identity);
            elapsedTime = 0;
        }
        else
        {
            elapsedTime += 1 * Time.deltaTime;
        }
    }
}

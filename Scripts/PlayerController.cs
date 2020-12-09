using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// カメラのトランスフォーム
    /// </summary>
    [SerializeField]
    private Transform cameraTransform = default;
    /// <summary>
    /// カメラの上下操作反転させるとき使う
    /// </summary>
    [SerializeField]
    private bool isCameraUpsideDown = false;

    /// <summary>
    /// 上の最大角
    /// </summary>
    private float maxAngle = 270;
    /// <summary>
    /// 下の最大角
    /// </summary>
    private float minAngle = 70;

    /// <summary>
    /// たらこ沼のプレハブ
    /// </summary>
    [SerializeField]
    private GameObject swampPrefab = default;

    /// <summary>
    /// たらこ沼パワー
    /// </summary>
    [SerializeField]
    private float defaultPower = 100;
    public float TarakoPower { get; private set; } = 100;
    /// <summary>
    /// 沼作るのに必要なパワー
    /// </summary>
    [SerializeField]
    private float createPower = 10;
    /// <summary>
    /// 拡大させるのに必要なパワー
    /// </summary>
    [SerializeField]
    private float extendPower = 1;
    /// <summary>
    /// 回復量
    /// </summary>
    [SerializeField]
    private float recoveryPower = 10;
    /// <summary>
    /// たらこパワーメーター
    /// </summary>
    [SerializeField]
    private RectTransform tarakoPowerFill = default;


    /// <summary>
    /// 最初のフレームで行う処理
    /// </summary>
    void Start()
    {
        cameraTransform = this.transform.Find("MainCamera").transform;
        // カメラのロックと非表示化
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        tarakoPowerFill.sizeDelta = new Vector2(tarakoPowerFill.rect.width, (136f / 100f) * TarakoPower);
    }

    /// <summary>
    /// プレイヤーを動かす
    /// </summary>
    /// <param name="speed">移動スピード</param>
    /// <param name="horizontal">水平方向入力</param>
    /// <param name="vertical">垂直方向の入力</param>
    public void Move(float speed,float horizontal, float vertical)
    {
        // 前後移動
        this.transform.Translate(Vector3.forward * vertical * speed);
        // 左右移動
        this.transform.Translate(Vector3.right * horizontal * speed);
    }

    /// <summary>
    /// 視点を動かす
    /// </summary>
    /// <param name="speed">カメラ感度</param>
    /// <param name="horizontal">水平方向の入力</param>
    /// <param name="vertical">垂直方向の入力</param>
    public void Rotate(float speed, float horizontal, float vertical)
    {
        // 左右を向く
        this.transform.Rotate(Vector3.up * speed * horizontal);
        Vector3 cameraLocalAngles = cameraTransform.localEulerAngles;
        cameraLocalAngles.x += isCameraUpsideDown ? vertical : -vertical * speed;
        if (cameraLocalAngles.x > minAngle && cameraLocalAngles.x < 180)
            cameraLocalAngles.x = minAngle;
        if (cameraLocalAngles.x < maxAngle && cameraLocalAngles.x > 180)
            cameraLocalAngles.x = maxAngle;
        // 上下を向く
        cameraTransform.localEulerAngles = cameraLocalAngles;
        //headTransform.localEulerAngles = cameraLocalAngles;
    }

    /// <summary>
    /// キャラクターを中心にたらこの沼を作る
    /// </summary>
    public void CreateSwamp()
    {
        if (TarakoPower <= 0)
        {
            return;
        }
        TarakoPower -= createPower;

        Instantiate(swampPrefab, new Vector3(this.transform.position.x, 0f, this.transform.position.z), this.transform.rotation);
    }

    /// <summary>
    /// 沼を広げる
    /// </summary>
    /// <param name="swamp">対象の沼スクリプト</param>
    public void ExtendSwamp(Swamp swamp)
    {
        if (TarakoPower <= 0)
        {
            return;
        }
        TarakoPower -= extendPower;
        swamp.Extend();
    }

    public void RecoveryTarakoPower()
    {
        TarakoPower += recoveryPower;
        if(TarakoPower >= defaultPower)
        {
            TarakoPower = defaultPower;
        }
    }
}

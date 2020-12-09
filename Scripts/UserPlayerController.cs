using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class UserPlayerController : MonoBehaviour
{
    /// <summary>
    /// コントローラー
    /// </summary>
    private PlayerController playerController;
    /// <summary>
    /// キーボードの上下左右の入力
    /// </summary>
    private float inputVertical;
    private float inputHorizontal;

    /// <summary>
    /// マウスの上下左右の入力
    /// </summary>
    private float mouseVertical;
    private float mouseHorizontal;

    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 1.0f;
    /// <summary>
    /// 回転スピード
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 1.0f;

    /// <summary>
    /// プレイヤーのアニメーターコントローラー
    /// </summary>
    private Animator playerAnimator;
    
    /// <summary>
    /// プレイヤーが乗っている沼
    /// </summary>
    private GameObject targetSwamp;
    /// <summary>
    /// 沼クラス
    /// </summary>
    private Swamp swamp;

    // Start is called before the first frame update
    void Start()
    {
        this.playerController = this.GetComponent<PlayerController>();
        // アニメーター取得
        playerAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 沼上だったら拡大，違ったら生成
        if (targetSwamp != null && Input.GetMouseButton(0) && playerController.TarakoPower > 0)
        {
            // 拡大
            playerController.ExtendSwamp(swamp);

            playerAnimator.SetBool("isGrab", true);

        }
        else if (Input.GetMouseButtonDown(0) && playerController.TarakoPower > 0)
        {
            // 生成
            playerAnimator.SetBool("isGrab", true);
            // 握るアニメーション再生
            playerAnimator.SetTrigger("Grab");

            playerController.CreateSwamp();
        }
        else
        {
            playerAnimator.SetBool("isGrab", false);
        }

        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        mouseHorizontal = Input.GetAxis("Mouse X");
        mouseVertical = Input.GetAxis("Mouse Y");

        // 歩くアニメーション
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            playerAnimator.SetBool("isWalk", true);
        }
        else
        {
            playerAnimator.SetBool("isWalk", false);
        }

        // 握り中なら動かない
        if(!Input.GetMouseButton(0) || playerController.TarakoPower <= 0) playerController.Move(moveSpeed, inputHorizontal, inputVertical);
        playerController.Rotate(rotateSpeed, mouseHorizontal, mouseVertical);
    }

    private void OnTriggerEnter(Collider other)
    {
        targetSwamp = other.gameObject;
        swamp = targetSwamp.GetComponent<Swamp>();
    }

    private void OnTriggerExit(Collider other)
    {
        targetSwamp = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "RecoveryItem")
        {
            playerController.RecoveryTarakoPower();
            Destroy(collision.gameObject);
        }
    }
}

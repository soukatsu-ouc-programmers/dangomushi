using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// 追いかける対象のトランスフォーム
    /// </summary>
    private Transform target;

    /// <summary>
    /// だんごむしが追いかけるスピード
    /// </summary>
    [SerializeField]
    private float speed = 0.1f;

    /// <summary>
    /// だんごむしについてるアニメーターコントローラー
    /// </summary>
    private Animator animator;
    /// <summary>
    /// だんごむしが丸まった時用のスフィアコライダー
    /// </summary>
    private SphereCollider sphereCollider;
    /// <summary>
    /// だんごむし本体のコライダー
    /// </summary>
    private MeshCollider meshCollider;

    /// <summary>
    /// とびかかる距離
    /// </summary>
    [SerializeField]
    private float closeDistance = 10;
    /// <summary>
    /// 攻撃状態
    /// </summary>
    private enum attackState
    {
        NotAttack,
        Attack,
        Attacking
    };
    private attackState state;
    /// <summary>
    /// 攻撃時に加える力の大きさ
    /// </summary>
    [SerializeField]
    private float attackPower = 10;
    /// <summary>
    /// ゲームマスタースクリプト
    /// </summary>
    private GameMaster gameMaster;

    /// <summary>
    /// 死んでいるかどうか
    /// </summary>
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // アニメーターの取得
        animator = this.GetComponent<Animator>();
        // プレイヤーのTransform取得
        target = GameObject.FindWithTag("Player").transform;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        sphereCollider = this.GetComponent<SphereCollider>();
        meshCollider = this.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == attackState.NotAttack 
            && Vector3.Distance(this.transform.position, target.transform.position) <= closeDistance)
        {
            state = attackState.Attack;
            StartCoroutine("Attack");
        }
        else if(state == attackState.NotAttack)
        {
            Chase();
        }else if(state == attackState.Attacking)
        {
            //this.transform.Translate(Vector3.up * 0.2f);
            //this.transform.LookAt(target);
            // プレイヤーの方を向く
            Vector3 targetPositon = target.position;
            if (transform.position.y != target.position.y)
            {
                targetPositon = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            }
            Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);

            //this.transform.Translate(Vector3.forward * speed * 1.5f);
            //this.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Swamp" || this.transform.position.y > 1f || isDead)
        {
            return;
        }
        //StartCoroutine("Sank");
        Dead();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Swamp" || this.transform.position.y > 1f || isDead)
        {
            return;
        }
        Dead();
    }

    private void Chase()
    {
        // プレイヤーの方を向く
        Vector3 targetPositon = target.position;
        if (transform.position.y != target.position.y)
        {
            targetPositon = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        // 前進
        this.transform.Translate(Vector3.forward * speed);
    }

    private void ChaseStop()
    {
    }

    public void Dead()
    {
        isDead = true;
        this.enabled = false;
        StopAllCoroutines();
        StartCoroutine("Sank");
    }
    public IEnumerator Sank()
    {
        //animator.SetTrigger("Sank");
        yield return new WaitForSeconds(0.5f);
        //this.GetComponent<MeshCollider>().enabled = false;
        meshCollider.enabled = false;
        sphereCollider.enabled = false;

        //this.enabled = false;
        yield return new WaitForSeconds(1f);
        gameMaster.ScoreUp();
        Destroy(this.gameObject);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);

        // 斜方投射
        state = attackState.Attacking;
        yield return new WaitForSeconds(0.1f);

        animator.SetBool("isAttack", true);
        sphereCollider.enabled = true;
        meshCollider.enabled = false;

        Vector3 forceDirection = this.transform.up + this.transform.forward;
        this.GetComponent<Rigidbody>().AddForce(forceDirection * attackPower, ForceMode.Impulse);

        //isAttack = true;
        //this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 1, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        state = attackState.Attack;

        yield return new WaitForSeconds(1f);

        animator.SetBool("isAttack", false);
        meshCollider.enabled = true;
        sphereCollider.enabled = false;
        state = attackState.NotAttack;


        //ChaseStart();
    }
}

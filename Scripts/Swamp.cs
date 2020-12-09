using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour
{
    /// <summary>
    /// 沼の拡大率
    /// </summary>
    [SerializeField]
    private float extendSpeed = 0.03f;
    /// <summary>
    /// 沼の縮小率
    /// </summary>
    [SerializeField]
    private float shrinkSpeed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shrink();
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void Extend()
    {
        this.transform.localScale += new Vector3(extendSpeed, 0, extendSpeed);
    }

    public void Shrink()
    {
        if (this.transform.localScale.x <= 0.1f) Destroy(this.gameObject);
        this.transform.localScale -= new Vector3(shrinkSpeed, 0, shrinkSpeed);
    }
}

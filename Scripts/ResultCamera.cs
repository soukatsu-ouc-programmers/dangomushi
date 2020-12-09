using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// リザルトシーンでのカメラの動き
/// </summary>
public class ResultCamera : MonoBehaviour
{
    /// <summary>
    /// 回転の中心
    /// </summary>
    private Vector3 center = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(center, Vector3.up, 10 * Time.deltaTime);
    }
}

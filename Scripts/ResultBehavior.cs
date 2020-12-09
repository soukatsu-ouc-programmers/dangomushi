using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // カメラのロックと非表示化
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}

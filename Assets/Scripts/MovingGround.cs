using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    //北京移动速度
    public float speed = 2.0f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    private void Update()
    {
        //依据游戏状态设置背景移动速度
        rb.velocity = (GameManager.instance.gameState == GameManager.GameState.START) ? new Vector2(-speed, 0)
           : Vector2.zero;
    }
}

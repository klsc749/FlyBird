using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    //向上的力
    public float upForce = 300;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(GameManager.instance.gameState == GameManager.GameState.START)
        {
            if(Input.GetMouseButtonDown(0))
                Fly();
            rb.gravityScale = 1;
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    /// <summary>
    /// 控制小鸟飞起
    /// </summary>
    private void Fly()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * upForce);
        AudioManager.instance.PlayFly();
    }

    /// <summary>
    /// 小鸟死亡
    /// </summary>
    private void Die()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        GameManager.instance.GameOver();
        AudioManager.instance.PlayDie();
    }

    /// <summary>
    /// 通过管道得分
    /// </summary>
    private void Point()
    {
        GameManager.instance.PassPipe();
        AudioManager.instance.PlayPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Tube"))
        {
            Die();
        }
        if(collision.gameObject.CompareTag("TubePass"))
        {
            Point();
        }
    }
}

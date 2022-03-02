using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    //�����ƶ��ٶ�
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
        //������Ϸ״̬���ñ����ƶ��ٶ�
        rb.velocity = (GameManager.instance.gameState == GameManager.GameState.START) ? new Vector2(-speed, 0)
           : Vector2.zero;
    }
}

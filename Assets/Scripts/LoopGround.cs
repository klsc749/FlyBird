using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGround : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float boxColliderWidth;

    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        boxColliderWidth = groundCollider.size.x;
    }

    void Update()
    {
        //更新背景图的位置
        if (transform.position.x  < -18.71f )
        {
            transform.position = new Vector3(
                transform.position.x + 5 * boxColliderWidth,
                transform.position.y,
                transform.position.z);
        }
    }
}

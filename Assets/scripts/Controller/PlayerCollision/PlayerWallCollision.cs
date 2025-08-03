using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWallCollision : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float skinWidth = 0.1f;

    private Rigidbody2D rb;
    private Collider2D playerCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        // 检测四个方向的碰撞
        Vector2[] rayDirections = {
            Vector2.up, Vector2.down,
            Vector2.left, Vector2.right
        };

        foreach (Vector2 dir in rayDirections)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                dir,
                playerCollider.bounds.extents.magnitude + skinWidth,
                wallLayer
            );

            if (hit.collider != null)
            {
                // 限制该方向移动
                if (Vector2.Dot(rb.velocity, dir) > 0)
                {
                    rb.velocity = new Vector2(
                        dir.x == 0 ? rb.velocity.x : 0,
                        dir.y == 0 ? rb.velocity.y : 0
                    );
                }
            }
        }
    }
}
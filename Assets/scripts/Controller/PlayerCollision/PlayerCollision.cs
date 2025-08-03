using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCollision : MonoBehaviour
{
    [Header("Åö×²¼ì²â")]
    public LayerMask wallLayer;
    public float collisionOffset = 0.1f;

    private Rigidbody2D rb;
    private ContactFilter2D contactFilter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(wallLayer);
    }

    void FixedUpdate()
    {
        // ¼ì²âÇ°·½Åö×²
        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = rb.Cast(rb.velocity.normalized, contactFilter, hits, collisionOffset);

        if (hitCount > 0)
        {
            // Åöµ½Ç½±ÚÊ±Í£Ö¹ÒÆ¶¯
            rb.velocity = Vector2.zero;
        }
    }
}
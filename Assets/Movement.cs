using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    void Update()
    {
        // Detecta clique do mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = transform.position.z; // Evita problemas de profundidade
            targetPosition = mouseWorldPos;
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 currentPosition = rb.position;
            Vector2 direction = ((Vector2)targetPosition - currentPosition).normalized;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);

            rb.MovePosition(newPosition);

            // Chegou no destino?
            if (Vector2.Distance(newPosition, targetPosition) < 0.05f)
            {
                isMoving = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ClickToMove : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    private Rigidbody2D rigidBody;
    private Camera mainCamera; // Referência à câmera principal
    private bool isMoving = false;
    private Vector2 targetPosition;
    private Tilemap tilemap;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Obtém o componente NavMeshAgent
        mainCamera = Camera.main; // Obtém a câmera principal
        targetPosition = transform.position;
        tilemap = GetComponentInParent<Grid>().GetComponentInChildren<Tilemap>();

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 currentPosition = rigidBody.position;
            Vector2 direction = ((Vector2)targetPosition - currentPosition).normalized;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.fixedDeltaTime);

            rigidBody.MovePosition(newPosition);

            print(Vector2.Distance(newPosition, currentPosition));
            if (Vector2.Distance(newPosition, currentPosition) < 0.05f)
            {
                isMoving = false;
                rigidBody.linearVelocity = Vector2.zero;
            }
        }
    }

    

    void OnGoTo(InputValue mousePos)
    {
        if (!isMoving)
        {
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos.Get<Vector2>()); // Cria um raio da posição do mouse
            mouseWorldPos.y = mouseWorldPos.y + 0.5f;
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.down);
            if(hit.collider != null)
            {
                Vector3 vector3Int = tilemap.CellToWorld(tilemap.WorldToCell(hit.point));
                mouseWorldPos = new Vector2(vector3Int.x, vector3Int.y);
                targetPosition = mouseWorldPos;
                isMoving = true;
            }
            
        }
    }
}

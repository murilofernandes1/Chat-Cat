using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ClickToMove : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    public Rigidbody2D rigidBody;
    public Camera mainCamera; // Referência à câmera principal
    public Tilemap floor;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>(); // Obtém o componente NavMeshAgent
        mainCamera = Camera.main; // Obtém a câmera principal
        targetPosition = transform.position;
        floor = GetComponentInParent<Grid>().GetComponentInChildren<Tilemap>();

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
            if (Vector2.Distance(newPosition, currentPosition) < 0.1f)
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
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.down);
            if(hit.collider != null)
            {
                Vector3 vector3Int = floor.CellToWorld(floor.WorldToCell(hit.point));
                mouseWorldPos = new Vector2(vector3Int.x, vector3Int.y);
                targetPosition = mouseWorldPos;
                isMoving = true;
            }
            
        }
    }
}

using UnityEngine;

public class GoToClickPosition : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    private UnityEngine.AI.NavMeshAgent agent; // Componente do sistema de navegação
    private Camera mainCamera; // Referência à câmera principal

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // Obtém o componente NavMeshAgent
        mainCamera = Camera.main; // Obtém a câmera principal
        print(agent.gameObject.name);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica se o botão esquerdo do mouse foi pressionado
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cria um raio da posição do mouse
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Verifica se o raio colidiu com algo
            {
                if (agent != null)
                {
                    agent.SetDestination(hit.point); // Move o personagem para o ponto de colisão
                }
                else
                {
                    // Sem NavMeshAgent, mova diretamente
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, speed * Time.deltaTime);
                }
            }
        }
    }
}
using UnityEngine;

public class Box : MonoBehaviour
{
    private BoxManager boxManager;

    private void Start()
    {
        boxManager = FindObjectOfType<BoxManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 캐릭터가 박스에 닿으면 상호작용 가능 상태로 설정
            boxManager.SetBoxInteraction(gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 캐릭터가 박스를 벗어나면 상호작용 불가능 상태로 설정
            boxManager.SetBoxInteraction(gameObject, false);
        }
    }
}

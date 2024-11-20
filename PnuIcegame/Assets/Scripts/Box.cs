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
            // ĳ���Ͱ� �ڽ��� ������ ��ȣ�ۿ� ���� ���·� ����
            boxManager.SetBoxInteraction(gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ĳ���Ͱ� �ڽ��� ����� ��ȣ�ۿ� �Ұ��� ���·� ����
            boxManager.SetBoxInteraction(gameObject, false);
        }
    }
}

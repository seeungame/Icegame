using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoxManager : MonoBehaviour
{
    [System.Serializable]
    public class BoxData
    {
        public GameObject boxObject; // �ڽ� ������Ʈ
        public bool canMove = false; // �̵� ���� ����
    }
    public List<BoxData> boxes = new List<BoxData>(); // ������ �ڽ� ����Ʈ
    public float moveDistance = 1f; // �ڽ� �̵� �Ÿ�

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GrabBox();
    }
    void GrabBox()
    {
        foreach (var box in boxes)
        {
            if (box.canMove)
            {
                Debug.Log($"Moving box: {box.boxObject.name}");
                MoveBox(box.boxObject);
            }
        }
    }
    private void MoveBox(GameObject boxObject)
    {
        // G Ű�� ����Ű �Է��� ���ÿ� ����
        if (Input.GetKey(KeyCode.G))
        {
            Debug.Log("Press G");
            Vector3 direction = Vector3.zero;

            // ���� ĳ���Ϳ� �ڽ��� ��ġ
            Vector3 characterPosition = FindObjectOfType<CharacterManager>().transform.position;
            Vector3 boxPosition = boxObject.transform.position;

            // ĳ���Ϳ� �ڽ��� ��� ��ġ ���
            float deltaX = Mathf.Abs(characterPosition.x - boxPosition.x);
            float deltaY = Mathf.Abs(characterPosition.y - boxPosition.y);

            // ����Ű �Է� ó�� (��� ��ġ�� ���� ����)
            if (Input.GetKeyDown(KeyCode.UpArrow) && deltaX < 0.5f) // ĳ���Ͱ� �ڽ��� �¿� �߽ɼ��� ���� ���� ���� �̵�
            {
                direction = Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && deltaX < 0.5f) // ĳ���Ͱ� �ڽ��� �¿� �߽ɼ��� ���� ���� �Ʒ��� �̵�
            {
                direction = Vector3.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && deltaY < 0.5f) // ĳ���Ͱ� �ڽ��� ���� �߽ɼ��� ���� ���� �������� �̵�
            {
                direction = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && deltaY < 0.5f) // ĳ���Ͱ� �ڽ��� ���� �߽ɼ��� ���� ���� ���������� �̵�
            {
                direction = Vector3.right;
            }

            // ������ ������ ��쿡�� �̵� ó��
            if (direction != Vector3.zero)
            {
                Vector3 newPosition = boxObject.transform.position + direction;

                // �Ҽ��� ������ �����Ͽ� �밢�� ���� ����
                newPosition.x = Mathf.Round(newPosition.x * 1000f) / 1000f;
                newPosition.y = Mathf.Round(newPosition.y * 1000f) / 1000f;

                if (IsPositionValid(newPosition))
                {
                    boxObject.transform.position = newPosition; // �ڽ� �̵�
                    Debug.Log($"Box moved to: {newPosition}");
                }
                else
                {
                    Debug.Log("Box movement blocked: Invalid position");
                }
            }
        }
    }





    bool IsPositionValid(Vector3 gridPosition)
    {
        return gridPosition.x >= -6.5 && gridPosition.x <= 6.5 && gridPosition.y >= -6.25 && gridPosition.y <= 6.75;
    }

    public void SetBoxInteraction(GameObject box, bool canInteract)
    {
        // �ڽ��� ��ȣ�ۿ� ���� ������Ʈ
        foreach (var b in boxes)
        {
            if (b.boxObject == box)
            {
                b.canMove = canInteract;
                Debug.Log($"Box {box.name} interaction set to: {canInteract}");
            }
        }
    }
}

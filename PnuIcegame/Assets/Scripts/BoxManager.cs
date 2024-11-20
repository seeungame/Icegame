using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoxManager : MonoBehaviour
{
    [System.Serializable]
    public class BoxData
    {
        public GameObject boxObject; // 박스 오브젝트
        public bool canMove = false; // 이동 가능 여부
    }
    public List<BoxData> boxes = new List<BoxData>(); // 관리할 박스 리스트
    public float moveDistance = 1f; // 박스 이동 거리

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
        // G 키와 방향키 입력을 동시에 감지
        if (Input.GetKey(KeyCode.G))
        {
            Debug.Log("Press G");
            Vector3 direction = Vector3.zero;

            // 현재 캐릭터와 박스의 위치
            Vector3 characterPosition = FindObjectOfType<CharacterManager>().transform.position;
            Vector3 boxPosition = boxObject.transform.position;

            // 캐릭터와 박스의 상대 위치 계산
            float deltaX = Mathf.Abs(characterPosition.x - boxPosition.x);
            float deltaY = Mathf.Abs(characterPosition.y - boxPosition.y);

            // 방향키 입력 처리 (상대 위치에 따라 제한)
            if (Input.GetKeyDown(KeyCode.UpArrow) && deltaX < 0.5f) // 캐릭터가 박스의 좌우 중심선에 있을 때만 위로 이동
            {
                direction = Vector3.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && deltaX < 0.5f) // 캐릭터가 박스의 좌우 중심선에 있을 때만 아래로 이동
            {
                direction = Vector3.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && deltaY < 0.5f) // 캐릭터가 박스의 상하 중심선에 있을 때만 왼쪽으로 이동
            {
                direction = Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && deltaY < 0.5f) // 캐릭터가 박스의 상하 중심선에 있을 때만 오른쪽으로 이동
            {
                direction = Vector3.right;
            }

            // 방향이 설정된 경우에만 이동 처리
            if (direction != Vector3.zero)
            {
                Vector3 newPosition = boxObject.transform.position + direction;

                // 소수점 단위를 정리하여 대각선 문제 방지
                newPosition.x = Mathf.Round(newPosition.x * 1000f) / 1000f;
                newPosition.y = Mathf.Round(newPosition.y * 1000f) / 1000f;

                if (IsPositionValid(newPosition))
                {
                    boxObject.transform.position = newPosition; // 박스 이동
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
        // 박스의 상호작용 상태 업데이트
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

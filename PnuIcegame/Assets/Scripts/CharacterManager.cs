using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterDataManager dataManager; // JSON 데이터 관리 스크립트
    private CharacterData characterData;     // 캐릭터 데이터

    private bool canMove = true;
    public Vector3 startPosition;
    private Vector3 oldPosition;

    private BoxManager boxManager;

    void Start()
    {
        // JSON에서 캐릭터 데이터 로드
        characterData = dataManager.LoadCharacterData();
        startPosition = transform.position;
        oldPosition = transform.localPosition;
        boxManager = FindObjectOfType<BoxManager>();
    }

    private void Update()
    {
        if (canMove)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        // 플레이어 이동 입력
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 newPosition = oldPosition + new Vector3(0, 1f, 0);
            if (IsPositionValid(newPosition) && CanMoveThroughBox(newPosition))
            {
                oldPosition = newPosition;
                transform.position = oldPosition;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 newPosition = oldPosition + new Vector3(0, -1f, 0);
            if (IsPositionValid(newPosition) && CanMoveThroughBox(newPosition))
            {
                oldPosition = newPosition;
                transform.position = oldPosition;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 newPosition = oldPosition + new Vector3(-1f, 0, 0);
            if (IsPositionValid(newPosition) && CanMoveThroughBox(newPosition))
            {
                oldPosition = newPosition;
                transform.position = oldPosition;
                FlipSprite(-1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 newPosition = oldPosition + new Vector3(1f, 0, 0);
            if (IsPositionValid(newPosition)&& CanMoveThroughBox(newPosition))
            {
                oldPosition = newPosition;
                transform.position = oldPosition;
                FlipSprite(1);
            }
        }
    }

    bool IsPositionValid(Vector3 gridPosition)
    {
        return gridPosition.x >= -6.5 && gridPosition.x <= 6.5 && gridPosition.y >= -6.5 && gridPosition.y <= 6.5;
    }

    private bool CanMoveThroughBox(Vector3 newPosition)
    {
        foreach (var box in boxManager.boxes)
        {
            Vector3 boxPosition = box.boxObject.transform.position;

            // 0.25 오프셋 적용하여 비교
            Vector3 adjustedBoxPosition = boxPosition + new Vector3(0, 0.25f, 0);

            if (Vector3.Distance(adjustedBoxPosition, newPosition) < 0.01f && !Input.GetKey(KeyCode.G))
            {
                Debug.Log("Cannot pass: Box is blocking the way.");
                return false;
            }
        }
        return true;
    }



    void FlipSprite(float moveHorizontal)
    {
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
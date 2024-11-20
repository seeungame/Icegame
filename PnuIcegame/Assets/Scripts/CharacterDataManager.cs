using System.IO;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public float moveSpeed;         // 캐릭터 이동 속도
    public float moveDistance;
    public string characterName;    // 캐릭터 이름
}

public class CharacterDataManager : MonoBehaviour
{
    private string filePath;

    void Awake()
    {
        // StreamingAssets 폴더의 경로 설정
        filePath = Path.Combine(Application.streamingAssetsPath, "characterData.json");
    }

    /// <summary>
    /// JSON 파일에서 캐릭터 데이터를 로드합니다.
    /// </summary>
    /// <returns>캐릭터 데이터 객체</returns>
    public CharacterData LoadCharacterData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<CharacterData>(jsonData);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("JSON 데이터를 로드하는 중 오류가 발생했습니다: " + ex.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("JSON 파일이 존재하지 않습니다: " + filePath);
            return null;
        }
    }

    /// <summary>
    /// 기본 캐릭터 데이터를 생성합니다.
    /// </summary>
    /// <returns>기본 캐릭터 데이터 객체</returns>
    public CharacterData CreateDefaultCharacterData()
    {
        return new CharacterData
        {
            moveSpeed = 1f,
            characterName = "Rebel",
            moveDistance = 1f
        };
    }
}


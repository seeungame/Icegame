using System.IO;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public float moveSpeed;         // ĳ���� �̵� �ӵ�
    public float moveDistance;
    public string characterName;    // ĳ���� �̸�
}

public class CharacterDataManager : MonoBehaviour
{
    private string filePath;

    void Awake()
    {
        // StreamingAssets ������ ��� ����
        filePath = Path.Combine(Application.streamingAssetsPath, "characterData.json");
    }

    /// <summary>
    /// JSON ���Ͽ��� ĳ���� �����͸� �ε��մϴ�.
    /// </summary>
    /// <returns>ĳ���� ������ ��ü</returns>
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
                Debug.LogError("JSON �����͸� �ε��ϴ� �� ������ �߻��߽��ϴ�: " + ex.Message);
                return null;
            }
        }
        else
        {
            Debug.LogWarning("JSON ������ �������� �ʽ��ϴ�: " + filePath);
            return null;
        }
    }

    /// <summary>
    /// �⺻ ĳ���� �����͸� �����մϴ�.
    /// </summary>
    /// <returns>�⺻ ĳ���� ������ ��ü</returns>
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


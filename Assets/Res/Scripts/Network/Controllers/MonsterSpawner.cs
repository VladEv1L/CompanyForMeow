using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner instance;

    public GameObject Monster;

    public void Start()
    {
        instance = this;
    }
    public void SpawnMonster()
    {
        Monster.SetActive(true);
    }
}

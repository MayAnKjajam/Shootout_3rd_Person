using UnityEngine;

public class AIEnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float xMin, xMax, zMin, zMax;
    public GameObject AIEnemy;
    public int EnemyCount;

    private void Start()
    {
        for (int i = EnemyCount; i >= 0; i--)
        {
            GameObject Enemy = GameObject.Instantiate(AIEnemy, transform);
            Enemy.transform.position = new Vector3(Random.Range(xMin, xMax), transform.position.y, Random.Range(zMin, zMax));
        }
    }
   
}

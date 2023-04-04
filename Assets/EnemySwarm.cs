using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarm : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyType
    {
        public string name;
        public Sprite[] sprites;
        public int points;
        public int rowcount;
    }

    [Header("Spawning")]
    [SerializeField]
    private EnemyType[] enemyTypes;
    [SerializeField]
    private int columncount = 11;
    [SerializeField]
    private int yspacing;
    [SerializeField]
    private int xspacing;
    [SerializeField]
    private Transform spawnStart;

    private float minx;
    // Start is called before the first frame update
    void Start()
    {
        minx = spawnStart.position.x;
        GameObject enemy = new GameObject { name = "Enemy" };
        Vector2 currentposition = spawnStart.position;

        int RowId = 0;
        foreach (var EnemyType in enemyTypes)
        {
            var EnemyName = EnemyType.name.Trim();
            for(int i = 0, length = EnemyType.rowcount; i < length; i++)
            {
                for(int j = 0; j<columncount; j++)
                {
                    var Enemy = new GameObject() { name = EnemyName };
                    Enemy.AddComponent<Anima>().sprites = EnemyType.sprites;
                    Enemy.transform.position = currentposition;
                    Enemy.transform.SetParent(enemy.transform);
                    
                    currentposition.x += xspacing;
                }
                currentposition.x = minx;
                currentposition.y -= yspacing;
                RowId++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

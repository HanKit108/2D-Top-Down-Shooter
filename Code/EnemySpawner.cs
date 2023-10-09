using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawner : MonoBehaviour {
    
    [SerializeField] private int enemyNumber;
    [SerializeField] private Transform enemyPrefab;

    private int currentEnemyNumber = 0;
    private BoxCollider2D spawnRegion;


    private void Awake() {
        spawnRegion = GetComponent<BoxCollider2D>();
    }

    void Start() {
        SpawnEnemies();
    }

    public void EnemyDied() {
        currentEnemyNumber--;
        if(currentEnemyNumber == 0) {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies() {
        for(int i = 0; i < enemyNumber; i++) {
            Vector3 dir = GetRandomDirection();
            var enemy = Instantiate(enemyPrefab, dir, Quaternion.identity);
            enemy.gameObject.GetComponent<EnemyManager>().SetEnemySpawner(this);
            currentEnemyNumber++;
        }
    }

    private Vector3 GetRandomDirection() {
        float width = spawnRegion.size.x / 2;
        float height = spawnRegion.size.y / 2;
        return new Vector3(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height));
    }
}

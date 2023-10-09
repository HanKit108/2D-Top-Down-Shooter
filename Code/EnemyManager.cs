using System;
using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
public class EnemyManager : MonoBehaviour {
    private EnemySpawner enemySpawner;
    private UnitHealth unitHealth;

    private void Awake() {
        unitHealth = GetComponent<UnitHealth>();
        unitHealth.OnUnitDied += UnitHealth_OnUnitDied;
    }

    private void UnitHealth_OnUnitDied(object sender, EventArgs e) {
        if(enemySpawner != null) {
            enemySpawner.EnemyDied();
            GetComponent<ItemWorldSpawner>().SpawnRandomItem();
        }
    }

    public void SetEnemySpawner(EnemySpawner spawner){
        enemySpawner = spawner;
    }
}

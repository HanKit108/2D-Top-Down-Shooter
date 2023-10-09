using UnityEngine;

[CreateAssetMenu(fileName ="AI Values", menuName = "AI Values", order = 51)]
public class EnemyAIValues : ScriptableObject{
    
    [Range(5f,20f)]public float ChaseRange = 10f;
    [Range(5f,20f)]public float LeaveRange = 15f;
}

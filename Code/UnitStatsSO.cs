using UnityEngine;

[CreateAssetMenu(fileName ="Unit Stats", menuName = "Unit Stats", order = 51)]
public class UnitStatsSO : ScriptableObject
{
    [Range(1f, 1000f)] public float MaxHealth = 20f;
    [Range(0f, 10f)] public float MoveSpeed = 3f;
    [Range(0f, 100f)] public float Damage = 5f;
    [Range(0f, 20f)] public float AttackRange = 1f;
    [Range(1f, 0.01f)] public float AttackCooldown = 0.5f;
}

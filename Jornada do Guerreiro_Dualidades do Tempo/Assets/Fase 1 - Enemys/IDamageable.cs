// Remova o namespace Enemies
using System.Collections;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount);
    void TakeDamageBola(int amount);
}

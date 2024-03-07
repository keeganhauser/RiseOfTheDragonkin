using UnityEngine;

public abstract class CombatBehavior : MonoBehaviour
{
    public virtual CombatAction? DecideCombatAction() { return null; }
}

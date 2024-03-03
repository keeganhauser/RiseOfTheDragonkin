using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatBehavior : MonoBehaviour
{
    public virtual CombatAction? DecideCombatAction() {  return null; }
}

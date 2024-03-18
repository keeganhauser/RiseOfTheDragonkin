using System;

public class EnemyEvents
{
    public event Action<int> onEnemyAttack;
    public void EnemyAttack(int damage)
    {
        onEnemyAttack?.Invoke(damage);
    }

    public event Action onEnemyDecideAttack;
    public void EnemyDecideAttack()
    {
        onEnemyDecideAttack?.Invoke();
    }

    // TODO: Fill these out
    public event Action onEnemyDefend;

    public event Action onEnemyUseItem;

    public event Action onEnemyEscape;
}

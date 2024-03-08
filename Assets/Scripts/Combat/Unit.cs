using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public enum Type
    {
        Player,
        Enemy
    }

    private string unitName;
    public string UnitName { get; set; }

    [SerializeField]
    private int unitLevel;
    public int UnitLevel => unitLevel;

    [SerializeField]
    private int damage;
    public int Damage => damage;

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField]
    private int speed;
    public int Speed => speed;

    [SerializeField]
    private Vector2 positionOffset = Vector2.zero;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public UnityEvent OnDeath;
    public UnityEvent OnHealthChange;

    public bool IsDefending;
    public CombatAction? combatAction;
    public CombatBehavior combatBehavior;

    private void Awake()
    {
        OnDeath = new UnityEvent();
        OnHealthChange = new UnityEvent();
        IsDefending = false;
        currentHealth = maxHealth;
        combatBehavior = GetComponent<CombatBehavior>();
    }

    private void TakeDamage(int damage)
    {
        if (IsDefending)
        {
            damage /= 2;
            IsDefending = false;
        }
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        OnHealthChange?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void DealDamage(Unit other, int? amount = null)
    {
        other.TakeDamage(amount ?? damage);
    }

    public void DecideCombatAction()
    {
        combatAction = combatBehavior?.DecideCombatAction();
    }
}

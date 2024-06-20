using UnityEngine;

public abstract class Entity : MonoBehaviour
{
  [field: SerializeField] public Transform MainSprite { get; private set; }
  [field: SerializeField] public Transform HandPoint { get; private set; }
  public StatHandler Stat { get; private set; }
  public Animator Animator { get; private set; }
  public Rigidbody2D Rigid { get; private set; }
  public Collider2D Collider { get; private set; }
  public HealthSystem Health { get; private set; }
  public ManaSystem Mana { get; private set; }

  protected virtual void Awake()
  {
    Animator = GetComponent<Animator>();
    Rigid = GetComponent<Rigidbody2D>();
    Collider = GetComponent<Collider2D>();
    Stat = GetComponent<StatHandler>();
    Health = GetComponent<HealthSystem>();
    Mana = GetComponent<ManaSystem>();
  }
}

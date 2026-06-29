using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}
public interface IHealable
{
    void Heal(float amount);
}
public interface Interactuable
{
    void Interact();
}
public interface ICollectable
{
    void Collect();
}

public interface IActivatable
{
    void Activate();
    void Deactivate();
}
  

using System;

public interface IHittablle
{
    void ProcessDamage(float damage);
    event Action OnHit;
}

using System;

public interface IHittablle
{
    void ProcessDamage(float damage);
    bool CanHit { get; set; }
    event Action OnHit;
}

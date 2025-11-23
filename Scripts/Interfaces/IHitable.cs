using UnityEngine;

public interface IHitable
{
    void OnHit(int damage,Vector3 impactVector);

    bool isAlive();
}
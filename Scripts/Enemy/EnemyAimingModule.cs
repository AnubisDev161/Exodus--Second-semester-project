using UnityEngine;

public class EnemyAimingModule : MonoBehaviour
{
    public void UpdateTarget(Transform target)
    {
        gameObject.transform.LookAt(target);
    }
}

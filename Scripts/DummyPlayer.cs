using UnityEngine;

public class DummyPlayer : MonoBehaviour, IDetectable
{
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    
}

using UnityEngine;

public class MuzzleFlashDestroyer : MonoBehaviour
{
    [SerializeField] float destructionTime;
    float currentTime;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= destructionTime)
        {
            Destroy(gameObject);
        }
    }
}

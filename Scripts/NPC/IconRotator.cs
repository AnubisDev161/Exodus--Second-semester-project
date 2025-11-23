using UnityEngine;

[RequireComponent (typeof(Canvas))]
public class IconRotator : MonoBehaviour
{
    void Update()
    {
        // AI generated
        Vector3 lookPos = Camera.main.transform.position;
        lookPos.y = transform.position.y; // Lock Y rotation
        transform.LookAt(lookPos);

        // Add 180 degree rotation if facing backwards
        transform.Rotate(0, 180, 0);

        // AI generated end
    }
}

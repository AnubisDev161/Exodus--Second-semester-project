using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AggroRange : MonoBehaviour
{
    [SerializeField] private UnityEvent<Player> playerEnteredAggroRange;
    [SerializeField] private UnityEvent playerCaughtInstantly;
    [SerializeField] private float playerCaughtInstantlyTime;

    private bool playerIsInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player detectableComponent))
        {
            playerEnteredAggroRange?.Invoke(detectableComponent);
            playerIsInRange = true;
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.TryGetComponent<Player>(out Player detectableComponent))
        {
            playerEnteredAggroRange?.Invoke(null);
            playerIsInRange = false;
        }
    }

    public void Disable(bool disable)
    {
        gameObject.SetActive(!disable);

        if(!disable)
        {
            StartCoroutine(playerCaughtTimer());
        }
    }

    private IEnumerator playerCaughtTimer()
    {
        yield return new WaitForSeconds(playerCaughtInstantlyTime);
        if(playerIsInRange)
        {
            playerCaughtInstantly?.Invoke();
        }
    }
}

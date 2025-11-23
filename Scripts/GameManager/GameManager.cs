using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;
        DontDestroyOnLoad(gameObject);
    }

}

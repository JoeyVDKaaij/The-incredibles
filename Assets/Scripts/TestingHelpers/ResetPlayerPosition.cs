using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    private Transform playerInitialPosition;

    private void Awake()
    {
        Debug.Log($"Player initial position: {gameObject.transform.position}");
        playerInitialPosition = gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Resetting player position....");
            gameObject.transform.position = playerInitialPosition.position;
        }
    }
}

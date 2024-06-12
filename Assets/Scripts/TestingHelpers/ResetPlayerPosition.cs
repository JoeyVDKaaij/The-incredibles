using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    private Vector3 playerInitialPosition;

    private void Awake()
    {
        playerInitialPosition = gameObject.transform.position;
        Debug.Log($"Player initial position: {playerInitialPosition}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Resetting player position....");
            gameObject.transform.position = playerInitialPosition;
        }
    }
}

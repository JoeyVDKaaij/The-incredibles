using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    
    [Header("Timer Settings")]
    [SerializeField, Tooltip("Set the game object that is going to be used as a timer.")]
    private TMP_Text timerText = null;
    [SerializeField, Tooltip("Set the maximum time that the player has before game over."), Min(1)]
    private float maxTime = 10;

    private float _timeLeft = 0;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            if (transform.parent.gameObject != null)
                DontDestroyOnLoad(transform.parent.gameObject);
            else 
                DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (instance == this) instance = null;
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    void Start()
    {
        _timeLeft = maxTime;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            Debug.Log("Time's up!");

            _timeLeft = maxTime;
        }
        
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        if (timerText != null)
        {
            int timeLeftText = Mathf.Min(0, (int)_timeLeft);
            timerText.SetText(timeLeftText.ToString());
        }
    }
}

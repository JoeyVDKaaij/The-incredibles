using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using FMODUnity;

[RequireComponent(typeof(PlayableDirector))]
public class GameOverScript : MonoBehaviour
{
    [Header("Game Over Settings")] 
    [SerializeField, Tooltip("Start Game Over sequence by pressing space.")]
    private bool debugStartGameOver = false;
    [SerializeField, Tooltip("Start Game Over sequence by collisions.")]
    private bool startGameOverByCollision = false;
    [SerializeField]
    private EventReference gameOverEvent;
    
    private PlayableDirector _gameOverAnimation;

    private void Start()
    {
        _gameOverAnimation = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (debugStartGameOver && Input.GetKeyDown(KeyCode.Space) || UIManager.instance.gameOver)
        {
            StartGameOver();    
        }
    }

    public void StartGameOver()
    {
        if (!gameOverEvent.IsNull)
            RuntimeManager.PlayOneShot(gameOverEvent, transform.position);
        StartCoroutine(nameof(GameOver));
    }

    IEnumerator GameOver()
    {
        _gameOverAnimation.Play();
        
        while (_gameOverAnimation.state == PlayState.Playing)
            yield return null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && startGameOverByCollision)
        {
            StartGameOver();
        }
    }
}
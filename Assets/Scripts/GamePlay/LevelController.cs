using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public BasicHealthSystem Player1;
    public BasicHealthSystem Player2;
    public int EndScreenIndex;
    public float EndScreenDuration = 2.0f;
    public int NextLevelIndex;
    public AudioClip GameOverClip;

    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetGameOver()
    {
        if (Options.CoopMode)
        {
            if(Player1.isDead() && Player2.isDead())
            {
                audioSource.PlayOneShot(GameOverClip);
                StartCoroutine(DeathAnimation());
            }
        }
        else 
        {
            if(Player1.isDead())
            {
                audioSource.PlayOneShot(GameOverClip);
                StartCoroutine(DeathAnimation());
            }
        }
        
    }

    public void MoveToNextLevel()
    {
        Options.CheckPointIndex = NextLevelIndex;
        SceneManager.LoadScene(NextLevelIndex);
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(EndScreenDuration);
        SceneManager.LoadScene(EndScreenIndex);
    }
}

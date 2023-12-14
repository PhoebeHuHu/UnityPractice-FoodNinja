using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 2.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    private int score;
    public int live;
    public bool isGameActive;
    public AudioClip buttonSound;
    public AudioClip scoreSound;
    public AudioClip gameOverSound;
    private bool hasGameOverSoundPlayed = false;
    public AudioClip badSound;
    private AudioSource bgm;
    public AudioSource gameSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        bgm = audioSources[0];
        gameSound = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTarget()
    {
        while(isGameActive) { 
            //every 1 sec spawn a target
            yield return new WaitForSeconds(spawnRate);
            //randomly spawn target in the list
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void ScoreUpdate(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        bgm.Stop();
        if (! hasGameOverSoundPlayed)
        {
            gameSound.PlayOneShot(gameOverSound, 0.6f);
            hasGameOverSoundPlayed = true;
        }
        isGameActive = false;
    }
    public void RestartGame()
    { 
        isGameActive = true;
        hasGameOverSoundPlayed = false;
        StartCoroutine(RestartGameAfterDelay());

    }
    private IEnumerator RestartGameAfterDelay()
    {
        // 等待音效播放完毕，假设音效长度约为1秒
        yield return new WaitForSeconds(1f);

        // 现在重新加载场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(StartGameAfterDelay(difficulty));
        
    }
    private IEnumerator StartGameAfterDelay(int difficulty)
    {
        // 等待音效播放完毕，假设音效长度约为1秒
        yield return new WaitForSeconds(1f);
        spawnRate /= difficulty;
        titleScreen.SetActive(false);
        StartCoroutine(SpawnTarget());
        score = 0;
        ScoreUpdate(0);
        LiveUpdate();
    }

    public void ButtonSound()
    {
        gameSound.PlayOneShot(buttonSound);
    }

    public void LiveUpdate()
    {
        liveText.text = "Live: " + live;
    }
}

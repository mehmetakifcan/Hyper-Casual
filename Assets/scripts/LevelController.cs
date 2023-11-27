using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = false;

    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, finishScoreText, currentLevelText, nextLevelText,startingMenuMoneyText, gameOverMenuMoneyText,finishGameMenuText;
    public Slider levelProgewaaBar;
    public float maxDistance;
    public GameObject finishline;
    public AudioSource gameMusicAudioSource;
    public AudioClip victoryAudioClip, gameOverAudioClip;
    public DailyReward dailyReward;

    int currentLevel;
    int score;


    // Start is called before the first frame update
    void Start()
    {

        Current = this;
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        
    
        //karakterkontrol.Current = GameObject.FindObjectOfType<karakterkontrol>();
        GameObject.FindObjectOfType<MarketController>().InitializeMarketController();
        dailyReward.IntializedDailyReward();
        currentLevelText.text = (currentLevel + 1).ToString();
        nextLevelText.text = (currentLevel + 2).ToString();
        UptadeMoneyText();
          
            
       
        gameMusicAudioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            karakterkontrol player = karakterkontrol.Current;
            float distance = finishline.transform.position.z - karakterkontrol.Current.transform.position.z;
            levelProgewaaBar.value = 1 - (distance / maxDistance);
        }
        
    }

    public void StartLevel()
    {
        AdController.Current.bannerView.Hide();
        maxDistance = finishline.transform.position.z - karakterkontrol.Current.transform.position.z;
        karakterkontrol.Current.ChangeSpeed(karakterkontrol.Current.runningspeed);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        karakterkontrol.Current.animator.SetBool("running", true);


        gameActive = true;
    }
    public void RestartLevel()
    {
        LevelLoader.Current.ChangeLevel(SceneManager.GetActiveScene().name);
       
    }
    public void LoadNexttLevel()
    {
        LevelLoader.Current.ChangeLevel("Level " + (currentLevel + 1));
    }
    public void GameOver()
    {
        AdController.Current.bannerView.Show();
        UptadeMoneyText();
        gameMusicAudioSource.Stop();
        gameMusicAudioSource.PlayOneShot(gameOverAudioClip);
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;
        

    }
    public void FinishGame()
    {
        AdController.Current.bannerView.Show();
        GiveMoneyToPlayer(score);
        finishGameMenuText.text = PlayerPrefs.GetInt("money").ToString();
        gameMusicAudioSource.Stop();
        gameMusicAudioSource.PlayOneShot(victoryAudioClip);
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        finishScoreText.text = score.ToString();
        

        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;
    }
    public void ChangeScore(int increment)
    {
        score += increment;
        scoreText.text = score.ToString();
    }
    public void UptadeMoneyText()
    {
        int money = PlayerPrefs.GetInt("money");
        startingMenuMoneyText.text = money.ToString();
        gameOverMenuMoneyText.text = money.ToString();
        finishGameMenuText.text = money.ToString();


    }
    public void GiveMoneyToPlayer(int increment)
    {
        int money = PlayerPrefs.GetInt("money");
        money = Mathf.Max(0,money + increment);
        PlayerPrefs.SetInt("money", money);
        UptadeMoneyText();
    }





}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using SgLib;
using Gley.AllPlatformsSave;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int CurrentLevel;

    public GameObject GameOverUI, GameWinUI, adsBtn;
    public bool finished;
    public Text currentText;
    public GameObject[] Level;
    public AudioSource BackgroundSound;
    public AudioClip boo, Scream,OhNo,CollectSound;
    public AudioSource Fx;
    public NavMeshSurface meshSurface;
    public Text TotalCoin;
    int TotalEnemy, EnemyLeft;
    [HideInInspector] public int coinLevel;
    bool rewardDone = false;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
       CurrentLevel = PlayerPrefs.GetInt("Level", 0);
        Instantiate(Level[CurrentLevel]);

        currentText.text = "DAY " + (CurrentLevel);
    }

    void Start()
    {
        TotalEnemy = GameObject.FindGameObjectsWithTag("Hunter").Length;
        meshSurface.BuildNavMesh();
        Invoke("ShowBanner", 1f);
    }

    void ShowBanner()
    {
        AdsManager.Instance.ShowBanner();
    }
    

    
    void Update()
    {
      EnemyLeft = GameObject.FindGameObjectsWithTag("Hunter").Length;
        if (EnemyLeft <= 0 && !finished) {

            GameWin();
        }

        TotalCoin.text = ""+(CoinManager.Instance.Coins);

        if (AdsManager.Instance.RewardAvailable() && !rewardDone)
        {
            adsBtn.SetActive(true);
        }
        else
        {
            adsBtn.SetActive(false);
        }

    }

    IEnumerator End() {
        yield return new WaitForSeconds(2f);
        GameOverUI.SetActive(true);

    }

    public void GameEnd() {
        finished = true;
         //Adcontrol.instance.ShowInterstitial();
        GameOverUI.SetActive(true);

    }

    public void GameWin() {
        finished = true;
        //dcontrol.instance.ShowInterstitial();
        PlayerPrefs.SetInt("Level", (CurrentLevel + 1));
        GameWinUI.SetActive(true);


    }





    public void Restart() {
        AdsManager.Instance.ShowInterstitial(GoToRestart);
        
    }


    void GoToRestart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        AdsManager.Instance.HideBanner();
    }

    public void DoubleRewardBtn()
    {
        AdsManager.Instance.RewardVideo(CompleteMethod);
    }

    private void CompleteMethod(bool completed)
    {
        if (completed)
        {
            CoinManager.Instance.AddCoins(coinLevel);
            rewardDone = true;
        }
    }


    public void spooked() {

        Fx.PlayOneShot(boo);
    }

    public void CollectCoin() {
        Fx.PlayOneShot(CollectSound);
    }

    public void Die() {
        Fx.PlayOneShot(OhNo);
        GameEnd();

    }


    public void Shop()
    {
        SceneManager.LoadScene("Shop");

    }
}

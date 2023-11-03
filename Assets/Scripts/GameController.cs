using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    bool isGameOver;
    bool pauseGame;
    [HideInInspector]
    public bool isEnoughItem;
    
    static bool playAgain;
    [HideInInspector]
    public bool winner;
    bool newGame;
    int item =0;
    public Transform startPlayer;
    public AudioClip soundBtn;

    AudioSource audioSource;
    UIController ui;
    FadeBackground fadebg;
    PlayerController player;
    // Start is called before the first frame update
   
    void Start()
    {
        Time.timeScale = 1;

        ui = FindObjectOfType<UIController>();
        fadebg = FindObjectOfType<FadeBackground>();
        player = FindObjectOfType<PlayerController>();

        audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("newGame")== 1)
        {
            setNewGame(true);
        }
        else
        {
            setNewGame(false);
        }
        Debug.Log(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            audioSource.Pause();
            Time.timeScale = 0;
            ui.isShowGameOver(true);
            ui.isShowTimeCountDown(false);
        }
        if (winner)
        {
            audioSource.Pause();
            Time.timeScale = 0;
            ui.isShowWinner(true);
            ui.isShowTimeCountDown(false);
        }
        if (item == 4)
        {
            //Debug.Log("Enough Item");
            isEnoughItem = true;
        }
      
        if (Input.GetKeyDown(KeyCode.P) && !pauseGame)
        {
            pauseGame = !pauseGame;
            ui.isShowPause(true);
            Time.timeScale = 0;
            audioSource.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && pauseGame)
        {
            resume();
        }
    }

    public void SetGameOver(bool state)
    {
        isGameOver = state;
    }
    public void resume()
    {
        pauseGame = !pauseGame;
        ui.isShowPause(false);
        Time.timeScale = 1;
        audioSource.Play();
        audioSource.PlayOneShot(soundBtn);
    }
    public void setNewGame(bool state)
    {
        newGame = state;
    }
    public bool getNewGame()
    {
        return newGame;
    }
    public void rePlay()
    {
        audioSource.PlayOneShot(soundBtn);
        SaveSystem.HardResetGame();
        setPlayAgain(true);
        fadebg.SetTriggerFadeOut("Map1");
    }
    public void exitGame()
    {
        audioSource.PlayOneShot(soundBtn);
        //luu data
        if (!player.isDeath)
        {
            SaveSystem.SavePlayer(player);

            Debug.Log("Da luu");
            
           
        }
        
        fadebg.SetTriggerFadeOut("Menu");
    }
    public void IncreateItem()
    {
        item++;
    }
    public void setPlayAgain(bool state)
    {
        playAgain = state;
    }
    public bool GetPlayAgain()
    {
        return playAgain;
    }
}

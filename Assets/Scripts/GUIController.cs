using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GUIController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioClip soundBtn;
    public Button continueBtn;
    AudioSource audioSource;
    TextMeshProUGUI textContinue;
    FadeBackground fadebg;
    PlayerData data;
    int scene;
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        fadebg = FindObjectOfType<FadeBackground>();
        textContinue = continueBtn.GetComponentInChildren<TextMeshProUGUI>();

        Time.timeScale = 1;

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        data = SaveSystem.loadPlayer();

        if (data == null)
        {
            disableBtn();
            textContinue.color = new Color(1, 0.3378351f,0,0.5f);
        }
        else
        {
            textContinue.color = new Color(1, 0.3378351f, 0, 1);
        }
    }
    public void newGame()
    {
        //gc.setNewGame(true);
        PlayerPrefs.SetInt("newGame", 1);
        fadebg.SetTriggerFadeOut("Map1");
      
    }
    public void continueGame()
    {
        PlayerPrefs.SetInt("newGame", 0);

        scene = data.scene;
        PlayerPrefs.SetInt("scene", scene);

        PlayerPrefs.SetFloat("position_x", data.position[0]);
        PlayerPrefs.SetFloat("position_y", data.position[1]);
        PlayerPrefs.SetFloat("position_z", data.position[2]);

        PlayerPrefs.SetInt("currentHealth", data.currentHealth);
        PlayerPrefs.SetInt("countList", data.listitemTag.Count);

        for(int i = 0; i < data.listitemTag.Count; i++)
        {
            PlayerPrefs.SetString("itemTag" + i, data.listitemTag[i]);
        }

        PlayerPrefs.SetFloat("timeValue", data.timeValue);

        fadebg.SetTriggerFadeOut("Map" + scene);
    }
    
    public void quitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    public void playSoundBtn()
    {
        audioSource.PlayOneShot(soundBtn);
    }
     void disableBtn()
    {
        continueBtn.enabled = false;        
    }
}

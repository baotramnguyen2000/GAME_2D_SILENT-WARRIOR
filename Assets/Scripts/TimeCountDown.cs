using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeCountDown : MonoBehaviour
{
    public float timeValue = 600;
    public TextMeshProUGUI timeText;
    public AudioClip soundCountDown;

    GameController gc;
    Animator animator;
    AudioSource audioSource;

    int countPlaySound = 0;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gc = FindObjectOfType<GameController>();
        animator.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            StartCoroutine(showGameover()); 
        }
        DisplayTime(timeValue);
    }
    IEnumerator showGameover()
    {
        yield return new WaitForSeconds(1);
        gc.SetGameOver(true);
    }
    void DisplayTime(float timetoDisplay)
    {
        if (timetoDisplay < 0)
        {
            timetoDisplay = 0;
        }
        else if (timetoDisplay > 0)
        {
            timetoDisplay += 1;
        }
        if (timetoDisplay <= 11)
        {
            countPlaySound++;
            if (!audioSource.isPlaying && countPlaySound == 1)
            {
                audioSource.PlayOneShot(soundCountDown);
            }
            timeText.color = Color.red;
        }
        else {
            timeText.color = Color.white;
        }
        if (timetoDisplay <= 4)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
        float minutes = Mathf.FloorToInt(timetoDisplay / 60);
        float second = Mathf.FloorToInt(timetoDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }
    
}

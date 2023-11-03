using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientController : MonoBehaviour
{
    FadeBackground fadebg;
    public Animator anim;


    GameController gc;
    int scene;
    private void Start()
    {
        fadebg = FindObjectOfType<FadeBackground>();
        gc = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
    }

    public void triggerRecover()
    {
        anim.SetTrigger("recover");
        Jump();
    }

    void Jump()
    {
        transform.LeanMoveLocal(new Vector2(transform.position.x,transform.position.y + 0.2f),0.5f).setEaseOutQuart().setLoopPingPong();
    }
    public void nextScene()
    {
        if(scene == 3)
        {
            gc.winner = true;
        }
        else
        {
            scene += 1;
            fadebg.SetTriggerFadeOut("Map"+scene);
            PlayerPrefs.SetInt("nextLevel", 1);
        }
    }
}

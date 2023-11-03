using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeBackground : MonoBehaviour
{
    public Animator animator;
    string fadeoutCurrent;
    public void SceneFadeOut()
    {
        SceneManager.LoadScene(fadeoutCurrent);
    }
    public void SetTriggerFadeOut(string sceneFadeout)
    {
        fadeoutCurrent = sceneFadeout;
        animator.SetTrigger("FadeOut");
    }
}

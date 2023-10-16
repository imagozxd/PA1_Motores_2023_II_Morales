using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator animator1;

    public static Fade Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        //Invoke("FadeOut",2);
    }

    public void FadeOut()
    {
        animator1.Play("FadeOut");
    }
    public void FadeHit()
    {
        //animator1.Play("Hit");
        StartCoroutine(PlayHitAnimation());
    }
    IEnumerator PlayHitAnimation()
    {
        animator1.Play("Hit");

        yield return new WaitForSeconds(1.0f); 

    }
}

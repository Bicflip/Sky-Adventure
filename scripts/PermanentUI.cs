using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PermanentUI : MonoBehaviour
{
    public int health = 3;
    public int cherries = 0;
    public Text cherrytext;
    public Text HPAmount;

    public static PermanentUI perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        cherries = 0;
        cherrytext.text = cherries.ToString();
        health = 3;
        HPAmount.text = health.ToString();
    }

}
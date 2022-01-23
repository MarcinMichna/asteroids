using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : NetworkBehaviour
{
    [SyncVar] public int score;

    public Text scoreText;

    public void handleDeath()
    {
        score = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
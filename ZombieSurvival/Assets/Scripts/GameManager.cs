using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public PlayerController player;
    public Text healthText;
    public Text ammoText;
    public Text scoreText;

    private int _score = 0;

    // Use this for initialization
    void Start()
    {
        healthText.text = "Health: " + player.health.ToString();
        ammoText.text = "Ammo: " + player.ammo + "/7";
        scoreText.text = "Score: " + _score;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHealth()
    {
        healthText.text = "Health: " + player.health.ToString();
    }

    public void updateAmmo()
    {
        ammoText.text = ammoText.text = "Ammo: " + player.ammo + "/7";
    }

    public void updateScore()
    {
        _score += 10;
        scoreText.text = "Score: " + _score;
    }
}
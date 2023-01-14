using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameFlow : MonoBehaviour
{
    public GameObject maid, restartScreen;
    public AudioSource levelSound, effectSound;
    public AudioClip dead, confirm;
    bool gameOver = false;

    //Player Configs
    Player[] maidLife;


    void Start()
    {
        maidLife = maid.GetComponents<Player>();
    }


    void Update()
    {
        maidLife[0].IsLive();
        GameOver();
    }

    void GameOverSounds()
    {

        if (!gameOver) effectSound.PlayOneShot(dead);

        levelSound.Stop();
    }

    void GameOver()
    {
        if (maidLife[0].currentHealth <= 0)
        {
            restartScreen.SetActive(true);
            GameOverSounds();
            maid.SetActive(false);
            gameOver = true;
        }
    }
}

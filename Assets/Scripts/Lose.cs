using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public float loseHeight = -100f;
    void Update()
    {
        if (gameObject.transform.position.y < loseHeight)
        {
            gameObject.SetActive(false);

            GameController.instance.GameOver();

            Time.timeScale = 0;
        }
    }
}

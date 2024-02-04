using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameOverText.SetActive(true);
    }
}

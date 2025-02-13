using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    public int lives = 3;
    
    void Start() {
        if (FindObjectOfType<Game_Controller>() != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        
    }

    public void EndGame(string endState) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (endState == "Lose") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class Game_Controller : MonoBehaviour
{
    // Create an instance variable to reference this specific instance
    public static Game_Controller instance;

    // Reference to the intro screen on startup
    public GameObject IntroScreen;

    // Private variables for game manager functions
    private int deaths = 0;
    private GameObject endScreen;
    private TMP_Text endText;
    private float waitAfterEnd;

    // On level load, Ensure there is no existing game managers.
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        // if a game manager already exists, destroy the new one
        else {
            Destroy(gameObject);
        }
    }

    // Play the intro sequence on game start
    void Start()
    {
        StartCoroutine("IntroSequence");
    }

    // Coroutine to play intro sequence for 18 seconds or until it is cancelled
    // freeze time while the intro screen is active to remove player interactions, resume time after completion
    private IEnumerator IntroSequence() {
        IntroScreen.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(18);
        IntroScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Public function for other gameobjects to call the end of the game
    // Pass it a string with the state of the game ending
    // Defines the ending variables and begins the ending sequence coroutine
    public void EndGame(string endState) {
        // Get the ending text object
        endText = GameObject.Find("EndingText").GetComponent<TMP_Text>();

        // If the player won, define the winning ending screen, set the winning text, reset the death count, define end screen run time
        if (endState == "Win") {
            endScreen = GameObject.Find("WinScreen");
            endText.SetText($"You Won!\n\nTime Taken: {Time.timeSinceLevelLoad:F2} Seconds\nTotal Deaths: {deaths}");
            deaths = 0;
            waitAfterEnd = 5;
        }

        // If the player lost, define the losing ending screen, set the losing text, define end screen run time
        else if (endState == "Lose") {
            deaths += 1;
            // if the player has lost before, remind them they stink
            if (deaths > 1) {
                endText.SetText("You Died... Again.");
            }
            else {
                endText.SetText("You Died.");
            }
            endScreen = GameObject.Find("LoseScreen");
            waitAfterEnd = 2;
        }

        // Start ending sequence and freeze time
        StartCoroutine(EndingSequence());
        Time.timeScale = 0;
    }

    // Begin the ending sequence based on the variables defined in the endgame function 
    IEnumerator EndingSequence() {

        // Fade the ending screen in with a for loop
        for (float alpha = 0; alpha < 1.05; alpha += 0.05f) {
            // Replace the screen with a slightly more opaque one each time
            Image screenImage = endScreen.GetComponent<Image>();
            Color screenColor = screenImage.color;
            screenColor.a = alpha;
            screenImage.color = screenColor;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        // Keep the ending screen on for the designated amount of time
        yield return new WaitForSecondsRealtime(waitAfterEnd);

        // Reload the level and unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    // Public accessor for current player deaths
    public float GetDeaths(){
        return deaths;
    }

    // Player input to escape intro or game
    public void OnEscape() {
        // If the intro sequence is running, escape will cancel it
        if (GameObject.Find("IntroScreen") != null) {
            StopCoroutine("IntroSequence");
            Time.timeScale = 1;
            IntroScreen.SetActive(false);
        }
        // Otherwise, escape will exit the application
        else {
            Application.Quit();
        }
    }
}

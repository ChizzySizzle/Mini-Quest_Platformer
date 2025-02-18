
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller instance;
    public GameObject IntroScreen;

    private int deaths = 0;
    private GameObject endScreen;
    private TMP_Text endText;
    private float waitAfterEnd;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine("IntroSequence");
    }

    private IEnumerator IntroSequence() {
        IntroScreen.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(18);
        IntroScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndGame(string endState) {
        endText = GameObject.Find("EndingText").GetComponent<TMP_Text>();

        if (endState == "Win") {
            endScreen = GameObject.Find("WinScreen");
            endText.SetText($"You Won!\n\nTime Taken: {Time.timeSinceLevelLoad:F2} Seconds\nTotal Deaths: {deaths}");
            deaths = 0;
            waitAfterEnd = 5;
        }

        else if (endState == "Lose") {
            deaths += 1;
            if (deaths > 1) {
                endText.SetText("You Died... Again.");
            }
            else {
                endText.SetText("You Died.");
            }
            endScreen = GameObject.Find("LoseScreen");
            waitAfterEnd = 2;
        }

        StartCoroutine(EndingSequence(endState));
        Time.timeScale = 0;
    }

    IEnumerator EndingSequence(string endstate) {

        for (float alpha = 0; alpha < 1.05; alpha += 0.05f) {
            Image screenImage = endScreen.GetComponent<Image>();
            Color screenColor = screenImage.color;
            screenColor.a = alpha;
            screenImage.color = screenColor;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        yield return new WaitForSecondsRealtime(waitAfterEnd);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public float GetDeaths(){
        return deaths;
    }

    public void OnEscape() {
        if (GameObject.Find("IntroScreen") != null) {
            StopCoroutine("IntroSequence");
            Time.timeScale = 1;
            IntroScreen.SetActive(false);
        }
        else {
            Application.Quit();
        }
    }
}

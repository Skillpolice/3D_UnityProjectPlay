using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text timerTextEnd;
    public GameObject losePanel;

    [SerializeField] public float timer;
    float timer1;
    float ct = 0;

    private void Start()
    {
        losePanel.SetActive(false);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timer1 += Time.deltaTime;

        }
        else
        {
            timer = 0;
            timer1 = 600;
            losePanel.SetActive(true);
        }

        DisplayTime(timer);

        DisplayTimeEnd(timer1);
    }

    public void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minuts = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minuts, seconds);
    }
    public void DisplayTimeEnd(float timeToDisplayEnd)
    {
        if (timeToDisplayEnd < 0)
        {
            timeToDisplayEnd = 0;
        }
        else if (timeToDisplayEnd > 0)
        {
            timeToDisplayEnd += 1;
        }

        float minuts = Mathf.FloorToInt(timeToDisplayEnd / 60);
        float seconds = Mathf.FloorToInt(timeToDisplayEnd % 60);
        timerTextEnd.text = "You time: " + string.Format("{0:00}:{1:00}", minuts, seconds);
    }
}

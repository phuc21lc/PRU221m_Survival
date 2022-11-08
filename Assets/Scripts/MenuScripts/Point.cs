using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTimerDisplay(Timer.Instance.timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateTimerDisplay(float time)
    {
        if (time < 0)
        {
            time = 0;
        }

        if (time > 3660)
        {
            Debug.LogError("Timer cannot display values above 3660 seconds");
            return;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();

        //Use this for a single text object
        //timerText.text = currentTime.ToString();
    }
}

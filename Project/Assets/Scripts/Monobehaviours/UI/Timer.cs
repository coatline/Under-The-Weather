using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event System.Action Finished;
    
    [SerializeField] TMP_Text timeText;

    float timer;

    void Update()
    {
        if (timer > 0)
        {
            timeText.text = $"{(int)timer}";
            timer -= Time.deltaTime;
        }
        else if(timer < 0)
        {
            timer = 0;
            timeText.text = $"{(int)timer}";
            Finished?.Invoke();
        }
    }

    public void StartTimer(float time)
    {
        timer = time;
    }
}

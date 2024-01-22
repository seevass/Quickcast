using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timer;
    public float time = 0;
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        time = time + Time.deltaTime;
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        timer.text = minutes + ":" + seconds;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] float doubleDistasterChance;
    [SerializeField] float initialBuildTime;
    [SerializeField] float buildTime;

    [SerializeField] TMP_Text doubleDisasterText;
    [SerializeField] Canvas screenSpaceCanvas;
    [SerializeField] TMP_Text spaceToSkipText;
    [SerializeField] TMP_Text modeText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] Image gameOverUi;
    Weather doubleWeatherScript;
    Weather weatherScript;
    public bool storming;
    bool stormed;

    private void Awake()
    {
        C.Reference();
    }

    void Start()
    {
        C.ddd.Say("Jim: it seems that the weather app is saying that the weather is supposed to be OUT OF CONTROL today.");
        weatherScript = GetComponent<Weather>();
        doubleWeatherScript = transform.GetChild(0).GetComponent<Weather>();
        SetTimer(initialBuildTime);
    }

    float timer;

    void Update()
    {
        if (C.gameOver)
        {
            gameOverUi.gameObject.SetActive(true);

            var text = gameOverUi.transform.GetChild(0).GetComponent<TMP_Text>();

            if (text.text != "") { return; }

            C.ddd.Say("Narrator: Don't worry, I will adopt Jim's cat. Although, have you seen him anywhere?");
            var rand = Random.Range(0, 14);

            switch (rand)
            {
                case 0: text.text = "He won't be missed."; break;
                case 1: text.text = "Oh. Somebody will have to clean that up..."; break;
                case 2: text.text = "Dang it Jim."; break;
                case 3: text.text = "He lived a unproductive life."; break;
                case 4: text.text = "Jim. He had a good name."; break;
                case 5: text.text = "I will always remember those 14 pixels of Jim.."; break;
                case 6: text.text = "I will never forget his red shirt."; break;
                case 7: text.text = "Should've listened to the weather app..."; break;
                case 8: text.text = "Somebody get the mop!"; break;
                case 9: text.text = "'Bout time."; break;
                case 10: text.text = "I worked with Jim at Walmart yesterday. I won't miss him."; break;
                case 11: text.text = "Who?"; break;
                case 12: text.text = "Eh? Did you say Pim!? Oh just Jim? Phew."; break;
                case 13: text.text = "Now who wants to pay off his debt? Anyone?"; break;
            }
        }

        if (timer >= 0)
        {
            timeText.text = $"{(int)timer}";
            timer -= Time.deltaTime;

            if (!stormed && !C.gameOver)
            {
                if (Mathf.Abs(timer - 80) < 1)
                {
                    C.ddd.Say("Jim: Ahh... Whatever. They are definitely over reacting with the OUT OF CONTROL thing.");
                }
                else if (Mathf.Abs(timer - 70) < 1)
                {
                    C.ddd.Say("Narrator: Jim is an idiot but he does have a cat that needs feeding so I guess you can try protecting him if you want.");
                }
                else if (Mathf.Abs(timer - 60) < 1)
                {
                    C.ddd.Say("Press E to toggle attach mode.");
                }
                else if (Mathf.Abs(timer - 50) < 1)
                {
                    C.ddd.Say("While in attach mode, left click on a pair of nearby blocks to connect them.");
                }
                else if (Mathf.Abs(timer - 40) < 1)
                {
                    C.ddd.Say("While in attach mode, right click on a block to delete it.");
                }
                else if (Mathf.Abs(timer - 30) < 1)
                {
                    C.ddd.Say("Scroll up or down to rotate blocks.");
                }
                else if (Mathf.Abs(timer - 20) < 1)
                {
                    C.ddd.Say("Narrator: Each round there is a 20% chance of a DOUBLE DISASTER! Man I sure love those...");
                }
            }
        }
        else
        {
            if (!storming)
            {
                stormed = true;
                storming = true;
                weatherScript.StartCoroutine(weatherScript.ChooseNewState());
                SetTimer(weatherScript.disasterDuration);
                spaceToSkipText.enabled = false;

                if (Random.Range(0, 101) <= doubleDistasterChance)
                {
                    Instantiate(doubleDisasterText, screenSpaceCanvas.transform);
                    doubleWeatherScript.prevState = weatherScript.currentState;
                    doubleWeatherScript.currentState = weatherScript.currentState;
                    StartCoroutine(doubleWeatherScript.ChooseNewState());
                    modeText.text = $"DOUBLE DISASTER \n {weatherScript.currentState} + {doubleWeatherScript.currentState}";
                }
                else
                {
                    modeText.text = "Disaster";
                }
            }
            else
            {
                storming = false;
                doubleWeatherScript.ResetStates(false);
                weatherScript.ResetStates(false);
                spaceToSkipText.enabled = true;
                modeText.text = "Building";
                SetTimer(buildTime);
            }
        }

        if (!storming)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetTimer(.1f);
            }
        }
    }

    void SetTimer(float time)
    {
        timer = time;
    }
}

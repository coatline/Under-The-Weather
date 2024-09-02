using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Game : Singleton<Game>
{
    [SerializeField] float doubleDistasterChance;
    [SerializeField] float initialBuildTime;
    [SerializeField] float buildTime;

    [SerializeField] TMP_Text gameOverDescription;
    [SerializeField] Image gameOverUi;

    [SerializeField] TMP_Text doubleDisasterText;
    [SerializeField] TMP_Text spaceToSkipText;
    [SerializeField] Canvas screenSpaceCanvas;
    [SerializeField] DropDownDialogue ddd;
    [SerializeField] TMP_Text modeText;
    [SerializeField] Timer timer;

    [SerializeField] DisasterController[] disasterControllers;

    public bool IsDisastering { get; private set; }
    public bool GameOver { get; private set; }

    void Start()
    {
        timer.StartTimer(initialBuildTime);
        timer.Finished += TimesUp;

        StartCoroutine(Intro());
    }

    void Update()
    {
        // If we are in the build phase and we skip it
        if (IsDisastering == false)
            if (Input.GetKeyDown(KeyCode.Space))
                timer.StartTimer(.1f);
    }

    void TimesUp()
    {
        if (IsDisastering)
            StopDisastering();
        else
            StartDisaster();
    }

    void StartDisaster()
    {
        IsDisastering = true;
        spaceToSkipText.enabled = false;

        disasterControllers[0].StartRandomDisaster();
        timer.StartTimer(disasterControllers[0].CurrentDisaster.Duration);

        if (Random.Range(0, 101) <= doubleDistasterChance)
        {
            disasterControllers[1].StartRandomDisaster();
            Instantiate(doubleDisasterText, screenSpaceCanvas.transform);
            modeText.text = $"DOUBLE DISASTER \n {disasterControllers[0].CurrentDisaster.name} + {disasterControllers[1].CurrentDisaster.name}";
        }
        else
            modeText.text = "Disaster";
    }

    void StopDisastering()
    {
        IsDisastering = false;
        spaceToSkipText.enabled = true;

        timer.StartTimer(buildTime);

        disasterControllers[0].StopDisaster();
        disasterControllers[1].StopDisaster();

        modeText.text = "Building";
    }

    void GameLost()
    {
        gameOverUi.gameObject.SetActive(true);

        ddd.Say("Narrator: Don't worry, I will adopt Jim's cat. Although, have you seen him anywhere?");

        var rand = Random.Range(0, 14);
        string message = "";

        switch (rand)
        {
            case 0: message = "He won't be missed."; break;
            case 1: message = "Oh. Somebody will have to clean that up..."; break;
            case 2: message = "Dang it Jim."; break;
            case 3: message = "He lived a unproductive life."; break;
            case 4: message = "Jim. He had a good name."; break;
            case 5: message = "I will always remember those 14 pixels of Jim.."; break;
            case 6: message = "I will never forget his red shirt."; break;
            case 7: message = "Should've listened to the weather app..."; break;
            case 8: message = "Somebody get the mop!"; break;
            case 9: message = "'Bout time."; break;
            case 10: message = "I worked with Jim at Walmart yesterday. I won't miss him."; break;
            case 11: message = "Who?"; break;
            case 12: message = "Eh? Did you say Pim!? Oh just Jim? Phew."; break;
            case 13: message = "Now who wants to pay off his debt? Anyone?"; break;
        }

        gameOverDescription.text = message;
    }

    IEnumerator Intro()
    {
        ddd.Say("Jim: it seems that the weather app is saying that the weather is supposed to be OUT OF CONTROL today.");
        yield return new WaitForSeconds(10);
        ddd.Say("Jim: Ahh... Whatever. They are definitely over reacting with the OUT OF CONTROL thing.");
        yield return new WaitForSeconds(10);
        ddd.Say("Narrator: Jim is an idiot but he does have a cat that needs feeding so I guess you can try protecting him if you want.");
        yield return new WaitForSeconds(10);
        ddd.Say("Press E to toggle attach mode.");
        yield return new WaitForSeconds(10);
        ddd.Say("While in attach mode, left click on a pair of nearby blocks to connect them.");
        yield return new WaitForSeconds(10);
        ddd.Say("While in attach mode, right click on a block to delete it.");
        yield return new WaitForSeconds(10);
        ddd.Say("Scroll up or down to rotate blocks.");
        yield return new WaitForSeconds(10);
        ddd.Say("Narrator: Each round there is a 20% chance of a DOUBLE DISASTER! Man I sure love those...");
    }
}

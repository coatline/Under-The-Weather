using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class C
{
    public static AudioHandler audioHandler;
    public static Material currentMaterial;
    public static Color hightlightedColor;
    public static LayerMask pointerLayer;
    public static DropDownDialogue ddd;
    public static LayerMask windLayer;
    public static Player playerScript;
    public static bool InConnectMode;
    public static Animator mainCamAni;
    public static Camera mainCam;
    public static bool gameOver;
    public static int size = 1;
    public static Game game;

    public enum Material
    {
        wood,
        stone,
        concrete
    }

    public static void Reference()
    {
        audioHandler = AudioHandler.FindObjectOfType<AudioHandler>();
        ddd = DropDownDialogue.FindObjectOfType<DropDownDialogue>();
        game = Game.FindObjectOfType<Game>();
        windLayer = LayerMask.NameToLayer("Wind");
        pointerLayer = LayerMask.NameToLayer("Pointer");
        hightlightedColor = new Color(.5f, 1, .5f);
        playerScript = Player.FindObjectOfType<Player>();
        mainCam = Camera.main;
        mainCamAni = mainCam.GetComponent<Animator>();
        gameOver = false;
    }

    static C()
    {
        
    }
}

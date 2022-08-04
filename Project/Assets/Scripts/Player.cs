using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] SpriteRenderer pointerSprite;
    [SerializeField] SpriteRenderer woodPrefab;
    [SerializeField] SpriteRenderer stonePrefab;
    [SerializeField] SpriteRenderer concretePrefab;
    [SerializeField] Material lineMaterial;
    [SerializeField] GraphicRaycaster grt;
    [SerializeField] GraphicRaycaster gr;
    [SerializeField] Sprite circleSprite;
    List<LineRenderer> lineRenderers;
    List<FixedJoint2D> fixedJoints;
    List<GameObject> oneGobs;
    List<GameObject> twoGobs;
    GameObject overGobOne;
    GameObject overGobTwo;
    Pointer p;

    Vector2 mousePosInWorld;

    void Start()
    {
        lineRenderers = new List<LineRenderer>();
        fixedJoints = new List<FixedJoint2D>();
        oneGobs = new List<GameObject>();
        twoGobs = new List<GameObject>();

        p = pointerSprite.GetComponent<Pointer>();
    }

    void Update()
    {
        RenderLines();

        mousePosInWorld = C.mainCam.ScreenToWorldPoint(Input.mousePosition);

        pointerSprite.transform.position = mousePosInWorld;

        if (!C.game.storming && !C.gameOver)
        {
            p.gameObject.SetActive(true);

            if (!C.InConnectMode)
            {
                switch (C.currentMaterial)
                {
                    case C.Material.wood: pointerSprite.sprite = woodPrefab.sprite; break;
                    case C.Material.stone: pointerSprite.sprite = stonePrefab.sprite; break;
                    case C.Material.concrete: pointerSprite.sprite = concretePrefab.sprite; break;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    PointerEventData ped = new PointerEventData(null);

                    ped.position = Input.mousePosition;

                    List<RaycastResult> results = new List<RaycastResult>();

                    gr.Raycast(ped, results);
                    grt.Raycast(ped, results);

                    if (results.Count == 0 && !p.currentOverGob)
                    {
                        switch (C.currentMaterial)
                        {
                            case C.Material.wood:

                                var wood = Instantiate(woodPrefab, mousePosInWorld, p.transform.rotation);
                                wood.transform.localScale = new Vector3(C.size, C.size);
                                wood.GetComponent<Rigidbody2D>().mass += C.size - 1;
                                C.audioHandler.soundEffectAudioSource.PlayOneShot(C.audioHandler.PlaceDownWoodSound());

                                break;

                            case C.Material.stone:

                                var stone = Instantiate(stonePrefab, mousePosInWorld, p.transform.rotation);
                                stone.transform.localScale = new Vector3(C.size, C.size);
                                stone.GetComponent<Rigidbody2D>().mass += C.size - 1;
                                C.audioHandler.soundEffectAudioSource.PlayOneShot(C.audioHandler.PlaceDownStoneSound());

                                break;

                            case C.Material.concrete:

                                var concrete = Instantiate(concretePrefab, mousePosInWorld, p.transform.rotation);
                                concrete.transform.localScale = new Vector3(C.size, C.size);
                                concrete.GetComponent<Rigidbody2D>().mass += C.size - 1;
                                C.audioHandler.soundEffectAudioSource.PlayOneShot(C.audioHandler.PlaceDownConcreteSound());

                                break;
                        }
                    }
                }
            }
            else
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if (p.currentOverGob)
                    {
                        if (overGobOne)
                        {
                            if (Vector2.Distance(p.currentOverGob.transform.position, overGobOne.transform.position) > 1.9f || p.currentOverGob == overGobOne)
                            {
                                overGobOne.GetComponent<SpriteRenderer>().color = Color.white;
                                overGobOne = null;
                                return;
                            }

                            overGobTwo = p.currentOverGob;

                            var lineR = overGobOne.GetComponent<LineRenderer>();

                            if (!lineR)
                            {
                                lineR = overGobOne.AddComponent<LineRenderer>();

                                lineR.SetColors(new Color(.5f, 1, .5f), new Color(1, 1, 1));
                                lineR.material = lineMaterial;
                                lineR.SetWidth(.015f, .06f);

                                lineRenderers.Add(lineR);
                                oneGobs.Add(overGobOne);
                                twoGobs.Add(overGobTwo);
                            }
                            else
                            {
                                var index = 100;

                                for (int i = 0; i < oneGobs.Count; i++)
                                {
                                    if (oneGobs[i] == overGobOne)
                                    {
                                        index = i;
                                        twoGobs[index] = overGobTwo;
                                        break;
                                    }
                                }

                            }

                            var fixedJoint = overGobOne.GetComponent<FixedJoint2D>();

                            if (!fixedJoint)
                            {
                                fixedJoint = overGobOne.AddComponent<FixedJoint2D>();

                                fixedJoint.enableCollision = true;
                                fixedJoint.breakForce = 6000;
                                fixedJoint.connectedBody = overGobTwo.GetComponent<Rigidbody2D>();
                                fixedJoints.Add(fixedJoint);
                            }

                            overGobOne.GetComponent<SpriteRenderer>().color = Color.white;
                            overGobOne = null;
                            overGobTwo.GetComponent<SpriteRenderer>().color = Color.white;
                            overGobTwo = null;

                            C.audioHandler.soundEffectAudioSource.PlayOneShot(C.audioHandler.AttachSound());
                        }
                        else
                        {
                            overGobOne = p.currentOverGob;
                            overGobOne.GetComponent<SpriteRenderer>().color = C.hightlightedColor;
                        }
                    }
                    else
                    {
                        if (overGobOne)
                        {
                            overGobOne.GetComponent<SpriteRenderer>().color = Color.white;
                            overGobOne = null;
                        }
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    if (!p.currentOverGob) { return; }

                    C.audioHandler.soundEffectAudioSource.PlayOneShot(C.audioHandler.DeleteSound());
                    Destroy(p.currentOverGob);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                C.InConnectMode = !C.InConnectMode;
                pointerSprite.sprite = circleSprite;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                C.InConnectMode = false;
                C.size = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                C.InConnectMode = false;
                C.size = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                C.InConnectMode = false;
                C.size = 3;
            }
        }
        else
        {
            if (overGobOne)
            {
                overGobOne.GetComponent<SpriteRenderer>().color = Color.white;
                overGobOne = null;
            }

            p.gameObject.SetActive(false);
        }
    }

    void RenderLines()
    {
        for (int i = lineRenderers.Count - 1; i >= 0; i--)
        {
            if (fixedJoints[i] == null || lineRenderers[i] == null || oneGobs[i] == null || twoGobs[i] == null || Vector2.Distance(oneGobs[i].transform.position, twoGobs[i].transform.position) > 2f)
            {
                C.audioHandler.soundEffectAudioSource.PlayOneShot(C.audioHandler.BreakAttachmentSound());
                Destroy(lineRenderers[i]);
                Destroy(fixedJoints[i]);
                lineRenderers.RemoveAt(i);
                oneGobs.RemoveAt(i);
                twoGobs.RemoveAt(i);
                fixedJoints.RemoveAt(i);
                continue;
            }

            lineRenderers[i].SetPosition(0, oneGobs[i].transform.position);
            lineRenderers[i].SetPosition(1, twoGobs[i].transform.position);
        }
    }
}

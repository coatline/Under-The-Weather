using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    BuildingMaterial Material => materials[materialIndex];

    [SerializeField] SpriteRenderer pointerSprite;
    [SerializeField] BuildingMaterial[] materials;
    [SerializeField] Material lineMaterial;
    [SerializeField] GraphicRaycaster grt;
    [SerializeField] GraphicRaycaster gr;
    [SerializeField] Sprite circleSprite;
    [SerializeField] Camera mainCam;
    [SerializeField] Game game;
    bool inConnectMode;
    int materialIndex;
    int size;

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
        p = pointerSprite.GetComponent<Pointer>();

        lineRenderers = new List<LineRenderer>();
        fixedJoints = new List<FixedJoint2D>();
        oneGobs = new List<GameObject>();
        twoGobs = new List<GameObject>();

        size = 1;
    }

    void Update()
    {
        RenderLines();

        mousePosInWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);

        pointerSprite.transform.position = mousePosInWorld;

        if (!game.storming && !Game.I.GameOver)
        {
            p.gameObject.SetActive(true);

            if (!inConnectMode)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PointerEventData ped = new PointerEventData(null);

                    ped.position = Input.mousePosition;

                    List<RaycastResult> results = new List<RaycastResult>();

                    gr.Raycast(ped, results);
                    grt.Raycast(ped, results);

                    if (results.Count == 0 && !p.currentOverGob)
                    {

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

                                //lineR.colorGradient = new Gradient(); new Color(.5f, 1, .5f), new Color(1, 1, 1);
                                //lineR.SetWidth(.015f, .06f);
                                lineR.material = lineMaterial;

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

                            AudioHandler.I.PlayGeneralSfx("Attach");
                        }
                        else
                        {
                            overGobOne = p.currentOverGob;
                            //overGobOne.GetComponent<SpriteRenderer>().color = hightlightedColor;
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

                    AudioHandler.I.PlayGeneralSfx("Delete");
                    Destroy(p.currentOverGob);
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                inConnectMode = !inConnectMode;
                pointerSprite.sprite = circleSprite;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inConnectMode = false;
                size = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inConnectMode = false;
                size = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inConnectMode = false;
                size = 3;
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
                AudioHandler.I.PlayGeneralSfx("Break Attachment");
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

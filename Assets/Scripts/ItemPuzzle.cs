using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemPuzzle : MonoBehaviour
{
    public static ItemPuzzle Instance;

    #region Variables Declaration
    public GameObject textUiPressF;
    public GameObject textUiInstruction;

    float movingItemsSpeed = 4f;
    int selectedItem = 0;
    int canMove = 0;
    int puzzlePiecePlaced = 0;

    bool canAsemble = false;
    bool canMoveItems = false;

    public bool showArrow = false;

    float time = 1;

    [HideInInspector]
    public bool shieldVikingCollected = false;
    [HideInInspector]
    public bool axeVikingCollected = false;
    [HideInInspector]
    public bool bodyVikingCollected = false;
    [HideInInspector]
    public bool dragonSkullCollected = false;
    [HideInInspector]
    public bool humanSkullCollected = false;
    [HideInInspector]
    public bool knightShieldCollected = false;
    [HideInInspector]
    public bool yellowTreasureCollected = false;
    [HideInInspector]
    public bool dungeonRocksCollected = false;
    [HideInInspector]
    public bool boneAxeCollected = false;

    bool shieldVikingPlaced = false;
    bool axeVikingPlaced = false;
    bool bodyVikingPlaced = false;
    bool dungeonRocksPlaced = false;
    bool dragonSkullPlaced = false;
    bool yellowTreasurePlaced = false;

    public GameObject castleDoor;
    public Transform castleDoorOpenedLocation;
    public Camera mainCamera;
    Vector3 mainCameraOriginalPosition;
    Quaternion mainCameraOriginalRotation;
    bool canMoveCamera = true;
    bool savePosition = true;
    bool done = false;
    public Camera openingCamera;

    //available items that can be collected
    public GameObject shieldViking;
    public GameObject axeViking;
    public GameObject bodyViking;
    public GameObject dragonSkull;
    public GameObject humanSkull;
    public GameObject knightShield;
    public GameObject yellowTreasure;
    public GameObject dungeonRocks;
    public GameObject boneAxe;

    Vector3 shieldVikingInitial;
    Vector3 axeVikingInitial;
    Vector3 bodyVikingInitial;
    Vector3 dragonSkullInitial;
    Vector3 humanSkullInitial;
    Vector3 knightShieldInitial;
    Vector3 yellowTreasureInitial;
    Vector3 dungeonRocksInitial;
    Vector3 boneAxeInitial;

    bool canReset0 = true;
    bool canReset1 = true;
    //bool canReset2 = true;
    bool canReset3 = true;
    bool canReset4 = true;
    bool canReset5 = true;
    bool canReset6 = true;
    //bool canReset7 = true;
    bool canReset8 = true;
    //bool canReset9 = true;
    bool canReset10 = true;
    bool canReset11 = true;

    //Currently sellected item
    GameObject selectedCube;
    [SerializeField]
    GameObject ghostSelectedCube;
    
    //location of puzzle pieces placed correctly
    public GameObject bodyVikingCorrect;
    public GameObject axeVikingCorrect;
    public GameObject shieldVikingCorrect;
    public GameObject dungeonRocksCorrect;
    public GameObject dragonSkullCorrect;
    public GameObject yellowTreasureCorrect;

    //lights
    public Light shieldLight;
    public Light axeLight;
    public Light dungeonRocksLight;
    public Light vikingBodyLight;
    public Light dragonSkullLight;
    public Light yellowTreasureLight;
    public Light boneAxeLight;
    public Light humanSkullLight;
    public Light knightShieldLight;

    public Light shieldLight1;
    public Light axeLight1;
    public Light dungeonRocksLight1;
    public Light vikingBodyLight1;
    public Light dragonSkullLight1;
    public Light yellowTreasureLight1;
    public Light boneAxeLight1;
    public Light humanSkullLight1;
    public Light knightShieldLight1;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        textUiPressF.SetActive(false);
        textUiInstruction.SetActive(false);

        shieldViking.SetActive(false);
        axeViking.SetActive(false);
        dungeonRocks.SetActive(false);
        bodyViking.SetActive(false);
        dragonSkull.SetActive(false);
        humanSkull.SetActive(false);
        knightShield.SetActive(false);
        yellowTreasure.SetActive(false);
        boneAxe.SetActive(false);

        shieldVikingInitial = shieldViking.transform.position;
        axeVikingInitial = axeViking.transform.position;
        bodyVikingInitial = bodyViking.transform.position;
        dragonSkullInitial = dragonSkull.transform.position;
        humanSkullInitial = humanSkull.transform.position;
        knightShieldInitial = knightShield.transform.position;
        yellowTreasureInitial = yellowTreasure.transform.position;
        dungeonRocksInitial = dungeonRocks.transform.position;
        boneAxeInitial = boneAxe.transform.position;

        TurnLightsOff();

        shieldLight1.transform.position = shieldLight.transform.position;
        axeLight1.transform.position = axeLight.transform.position;
        dungeonRocksLight1.transform.position = dungeonRocksLight.transform.position;
        vikingBodyLight1.transform.position = vikingBodyLight.transform.position;
        dragonSkullLight1.transform.position = dragonSkullLight.transform.position;
        yellowTreasureLight1.transform.position = yellowTreasureLight.transform.position;
        boneAxeLight1.transform.position = boneAxeLight.transform.position;
        humanSkullLight1.transform.position = humanSkullLight.transform.position;
        knightShieldLight1.transform.position = knightShieldLight.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //player can exit the game even if he is interacting with the items
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (canAsemble)
        {
            SpawnCollectedObject();
        }

        if(Input.GetKeyDown(KeyCode.F) && canAsemble)
        {
            canMove++;
            if (canMove % 2 == 0)
            {
                canMoveItems = false;
                GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>().enabled = true;
                TurnLightsOff();
                textUiInstruction.SetActive(false);
                textUiPressF.SetActive(true);
            }
            else
            {
                canMoveItems = true;
                GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>().enabled = false;
                textUiInstruction.SetActive(true);
                textUiPressF.SetActive(false);
            }
        }

        //movement inputs for the puzzle pieces
        if (canMoveItems)
        {
            if(Input.GetAxis("Mouse ScrollWheel") > 0f && movingItemsSpeed <= 30f)
            {
                movingItemsSpeed++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && movingItemsSpeed >= 2f)
            {
                movingItemsSpeed--;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && selectedItem > 0)
            {
                selectedItem--;
                ResetPosition();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && selectedItem < 8)
            {
                selectedItem++;
                ResetPosition();
            }

            SelectedItem();

            if (Input.GetKey(KeyCode.W))
            {
                selectedCube.transform.position += Vector3.up * movingItemsSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                selectedCube.transform.position += Vector3.down * movingItemsSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                selectedCube.transform.position += Vector3.left * movingItemsSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                selectedCube.transform.position += Vector3.right * movingItemsSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                selectedCube.transform.position += Vector3.forward * movingItemsSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                selectedCube.transform.position += Vector3.back * movingItemsSpeed * Time.deltaTime;
            }
        }

        //this if statement increases the variable time, which is used to know when the player should regain control over his movement, after the castle door
        //has been opened
        if (Vector3.Distance(mainCamera.transform.position, mainCameraOriginalPosition) < 0.1f)
        {
            time = time + 1 * Time.fixedDeltaTime;
        }

        //when all pieces have benn placed, the player controll over his movement is disabled,
        //the camera moves, door opens, camera moves back to it s original position, waits for a small time and then the player regains controll over his movement
        if (puzzlePiecePlaced == 6)
        {
            //castleDoor.transform.position = castleDoorOpenedLocation.position;
            if(savePosition)
            {
                mainCameraOriginalPosition = mainCamera.transform.position;
                mainCameraOriginalRotation = mainCamera.transform.rotation;
                savePosition = false;
                GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
            }
            
            if(canMoveCamera)
            {
                mainCamera.transform.DOMove(openingCamera.transform.position, 100f * Time.fixedDeltaTime).SetEase(Ease.Linear);
                mainCamera.transform.DORotateQuaternion(openingCamera.transform.rotation, 50f * Time.fixedDeltaTime).SetEase(Ease.Linear);
            }
            
            if (Vector3.Distance(mainCamera.transform.position, openingCamera.transform.position) < 1f)
            {
                canMoveCamera = false;
                castleDoor.transform.DOMove(castleDoorOpenedLocation.position, 200f * Time.fixedDeltaTime).SetEase(Ease.Linear);
            }

            if (Vector3.Distance(castleDoor.transform.position, castleDoorOpenedLocation.transform.position) < 1f && !done)//(castleDoor.transform.position == castleDoorOpenedLocation.transform.position)
            {
                if (Vector3.Distance(mainCamera.transform.position, mainCameraOriginalPosition) < 0.1f)
                {
                    time = time + 1 * Time.fixedDeltaTime;
                    done = true;
                    Debug.Log(time);
                }

                if (!done)
                {
                    mainCamera.transform.DOMove(mainCameraOriginalPosition, 100f * Time.fixedDeltaTime);
                    mainCamera.transform.DORotateQuaternion(mainCameraOriginalRotation, 50f * Time.fixedDeltaTime);
                }
            }

            if(done && time > 5f)
            {
                mainCamera.transform.position = mainCameraOriginalPosition;
                mainCamera.transform.rotation = mainCameraOriginalRotation;
                GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>().enabled = true;
                GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = true;
                textUiInstruction.SetActive(false);
                puzzlePiecePlaced = 0;
                canMove++;
                canAsemble = false;
                selectedItem = 1;
                TurnLightsOff();
                GameObject.Find("ItemPuzzleScript").GetComponent<ItemPuzzle>().enabled = false;
            }
        }
    }

    void TurnLightsOff()
    {
        shieldLight.enabled = false;
        axeLight.enabled = false;
        dungeonRocksLight.enabled = false;
        vikingBodyLight.enabled = false;
        dragonSkullLight.enabled = false;
        yellowTreasureLight.enabled = false;
        boneAxeLight.enabled = false;
        humanSkullLight.enabled = false;
        knightShieldLight.enabled = false;
        shieldLight1.enabled = false;
        axeLight1.enabled = false;
        dungeonRocksLight1.enabled = false;
        vikingBodyLight1.enabled = false;
        dragonSkullLight1.enabled = false;
        yellowTreasureLight1.enabled = false;
        boneAxeLight1.enabled = false;
        humanSkullLight1.enabled = false;
        knightShieldLight1.enabled = false;
    }

    void SelectedItem()     
    {
        //resetting the lights
        TurnLightsOff();

        //if an item is selected and if it is collected it enables it's movement and interactions
        if (selectedItem == 0)
        {
            shieldLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (!shieldVikingPlaced && shieldVikingCollected)
            {
                selectedCube = shieldViking;
                shieldLight.enabled = true;
                shieldLight1.enabled = false;

                if (Vector3.Distance(shieldVikingCorrect.transform.position, shieldViking.transform.position) < 3f && !shieldVikingPlaced)
                {
                    shieldViking.transform.position = Vector3.Lerp(shieldViking.transform.position, shieldVikingCorrect.transform.position, 1 * Time.fixedDeltaTime);
                }
                if (Vector3.Distance(shieldVikingCorrect.transform.position, shieldViking.transform.position) < 0.3f)
                {
                    shieldViking.transform.position = shieldVikingCorrect.transform.position;
                    shieldViking.transform.rotation = shieldVikingCorrect.transform.rotation;
                    shieldVikingPlaced = true;
                    canReset0 = false;
                    selectedItem = 8;
                    puzzlePiecePlaced++;
                }
            } 
        }
        else if (selectedItem == 1)
        {
            axeLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (!axeVikingPlaced && axeVikingCollected)
            {
                selectedCube = axeViking;
                axeLight.enabled = true;
                axeLight1.enabled = false;

                if (Vector3.Distance(axeVikingCorrect.transform.position, axeViking.transform.position) < 3f && !axeVikingPlaced)
                {
                    axeViking.transform.position = Vector3.Lerp(axeViking.transform.position, axeVikingCorrect.transform.position, 1 * Time.fixedDeltaTime);
                }
                if (Vector3.Distance(axeVikingCorrect.transform.position, axeViking.transform.position) < 0.3f)
                {
                    axeViking.transform.position = axeVikingCorrect.transform.position;
                    axeViking.transform.rotation = axeVikingCorrect.transform.rotation;
                    axeViking.transform.localScale = axeVikingCorrect.transform.localScale;
                    axeVikingPlaced = true;
                    selectedItem = 8;
                    canReset1 = false;
                    puzzlePiecePlaced++;
                }
            }
        }
        else if (selectedItem == 2)
        {
            vikingBodyLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (!bodyVikingPlaced && bodyVikingCollected)
            {
                selectedCube = bodyViking;
                vikingBodyLight.enabled = true;
                vikingBodyLight1.enabled = false;

                if (Vector3.Distance(bodyVikingCorrect.transform.position, bodyViking.transform.position) < 3f && !bodyVikingPlaced)
                {
                    bodyViking.transform.position = Vector3.Lerp(bodyViking.transform.position, bodyVikingCorrect.transform.position, 1 * Time.fixedDeltaTime);
                }
                if (Vector3.Distance(bodyVikingCorrect.transform.position, bodyViking.transform.position) < 0.3f)
                {
                    bodyViking.transform.position = bodyVikingCorrect.transform.position;
                    bodyViking.transform.rotation = bodyVikingCorrect.transform.rotation;
                    bodyViking.transform.localScale = bodyVikingCorrect.transform.localScale;
                    bodyVikingPlaced = true;
                    selectedItem = 8;
                    canReset3 = false;
                    puzzlePiecePlaced++;
                }
            }
        }
        else if (selectedItem == 3)
        {
            dragonSkullLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (!dragonSkullPlaced && dragonSkullCollected)
            {
                selectedCube = dragonSkull;
                dragonSkullLight.enabled = true;
                dragonSkullLight1.enabled = false;

                if (Vector3.Distance(dragonSkullCorrect.transform.position, dragonSkull.transform.position) < 3f && !dragonSkullPlaced)
                {
                    dragonSkull.transform.position = Vector3.Lerp(dragonSkull.transform.position, dragonSkullCorrect.transform.position, 1 * Time.fixedDeltaTime);
                }
                if (Vector3.Distance(dragonSkullCorrect.transform.position, dragonSkull.transform.position) < 0.3f)
                {
                    dragonSkull.transform.position = dragonSkullCorrect.transform.position;
                    dragonSkull.transform.rotation = dragonSkullCorrect.transform.rotation;
                    dragonSkull.transform.localScale = dragonSkullCorrect.transform.localScale;
                    dragonSkullPlaced = true;
                    selectedItem = 8;
                    canReset4 = false;
                    puzzlePiecePlaced++;
                }
            }
        }
        else if (selectedItem == 4)
        {
            humanSkullLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (humanSkullCollected)
            {
                selectedCube = humanSkull;
                humanSkullLight.enabled = true;
                humanSkullLight1.enabled = false;
            }
        }
        else if (selectedItem == 5)
        {
            knightShieldLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (knightShieldCollected)
            {
                selectedCube = knightShield;
                knightShieldLight.enabled = true;
                knightShieldLight1.enabled = false;
            }
        }
        else if (selectedItem == 6)
        {
            yellowTreasureLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (!yellowTreasurePlaced && yellowTreasureCollected)
            {
                selectedCube = yellowTreasure;
                yellowTreasureLight.enabled = true;
                yellowTreasureLight1.enabled = false;


                if (Vector3.Distance(yellowTreasureCorrect.transform.position, yellowTreasure.transform.position) < 3f && !yellowTreasurePlaced)
                {
                    yellowTreasure.transform.position = Vector3.Lerp(yellowTreasure.transform.position, yellowTreasureCorrect.transform.position, 1 * Time.fixedDeltaTime);
                }
                if (Vector3.Distance(yellowTreasureCorrect.transform.position, yellowTreasure.transform.position) < 0.3f)
                {
                    yellowTreasure.transform.position = yellowTreasureCorrect.transform.position;
                    yellowTreasure.transform.rotation = yellowTreasureCorrect.transform.rotation;
                    yellowTreasure.transform.localScale = yellowTreasureCorrect.transform.localScale;
                    yellowTreasurePlaced = true;
                    selectedItem = 8;
                    canReset8 = false;
                    puzzlePiecePlaced++;
                }
            }
        }
        else if (selectedItem == 7)
        {
            dungeonRocksLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (!dungeonRocksPlaced && dungeonRocksCollected)
            {
                selectedCube = dungeonRocks;
                dungeonRocksLight.enabled = true;
                dungeonRocksLight1.enabled = false;

                if (Vector3.Distance(dungeonRocksCorrect.transform.position, dungeonRocks.transform.position) < 3f && !dungeonRocksPlaced)
                {
                    dungeonRocks.transform.position = Vector3.Lerp(dungeonRocks.transform.position, dungeonRocksCorrect.transform.position, 1 * Time.fixedDeltaTime);
                }
                if (Vector3.Distance(dungeonRocksCorrect.transform.position, dungeonRocks.transform.position) < 0.3f)
                {
                    dungeonRocks.transform.position = dungeonRocksCorrect.transform.position;
                    dungeonRocks.transform.rotation = dungeonRocksCorrect.transform.rotation;
                    dungeonRocks.transform.localScale = dungeonRocksCorrect.transform.localScale;
                    dungeonRocksPlaced = true;
                    selectedItem = 8;
                    canReset10 = false;
                    puzzlePiecePlaced++;
                }
            }
        }
        else if (selectedItem == 8)
        {
            boneAxeLight1.enabled = true;
            selectedCube = ghostSelectedCube;

            if (boneAxeCollected)
            {
                selectedCube = boneAxe;
                boneAxeLight.enabled = true;
                boneAxeLight1.enabled = false;
            }
        }
    }

    void ResetPosition()
    {
        if (canReset0 && shieldVikingCollected)
        {
            shieldViking.transform.position = shieldVikingInitial;
        }
        if (canReset1 && axeVikingCollected)
        {
            axeViking.transform.position = axeVikingInitial;
        }
        if (canReset3 && bodyVikingCollected)
        {
            bodyViking.transform.position = bodyVikingInitial;
        }
        if (canReset4 && dragonSkullCollected)
        {
            dragonSkull.transform.position = dragonSkullInitial;
        }
        if (canReset5 && humanSkullCollected)
        {
            humanSkull.transform.position = humanSkullInitial;
        }
        if (canReset6 && knightShieldCollected)
        {
            knightShield.transform.position = knightShieldInitial;
        }
        if (canReset8 && yellowTreasureCollected)
        {
            yellowTreasure.transform.position = yellowTreasureInitial;
        }
        if (canReset10 && dungeonRocksCollected)
        {
            dungeonRocks.transform.position = dungeonRocksInitial;
        }
        if (canReset11 && boneAxeCollected)
        {
            boneAxe.transform.position = boneAxeInitial;
        }
    }

    void SpawnCollectedObject()
    {
        if (shieldVikingCollected)
        {
            shieldViking.SetActive(true);
        }
        if (axeVikingCollected)
        {
            axeViking.SetActive(true);
        }
        if (bodyVikingCollected)
        {
            bodyViking.SetActive(true);
        }
        if (dragonSkullCollected)
        {
            dragonSkull.SetActive(true);
        }
        if (humanSkullCollected)
        {
            humanSkull.SetActive(true);
        }
        if (knightShieldCollected)
        {
            knightShield.SetActive(true);
        }
        if (yellowTreasureCollected)
        {
            yellowTreasure.SetActive(true);
        }
        if (dungeonRocksCollected)
        {
            dungeonRocks.SetActive(true);
        }
        if (boneAxeCollected)
        {
            boneAxe.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canAsemble = true;
        showArrow = false;
        textUiPressF.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canAsemble = false;
        textUiPressF.SetActive(false);
    }
}

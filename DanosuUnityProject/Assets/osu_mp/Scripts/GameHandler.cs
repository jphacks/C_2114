using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameHandler : MonoBehaviour
{
    // ----------------------------------------------------------------------------

    [Header("Objects")] public GameObject Circle; // Circle Object

    //[Header("Map")] public DefaultAsset MapFile; // Map file (.osu format), attach from editor
    public TextAsset MapFile; //FIXME: .osuファイルを.txtファイルに変更してInspector上に設定する必要があります。
    
    
    public AudioClip MainMusic; // Music file, attach from editor
    public List<AudioClip> GreatSoundList; // Hit sound
    public List<AudioClip> GoodSoundList; // Hit sound

    // ----------------------------------------------------------------------------

    const int SPAWN = -100; // Spawn coordinates for objects

    public static double timer = 0; // Main song timer
    public static int ApprRate = 600; // Approach rate (in ms)
    private int DelayPos = 0; // Delay song position

    public static int ClickedCount = 0; // Clicked objects counter
    private static int ObjCount = 0; // Spawned objects counter

    [SerializeField] private List<GameObject> CircleList; // Circles List
    private static string[] LineParams; // Object Parameters

    // Audio stuff
    private AudioSource Sounds;
    private AudioSource Music;
    public static AudioSource pSounds;
    public static AudioClip pHitSound;

    // Other stuff
    private Camera MainCamera;
    private GameObject rightHandCursorTrail;
    private GameObject leftHandCursorTrail;
    private GameObject rightFootCursorTrail;
    private GameObject leftFootCursorTrail;
    private Vector3 MousePosition;
    private Ray MainRay;
    private RaycastHit MainHit;

    [SerializeField] private GameObject cursorRightHandTargetGameObject;
    [SerializeField] private GameObject cursorLeftHandTargetGameObject;
    [SerializeField] private GameObject cursorRightFootTargetGameObject;
    [SerializeField] private GameObject cursorLeftFootTargetGameObject;
    [SerializeField] private GameObject circleObjectParentGameObject;
    [SerializeField] private float addCirclePositionY;

    [SerializeField] private List<GameObject> greatPrefabList;
    [SerializeField] private List<GameObject> goodPrefabList;

    private int greatEffectIndex;
    private int goodEffectIndex;

    [SerializeField] private int greatScoreRate = 100;
    [SerializeField] private int goodScoreRate = 50;
    [SerializeField] private int chainScoreRate = 5;

    //Application.dataPath 以下のOsu!の譜面データのファイルパスを保存します。
    [SerializeField] private List<string> mapFilePathList;


    private enum OPERATION_MODE
    {
        MOUSE_CURSOR,
        TDPT,
    }

    [SerializeField] private OPERATION_MODE OperationMode;
    [SerializeField] private float CircleGreatRadius = 0.15f;
    [SerializeField] private SoundManager soundManager;

    private void Start()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Music = GameObject.Find("Music Source").GetComponent<AudioSource>();
        Sounds = gameObject.GetComponent<AudioSource>();
        rightHandCursorTrail = GameObject.Find("RightHandCursorTrail");
        leftHandCursorTrail = GameObject.Find("LeftHandCursorTrail");
        rightFootCursorTrail = GameObject.Find("RightFootCursorTrail");
        leftFootCursorTrail = GameObject.Find("LeftFootCursorTrail");
        Music.clip = MainMusic;
        pSounds = Sounds;
        CircleList = new List<GameObject>();
        //ReadCircles(AssetDatabase.GetAssetPath(MapFile));
        //Debug.Log(Application.dataPath+mapFilePathList[0]);
        ReadCircles(Application.dataPath+mapFilePathList[0]);

        if (!PlayerPrefs.HasKey("GreatEffect"))
        {
            PlayerPrefs.SetInt("GreatEffect", 0);
            greatEffectIndex = 0;
        }
        else
        {
            greatEffectIndex = PlayerPrefs.GetInt("GreatEffect");
        }

        if (!PlayerPrefs.HasKey("GoodEffect"))
        {
            PlayerPrefs.SetInt("GoodEffect", 0);
            goodEffectIndex = 0;
        }
        else
        {
            goodEffectIndex = PlayerPrefs.GetInt("GoodEffect");
        }
    }

    // MAP READER

    void ReadCircles(string path)
    {
        StreamReader reader = new StreamReader(path);
        string line;

        // Skip to [HitObjects] part
        while (true)
        {
            if (reader.ReadLine() == "[HitObjects]")
                break;
        }

        int TotalLines = 0;

        // Count all lines
        while (!reader.EndOfStream)
        {
            reader.ReadLine();
            TotalLines++;
        }

        reader.Close();

        reader = new StreamReader(path);

        // Skip to [HitObjects] part again
        while (true)
        {
            if (reader.ReadLine() == "[HitObjects]")
                break;
        }

        // Sort objects on load
        int ForeOrder = TotalLines + 2; // Sort foreground layer
        int BackOrder = TotalLines + 1; // Sort background layer
        int ApproachOrder = TotalLines; // Sort approach circles layer

        // Some crazy Z axis modifications for sorting
        string TotalLinesStr = "0.";
        for (int i = 3; i > TotalLines.ToString().Length; i--)
            TotalLinesStr += "0";
        TotalLinesStr += TotalLines.ToString();
        float Z_Index = -(float.Parse(TotalLinesStr));

        while (!reader.EndOfStream)
        {
            // Uncomment to skip sliders
            /*while (true)
            {
                line = reader.ReadLine();
                if (line != null)
                {
                    if (!line.Contains("|"))
                        break;
                }
                else
                    break;
            }*/

            line = reader.ReadLine();
            if (line == null)
                break;

            LineParams = line.Split(','); // Line parameters (X&Y axis, time position)

            // Sorting configuration
            GameObject CircleObject = Instantiate(Circle, new Vector2(SPAWN, SPAWN), Quaternion.identity);
            CircleObject.transform.parent = circleObjectParentGameObject.transform;
            CircleObject.GetComponent<Circle>().Fore.sortingOrder = ForeOrder;
            CircleObject.GetComponent<Circle>().Back.sortingOrder = BackOrder;
            CircleObject.GetComponent<Circle>().Appr.sortingOrder = ApproachOrder;
            CircleObject.transform.localPosition += new Vector3((float) CircleObject.transform.localPosition.x,
                (float) CircleObject.transform.localPosition.y, (float) Z_Index);
            CircleObject.transform.SetAsFirstSibling();
            ForeOrder--;
            BackOrder--;
            ApproachOrder--;
            Z_Index += 0.01f;

            int FlipY = 384 - int.Parse(LineParams[1]); // Flip Y axis

            int AdjustedX = Mathf.RoundToInt(Screen.height * 1.333333f); // Aspect Ratio

            // Padding
            float Slices = 8f;
            float PaddingX = AdjustedX / Slices;
            float PaddingY = Screen.height / Slices;

            // Resolution set
            float NewRangeX = ((AdjustedX - PaddingX) - PaddingX);
            float NewValueX = ((int.Parse(LineParams[0]) * NewRangeX) / 512f) + PaddingX +
                              ((Screen.width - AdjustedX) / 2f);
            float NewRangeY = Screen.height;
            float NewValueY = ((FlipY * NewRangeY) / 512f) + PaddingY;

            Vector3 MainPos =
                MainCamera.ScreenToWorldPoint(new Vector3(NewValueX, NewValueY,
                    0)); // Convert from screen position to world position
            Circle MainCircle = CircleObject.GetComponent<Circle>();

            MainCircle.Set(MainPos.x, MainPos.y + addCirclePositionY, CircleObject.transform.position.z,
                int.Parse(LineParams[2]) - ApprRate);

            CircleList.Add(CircleObject);
        }

        GameStart();
    }

    // END MAP READER

    private void GameStart()
    {
        Application.targetFrameRate = -1; // Unlimited framerate
        Music.Play();
        StartCoroutine(UpdateRoutine()); // Using coroutine instead of Update()
    }

    private IEnumerator UpdateRoutine()
    {
        while (true)
        {
            // 曲の再生が終わっていたらシーン遷移します。
            if (Music.time + Time.deltaTime > Music.clip.length && Music.isPlaying)
            {
                PlayerPrefs.SetInt("State", 3);
                SceneManager.LoadScene("Studio3D");
            }

            if (ObjCount >= CircleList.Count)
            {
                yield return null;
                continue;
            }

            timer = (Music.time * 1000); // Convert timer
            DelayPos = (CircleList[ObjCount].GetComponent<Circle>().PosA);
            //MainRay = MainCamera.ScreenPointToRay(Input.mousePosition);

            //マウスカーソルの代わりにVRMアバターの右手をノーツを叩けたかの判定に使用します。
            // Debug.Log(Input.mousePosition);
            // Debug.Log(cursorTailPositionGameObject.transform.position);
            // Debug.Log(MainCamera.WorldToScreenPoint(cursorTailPositionGameObject.transform.position));

            // FIXME: 右手、左手、右足、左足それぞれのノーツの当たり判定をコピーペーストで書いています…。
            Vector3 tmpCursor;
            tmpCursor = MainCamera.WorldToScreenPoint(rightHandCursorTrail.transform.position);
            tmpCursor.z = 0;
            MainRay = MainCamera.ScreenPointToRay(tmpCursor);
            CursorCollideDetection(rightHandCursorTrail);

            // tmpCursor = MainCamera.WorldToScreenPoint(rightFootCursorTrail.transform.position);
            // tmpCursor.z = 0;
            // MainRay = MainCamera.ScreenPointToRay(tmpCursor);
            // CursorCollideDetection(rightFootCursorTrail);

            tmpCursor = MainCamera.WorldToScreenPoint(leftHandCursorTrail.transform.position);
            tmpCursor.z = 0;
            MainRay = MainCamera.ScreenPointToRay(tmpCursor);
            CursorCollideDetection(leftHandCursorTrail);

            // tmpCursor = MainCamera.WorldToScreenPoint(leftFootCursorTrail.transform.position);
            // tmpCursor.z = 0;
            // MainRay = MainCamera.ScreenPointToRay(tmpCursor);
            // CursorCollideDetection(leftFootCursorTrail);


            // Spawn object
            if (timer >= DelayPos)
            {
                CircleList[ObjCount].GetComponent<Circle>().Spawn();
                ObjCount++;
            }

            switch (OperationMode)
            {
                case OPERATION_MODE.MOUSE_CURSOR:
                    // Cursor trail movement
                    MousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                    rightHandCursorTrail.transform.position =
                        new Vector3(MousePosition.x + 0.3f, MousePosition.y + 0.3f, -9);
                    rightFootCursorTrail.transform.position =
                        new Vector3(MousePosition.x + 0.3f, MousePosition.y - 0.3f, -9);
                    leftHandCursorTrail.transform.position =
                        new Vector3(MousePosition.x - 0.3f, MousePosition.y + 0.3f, -9);
                    leftFootCursorTrail.transform.position =
                        new Vector3(MousePosition.x - 0.3f, MousePosition.y - 0.3f, -9);
                    break;
                case OPERATION_MODE.TDPT:
                    Vector3 position;
                    position = cursorRightHandTargetGameObject.transform.position;
                    rightHandCursorTrail.transform.position = new Vector3(position.x, position.y, -9);
                    // position = cursorRightFootTargetGameObject.transform.position;
                    // rightFootCursorTrail.transform.position = new Vector3(position.x, position.y, -9);
                    position = cursorLeftHandTargetGameObject.transform.position;
                    leftHandCursorTrail.transform.position = new Vector3(position.x, position.y, -9);
                    // position = cursorLeftFootTargetGameObject.transform.position;
                    // leftFootCursorTrail.transform.position = new Vector3(position.x, position.y, -9);

                    break;
                default:
                    break;
            }

            yield return null;
        }
    }

    private void CursorCollideDetection(GameObject cursorGameObject)
    {
        if (Physics.Raycast(MainRay, out MainHit))
        {
            if (MainHit.collider.name == "Circle(Clone)" &&
                timer >= MainHit.collider.gameObject.GetComponent<Circle>().PosA + ApprRate)
            {
                //GOOD, GREAT判定
                Vector2 circlePosition2D;
                var hitCirclePosition = MainHit.collider.gameObject.transform.position;
                circlePosition2D = new Vector2(hitCirclePosition.x, hitCirclePosition.y);
                Vector2 cursorPosition2D;
                cursorPosition2D = new Vector2(cursorGameObject.transform.position.x,
                    cursorGameObject.gameObject.transform.position.y);
                if (Vector2.Distance(circlePosition2D, cursorPosition2D) <= CircleGreatRadius)
                {
                    GameObject greatObject = Instantiate(greatPrefabList[greatEffectIndex],
                        MainHit.collider.gameObject.transform);
                    greatObject.transform.Translate(Vector3.back);
                    greatObject.transform.parent = circleObjectParentGameObject.transform;
                    Destroy(greatObject, 1.0f);
                    PlayerPrefs.SetInt("Great", PlayerPrefs.GetInt("Great") + 1);
                    PlayerPrefs.SetInt("Score",
                        PlayerPrefs.GetInt("Score") + greatScoreRate +
                        PlayerPrefs.GetInt("Chain") * chainScoreRate * 2);
                    PlayerPrefs.SetInt("Chain", PlayerPrefs.GetInt("Chain") + 1);
                    PlayerPrefs.SetInt("MaxChain",
                        Mathf.Max(PlayerPrefs.GetInt("Chain"), PlayerPrefs.GetInt("MaxChain")));
                    soundManager.GreatSE();
                }
                else
                {
                    GameObject goodObject = Instantiate(goodPrefabList[goodEffectIndex],
                        MainHit.collider.gameObject.transform);
                    goodObject.transform.parent = circleObjectParentGameObject.transform;
                    goodObject.transform.Translate(Vector3.back);
                    Destroy(goodObject, 1.0f);
                    PlayerPrefs.SetInt("Good", PlayerPrefs.GetInt("Good") + 1);
                    PlayerPrefs.SetInt("Score",
                        PlayerPrefs.GetInt("Score") + goodScoreRate + PlayerPrefs.GetInt("Chain") * chainScoreRate);
                    PlayerPrefs.SetInt("Chain", PlayerPrefs.GetInt("Chain") + 1);
                    PlayerPrefs.SetInt("MaxChain",
                        Mathf.Max(PlayerPrefs.GetInt("Chain"), PlayerPrefs.GetInt("MaxChain")));
                    soundManager.GoodSE();
                }

                MainHit.collider.gameObject.GetComponent<Circle>().Got();
                MainHit.collider.enabled = false;
                ClickedCount++;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //管道预制体
    public GameObject pipe;
    //生成管道的位置
    public Transform minYPosObj;
    public Transform maxYPosObj;
    public Transform genXPosObj;
    private float minPipesY;
    private float maxPipesY;
    //生成管道的频率
    private float genPipesRate = 3.0f;
    //管道初始化的点，相机视野之外
    private Vector2 initPos = new Vector2(-20.0f, 0f);
    //用列表储存生成的管道
    private List<GameObject> pipes = new List<GameObject>();
    //计时器
    private float timer = 0;
    //总共的管道数
    private const int pipesTotalCount = 5;
    //当前；列表的下标
    public int currIndex = 0;
    //通过的管道的个数
    public int passPipeCount = 0;

    //游戏结束UI
    public GameObject gameOverTip;
    //计数UI
    public Text countText;
    //准备UI
    public GameObject readyTip;

    /// <summary>
    /// 枚举游戏的状态
    /// </summary>
    public enum GameState {
        READY, START, PAUSE, OVER,
    }

    [HideInInspector] public GameState gameState = GameState.OVER;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        minPipesY = minYPosObj.position.y;
        maxPipesY = maxYPosObj.position.y;
        InitPipes();
        gameOverTip.SetActive(false);
        gameState = GameState.READY;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.READY:
                GameReady();
                break;
            case GameState.START:
                GameStart();
                break;
            case GameState.PAUSE:
                GamePause();
                break;
            case GameState.OVER:
                ReStart();
                break;
        }
    }

    /// <summary>
    /// 游戏准备
    /// </summary>
    private void GameReady()
    {
        readyTip.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            readyTip.SetActive(false);
            gameState = GameState.START;
        }
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    private void GameStart()
    {

        if(GameState.OVER != gameState && timer <= 0)
        {
            timer = genPipesRate;
            UpdatePipesPos();
            currIndex = (currIndex + 1) % pipesTotalCount;
        }
        if(Input.GetMouseButton(1))
        {
            gameState = GameState.PAUSE;
        }
        timer -= Time.deltaTime;
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    private void GamePause()
    {
        readyTip.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            readyTip.SetActive(false);
            gameState = GameState.START;
        }    
    }
     
    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        if (GameState.OVER == gameState)
            return;
        gameState = GameState.OVER;
        gameOverTip.SetActive(true);
    }

    /// <summary>
    /// 通过管道
    /// </summary>
    public void PassPipe()
    {
        passPipeCount++;
        countText.text = "Count : " + passPipeCount.ToString();
    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    private void ReStart()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 初始化管道列表
    /// </summary>
    private void InitPipes()
    {
        for(int i = 0; i < pipesTotalCount; i++)
        {
            GameObject newPipe = Instantiate(pipe, initPos, Quaternion.identity);
            pipes.Add(newPipe);
        }
    }

    /// <summary>
    /// 更新管道位置
    /// </summary>
    private void UpdatePipesPos()
    {
        float yPos = Random.Range(minPipesY, maxPipesY);
        pipes[currIndex].transform.position = new Vector2(genXPosObj.position.x, yPos);
    }

}

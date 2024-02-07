using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IFoodContainer
{
    public static GameManager Instance;

    public int AngleId;

    public TMP_Text angleIdText;
    public TMP_Text HeldFoodItemText;

    public FoodItem HeldFoodItem;

    public List<Station> Stations;


    public GameObject GameOverUI;
    public TMP_Text GameOverText;

    public float GameTimer;
    public int WaitingCustomers;
    public int Served;

    public bool isFinished = false;


    private void Awake()
    {
        Instance = this;
        GameTimer = 0;
        Time.timeScale = .02f;
        Invoke(nameof(DelayStart), 10 * Time.timeScale);
    }

    void DelayStart()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        GameTimer += Time.deltaTime;

        // if(Input.GetKeyDown(KeyCode.A))
        // {
        //     MoveLeft();
        // }
        //
        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     MoveRight();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     InteractButton();
        // }
        //
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     ServeButton();
        // }
    }

    public void InteractButton()
    {
        Stations[AngleId].OnInteract();
    }

    public void ServeButton()
    {
    }

    public void MoveLeft()
    {
        AngleId -= 1;
        if (AngleId < 0)
        {
            AngleId += 8;
        }

        angleIdText.SetText(AngleId.ToString());
    }

    public void MoveRight()
    {
        AngleId += 1;
        if (AngleId > 7)
        {
            AngleId -= 8;
        }

        angleIdText.SetText(AngleId.ToString());
    }

    // Food
    public void AddFood(FoodItem food)
    {
        HeldFoodItemText.SetText("Player Held Item: " + food.name);

        HeldFoodItem = food;
    }

    public FoodItem RemoveFood()
    {
        HeldFoodItemText.SetText("Player Held Item: Nothing");

        var temp = HeldFoodItem;
        HeldFoodItem = null;
        return temp;
    }


    public void GameOver()
    {
        GameOverUI.SetActive(true);

        var stringBuild = new StringBuilder();

        stringBuild.AppendLine("Game Over!");

        stringBuild.AppendLine("Time Survived: " + GameTimer);
        stringBuild.AppendLine("Served: " + Served);

        GameOverText.SetText(stringBuild.ToString());

        Time.timeScale = 0.02f;
        isFinished = true;
        //Invoke(nameof(OnRestartGame), 10f*Time.timeScale);
    }

    public void OnRestartGame()
    {
        return;

        Debug.Log("restart press");
        if (!isFinished)
        {
            return;
        }

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
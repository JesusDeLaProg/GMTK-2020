using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HealthDisplay Health;
    public LaserDisplay Laser;
    public Image FadeInOutMask;

    public AnimController dialogue;
    public PlayerController pc => GameObject.FindGameObjectWithTag("Spaceship").GetComponent<PlayerController>();

    public Dialogue GameStartDialogue;
    public Dialogue GameOverDialogue;
    public Dialogue LevelWinDialogue;

    public bool LaserReady{
        get{
            return Laser.laser;
        }
        set{
            Laser.laser = value;
        }
    }

    public int HitPoints{
        get{
            return Health.health;
        }
        set{
            Health.health = value;
            if(value == 0){
                EndGame();
            }
        }
    }

    public static GameManager instance {get;private set;}

    private int level = -1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        GameManager.instance = this;
        StartCoroutine(Health.FuelEmpty());
        SceneManager.sceneLoaded += StartLevel;
        StartCoroutine(LoadLevel("Random Map", 0f, 1f));
    }

    public void StartLevel(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Random Map")
        {
            ++level;
            Health.health = 5;
            ResetUI();

            var mapBuilder = GameObject.FindGameObjectWithTag("Map Builder").GetComponent<MapBuilder>();
            mapBuilder.GasStationSpawnDistance = 50 + 15 * level;
            mapBuilder.Seed = UnityEngine.Random.value;

            dialogue = GameObject.FindObjectOfType<AnimController>();
            dialogue.Dialogue = GameStartDialogue;
            dialogue.OnDialogueEnd = () => pc.Active = true;
            dialogue.StartAnim = true;
        }
    }
    
    public void ResetUI()
    {
        foreach(Image h in Health.Healths)
        {
            h.color = Color.white;
        }
    }

    public void EndLevel()
    {
        Debug.Log("Bravo !");
        dialogue.Dialogue = LevelWinDialogue;
        StartCoroutine(pc.Stop(1));
        dialogue.OnDialogueEnd = () => {
            StartCoroutine(LoadLevel("Random Map", 3f, 1));
        };
        dialogue.StartAnim = true;
    }

    public void EndGame()
    {
        Debug.Log("RIP !");

        GetComponent<AudioSource>().Play();
        pc.setSpriteDeadShip();
        dialogue.Dialogue = GameOverDialogue;
        StartCoroutine(pc.Stop(1));
        dialogue.OnDialogueEnd = () =>
        {
            StartCoroutine(LoadLevel("Random Map", 3f, 1f));
        };
        dialogue.StartAnim = true;
    }

    public void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            EndGame();
        }

    }

    private IEnumerator LoadLevel(string levelName, float durationOut, float durationIn)
    {
        var start = DateTime.Now;
        var color = Color.black;
        while((DateTime.Now - start).TotalSeconds < durationOut)
        {
            color = FadeInOutMask.color;
            FadeInOutMask.color = new Color(color.r, color.g, color.b, (float)((DateTime.Now - start).TotalSeconds / durationOut));
            yield return new WaitForFixedUpdate();
        }
        color = FadeInOutMask.color;
        FadeInOutMask.color = new Color(color.r, color.g, color.b, 255);
        SceneManager.LoadScene(levelName);
        start = DateTime.Now;
        while ((DateTime.Now - start).TotalSeconds < durationIn)
        {
            color = FadeInOutMask.color;
            FadeInOutMask.color = new Color(color.r, color.g, color.b, (float)(1 - (DateTime.Now - start).TotalSeconds / durationIn));
            yield return new WaitForFixedUpdate();
        }
        color = FadeInOutMask.color;
        FadeInOutMask.color = new Color(color.r, color.g, color.b, 0);
    }
}

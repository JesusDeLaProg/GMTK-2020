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
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Canvas"));
        GameManager.instance = this;
        StartCoroutine(Health.FuelEmpty());
        SceneManager.sceneLoaded += StartLevel;
        SceneManager.LoadScene("Random Map");
    }

    public void StartLevel(Scene scene, LoadSceneMode mode)
    {
        Health.health = 5;
        dialogue = GameObject.FindObjectOfType<AnimController>();
        dialogue.Dialogue = GameStartDialogue;
        dialogue.OnDialogueEnd = () => pc.Active = true;
        dialogue.StartAnim = true;
    }
    
    public void SetUI(){
        if(Input.GetKeyDown("space")){
           Health.health--;   
        }
    }

    public void EndLevel()
    {
        Debug.Log("Bravo !");
        dialogue.Dialogue = LevelWinDialogue;
        StartCoroutine(pc.Stop(1));
        pc.Active = false;
        dialogue.OnDialogueEnd = () => {
            StartCoroutine(LoadLevel("Random Map"));
        };
        dialogue.StartAnim = true;
    }

    public void EndGame()
    {
        Debug.Log("RIP !");
        GetComponent<AudioSource>().Play();
        pc.setSpriteDeadShip();
        dialogue.Dialogue = GameOverDialogue;
        pc.Active = false;
        dialogue.OnDialogueEnd = () =>
        {
            StartCoroutine(LoadLevel("Random Map"));
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

    private IEnumerator LoadLevel(string levelName)
    {
        var duration = 3f;
        var start = DateTime.Now;
        while((DateTime.Now - start).TotalSeconds < duration)
        {
            var color = FadeInOutMask.color;
            FadeInOutMask.color = new Color(color.r, color.g, color.b, (float)((DateTime.Now - start).TotalSeconds / duration));
            yield return new WaitForFixedUpdate();
        }
        SceneManager.LoadScene(levelName);
        start = DateTime.Now;
        duration = 1f;
        while ((DateTime.Now - start).TotalSeconds < duration)
        {
            var color = FadeInOutMask.color;
            FadeInOutMask.color = new Color(color.r, color.g, color.b, (float)(1 - (DateTime.Now - start).TotalSeconds / duration));
            yield return new WaitForFixedUpdate();
        }
    }
}

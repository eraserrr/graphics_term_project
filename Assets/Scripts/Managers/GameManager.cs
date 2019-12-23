using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //--------------------------------------------------------
    // Game variables

    public static int Level = 0;
    public static int lives = 3;

	public enum GameState { Init, Game, Dead, Scores }
	public static GameState gameState;

    private GameObject thanos;
    private GameObject captain;
    private GameObject spyder;
    private GameObject thor;
    private GameObject hulk;
    private GameGUINavigation gui;

	public static bool scared;
    static public int score;

	public float scareLength;
	private float _timeToCalm;
    private float stopTime = 3.0f;

    public float SpeedPerLevel;
    
    //-------------------------------------------------------------------
    // singleton implementation
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    //-------------------------------------------------------------------
    // function definitions

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)   
                Destroy(this.gameObject);
        }

        AssignGhosts();
    }

	void Start () 
	{
		gameState = GameState.Init;
	}

    void OnLevelWasLoaded()
    {
        if (Level == 0) lives = 3;

        Debug.Log("Level " + Level + " Loaded!");
        AssignGhosts();
        ResetVariables();


        // Adjust Ghost variables!
        captain.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        spyder.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        hulk.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        thor.GetComponent<GhostMove>().speed += Level * SpeedPerLevel;
        thanos.GetComponent<PlayerController>().speed += Level*SpeedPerLevel/2;
    }

    private void ResetVariables()
    {
        _timeToCalm = 0.0f;
        scared = false;
        PlayerController.killstreak = 0;
    }

    // Update is called once per frame
	void Update () 
	{
		if(scared && _timeToCalm <= Time.time)
			CalmGhosts();
        //if (_timeToStop <= stopTime)
            //StopGhosts();
	}

	public void ResetScene()
	{
        CalmGhosts();

		thanos.transform.position = new Vector3(15f, 11f, 0f);
		thor.transform.position = new Vector3(15f, 20f, 0f);
		captain.transform.position = new Vector3(14.5f, 17f, 0f);
		spyder.transform.position = new Vector3(16.5f, 17f, 0f);
		hulk.transform.position = new Vector3(12.5f, 17f, 0f);

        thanos.GetComponent<PlayerController>().ResetDestination();
        thor.GetComponent<GhostMove>().InitializeGhost();
        captain.GetComponent<GhostMove>().InitializeGhost();
        spyder.GetComponent<GhostMove>().InitializeGhost();
        hulk.GetComponent<GhostMove>().InitializeGhost();

        gameState = GameState.Init;  
        gui.H_ShowReadyScreen();

	}

	public void ToggleScare()
	{
		if(!scared)	ScareGhosts();
		else 		CalmGhosts();
	}

	public void ScareGhosts()
	{
		scared = true;
		captain.GetComponent<GhostMove>().Frighten();
		spyder.GetComponent<GhostMove>().Frighten();
		hulk.GetComponent<GhostMove>().Frighten();
		thor.GetComponent<GhostMove>().Frighten();
		_timeToCalm = Time.time + scareLength;

        Debug.Log("Ghosts Scared");
	}

	public void CalmGhosts()
	{
		scared = false;
		captain.GetComponent<GhostMove>().Calm();
		thor.GetComponent<GhostMove>().Calm();
		spyder.GetComponent<GhostMove>().Calm();
		hulk.GetComponent<GhostMove>().Calm();
	    PlayerController.killstreak = 0;
    }
/*
    public void StopGhosts()
    {
        blinky.GetComponent<GhostMove>().Stop();
        pinky.GetComponent<GhostMove>().Stop();
        inky.GetComponent<GhostMove>().Stop();
        clyde.GetComponent<GhostMove>().Stop();
    }
*/
    void AssignGhosts()
    {
        // find and assign ghosts
        thor = GameObject.Find("thor");
        spyder = GameObject.Find("spyder");
        captain = GameObject.Find("captain");
        hulk = GameObject.Find("hulk");
        thanos = GameObject.Find("thanos");

        if (thor == null || spyder == null || captain == null || hulk == null) Debug.Log("One of ghosts are NULL");
        if (thanos == null) Debug.Log("Pacman is NULL");

        gui = GameObject.FindObjectOfType<GameGUINavigation>();

        if(gui == null) Debug.Log("GUI Handle Null!");

    }

    public void LoseLife()
    {
        lives--;
        gameState = GameState.Dead;
    
        // update UI too
        UIScript ui = GameObject.FindObjectOfType<UIScript>();
        Destroy(ui.lives[ui.lives.Count - 1]);
        ui.lives.RemoveAt(ui.lives.Count - 1);
    }

    public static void DestroySelf()
    {

        score = 0;
        Level = 0;
        lives = 3;
        Destroy(GameObject.Find("Game Manager"));
    }
}

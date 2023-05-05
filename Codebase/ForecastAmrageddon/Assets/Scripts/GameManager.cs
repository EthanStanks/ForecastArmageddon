using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;



    [Header("Saving Member Fields")]


    // stuff to save
    // current map the player is playing on
    public int playingMapNum;
    // levels complete
    public bool isLevelOneComplete = false;
    public bool isLevelTwoComplete = false;
    public bool isLevelThreeComplete = false;
    // level's highscore
    public int lvlOneHighScore = 0;
    public int lvlTwoHighScore = 0;
    public int lvlThreeHighScore = 0;
    // show tutorial popup
    public bool isShowTutorial = true;
    // audio saving
    public float masterVolume = 50;
    public float sfxVolume = 50;
    public float musicVolume = 50;



    [Header("Prefabs")]


    // new tower card prefab
    public GameObject newTowerCard; // for creating a new tower card

    [Header("- Tower Prefabs")]
    // tower prefabs
    public GameObject basicTowerPrefab; // the basic tower prefab
    public GameObject sniperTowerPrefab; // the sniper tower prefab
    public GameObject machineGunTowerPrefab; // the machine gun tower prefab
    public GameObject supportTowerPrefab; // the support tower prefab
    public GameObject shotgunTowerPrefab; // the shotgun tower prefab

    [Header("- Tower Bullets Prefabs")]
    // tower bullet prefabs
    public GameObject basicTowerBulletPrefab; // the bullet prefab for the basic tower
    public GameObject pelletShotgunBulletPrefab; // the special pellet bullet for 

    [Header("- Enemy Prefabs")]
    // enemy prefabs
    public GameObject DeathExplosion;
    public GameObject basicEnemyPrefab; // the basic enemy prefab
    public GameObject fastEnemyPrefab; // The Fast Enemy
    public GameObject tankEnemyPrefab; // The Tank Enemy
    public GameObject tutorialEnemyPrefab; // Tutorial Enemy
    public GameObject towerKillerEnemyPrefab; // tower killer enemy
    public GameObject sludgerEnemyPrefab; // sludger enemy
    public GameObject baseKillerEnemyPrefab; // base kill enemy
    public GameObject acidEnemyPrefab; // acid enemy

    [Header("- Enemy Boss Prefabs")]
    // enemy boss prefabs
    public GameObject bufferBossPrefab; // buffer enemy boss
    public GameObject summonerBossPrefab; // summoner enemy boss
    public GameObject transporterBossPrefab; // transporter enemy boss

    [Header("- Enemy Bullets Prefabs")]
    // enemy bullet prefabs
    public GameObject basicEnemyBulletPrefab; // the bullet prefab for the basic enemy
    public GameObject enemyAcidBulletPrefab; // the acid bullet prefab

    [Header("- Weather Prefabs")]
    // weather prefabs
    public GameObject towerLightning; // the obj that strikes a tower with lightning
    // map one prefabs
    public GameObject rainObjectMap1; // the object with the rain particles
    public GameObject sunnyObjectMap1; // the object with the sun particle
    public GameObject windObjectMap1; // the obj with wind particle
    public GameObject snowObjectMap1; // obj with snow particle
    public GameObject lightningMap1; // obj with lightning particles
    //map two prefabs
    public GameObject rainObjectMap2; // the object with the rain particles
    public GameObject sunnyObjectMap2; // the object with the sun particle
    public GameObject windObjectMap2; // the obj with wind particle
    public GameObject snowObjectMap2; // obj with snow particle
    public GameObject lightningMap2; // obj with lightning particles
    //map three prefabs
    public GameObject rainObjectMap3; // the object with the rain particles
    public GameObject sunnyObjectMap3; // the object with the sun particle
    public GameObject windObjectMap3; // the obj with wind particle
    public GameObject snowObjectMap3; // obj with snow particle
    public GameObject lightningMap3; // obj with lightning particles



    [Header("Game Member Fields")]


    // GAME
    [Header("- Auto Member Fields")]
    // load scene obj
    public GameObject loaderObj;
    [Header("- Score Member Fields")]
    //player score
    public int playerScore; // used to keep track of the player score
    // score tracker
    public int scoreTracker; // used to keep track of the player score to see if it changes or stayed the same
    // cost to play cards
    public int scoreCost;
    // score count text
    public Text scoreCount; // used to access the text for the player score
    [Header("- World Member Fields")]
    // camera
    public Camera mainCamera; // used to access the main camera
    public Light worldLight; // used to access the light source on each map
    [Header("- Base Member Fields")]
    // Base
    public GameObject baseObj; // used to access the map's base
    // Base health
    public int baseHealthCurrent; // used to keep track of the base health during gameplay
    // Base health
    public int baseHealthOriginal; // used to remember what the starting base health was at the start of the game
    // base health bar
    public Image baseHealthBar; // used to access the fill amount for the base health bar
    // base health text
    public Text baseHealthText; // used to access the text for the base health
    [Header("- Round Member Fields")]
    // round count
    public int roundCount; // used to keep track of the current round
    // round count text
    public Text roundCountText; // used to access the round count text
    // enemy count
    public int enemyCount; // used to keep track of the current amount of enemies in play
    [Header("- Spawner Member Fields")]
    // enemy spawn
    public GameObject enemySpawner; // used to access the spawner for enemies in the map
    public GameObject ambushSpawner;
    [Header("- Tower Member Fields")]
    // hit tower spot
    private Transform mTowerSpotSelection; // used to keep track of the current glowing tower spot
    [Header("- Active Lists Member Fields")]
    // list of all towers in game
    public List<GameObject> activeTowerInGame = new List<GameObject>();
    // list of all enemies in game
    public List<GameObject> activeEnemiesInGame = new List<GameObject>();


    [Header("Trash Stuff")]


    // TRASH CAN
    // max trash count
    public int maxTrash; // used to set the maxium number of potentially trashed cards
    // trashcan
    public GameObject trashCan; // used to access the trash can obj
    // trash text
    public Text trashText; // used to access the text on the trash can
    // trashed count
    public int trashCount; // used to keep track of the current trashed amount
    // trash button
    public GameObject trashButton; // the button for trashing cards
    // trash lock
    public GameObject trashLockedButton; // the overlay that block the trash button if trash is full
    public Image trashSprite;
    // trash images
    public Sprite trash0;
    public Sprite trash1;
    public Sprite trash2;
    public Sprite trash3;
    public Sprite trash4;
    public Sprite trash5;
    public Sprite trash6;
    public Sprite trash7;
    public Sprite trash8;
    public Sprite trash9;
    public Sprite trash10;




    [Header("Card Stuff")]


    [Header("- Each Cards Script, Obj, & Blocker")]
    // CARD
    // card 1
    public CardStats cardOneScript; // used to get the script from card one
    public GameObject slotOneCard; // used to access the current card in slot one
    public GameObject cardBlock1; // used to access the card blocker for slot one
    // card 2
    public CardStats cardTwoScript; // used to get the script from card two
    public GameObject slotTwoCard; // used to access the current card in slot two
    public GameObject cardBlock2; // used to access the card blocker for slot two
    // card 3
    public CardStats cardThreeScript; // used to get the script from card three
    public GameObject slotThreeCard; // used to access the current card in slot three
    public GameObject cardBlock3; // used to access the card blocker for slot three
    // card 4
    public CardStats cardFourScript; // used to get the script from card four
    public GameObject slotFourCard; // used to access the current card in slot four
    public GameObject cardBlock4; // used to access the card blocker for slot four
    // card 5
    public CardStats cardFiveScript; // used to get the script from card five
    public GameObject slotFiveCard; // used to access the current card in slot five
    public GameObject cardBlock5; // used to access the card blocker for slot five
    // new card
    public CardStats cardNewScript; // used to get the script from the new card
    public GameObject slotNewCard; // used to access the current card in that is new

    [Header("- Card Info")]
    // clicked card
    public int clickedCard; // used to see what slot number the player clicked on
    // use card
    public bool isCardUsed; // used to see if a card was used
    // card used count
    public int cardsPlayed; // used to see if a card was played

    [Header("- Card Popup Stuff")]
    // use pop up display card
    public Text mPopUpDesc; // used to update the Use display card description

    [Header("- Card Raycasting Ignore List")]
    // ignore the tower triggers and enemies triggers
    public LayerMask ignoreLayer; // used for the raycasting to ignore certain layers


    [Header("UI Stuff")]


    // UI
    public GameObject UI; // used to access the ui
    [Header("- Menus")]
    // player interface
    public GameObject playerInterface; // used to access the player interface
    // pause menu
    public GameObject pauseMenu; // used to access the pause menu
    // loser screen
    public GameObject loserPopup; // used to access the loser screen
    // winner screen
    public GameObject winnerPopup; // used to access the winner screen

    [Header("- Card Pop Up")]
    // card pop up
    public GameObject cardPopup; // used to access the card popup screen
    // card popup card
    public GameObject cardPopupCard;

    // tower spot glow material
    [Header("- Ground Color Materials")]
    public Texture spotGlow; // used to change the tower spot when hovering over it during the use action
    // tower default material
    public Texture spotDefault; // used to change the tower spot back to normal after unhovering over it during the use action

    [Header("- Menu Navigations Buttons")]
    // navigation buttons
    public GameObject interfaceFirstButton, pauseFirstButton, pauseCloseButton, loseFirstButton, loseCloseButton, winnerFirstButton, winnerCloseButton;

    [Header("- Conveyor Object")]
    // card conveyor
    public GameObject cardConveyor;

    [Header("- Map Play Buttons")]
    public GameObject map2Button;
    public GameObject map3Button;

    [Header("- Map Play Button Blockers")]
    // map select blockers
    public GameObject mapBlock2;
    public GameObject mapBlock3;

    [Header("- Tower Pop Up Objs")]
    // TOWER POP UP
    public BasicTowerScript clickedOnTowerRef; // used to know what tower the player clicked on
    public GameObject towerPopUp; // used to access the tower pop up
    public Text towerPopUpTitle; // used to access the pop up's title
    public Image towerPopUpHPBar, towerPopUpDamageBar, towerPopUpAttackRateBar; // used to access the status bars

    [Header("- Alerts")]
    public GameObject ambushAlert;

    [Header("Event Stuff")]


    // EVENTS
    [Header("- Pause Bools")]
    // is pause
    public bool isPause; // used to see if the game is paused/unpaused

    [Header("- Game Condition Bools")]
    //  is game over
    public bool isGameOver; // used to see if the game is over
    // did player win
    public bool isWinner; // used to see if the player is the winner
    // did player lose
    public bool isLoser; // used to see if the player is a loser
    // is game playing
    public bool isGamePlaying; // used to see if the game is currently playing
    // is spawning again
    public bool isSpawnAgain;

    [Header("- Trash Bools")]
    // card trashed
    public bool isTrashed; // to see if a card is trashed in order to update the display cards on the conveyer

    [Header("Card Bools")]
    // card created
    public bool isNew; // used to see if a new card was instantiated
    public bool isDeEnhance;
    public bool isSmited; // used for tower damage card

    [Header("Animation Stuff")]
    // Animations
    public Animator mHoverAnimation; // used to access the tower pop up animation
    public Animator mConveyor1Moving; // used to access the conveyor animation for conveyor one
    public Animator mConveyor2Moving; // used to access the conveyor animation for conveyor two
    public Animator mConveyor3Moving; // used to access the conveyor animation for conveyor three
    public Animator mConveyor4Moving; // used to access the conveyor animation for conveyor four
    public Animator mConveyor5Moving; // used to access the conveyor animation for conveyor five
    public Animator cardMoving;
    public float cardSpeedMaster;
    public float conveyorSpeedMaster;

    [Header("More Card Stuff")]
    // Card slots fill
    public bool isSlot1Filled;
    public bool isSlot2Filled;
    public bool isSlot3Filled;
    public bool isSlot4Filled;
    public bool isSlot5Filled;
    public bool isSlot6Filled;

    // card's sprites
    public Sprite BlankCardSprite;
    public Sprite BasicTowerCardSprite;
    public Sprite SniperTowerCardSprite;
    public Sprite MachineGunTowerSprite;
    public Sprite SupportTowerSprite;
    public Sprite ShotgunTowerSprite;
    public Sprite BaseFixerBuffSprite;
    public Sprite ConveyorSpeedUpBuffSprite;
    public Sprite NewHandBuffSprite;
    public Sprite SlowEnemiesBuffSprite;
    public Sprite TowerEnhancmentBuffSprite;
    public Sprite TowerFixedBuffSprite;
    public Sprite AmbushTrapSprite;
    public Sprite ConveyorSlowTrapSprite;
    public Sprite DamageTowersSprite;
    public Sprite HasteTrapSprite;
    public Sprite IceStormTrapSprite;
    public Sprite WindStormTrapSprite;
    public Sprite RainBuffSprite;
    public Sprite SunnyBuffSprite;
    public Sprite SnowBuffSprite;
    public Sprite LightningBuffSprite;


    [Header("Weather Stuff")]

    //active weather particle obj
    public GameObject activeWeatherParticleObj;
    //Weather
    public List<WeatherEvents> weatherQue = new List<WeatherEvents>();
    // weather effect panel in player interface
    public GameObject weatherEffectPanel;
    // weather icon prefab
    public GameObject weatherIcon;
    // weather conveyor ui
    public GameObject weatherConveyor;
    // weather round count text
    public Text weatherRoundCountText;
    // weather round count int
    public int weatherRoundCount;
    // sunny sprite
    public Sprite sunnySprite;
    // rain sprite
    public Sprite rainSprite;
    // wind sprite
    public Sprite windSprite;
    // lightning sprite
    public Sprite lightningSprite;
    // ice storm sprite
    public Sprite iceStormSprite;
    // snow sprite
    public Sprite snowSprite;
    // current weather image
    public Image currentWeatherImg;
    // next weather image
    public Image nextWeatherImg;
    // new weather script
    public WeatherEvents newWeatherScript;
    // empty obj with the weather script on it
    public GameObject emptyObjWithWeatherScript;
    // each new weather obj
    public GameObject weather1Obj;
    public GameObject weather2Obj;
    public GameObject weather3Obj;
    public GameObject weather4Obj;
    public GameObject weather5Obj;
    public GameObject weather6Obj;
    // the current weather script
    public WeatherEvents currentWeatherScript;
    // enemies that are in the lightning strike zone
    public List<GameObject> lightningStrikedEnemies = new List<GameObject>();
    // enemies and towers that are in the ice storm rain zone
    public List<GameObject> iceStrikeObjs = new List<GameObject>();




    private void Awake()
    {
        Instance = this;
        LoadInfo(); // loads saved prefs

        //GAME
        baseObj = GameObject.FindGameObjectWithTag("Base"); // finds an obj with Base as the tag
        trashCan = GameObject.FindGameObjectWithTag("TrashCan"); // finds an obj with TrashCan as the tag
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // finds and gets the main camera in the scene
        enemySpawner = GameObject.FindGameObjectWithTag("Spawner"); // finds an obj with Spawner as the tag

    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) // if not on the main menu
        {
            GameStart(); // gets the game going 
            UpdateHudVisuals(); // make sure the hud is active at start
            conveyorSpeedMaster = mConveyor1Moving.speed;
            cardSpeedMaster = cardMoving.speed;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 4) // if not on the main menu or tutorial
        {
            KeyBinds(); // checks for key bind inputs

            if (isCardUsed) // when the user is in use mode
                WhileCardUsedIsTrue(); // calls the raycasting stuff

            //CardPlayedBarrier(); // since a tower got places we want to check the barrier
            enemyCount = activeEnemiesInGame.Count;

        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            KeyBinds(); // checks for key bind inputs

            if (isCardUsed) // when the user is in use mode
                WhileCardUsedIsTrue(); // calls the raycasting stuff

            if (baseHealthCurrent < baseHealthOriginal)
            {
                baseHealthCurrent = baseHealthOriginal;
                UpdateHudVisuals();
            }
        }

        //if (activeEnemiesInGame.Contains(null))
        //{
        //    foreach (var enemy in activeEnemiesInGame)
        //    {
        //        if (enemy == null)
        //            activeEnemiesInGame.Remove(enemy);
        //    }
        //}

        for(var i = activeEnemiesInGame.Count - 1; i > -1; i--)
        {
            if (activeEnemiesInGame[i] == null)
                activeEnemiesInGame.RemoveAt(i);
        }

        //if (activeTowerInGame.Contains(null))
        //{
        //    foreach(var tower in activeTowerInGame)
        //    {
        //        if (tower == null)
        //            activeTowerInGame.Remove(tower);
        //    }
        //}
        for (var i = activeTowerInGame.Count - 1; i > -1; i--)
        {
            if (activeTowerInGame[i] == null)
                activeTowerInGame.RemoveAt(i);
        }

    }
    public void SaveInfo() // used for saving the game stuff
    {
        // saves what maps the user completed
        PlayerPrefs.SetInt("isLevelOneComplete", Convert.ToInt32(isLevelOneComplete));
        PlayerPrefs.SetInt("isLevelTwoComplete", Convert.ToInt32(isLevelTwoComplete));
        PlayerPrefs.SetInt("isLevelThreeComplete", Convert.ToInt32(isLevelThreeComplete));
        // saves the highest score the user got
        PlayerPrefs.SetInt("lvlOneHighScore", lvlOneHighScore);
        PlayerPrefs.SetInt("lvlTwoHighScore", lvlTwoHighScore);
        PlayerPrefs.SetInt("lvlThreeHighScore", lvlThreeHighScore);
        // saves if they want to play the tutorial or not
        PlayerPrefs.SetInt("isShowTutorial", Convert.ToInt32(isShowTutorial));
        PlayerPrefs.Save(); // this is what saves it 
    }
    void LoadInfo() // used for loading the game stuff
    {
        if (PlayerPrefs.HasKey("isLevelOneComplete"))
            isLevelOneComplete = Convert.ToBoolean(PlayerPrefs.GetInt("isLevelOneComplete"));

        if (PlayerPrefs.HasKey("isLevelTwoComplete"))
            isLevelTwoComplete = Convert.ToBoolean(PlayerPrefs.GetInt("isLevelTwoComplete"));

        if (PlayerPrefs.HasKey("isLevelThreeComplete"))
            isLevelThreeComplete = Convert.ToBoolean(PlayerPrefs.GetInt("isLevelThreeComplete"));

        if (PlayerPrefs.HasKey("lvlOneHighScore"))
            lvlOneHighScore = PlayerPrefs.GetInt("lvlOneHighScore");
        if (PlayerPrefs.HasKey("lvlTwoHighScore"))
            lvlTwoHighScore = PlayerPrefs.GetInt("lvlTwoHighScore");
        if (PlayerPrefs.HasKey("lvlThreeHighScore"))
            lvlThreeHighScore = PlayerPrefs.GetInt("lvlThreeHighScore");

        if (PlayerPrefs.HasKey("isShowTutorial"))
            isShowTutorial = Convert.ToBoolean(PlayerPrefs.GetInt("isShowTutorial"));

        if (PlayerPrefs.HasKey("masterVolume"))
            masterVolume = PlayerPrefs.GetFloat("masterVolume");
        else masterVolume = 100f;

        if (PlayerPrefs.HasKey("sfxVolume"))
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        else sfxVolume = 100f;

        if (PlayerPrefs.HasKey("musicVolume"))
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        else musicVolume = 100f;

    }
    public void GameStart() // when I map starts
    {
        Time.timeScale = 1; // sets the game time to 1 (0 is like the game is paused)
        isGamePlaying = true; // tells us the game is currently running
        playerInterface.SetActive(true); // lets the interface be seen
        baseHealthCurrent = baseHealthOriginal; // setting the current health to the original
        scoreTracker = playerScore; // setting the tracker to the current score
        FirstFiveCards();


        if (SceneManager.GetActiveScene().buildIndex == 1)
            playingMapNum = 1;
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            playingMapNum = 2;
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            playingMapNum = 3;
        else if (SceneManager.GetActiveScene().buildIndex == 4)
            playingMapNum = 4;

        WeatherOnStart();
    }
    public void GameOver()
    {
        SaveInfo();
        isGamePlaying = false; // tells us the game is not running
        playerInterface.SetActive(false); // sets the player interface to not show
        winnerPopup.SetActive(false); // hides the winner popup screen if it was shown
        loserPopup.SetActive(false); // hides the loser popup screen if it was shown
        loaderObj.GetComponent<LevelLoader>().LoadLevel(0);
    }
    public void KeyBinds()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) // pause or unpause game with esc or p key
        {
            if (!isPause) PauseGame(); // if the game is currently not paused, pause
            else ResumeGame(); // else if the game is paused, resume
        }
    }

    #region UIGameEvents

    public void SetConveyorSpeed(float _ConveyorSpeed, float _CardSpeed) // changes the speed of the conveyors
    {
        mConveyor1Moving.speed = _ConveyorSpeed;
        mConveyor2Moving.speed = _ConveyorSpeed;
        mConveyor3Moving.speed = _ConveyorSpeed;
        mConveyor4Moving.speed = _ConveyorSpeed;
        mConveyor5Moving.speed = _ConveyorSpeed;

        cardMoving.speed = _CardSpeed;
    }


    public void UpdateHudVisuals()
    {
        roundCountText.text = roundCount.ToString() + " / 15"; // updates the round counter to the current round
        scoreCount.text = playerScore.ToString(); // updates the score
        baseHealthText.text = baseHealthCurrent.ToString(); // updates the base health bar text
        baseHealthBar.fillAmount = baseHealthCurrent / (float)baseHealthOriginal; // updates the base health bar 
        weatherRoundCountText.text = weatherRoundCount.ToString("F0"); // updates the weather round countdown
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // sets the game time to 0 (if the game was running it would be 1)
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton); // set a new selected object
        pauseMenu.SetActive(true); // shows the pause menu
        isPause = true; // tells us if the game is paused or not
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // hides the pause menu
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(pauseCloseButton); // set a new selected object
        Time.timeScale = 1; // sets the game time to 1 (if the game was paused it would be 0)
        isPause = false; // tells us if the game is paused or not
    }

    public void Loser()
    {
        Time.timeScale = 0;

        if (playingMapNum == 1)
        {
            if (playerScore > lvlOneHighScore)
                lvlOneHighScore = playerScore;
        }
        else if (playingMapNum == 2)
        {
            if (playerScore > lvlTwoHighScore)
                lvlTwoHighScore = playerScore;
        }
        else if (playingMapNum == 3)
        {
            if (playerScore > lvlThreeHighScore)
                lvlThreeHighScore = playerScore;
        }

        isGameOver = true;
        isLoser = true; // tells us they lost
        loserPopup.SetActive(true); // shows the losing screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(loseFirstButton); // set a new selected object
    }
    public void Winner()
    {
        Time.timeScale = 0;
        if (playingMapNum == 1)
        {
            isLevelOneComplete = true;
            if (playerScore > lvlOneHighScore)
                lvlOneHighScore = playerScore;
        }
        else if (playingMapNum == 2)
        {
            isLevelTwoComplete = true;
            if (playerScore > lvlTwoHighScore)
                lvlTwoHighScore = playerScore;
        }
        else if (playingMapNum == 3)
        {
            isLevelThreeComplete = true;
            if (playerScore > lvlThreeHighScore)
                lvlThreeHighScore = playerScore;
        }

        isGameOver = true;
        isWinner = true; // tell us the player won
        winnerPopup.SetActive(true); // shows the winning screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(winnerFirstButton); // set a new selected object
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloads the current scene aka Restart Level
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(pauseCloseButton); // set a new selected object
    }

    #endregion

    #region Enemy

    public void LastRound() // for when it's round 15 // call when enemy count goes down
    {
        if (roundCount == 15) // Only when it's round 15
        {
            if (activeEnemiesInGame.Count <= 0 && baseHealthCurrent <= 0) // when no more enemies are left and the base health is equal to or less than 0
                Loser(); // they lost
            else if (activeEnemiesInGame.Count <= 0 && baseHealthCurrent > 0) // when no more enemies are left and the base health is greater than 0
                Winner(); // they won
        }
    }

    #endregion


    #region Cards
    public void GlowTowerSpot() // highlights the hovered over tower spot
    {
        if (mTowerSpotSelection != null) // dehighlights the tower (resets it back to the original material)
        {
            var selectionRenderer = mTowerSpotSelection.GetComponent<Renderer>(); // gets the renderer on the tower spot
            selectionRenderer.material.mainTexture = spotDefault; // sets th renderer's material to the default material
            mTowerSpotSelection = null; // sets the selection to null to allow another square to be selected
        }
        // raycast stuff ///////////////////////////////////////////
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        ////////////////////////////////////////////////////////////
        if (Physics.Raycast(ray, out hit, 1000f, ~ignoreLayer)) // if it hit something
        {
            if (hit.transform.CompareTag("TowerSpot")) // if that hit has the tag TowerSpot
            {
                var selection = hit.transform; // takes the transform of the hit tower spot
                var selectionRenderer = selection.GetComponent<Renderer>(); // takes the render from the transform of the hit tower spot
                if (selectionRenderer != null) // if there is a render
                {
                    selectionRenderer.material.mainTexture = spotGlow; // changes the material to the glow material
                    mTowerSpotSelection = selection; // sets the current tower spot selection to this selection
                }
            }
        }
    }

    // Card Click Events
    public void UseCard() // when the player clicks use a card
    {
        isCardUsed = true; // tells us a card was used
    }
    void WhileCardUsedIsTrue() // called in a update function
    {
        if (GetClickedOnCardScript().mIsTower)
        {
            GlowTowerSpot(); // glows the tower spot they are hovering over
            InstantiateTower(); // places a tower down if they hit left click
        }
        else if (GetClickedOnCardScript().mIsBuff)
        {
            GetClickedOnCardScript().GetBuffCardFunction();
            isCardUsed = false;
            SpotEmpty(clickedCard);
            cardPopup.SetActive(false);
            if (!isSlot6Filled)
                NewRandomCard();
        }
        else if (GetClickedOnCardScript().mIsTrap)
        {
            GetClickedOnCardScript().GetTrapCardFunction();
            isCardUsed = false;
            SpotEmpty(clickedCard);
            cardPopup.SetActive(false);
            if (!isSlot6Filled)
                NewRandomCard();
        }
    }
    GameObject GetTowerPrefab()
    {
        CardStats clickedCard = GetClickedOnCardScript();
        if (clickedCard.mName == "Basic")
            return basicTowerPrefab;
        else if (clickedCard.mName == "Sniper")
            return sniperTowerPrefab;
        else if (clickedCard.mName == "Machine Gun")
            return machineGunTowerPrefab;
        else if (clickedCard.mName == "Support Tower")
            return supportTowerPrefab;
        else if (clickedCard.mName == "Shotgun")
            return shotgunTowerPrefab;
        else return null;
    }
    CardStats GetClickedOnCardScript()
    {
        switch (clickedCard)
        {
            case 1:
                return cardOneScript;
            case 2:
                return cardTwoScript;
            case 3:
                return cardThreeScript;
            case 4:
                return cardFourScript;
            case 5:
                return cardFiveScript;
        }
        return null;
    }
    public void InstantiateTower() // when the user plays a tower card, create the tower on that selected spot
    {
        if (Input.GetMouseButtonDown(0)) // if left mouse click
        {

            // raycasting stuff ////////////////////////////////////////
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ////////////////////////////////////////////////////////////

            if (Physics.Raycast(ray, out hit, 1000f, ~ignoreLayer)) // if it hits something
            {
                if (hit.transform.tag == "TowerSpot") // if the hit's tag is a tower spot
                {
                    isCardUsed = false; // resets this bool
                    if (mTowerSpotSelection != null) // sets material back to default
                    {
                        var selectionRenderer = mTowerSpotSelection.GetComponent<Renderer>(); // gets the renderer on the tower spot
                        selectionRenderer.material.mainTexture = spotDefault; // sets it back to default material
                        mTowerSpotSelection = null; // sets the spot selection to null to allow a new selection
                    }
                    Vector3 towerPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z); // sets the tower position to the hit's position
                    GameObject tower = Instantiate(GetTowerPrefab(), towerPosition, basicTowerPrefab.transform.rotation); // clones a new basic tower onto the tower spot
                    cardsPlayed++; // raises the cards played count 
                    //CardPlayedBarrier(); // since a tower got places we want to check the barrier
                    // Sets the tower to the card that just placed it ////////////////////////////////////////
                    CardStats cardScript = GetClickedOnCardScript();
                    tower.GetComponent<BasicTowerScript>().mName = cardScript.mName;
                    tower.GetComponent<BasicTowerScript>().mDesc = cardScript.mDesc;
                    tower.GetComponent<BasicTowerScript>().mHP = cardScript.mHP;
                    tower.GetComponent<BasicTowerScript>().mHPMaster = cardScript.mHP;
                    tower.GetComponent<BasicTowerScript>().mDamage = cardScript.mDamage;
                    tower.GetComponent<BasicTowerScript>().mDamageMaster = cardScript.mDamage;
                    tower.GetComponent<BasicTowerScript>().mFireRate = cardScript.mFireRate;
                    tower.GetComponent<BasicTowerScript>().mSpotRef = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<TowerSpotCheck>().isUsed = true;
                    int rngForIceStormSpot = UnityEngine.Random.Range(1, 4);
                    if (rngForIceStormSpot == 3)
                    {
                        iceStrikeObjs.Add(tower);

                    }
                    activeTowerInGame.Add(tower); // adds the tower to the list of active towers
                    if (currentWeatherScript != null)
                    {
                        if (currentWeatherScript.mWeatherEventIndex == 0)
                        {
                            tower.GetComponent<BasicTowerScript>().SunEffect();
                            tower.GetComponent<BasicTowerScript>().mIsEffectedBySun = true;
                        }
                        else if (currentWeatherScript.mWeatherEventIndex == 0)
                        {
                            tower.GetComponent<BasicTowerScript>().WindEffect();
                            tower.GetComponent<BasicTowerScript>().mIsEffectedByWind = true;
                        }

                    }
                    /////////////////////////////////////////////////////////////////////////////////////////
                    hit.transform.tag = "UsedSpot"; // sets the current tower spot to used so no other towers can be placed onto it
                    SpotEmpty(clickedCard);
                    cardPopup.SetActive(false); // makes the card popup go away
                    if (!isSlot6Filled) // if there is no new gen card waiting to move down
                        NewRandomCard(); // create a new card
                }
            }
        }
    }
    public void SpotEmpty(int currentSpot) // destoyers a slot's card after use or trashed
    {
        switch (currentSpot)
        {
            case 1:
                Destroy(slotOneCard);
                isSlot1Filled = false;
                break;
            case 2:
                Destroy(slotTwoCard);
                isSlot2Filled = false;
                break;
            case 3:
                Destroy(slotThreeCard);
                isSlot3Filled = false;
                break;
            case 4:
                Destroy(slotFourCard);
                isSlot4Filled = false;
                break;
            case 5:
                Destroy(slotFiveCard);
                isSlot5Filled = false;
                break;
        }
    }
    public void CancelUseCard() // when the player clicks cancle in the use card pop up
    {
        isCardUsed = false; // tells us a card wasnt used
        cardPopup.SetActive(false); // hides the pop up
    }

    public void TrashCard() // when the player clicks trash in the use card pop up
    {
        if (trashCount < maxTrash) // if the current trashed amount is less than the max trash count
        {
            trashCount++; // add one to the trashed count
            UpdateTrashSprite();
            trashText.text = trashCount.ToString("F0"); // update the trash text on the hud to the new count
            SpotEmpty(clickedCard); // tells the system to delete the card that the user clicked on
            if (!isSlot6Filled) // if there is no new gen card waiting to move down
                NewRandomCard(); // create a new card
            isTrashed = true; // tell us a card was trashed

            if (trashCount == maxTrash) // if the current count now equal the max trash count after trashing that card
            {
                trashButton.SetActive(false); // disable the trash card button
                trashLockedButton.SetActive(true); // show the trash maxed out button
            }
            cardPopup.SetActive(false); // hides the use card pop up
        }

    }
    public void UpdateTrashSprite()
    {
        switch (trashCount)
        {
            case 0:
                trashSprite.sprite = trash0;
                break;
            case 1:
                trashSprite.sprite = trash1;
                break;
            case 2:
                trashSprite.sprite = trash2;
                break;
            case 3:
                trashSprite.sprite = trash3;
                break;
            case 4:
                trashSprite.sprite = trash4;
                break;
            case 5:
                trashSprite.sprite = trash5;
                break;
            case 6:
                trashSprite.sprite = trash6;
                break;
            case 7:
                trashSprite.sprite = trash7;
                break;
            case 8:
                trashSprite.sprite = trash8;
                break;
            case 9:
                trashSprite.sprite = trash9;
                break;
            case 10:
                trashSprite.sprite = trash10;
                break;
        }

    }
    IEnumerator HideAmbushAlert(float wait)
    {
        yield return new WaitForSeconds(wait);
        ambushAlert.SetActive(false);
    }
    public void AmbushCheck()
    {
        if (cardFiveScript.mTrapCardIndex == 1)
        {
            ambushAlert.SetActive(true);
            StartCoroutine(HideAmbushAlert(4));
            ambushSpawner.GetComponent<AmbushSpawn>().Spawn();
            SpotEmpty(5);
            if (!isSlot6Filled)
                NewRandomCard();
        }
        else if (cardFourScript.mTrapCardIndex == 1)
        {
            ambushAlert.SetActive(true);
            StartCoroutine(HideAmbushAlert(4));
            ambushSpawner.GetComponent<AmbushSpawn>().Spawn();
            SpotEmpty(4);
            if (!isSlot6Filled)
                NewRandomCard();
        }
        else if (cardThreeScript.mTrapCardIndex == 1)
        {
            ambushAlert.SetActive(true);
            StartCoroutine(HideAmbushAlert(4));
            ambushSpawner.GetComponent<AmbushSpawn>().Spawn();
            SpotEmpty(3);
            if (!isSlot6Filled)
                NewRandomCard();
        }
        else if (cardTwoScript.mTrapCardIndex == 1)
        {
            ambushAlert.SetActive(true);
            StartCoroutine(HideAmbushAlert(4));
            ambushSpawner.GetComponent<AmbushSpawn>().Spawn();
            SpotEmpty(2);
            if (!isSlot6Filled)
                NewRandomCard();
        }
        else if (cardOneScript.mTrapCardIndex == 1)
        {
            ambushAlert.SetActive(true);
            StartCoroutine(HideAmbushAlert(4));
            ambushSpawner.GetComponent<AmbushSpawn>().Spawn();
            SpotEmpty(1);
            if (!isSlot6Filled)
                NewRandomCard();
        }
    }

    #region Card Blocking
    // tower card blocking on the conveyer belt
    //public void BlockTowerCards() // used for blocking the conver if the player isnt allowed to place towers
    //{
    //    cardBlock1.SetActive(true); // shows the blocker for card 1
    //    cardBlock2.SetActive(true); // shows the blocker for card 2
    //    cardBlock3.SetActive(true); // shows the blocker for card 3
    //    cardBlock4.SetActive(true); // shows the blocker for card 4
    //    cardBlock5.SetActive(true); // shows the blocker for card 5
    //    slotOneCard.SetActive(false); // hides card 1
    //    slotTwoCard.SetActive(false); // hides card 2
    //    slotThreeCard.SetActive(false); // hides card 3
    //    slotFourCard.SetActive(false); // hides card 4
    //    slotFiveCard.SetActive(false); // hides card 5
    //}
    //public void UnBlockTowerCards() // used for unblocking the conver if the player is allowed to place towers
    //{
    //    cardBlock1.SetActive(false); // hides the blocker for card 1
    //    cardBlock2.SetActive(false); // hides the blocker for card 2
    //    cardBlock3.SetActive(false); // hides the blocker for card 3
    //    cardBlock4.SetActive(false); // hides the blocker for card 4
    //    cardBlock5.SetActive(false); // hides the blocker for card 5
    //    slotOneCard.SetActive(true); // shows card 1
    //    slotTwoCard.SetActive(true); // shows card 2
    //    slotThreeCard.SetActive(true); // shows card 3
    //    slotFourCard.SetActive(true); // shows card 4
    //    slotFiveCard.SetActive(true); // shows card 5
    //}
    //public void CardPlayedBarrier() // used for blocking the cards on the amount of towers activly being played
    //{
    //    TowerBlockScoreCost();
    //    switch (roundCount)
    //    {
    //        case 1:
    //            IfCardsPlayed(5);
    //            break;
    //        case 2:
    //            IfCardsPlayed(7);
    //            break;
    //        case 3:
    //            IfCardsPlayed(9);
    //            break;
    //        case 4:
    //            IfCardsPlayed(11);
    //            break;
    //        case 5:
    //            IfCardsPlayed(13);
    //            break;
    //        case 6:
    //            IfCardsPlayed(15);
    //            break;
    //        case 7:
    //            IfCardsPlayed(17);
    //            break;
    //        case 8:
    //            IfCardsPlayed(19);
    //            break;
    //        case 9:
    //            IfCardsPlayed(21);
    //            break;
    //        case 10:
    //            IfCardsPlayed(23);
    //            break;
    //        case 11:
    //            IfCardsPlayed(25);
    //            break;
    //        case 12:
    //            IfCardsPlayed(27);
    //            break;
    //        case 13:
    //            IfCardsPlayed(29);
    //            break;
    //        case 14:
    //            IfCardsPlayed(31);
    //            break;
    //        case 15:
    //            IfCardsPlayed(33);
    //            break;
    //    }

    //}

    //private void IfCardsPlayed(int _cardsAvailable) // tells the game the limit of towers that the player is allowed to have in play 
    //{
    //    if (cardsPlayed < _cardsAvailable) // if the amount of active towers is the less than the amount passed
    //        UnBlockTowerCards(); // leave cards unblocked
    //    else BlockTowerCards(); // else blocks they have the maxed towers in play
    //}
    //private void TowerBlockScoreCost() // used to update the cost for the cards depending on the current round
    //{
    //    if (cardsPlayed > 4) // cost is 2 time the current round count
    //        scoreCost = 2 * roundCount;
    //    else scoreCost = 0; // first round
    //}
    #endregion

    public void FirstFiveCards()
    {
        // create 5 basic cards
        StartCoroutine(CreateStartingCard(0));
        StartCoroutine(CreateStartingCard(3));
        StartCoroutine(CreateStartingCard(6));
        StartCoroutine(CreateStartingCard(9));
        StartCoroutine(CreateStartingCard(12));
    }
    public void FourRandomCards()
    {
        StartCoroutine(WaitCreateRandomCard(3));
        StartCoroutine(WaitCreateRandomCard(6));
        StartCoroutine(WaitCreateRandomCard(9));
        StartCoroutine(WaitCreateRandomCard(12));
    }
    IEnumerator CreateStartingCard(float wait) // put whatever card u want the first 5 cards to be here
    {
        yield return new WaitForSeconds(wait);
        NewBasicTowerCard();
    }
    IEnumerator WaitCreateRandomCard(float wait)
    {
        yield return new WaitForSeconds(wait);
        NewRandomCard();
    }
    // CARD CREATION
    public void NewRandomCard()
    {
        int randomCard;
        if (playingMapNum != 4)
            randomCard = UnityEngine.Random.Range(1, 101);
        else randomCard = 1;

        if (randomCard >= 1 && randomCard <= 50)
            randomCard = 1;
        else if (randomCard >= 51 && randomCard <= 80)
            randomCard = 2;
        else randomCard = 3;

        switch (randomCard)
        {

            case 1:
                NewRandomTowerCard();
                break;
            case 2:
                NewRandomBuffCard();
                break;
            case 3:
                NewRandomTrapCard();
                break;

        }
    }
    public void NewRandomTowerCard()
    {
        int randomCard;
        if (playingMapNum != 4)
            randomCard = UnityEngine.Random.Range(1, 6);
        else randomCard = 1;

        switch (randomCard)
        {
            case 1:
                NewBasicTowerCard();
                break;
            case 2:
                NewSniperTowerCard();
                break;
            case 3:
                NewMachineGunTowerCard();
                break;
            case 4:
                NewTowerSupportTowerCard();
                break;
            case 5:
                NewShotgunTowerCard();
                break;
        }
    }
    public void NewRandomBuffCard()
    {
        int randomCard = UnityEngine.Random.Range(1, 11);
        switch (randomCard)
        {
            case 1:
                NewBuffCardTowerFixer();
                break;
            case 2:
                NewBuffCardTowerEnhance();
                break;
            case 3:
                NewBuffCardNewHand();
                break;
            case 4:
                NewBuffCardBaseFixer();
                break;
            case 5:
                NewBuffCardConveyorSpeedUp();
                break;
            case 6:
                NewBuffCardCSlowEnemies();
                break;
            case 7:
                NewBuffCardSunSpawn();
                break;
            case 8:
                NewBuffCardRainSpawn();
                break;
            case 9:
                NewBuffCardLightningSpawn();
                break;
            case 10:
                NewBuffCardSnowSpawn();
                break;
        }
    }
    public void NewRandomTrapCard()
    {
        int randomCard = UnityEngine.Random.Range(1, 7);
        switch (randomCard)
        {
            case 1:
                NewTrapCardHaste();
                break;
            case 2:
                NewAmbushCard();
                break;
            case 3:
                NewSlowDOwnConveyorCard();
                break;
            case 4:
                NewTowerDamageCard();
                break;
            case 5:
                NewIceStormTrap();
                break;
            case 6:
                NewWindStormTrap();
                break;
        }
    }
    void NewTowerCard(int hp, int damage, int cardSlot, int attackRate, string name, string desc, Sprite cardSprite, string objName)
    {
        isSlot6Filled = true; // tells the system to not make another card since the new card slot is filled
        GameObject card = Instantiate(newTowerCard, cardConveyor.transform);
        card.name = objName;
        //card.transform.position = new Vector3(360, 2.2f);
        card.transform.SetSiblingIndex(5); // sets the new card to behind the gerator but ontop of the conveyors
        cardNewScript = card.GetComponentInChildren<CardStats>(); // sets the new script to the script on the new card we just made
        cardNewScript.mCardSlot = cardSlot;
        cardNewScript.mHP = hp;
        cardNewScript.mDamage = damage;
        cardNewScript.mCardSlot = 6; // 6 = new card
        cardNewScript.SetSlotCard();
        cardNewScript.mFireRate = attackRate;
        cardNewScript.mName = name;
        cardNewScript.mDesc = desc;
        cardNewScript.mCardSprite = cardSprite; // sets the sprite of the card (image)
        cardNewScript.UpdateSprite();
        cardNewScript.mIsTower = true; // tells us that this card is a tower card
        cardNewScript.mIsBuff = false; // tells us that this card isnt a buff card
        cardNewScript.mIsTrap = false; // tells us that this card isnt a trap card
        cardNewScript.mTrapCardIndex = -69;
        slotNewCard = card;
    }
    void NewBuffCard(int cardSlot, string name, string desc, Sprite cardSprite, string objName, int buffCardIndex)
    {
        isSlot6Filled = true; // tells the system to not make another card since the new card slot is filled
        GameObject card = Instantiate(newTowerCard, cardConveyor.transform);
        card.name = objName;
        card.transform.SetSiblingIndex(5); // sets the new card to behind the gerator but ontop of the conveyors
        cardNewScript = card.GetComponentInChildren<CardStats>(); // sets the new script to the script on the new card we just made
        cardNewScript.mCardSlot = cardSlot;
        cardNewScript.mCardSlot = 6; // 6 = new card
        cardNewScript.SetSlotCard();
        cardNewScript.mName = name;
        cardNewScript.mDesc = desc;
        cardNewScript.mCardSprite = cardSprite; // sets the sprite of the card (image)
        cardNewScript.UpdateSprite();
        cardNewScript.mIsTower = false; // tells us that this card is a tower card
        cardNewScript.mIsBuff = true; // tells us that this card isnt a buff card
        cardNewScript.mIsTrap = false; // tells us that this card isnt a trap card
        cardNewScript.mBuffCardIndex = buffCardIndex;
        cardNewScript.mTrapCardIndex = -69;
        slotNewCard = card;
    }
    void NewTrapCard(int cardSlot, string name, string desc, Sprite cardSprite, string objName, int trapCardIndex)
    {
        isSlot6Filled = true; // tells the system to not make another card since the new card slot is filled
        GameObject card = Instantiate(newTowerCard, cardConveyor.transform);
        card.name = objName;
        card.transform.SetSiblingIndex(5); // sets the new card to behind the gerator but ontop of the conveyors
        cardNewScript = card.GetComponentInChildren<CardStats>(); // sets the new script to the script on the new card we just made
        cardNewScript.mCardSlot = cardSlot;
        cardNewScript.mCardSlot = 6; // 6 = new card
        cardNewScript.SetSlotCard();
        cardNewScript.mName = name;
        cardNewScript.mDesc = desc;
        cardNewScript.mCardSprite = cardSprite; // sets the sprite of the card (image)
        cardNewScript.UpdateSprite();
        cardNewScript.mIsTower = false; // tells us that this card is a tower card
        cardNewScript.mIsBuff = false; // tells us that this card isnt a buff card
        cardNewScript.mIsTrap = true; // tells us that this card isnt a trap card
        cardNewScript.mTrapCardIndex = trapCardIndex;
        slotNewCard = card;

    }

    // NEW TOWER CARDS
    public void NewBasicTowerCard() // create a basic tower card
    {
        NewTowerCard(10, 1, 6, 6, "Basic", "The most basic tower of the bunch. Shoots things normally... So basic", BasicTowerCardSprite, "BasicTowerCard");
    }
    public void NewSniperTowerCard() // create a sniper tower card
    {
        NewTowerCard(6, 4, 6, 10, "Sniper", "A strong but frail long range tower. Shoots the strongest bullet in the game but takes time to reload it. It doesn't like being too close to enemies.", SniperTowerCardSprite, "SniperTowerCard");
    }
    public void NewMachineGunTowerCard() // create a machine gun tower card
    {
        NewTowerCard(8, 1, 6, 1, "Machine Gun", "The Basic Tower's more aggressive sibling. It shoots fast. It goes brrr...", MachineGunTowerSprite, "MachineGunTowerCard");
    }
    public void NewTowerSupportTowerCard() // create a machine gun tower card
    {
        NewTowerCard(10, 0, 6, 0, "Support Tower", "A pacifistic tower (is that even a word?). It can't attack but it can heal and enhance others. Sadly, no matter how many times it buffs everyone, it still doesn't get thanked", SupportTowerSprite, "SupportTowerCard");
    }
    public void NewShotgunTowerCard() // create a machine gun tower card
    {
        NewTowerCard(7, 2, 6, 3, "Shotgun", "An erratic short ranged tower that fires pellets instead of bullets. It thinks it's so special...", ShotgunTowerSprite, "ShotgunTowerCard");
    }
    // NEW BUFF CARDS
    public void NewBuffCardTowerFixer() // create a Buff card
    {
        NewBuffCard(6, "Tower Fix Up", "All towers are healed by 1 HP.", TowerFixedBuffSprite, "TowerFixBuffCard", 0);
    }
    public void NewBuffCardTowerEnhance() // create a Buff card
    {
        NewBuffCard(6, "Tower Enhance", "All tower's damage and attack rate are enhanced.", TowerEnhancmentBuffSprite, "TowerEnhanceBuffCard", 1);
    }
    public void NewBuffCardNewHand() // create a Buff card
    {
        NewBuffCard(6, "New Hand", "All cards on the conveyor are deleted for new ones.", NewHandBuffSprite, "NewHandBuffCard", 2);
    }
    public void NewBuffCardBaseFixer() // create a Buff card
    {
        NewBuffCard(6, "Base Fixer", "Heals the base's health 10 HP.", BaseFixerBuffSprite, "BaseFixerBuffCard", 3);
    }
    public void NewBuffCardConveyorSpeedUp() // create a Buff card
    {
        NewBuffCard(6, "Conveyor Speed Up", "Increases conveyor speed.", ConveyorSpeedUpBuffSprite, "ConveyorSpeedUpBuffCard", 4);
    }
    public void NewBuffCardCSlowEnemies() // create a Buff card
    {
        NewBuffCard(6, "Slow Enemies", "Slows enemies speed.", SlowEnemiesBuffSprite, "SlowEnemiesBuffCard", 5);
    }
    public void NewBuffCardSunSpawn() // create a Buff card
    {
        NewBuffCard(6, "Sunny Weather", "Changes the current weather to sunny.", SunnyBuffSprite, "SunnyBuffCard", 6);
    }
    public void NewBuffCardRainSpawn() // create a Buff card
    {
        NewBuffCard(6, "Rain Weather", "Changes the current weather to rain.", RainBuffSprite, "RainBuffCard", 7);
    }
    public void NewBuffCardLightningSpawn() // create a Buff card
    {
        NewBuffCard(6, "Lightning Weather", "Changes the current weather to lightning.", LightningBuffSprite, "LightningBuffCard", 8);
    }
    public void NewBuffCardSnowSpawn() // create a Buff card
    {
        NewBuffCard(6, "Snow Weather", "Changes the current weather to snow.", SnowBuffSprite, "SnowBuffCard", 9);
    }
    // NEW TRAP CARDS
    public void NewTrapCardHaste()
    {
        NewTrapCard(6, "Enemy Haste", "All enemies movment speed is increased by 50%", HasteTrapSprite, "HasteTrapCard", 0);
    }
    public void NewAmbushCard()
    {
        NewTrapCard(6, "Ambush Spawn", "Spawns a new wave of enemies on other path", AmbushTrapSprite, "AmbushTrapCard", 1);
    }
    public void NewSlowDOwnConveyorCard()
    {
        NewTrapCard(6, "Slow Conveyor", "Slows the Conveyor speed", ConveyorSlowTrapSprite, "SlowedConveyorTrapCard", 2);
    }
    public void NewTowerDamageCard()
    {
        NewTrapCard(6, "Damage Tower", "Damages all towers on map", DamageTowersSprite, "DamageTowerTrapCard", 3);
    }
    public void NewIceStormTrap()
    {
        NewTrapCard(6, "Ice Storm", "Changes the current weather to an ice storm", IceStormTrapSprite, "IceStormTrapCard", 4);
    }
    public void NewWindStormTrap()
    {
        NewTrapCard(6, "Wind Storm", "Changes the current weather to a wind storm", WindStormTrapSprite, "WindStormTrapCard", 5);
    }
    #endregion

    #region Weather
    void WeatherOnStart()
    {
        if (playingMapNum != 4) // if not tutorial
        {
            for (int i = 0; i < 6; ++i)
            {
                CreateRandomWeather(i);
            }
            currentWeatherImg.sprite = weather1Obj.GetComponent<WeatherEvents>().mIconSprite;
            nextWeatherImg.sprite = weather2Obj.GetComponent<WeatherEvents>().mIconSprite;
        }
        else
        {
            currentWeatherImg.sprite = sunnySprite;
            nextWeatherImg.sprite = sunnySprite;
        }

    }
    // create new icon
    void CreateNewWeatherEvent(Sprite _WeatherIcon, int _WeatherEventIdex, int order)
    {
        GameObject weather = Instantiate(emptyObjWithWeatherScript, weatherConveyor.transform);
        weather.name = "Weather Event " + order.ToString("F0");
        weather.GetComponent<WeatherEvents>().mIconSprite = _WeatherIcon;
        weather.GetComponent<WeatherEvents>().mWeatherEventIndex = _WeatherEventIdex; // tells us what weather event it is // 0 = sunny // 1 = rain // 2 = wind // 3 = lightning // 4 = ice storm // 5 = snow
        weatherQue.Add(weather.GetComponent<WeatherEvents>());
        switch (order)
        {
            case 0:
                weather1Obj = weather;
                break;
            case 1:
                weather2Obj = weather;
                break;
            case 2:
                weather3Obj = weather;
                break;
            case 3:
                weather4Obj = weather;
                break;
            case 4:
                weather5Obj = weather;
                break;
            case 5:
                weather6Obj = weather;
                break;
        }
    }

    public void CreateRandomWeather(int order)
    {
        int randomEvent = UnityEngine.Random.Range(0, 6);
        switch (randomEvent)
        {
            case 0:
                CreateSunnyEvent(order);
                break;
            case 1:
                CreateRainEvent(order);
                break;
            case 2:
                CreateWindEvent(order);
                break;
            case 3:
                CreateLightningEvent(order);
                break;
            case 4:
                CreateIceStormEvent(order);
                break;
            case 5:
                CreateSnowEvent(order);
                break;
        }
    }
    #region Create Weather Events
    void CreateSunnyEvent(int order)
    {
        CreateNewWeatherEvent(sunnySprite, 0, order);
    }
    void CreateRainEvent(int order)
    {
        CreateNewWeatherEvent(rainSprite, 1, order);
    }
    void CreateWindEvent(int order)
    {
        CreateNewWeatherEvent(windSprite, 2, order);
    }
    void CreateLightningEvent(int order)
    {
        CreateNewWeatherEvent(lightningSprite, 3, order);
    }
    void CreateIceStormEvent(int order)
    {
        CreateNewWeatherEvent(iceStormSprite, 4, order);
    }
    void CreateSnowEvent(int order)
    {
        CreateNewWeatherEvent(snowSprite, 5, order);
    }
    #endregion
    #endregion
}

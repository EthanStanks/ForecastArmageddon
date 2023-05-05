using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] GameObject mMainMenu; // used to access the main menu screen
    [SerializeField] GameObject mMapPopUp; // used to access the map selection screen
    [SerializeField] GameObject mOptionScreen; // used to access the options screen
    [SerializeField] GameObject mCreditScreen; // used to access the credits screen
    [SerializeField] GameObject mAssetCredit;
    public GameObject mTutorialScreen;
    public bool mShowTutorial;
    public GameObject toggleButtonBackground;
    public GameObject toggleButton;
    bool hasBeenToggled;
    Color defaultColor = Color.white;
    public GameObject masterSoundScroller, musicSoundScroller, sfxSoundScroller;
    public Text masterSoundText, musicSoundText, sfxSoundText;
    public Text map1Score, map2Score, map3Score;

    public GameObject mainFirstButton, optionFirstButton, optionCloseButton, creditFirstButton, creditCloseButton, levelFirstButton, levelCloseButton, tutorialFirstButton, tutorialCloseButton;

    public AudioMixer mixer;   

    private void Start()
    {
        if (GameManager.Instance.isLevelOneComplete)
        {
            GameManager.Instance.mapBlock2.SetActive(false);
            GameManager.Instance.map2Button.SetActive(true);
        }
        else if (!GameManager.Instance.isLevelOneComplete)
        {
            GameManager.Instance.mapBlock2.SetActive(true);
            GameManager.Instance.map2Button.SetActive(false);
        }

        if (GameManager.Instance.isLevelTwoComplete)
        {
            GameManager.Instance.mapBlock3.SetActive(false);
            GameManager.Instance.map3Button.SetActive(true);
        }
        else if (!GameManager.Instance.isLevelTwoComplete)
        {
            GameManager.Instance.mapBlock3.SetActive(true);
            GameManager.Instance.map3Button.SetActive(false);
        }
        if (GameManager.Instance.isShowTutorial)
            mShowTutorial = true;
        else mShowTutorial = false;

        if (PlayerPrefs.HasKey("masterVolume"))
            mixer.SetFloat("MasterVol", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume")) * 20);
        else mixer.SetFloat("MasterVol", Mathf.Log10(1) * 20);

        if (PlayerPrefs.HasKey("musicVolume"))
            mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        else mixer.SetFloat("MusicVol", Mathf.Log10(1) * 20);

        if (PlayerPrefs.HasKey("sfxVolume"))
            mixer.SetFloat("FXVol", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
        else mixer.SetFloat("FXVol", Mathf.Log10(1) * 20);

        if (PlayerPrefs.HasKey("lvlOneHighScore"))
            map1Score.text = PlayerPrefs.GetInt("lvlOneHighScore").ToString();
        else map1Score.text = "0";

        if (PlayerPrefs.HasKey("lvlTwoHighScore"))
            map2Score.text = PlayerPrefs.GetInt("lvlTwoHighScore").ToString();
        else map2Score.text = "0";

        if (PlayerPrefs.HasKey("lvlThreeHighScore"))
            map3Score.text = PlayerPrefs.GetInt("lvlThreeHighScore").ToString();
        else map3Score.text = "0";
    }
    public void SkipTutorial()
    {
        mTutorialScreen.SetActive(false); // hides main menu screen
        mMapPopUp.SetActive(true); // shows the map selection screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(levelFirstButton); // set a new selected object
    }
    public void DontShowTutorial()
    {
        if (!hasBeenToggled)
        {
            hasBeenToggled = true;
            defaultColor = toggleButtonBackground.GetComponent<Image>().color;
        }

        if (toggleButton.GetComponent<Toggle>().isOn)
        {
            toggleButtonBackground.GetComponent<Image>().color = Color.white;
            GameManager.Instance.isShowTutorial = false;
            GameManager.Instance.SaveInfo();
        }
        else if (!toggleButton.GetComponent<Toggle>().isOn)
        {
            toggleButtonBackground.GetComponent<Image>().color = defaultColor;
            GameManager.Instance.isShowTutorial = true;
            GameManager.Instance.SaveInfo();
        }
    }
    public void TutorialBackButton()
    {
        mMainMenu.SetActive(true); // shows the main menu screen
        mTutorialScreen.SetActive(false); // hides the map selection screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(tutorialCloseButton); // set a new selected object
    }
    public void PlayButton()
    {
        if (mShowTutorial)
        {
            mMainMenu.SetActive(false); // hides main menu screen
            mTutorialScreen.SetActive(true); // shows the map selection screen
            EventSystem.current.SetSelectedGameObject(null); // clear selected object
            EventSystem.current.SetSelectedGameObject(tutorialFirstButton); // set a new selected object
        }
        else if (!mShowTutorial)
        {
            mMainMenu.SetActive(false); // hides main menu screen
            mMapPopUp.SetActive(true); // shows the map selection screen
            EventSystem.current.SetSelectedGameObject(null); // clear selected object
            EventSystem.current.SetSelectedGameObject(levelFirstButton); // set a new selected object
        }

    }

    // options button
    public void OptionButton()
    {
        mMainMenu.SetActive(false); // hides main menu screen
        mOptionScreen.SetActive(true); // shows the options screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(optionFirstButton); // set a new selected object
    }


    // credits button
    public void CreditsButton()
    {
        mMainMenu.SetActive(false); // hides main menu screen
        mCreditScreen.SetActive(true); // shows the credits screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(creditFirstButton); // set a new selected object
    }
    // back button in options
    public void OptionReturn()
    {
        GameManager.Instance.SaveInfo();
        mMainMenu.SetActive(true); // shows the main menu screen
        mOptionScreen.SetActive(false); // hides the option screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(optionCloseButton); // set a new selected object
    }
    // back button in credits
    public void CreditReturn()
    {
        mMainMenu.SetActive(true); // shows the main menu screen
        mCreditScreen.SetActive(false); // hides the credit screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(creditCloseButton); // set a new selected object
    }
    // map selection back button
    public void MapReturn()
    {
        mMainMenu.SetActive(true); // shows the main menu screen
        mMapPopUp.SetActive(false); // hides the map selection screen
        EventSystem.current.SetSelectedGameObject(null); // clear selected object
        EventSystem.current.SetSelectedGameObject(levelCloseButton); // set a new selected object
    }

    // quit button
    public void QuitGame()
    {
        GameManager.Instance.SaveInfo();
        Application.Quit(); // quits the game when it's an executable
    }

    public void ResetSaveFile()
    {
        PlayerPrefs.DeleteAll();
    }

    public void AssetReturn()
    {
        mAssetCredit.SetActive(false);
        mCreditScreen.SetActive(true);
    }
    public void ShowAsset()
    {
        mAssetCredit.SetActive(true);
        mCreditScreen.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkUIScript : MonoBehaviour
{
    [Header("Debug Canvas")]
    [SerializeField] Image _debugBG;
    [SerializeField] Image _debugTitleBG;
    [SerializeField] Text _debugTitleText;

    [SerializeField] Sprite _debugBGImage;
    [SerializeField] Sprite _debugTitleImage;
    [SerializeField] string _debugTitleString;

    [Header("MainMenu Canvas")]
    [SerializeField] Image _mainMenuBG;
    [SerializeField] Image _mainMenuTitle;
    [SerializeField] Text _mainMenuTitleText;

    [SerializeField] Sprite _mainMenuBGImage;
    [SerializeField] Sprite _mainMenuTitleImage;
    [SerializeField] string _mainMenuTitleString;

    [Header("ControlsMenu Canvas")]
    [SerializeField] Image _controlsBG;
    [SerializeField] Image _controlsTitle;
    [SerializeField] Text _controlsTitleText;
    [SerializeField] Image _button1Image;
    [SerializeField] Text _button1Text;
    [SerializeField] Image _button2Image;
    [SerializeField] Text _button2Text;
    [SerializeField] Image _button3Image;
    [SerializeField] Text _button3Text;
    [SerializeField] Image _button4Image;
    [SerializeField] Text _button4Text;

    [SerializeField] Sprite _controlsBGImage;
    [SerializeField] Sprite _controlsTitleImage;
    [SerializeField] string _controlsTitleString;
    [SerializeField] Sprite _button1Sprite;
    [SerializeField] string _button1String;
    [SerializeField] Sprite _button2Sprite;
    [SerializeField] string _button2String;
    [SerializeField] Sprite _button3Sprite;
    [SerializeField] string _button3String;
    [SerializeField] Sprite _button4Sprite;
    [SerializeField] string _button4String;

    [Header("Game Canvas")]
    [SerializeField] Image _gameBGLeft;
    [SerializeField] Image _gameBGRight;

    [SerializeField] Image _healthImage;
    [SerializeField] Text _healthText;
    [SerializeField] Image _ammoImage;
    [SerializeField] Text _ammoText;
    [SerializeField] Image _scoreImage;
    [SerializeField] Text _scoreText;
    [SerializeField] Image _weaponImage;
    [SerializeField] Text _weaponText;

    [SerializeField] Sprite _BGLeftImage;
    [SerializeField] Sprite _BGRightImage;

    [SerializeField] Sprite _healthSprite;
    [SerializeField] Sprite _ammoSprite;
    [SerializeField] Sprite _scoreSprite;
    [SerializeField] Sprite _weaponSprite;

    [Header("Editor Canvas")]
    [SerializeField] Image _editorBG;
    [SerializeField] Image _editorTitle;
    [SerializeField] Text _editorTitleText;

    [SerializeField] Sprite _editorBGImage;
    [SerializeField] Sprite _editorTitleImage;
    [SerializeField] string _editorTitleString;

    [Header("EndGameMenu Canvas")]
    [SerializeField] Image _endBG;
    [SerializeField] Image _endTitle;
    [SerializeField] Text _endTitleText;
    [SerializeField] Image _endScoreBG;
    [SerializeField] Text _endScoreBGText;

    [SerializeField] Sprite _engBGImage;
    [SerializeField] Sprite _endTitleImage;
    [SerializeField] string _endTitleString;
    [SerializeField] Sprite _endScoreBGSprite;

    [Header("Debug")]
    [SerializeField] GameManagerScript _gameManager;

    private void Start()
    {
        SetUpReference();
        SubscribeEvents();
    }
    private void SetUpReference()
    {
        _gameManager = GameManagerScript.GMInstance;
    }
    private void SubscribeEvents()
    {
        _gameManager.OnGMSetUpComplete += SetUpUI;
    }
    private void SetUpUI()
    {
        // Debug
        _debugBG.sprite = _debugBGImage;
        _debugTitleBG.sprite = _debugTitleImage;
        _debugTitleText.text = _debugTitleString;

        // Main Menu
        _mainMenuBG.sprite = _mainMenuBGImage;
        _mainMenuTitle.sprite = _mainMenuTitleImage;
        _mainMenuTitleText.text = _mainMenuTitleString;

        // ControlsMenu
        _controlsBG.sprite = _controlsBGImage;
        _controlsTitle.sprite = _controlsTitleImage;
        _controlsTitleText.text = _controlsTitleString;

        _button1Image.sprite = _button1Sprite;
        _button1Text.text = _button1String;

        _button2Image.sprite = _button2Sprite;
        _button2Text.text = _button2String;

        _button3Image.sprite = _button3Sprite;
        _button3Text.text = _button3String;

        _button4Image.sprite = _button4Sprite;
        _button4Text.text = _button4String;

        // Editor Menu
        _editorBG.sprite = _editorBGImage;
        _editorTitle.sprite = _editorTitleImage;
        _editorTitleText.text = _editorTitleString;

        // End Screen
        _endBG.sprite = _engBGImage;
        _endTitle.sprite = _endTitleImage;
        _endTitleText.text = _endTitleString;
        //_endScoreBG.sprite = _endScoreBGSprite;

        // Game
        //_gameBGLeft.sprite = _BGLeftImage;
        //_gameBGRight.sprite = _BGRightImage;

        _healthImage.sprite = _healthSprite;
        _ammoImage.sprite = _ammoSprite;
        _scoreImage.sprite = _scoreSprite;
        _weaponImage.sprite = _weaponSprite;

    }

    public Text HealthTextUI { get { return _healthText;} set { _healthText = value; } }
    public Text AmmoTextUI { get { return _ammoText; } set { _ammoText = value; } }
    public Text ScoreTextUI { get { return _scoreText; } set { _scoreText = value; } }
    public Text WeaponTextUI { get { return _weaponText; } set { _weaponText = value; } }
    public Text ScoreEndScreenUI { get { return _endScoreBGText; } set { _endScoreBGText = value; } }

}

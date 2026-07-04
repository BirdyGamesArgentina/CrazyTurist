using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoSingleton<MenuManager>
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _howToPlayBackButton;
    [SerializeField] private Button _howToPlayPlayButton;
    [SerializeField] private Button _creditsBackButton;

    [SerializeField] private UIPanelTransition _settingsPanel;
    [SerializeField] private UIPanelTransition _howToPlayPanel;
    [SerializeField] private UIPanelTransition _creditsPanel;

    SaveData _saveData = new SaveData();

    protected override void OnAwake()
    {
        _playButton.onClick.AddListener(Btn_PlayCheck);
        _optionsButton.onClick.AddListener(Btn_Options);
        _howToPlayButton.onClick.AddListener(Btn_HowToPlay);
        _creditsButton.onClick.AddListener(Btn_Credits);
        _backButton.onClick.AddListener(Btn_OptionsBack);
        _howToPlayBackButton.onClick.AddListener(Btn_OptionsBack);
        _creditsBackButton.onClick.AddListener(Btn_OptionsBack);
        _howToPlayPlayButton.onClick.AddListener(Btn_Play);

        if (JSONSerialization.IsFileExist(SaveDataUtilities.SaveDataPath))
        {
            JSONSerialization.Deserialize(SaveDataUtilities.SaveDataPath, _saveData);
        }
        else
        {
            JSONSerialization.Serialize(SaveDataUtilities.SaveDataPath, _saveData);
        }
    }

    private void Btn_PlayCheck()
    {
        if (_saveData.firstPlay)
            Btn_Play();
        else
        {
            _howToPlayPanel.Open();
            _howToPlayPlayButton.gameObject.SetActive(true);
            _howToPlayBackButton.gameObject.SetActive(false);
            _saveData.firstPlay = true;
            JSONSerialization.Serialize(SaveDataUtilities.SaveDataPath, _saveData);
        }
    }

    private void Btn_Play()
    {
        SceneLoader.Load(1);
    }

    private void Btn_Options()
    {
        _settingsPanel.Open();
    }
    private void Btn_OptionsBack()
    {
        _settingsPanel.Close();
        _creditsPanel.Close();
        _howToPlayPanel.Close();
    }

    private void Btn_HowToPlay()
    {
        _howToPlayPanel.Open();
        _howToPlayPlayButton.gameObject.SetActive(false);
        _howToPlayBackButton.gameObject.SetActive(true);
    }

    private void Btn_Credits()
    {
        _creditsPanel.Open();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoSingleton<MenuManager>
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _highscoreButton;

    protected override void OnAwake()
    {
        _playButton.onClick.AddListener(Btn_Play);
        _optionsButton.onClick.AddListener(Btn_Options);
        _highscoreButton.onClick.AddListener(Btn_Highscore);
    }

    private void Btn_Play()
    {
        SceneLoader.Load(1);
    }

    private void Btn_Options()
    {

    }

    private void Btn_Highscore()
    {

    }
}

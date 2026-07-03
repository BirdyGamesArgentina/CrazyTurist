using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TextMeshProUGUI _panelTitle;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _resetButton.onClick.AddListener(Btn_Reset);
        _backToMenuButton.onClick.AddListener(Btn_BackToMenu);
    }

    private void Btn_Reset()
    {
        SceneLoader.Load(1);
    }

    private void Btn_BackToMenu()
    {
        SceneLoader.Load(0);
    }
}

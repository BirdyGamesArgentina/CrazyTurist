using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TextMeshProUGUI _panelTitle;
    [SerializeField] private TextMeshProUGUI _subText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private UIPanelTransition _panelTransition;
    [SerializeField] private UIPanelTransition _textPanelTransition;
    [SerializeField, TextArea] private string loseText;
    [SerializeField, TextArea] private string winText;

    private void Awake()
    {
        _resetButton.onClick.AddListener(Btn_Reset);
        _backToMenuButton.onClick.AddListener(Btn_BackToMenu);
    }

    private void Start()
    {
        var _eventBus = ServiceLocator.Instance.GetService<IEventBus>();
        _eventBus.Subscribe<OnEndScreenResult>(OnEndScreen);
    }

    public void OpenOver()
    {
        Time.timeScale = 0;
    }

    private void OnEndScreen(OnEndScreenResult result)
    {
        _panelTitle.text = result.Title;
        _scoreText.text = "Tu puntaje: " + result.Score;
        _subText.text = result.Title == "Perdiste" ? loseText : winText;
        _panelTransition.Open();
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2);
        _textPanelTransition.Open();
    }

    private void Btn_Reset()
    {
        Time.timeScale = 1;
        SceneLoader.Load(1);
    }

    private void Btn_BackToMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Load(0);
    }
}

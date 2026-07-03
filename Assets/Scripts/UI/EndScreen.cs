using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TextMeshProUGUI _panelTitle;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private UIPanelTransition _panelTransition;

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

    private void OnEndScreen(OnEndScreenResult result)
    {
        _panelTitle.text = result.Title;
        _scoreText.text = "Tu puntaje: " + result.Score;
        _panelTransition.Open();
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

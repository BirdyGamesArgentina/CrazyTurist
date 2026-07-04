using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using System;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class GameLoop : MonoSingleton<GameLoop>
{
    public event Action OnStartGame;
    public event Action OnWin;
    public event Action OnLose;

    protected override void OnAwake()
    {
        txt_timer.text = $"--:--";
    }

    // timer para el Juego
    float timer;
    [SerializeField] float time_to_play = 120f;
    bool inGame = false;
    [SerializeField] TextMeshProUGUI txt_timer;

    public void BeginGame() /// Esto se ejecuta con el trigger InGame
    {
        timer = time_to_play;
        inGame = true;
        OnStartGame.Invoke();
    }
    public static void Win(long score) => Instance._win(score);
    public static void Lose(long score) => Instance._lose(score);

    void _win(long score)
    {
        inGame = false;
        OnWin?.Invoke();
        ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Ganaste", score));
    }
    void _lose(long score)
    {
        inGame = false;
        OnLose?.Invoke();
        ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Perdiste", score));
    }


    private void Update()
    {
        if (inGame)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(timer / 60);
                int seconds = Mathf.FloorToInt(timer % 60);

                txt_timer.text = $"{minutes:00}:{seconds:00}";
            }
            else
            {
                timer = 0;

                txt_timer.text = $"<color=red>00:00</color>";

                if (PointOfInterestSystem.WinByInterest)
                {
                    _win(PointOfInterestSystem.CurrentScore);
                }
                else
                {
                    _lose(PointOfInterestSystem.CurrentScore);
                }
            }
        }
    }
}

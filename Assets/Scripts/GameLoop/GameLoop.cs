using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using System;
using UnityEngine;

public class GameLoop : MonoSingleton<GameLoop>
{
    public event Action OnStartGame;
    public event Action OnWin;
    public event Action OnLose;

    protected override void OnAwake()
    {
        
    }

    // timer para el Juego
    float timer;
    [SerializeField] float time_to_play = 2f;

    public void BeginGame() /// Esto se ejecuta con el trigger InGame
    {
        OnStartGame.Invoke();
    }
    public static void Win(long score) => Instance._win(score);
    public static void Lose(long score) => Instance._lose(score);

    void _win(long score)
    {
        OnWin.Invoke();
        ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Ganaste", score));
    }
    void _lose(long score)
    {
        OnLose.Invoke();
        ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Perdiste", score));
    }


    private void Update()
    {
        
    }
}

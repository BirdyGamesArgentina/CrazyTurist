using Game.Scripts.Shared.Events;
using Game.Scripts.Shared.ServiceLocator;
using System;
using System.Xml.Linq;
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
    bool inGame = false;

    public void BeginGame() /// Esto se ejecuta con el trigger InGame
    {
        inGame = true;
        OnStartGame.Invoke();
    }
    public static void Win(long score) => Instance._win(score);
    public static void Lose(long score) => Instance._lose(score);

    void _win(long score)
    {
        inGame = false;
        OnWin.Invoke();
        ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Ganaste", score));
    }
    void _lose(long score)
    {
        inGame = false;
        OnLose.Invoke();
        ServiceLocator.Instance.GetService<IEventBus>().Publish(new OnEndScreenResult("Perdiste", score));
    }


    private void Update()
    {
        if (inGame)
        {
            if (timer < time_to_play)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                
            }
        }
    }
}

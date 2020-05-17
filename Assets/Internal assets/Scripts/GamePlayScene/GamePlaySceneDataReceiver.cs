using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneDataReceiver : MonoBehaviour
{
    [SerializeField]
    private GamePlaySceneData _gamePlaySceneData;
    public static GamePlaySceneDataReceiver Instance
    {
        get; private set;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public GamePlaySceneData GamePlaySceneData
    {
        get
        {
            return _gamePlaySceneData;
        }
    }
}

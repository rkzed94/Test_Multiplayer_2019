using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiPlayerGame.Game;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    public CharacterProperty[] characters;
    public int selectedChara;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        characters = new CharacterProperty[3];

    }

}

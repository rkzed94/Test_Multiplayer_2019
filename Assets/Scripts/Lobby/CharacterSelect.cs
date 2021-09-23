using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using MultiPlayerGame.Game;

public class CharacterSelect : MonoBehaviour
{
    public CharacterProperty[] _characters;

    public GameObject _blocker;
    public GameObject teamSelectUi;


    //* Character1 Stats
    public string char1_name;
    public float char1_attack;
    public float char1_critical;
    public float char1_range;
    public Sprite char1_image;

    //* Character2 Stats
    public string char2_name;
    public float char2_attack;
    public float char2_critical;
    public float char2_range;
    public Sprite char2_image;

    //* Character3 Stats
    public string char3_name;
    public float char3_attack;
    public float char3_critical;
    public float char3_range;
    public Sprite char3_image;

    //* Selected Character
    public string _name;
    public float _attack;
    public float _critical;
    public float _range;
    public int _active;


    void Start()
    {
        _characters = new CharacterProperty[3];

        _characters[0].name = char1_name;
        _characters[0].attack = char1_attack;
        _characters[0].critical = char1_critical;
        _characters[0].range = char1_range;
        _characters[0].image = char1_image;

        _characters[1].name = char1_name;
        _characters[1].attack = char2_attack;
        _characters[1].critical = char2_critical;
        _characters[1].range = char2_range;
        _characters[1].image = char2_image;

        _characters[2].name = char1_name;
        _characters[2].attack = char3_attack;
        _characters[2].critical = char3_critical;
        _characters[2].range = char3_range;
        _characters[2].image = char3_image;

        CharacterManager.Instance.characters = _characters;

        //* Default selected Character;
        _active = 0;
        _name = _characters[0].name;
        _attack = _characters[0].attack;
        _critical = _characters[0].critical;
        _range = _characters[0].range;

        CharacterManager.Instance.selectedChara = 0;
    }

    private void Update()
    {
        //* Set blocker to active when the player already joined a team.
        // if (GameManager.Instance.joinedTeam)
        // {
        //     _blocker.SetActive(true);
        // }
        // else
        // {
        //     _blocker.SetActive(false);
        // }
    }

    public void Btn_SelectCharacter(int num)
    {
        _active = num;
        _name = _characters[num].name;
        _attack = _characters[num].attack;
        _critical = _characters[num].critical;
        _range = _characters[num].range;

        CharacterManager.Instance.selectedChara = num;
    }

}


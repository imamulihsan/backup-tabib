using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int currentHealth;
    public int currentXP;
    public int damageValue;
    public int currentLevel;
    public int i;
    public bool questFinished; 
    // public SerializableDictionary<string, bool> questFinished;

    public Vector3 playerPosition;
    // public SerializableDictionary<string, bool> itemButton;

    //the values defined in this constructor will be the default values
    //the game starts with when there's no data to load
    
    public GameData()
    {
        this.currentHealth = 2;
        this.currentXP = 0;
        this.damageValue = 1;
        this.currentLevel = 1;
        this.i = 0;
        // this.questFinished = questFinished;
         
        questFinished = true;
        playerPosition = new Vector3(-639.1f, -127.5f, 0f);
        // playerPosition = Vector3.zero;
        // itemButton = new SerializableDictionary<string, bool>();
    }
}

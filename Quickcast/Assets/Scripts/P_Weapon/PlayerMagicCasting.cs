using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMagicCasting : MonoBehaviour
{
    [SerializeField] Demo gestureRecog;
    [SerializeField] float slowedTimeScale = .7f;
    [SerializeField] int maxSpells = 5;

    //which spell being casted
    public bool isCasting = false;
    public bool isCasting1 = false;
    public bool isCasting2 = false;
    public bool isCasting3 = false;
    public bool isCasting4 = false;
    public bool isCasting5 = false;

    public string gestureSymbol;
    public float gestureAccuracy;
    public float gestureAccuracyToBeat = 0.65f;

    public List<string> spells = new List<string>();

    //text popups
    [SerializeField] public GameObject floatingPoints;

    private void Update()
    {
        if(isCasting)
        {
            Time.timeScale = slowedTimeScale;
        }
        else
        {
            Time.timeScale = 1;
        }
        addSpell(isCasting1, "circle");
        addSpell(isCasting2, "triangle");
    }
    private void SeparateString(string inputString, out string word, out float number)
    {
        string[] words = inputString.Split(' ');
        word = words[0];
        number = float.Parse(words[words.Length - 1]);
    }

    private void addSpell(bool castingSpellNumber, string spellName)
    {
        if (castingSpellNumber)
        {
            if (Input.GetMouseButtonUp(1)) 
            {
                SeparateString(gestureRecog.message, out gestureSymbol, out gestureAccuracy);
                if (gestureSymbol == spellName && gestureAccuracy > gestureAccuracyToBeat)
                {
                    spells.Add(gestureSymbol);
                    popupText("Success!");
                    cancelAllCasts();
                } else
                {
                    popupText("Try again!");
                }
            }
        }
    }

    public void popupText(string message)
    {
        GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity);
        points.transform.GetChild(0).GetComponent<TextMeshPro>().text = message;
    }

    public void popupText_Side(string message)
    {
        
        GameObject points = Instantiate(floatingPoints, transform.position + Vector3.left * 3, Quaternion.identity);
        points.transform.GetChild(0).GetComponent<TextMeshPro>().text = message;
    }

    private void cancelAllCasts()
    {
        isCasting = false;
        isCasting1 = false;
        isCasting2 = false;
        isCasting3 = false;
        isCasting4 = false;
        isCasting5 = false;
    }

    private void OnConjureSpell1()
    {
        Debug.Log(Application.persistentDataPath);
        cancelAllCasts();
        if (spells.Count >= maxSpells)
        {
            popupText("Max spells!");
        } else
        {
            popupText("Casting!");
            isCasting = true;
            isCasting1 = true;
        }
    }

    private void OnConjureSpell2()
    {
        cancelAllCasts();
        if (spells.Count >= maxSpells)
        {
            popupText("Max spells!");
        }
        else
        {
            popupText("Casting!");
            isCasting = true;
            isCasting2 = true;
        }
    }
}

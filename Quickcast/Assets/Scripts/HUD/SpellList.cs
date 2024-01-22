using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellList : MonoBehaviour
{
    [SerializeField] PlayerMagicCasting magicCasting;
    public TMP_Text spellList;
    // Start is called before the first frame update
    void Start()
    {
        spellList = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (magicCasting.spells.Count == 0)
        {
            spellList.text = "Spells: Empty!";
        } else
        {
            spellList.text = "Spells: " + $"{string.Join(",", magicCasting.spells)}";
        }
    }
}

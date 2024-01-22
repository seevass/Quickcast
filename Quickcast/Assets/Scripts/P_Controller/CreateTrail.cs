using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTrail : MonoBehaviour
{
    [SerializeField] private GameObject PrefabTrail;
    // Update is called once per frame
    void Update()
    {
    }
    private void OnDrawSpell()
    {
        
        Debug.Log("SpellDrawn");
        if (GetComponent<PlayerMagicCasting>().isCasting!)
        {
            Instantiate(PrefabTrail);
        }
    }
}

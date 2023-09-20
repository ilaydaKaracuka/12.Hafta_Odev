using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName="Building Preset",menuName ="New Building Preset")]  //klasöre Building Presets demiþtik burada preset

// Assets kýsmýnda sað click yapýnca create+ new building preset özelliði geldi
public class BuildingPreset : ScriptableObject
{
    public int cost; //maliyet
    public int costPerTurn; 
    public GameObject prefab; //ev
    public int population;
    public int jobs; //fabrika kuruyorsak iþle ilgili bilgiler
    public int food; //tarla vs.



}

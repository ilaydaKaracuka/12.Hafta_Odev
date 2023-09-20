using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName="Building Preset",menuName ="New Building Preset")]  //klas�re Building Presets demi�tik burada preset

// Assets k�sm�nda sa� click yap�nca create+ new building preset �zelli�i geldi
public class BuildingPreset : ScriptableObject
{
    public int cost; //maliyet
    public int costPerTurn; 
    public GameObject prefab; //ev
    public int population;
    public int jobs; //fabrika kuruyorsak i�le ilgili bilgiler
    public int food; //tarla vs.



}

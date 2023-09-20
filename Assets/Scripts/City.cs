using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public int money;
    public int day;
    public int curPopulation;
    public int curJobs;
    public int curFood;
    public Canvas canvas;
    public Canvas canvas2;
 
    public int maxPopulation; //�ehir max ka� populasyon kald�rabilir
    public int maxJobs;
    public int incomePerJob; //her i� ba�� gelir
    public TextMeshProUGUI statsText, statsText2;
    public List<building> buildings = new List<building>();
    public static City instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        UpdateStatText(); 
    }
    public void OnPlaceBuilding(building building) //yap� eklersek
    {
        money -= building.preset.cost; //moneyden yapt��� yap�lar�n maliyetini d��s�n
        maxPopulation += building.preset.population;
        maxJobs +=building.preset.jobs;
        buildings.Add(building);
        UpdateStatText(); //her bina ekledik�e textde de g�ncellesin
    
    }
    public void OnRemoveBuilding(building building) //yap�y� silersek 
    {
       
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText(); //her bina silindik�e textde de g�ncellesin
    }
    void UpdateStatText() //text de verileri g�ncellesin
    {
        statsText.text = string.Format("Day: {0} Money:{1} Pop:{2} /{3} Jobs:{4}/{5}  Food:{6} ", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });
        //new object[7] 7 elemanl� olacak bunlar day,money,curPopulation.....
        statsText2.text = string.Format("Day: {0} Money:{1} Pop:{2} /{3} Jobs:{4}/{5}  Food:{6} ", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });

    }
    public void EndTurn()
    {
        day++; //g�n arts�n end turn butonuna bas�nca
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        UpdateStatText();

    }
    

    void CalculateMoney()
    {
        money += curJobs * incomePerJob;// herbir i�i birim geliriyle �arps�n  moneye eklesin
        foreach (building building in buildings) // buildings i�erisinden 
            money -= building.preset.costPerTurn;//d�n���m maliyetini d��
    }

    void CalculatePopulation()
    {
        if(curFood>=curPopulation &&curPopulation<maxPopulation ) //curFood= �uanki g�da
        {
            curFood -= curPopulation / 4; //mevcut yiyece�i n�fusun 4 de biriyle azalts�n
            curPopulation = Mathf.Min(curPopulation+(curFood/4),maxPopulation);//hangiis min ise onu al

        
        }
        else if(curFood<curPopulation)
        {
            curPopulation = curFood;
        }
    }
    void CalculateJobs()
    {
        curJobs = Mathf.Min(curPopulation, maxJobs);
    }

    void CalculateFood()
    {
        curFood = 0;
        foreach(building building in buildings)
        {
            curFood += building.preset.food; 
        }
    }


    public void indoor()
    {
        canvas.enabled = false;
        canvas2.enabled = true;
    }
    public void outdoor()
    {
        canvas2.enabled = false;
        canvas.enabled = true;
    }










}

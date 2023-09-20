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
 
    public int maxPopulation; //þehir max kaç populasyon kaldýrabilir
    public int maxJobs;
    public int incomePerJob; //her iþ baþý gelir
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
    public void OnPlaceBuilding(building building) //yapý eklersek
    {
        money -= building.preset.cost; //moneyden yaptýðý yapýlarýn maliyetini düþsün
        maxPopulation += building.preset.population;
        maxJobs +=building.preset.jobs;
        buildings.Add(building);
        UpdateStatText(); //her bina ekledikçe textde de güncellesin
    
    }
    public void OnRemoveBuilding(building building) //yapýyý silersek 
    {
       
        maxPopulation -= building.preset.population;
        maxJobs -= building.preset.jobs;
        buildings.Remove(building);
        Destroy(building.gameObject);
        UpdateStatText(); //her bina silindikçe textde de güncellesin
    }
    void UpdateStatText() //text de verileri güncellesin
    {
        statsText.text = string.Format("Day: {0} Money:{1} Pop:{2} /{3} Jobs:{4}/{5}  Food:{6} ", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });
        //new object[7] 7 elemanlý olacak bunlar day,money,curPopulation.....
        statsText2.text = string.Format("Day: {0} Money:{1} Pop:{2} /{3} Jobs:{4}/{5}  Food:{6} ", new object[7] { day, money, curPopulation, maxPopulation, curJobs, maxJobs, curFood });

    }
    public void EndTurn()
    {
        day++; //gün artsýn end turn butonuna basýnca
        CalculateMoney();
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        UpdateStatText();

    }
    

    void CalculateMoney()
    {
        money += curJobs * incomePerJob;// herbir iþi birim geliriyle çarpsýn  moneye eklesin
        foreach (building building in buildings) // buildings içerisinden 
            money -= building.preset.costPerTurn;//dönüþüm maliyetini düþ
    }

    void CalculatePopulation()
    {
        if(curFood>=curPopulation &&curPopulation<maxPopulation ) //curFood= þuanki gýda
        {
            curFood -= curPopulation / 4; //mevcut yiyeceði nüfusun 4 de biriyle azaltsýn
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

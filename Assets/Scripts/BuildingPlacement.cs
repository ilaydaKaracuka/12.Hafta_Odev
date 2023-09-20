using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing; //yap�y� yerle�tirece�imiz yerde bi �ey var m� yok mu
    private bool currentlyBulldozering; //silmek i�in

    private BuildingPreset curBuildingPreset;   //building presetler vard� home,farm vs. 
    private float indicatorUpdateTime = 0.05f;  //biraz gecikme i�in 
    private float lastUpdateTime; 
    private Vector3 curIndicatorPos; //�uan ki indicator(g�sterge)pozisyonu
    public GameObject placementIndicator; //yap�y� yerle�tirece�in zaman g�sterilen transparan obje
    public GameObject bulldozerIndicator; //yap�y� silece�in zaman g�sterilen transparan obje
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        /* if(City.instance.money<preset.cost //param var m� ?B�t�e yap�n�n maliyetinden azsa yapamaz
            return;
         */
        currentlyPlacing = true; 
        curBuildingPreset = preset;  // preset neyse ev , fabrika vs.
        placementIndicator.SetActive(true);
     }
    public void CancelBuildingPlacement()  //iptal. silmek degil
    {
        
        currentlyPlacing = false;
       
        placementIndicator.SetActive(false);
    }

    public void ToggleBulldoze() //yap� silme
    {

        currentlyBulldozering= !currentlyBulldozering; //ne se�ildiyse z�tt� olsun

        bulldozerIndicator.SetActive(currentlyBulldozering); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //iptal etmek i�in escap tu�u
            CancelBuildingPlacement();
        if (Time.time - lastUpdateTime > indicatorUpdateTime) //�uan ki zaman=Time.time  ge�en s�reyi a�m��sa update yapabilir
        {                                                     //indicatorUpdateTime=gecikme zaman� 
            lastUpdateTime = Time.time;
            curIndicatorPos = Selector.instance.GetCurTilePosition();
            if (currentlyPlacing)  //
                placementIndicator.transform.position = curIndicatorPos;
            else if (currentlyBulldozering)
                bulldozerIndicator.transform.position = curIndicatorPos;

        }
        if (Input.GetMouseButtonDown(0) && currentlyPlacing) //mouse sol tu�a bas�nca currentlyplacing m�saitse(true ise)
            PlaceBuilding();                  //yani yerle�tirilecek konum m�saitse PlaceBuilding() �a��r ve yerle�tir
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)  //currentlyBulldozeirng true ise ve sol click bas�l�rsa
            Bulldoze(); //silme i�lemi yap
    }

    void PlaceBuilding() //bina yapmak
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity); //instaniate(obje,konum,y�n)
        City.instance.OnPlaceBuilding(buildingObj.GetComponent<building>()); //bina ekleyince City scriptinde OnPlaceBuilding
                                                                             //fonksiyonunda money,jobs,food vs. hesaplar� g�ncellesin
        CancelBuildingPlacement(); //yerle�tirdikten sonra cancel yapabiliyorsun
    }
    void Bulldoze()   //bina silmek
    {
        building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position==curIndicatorPos);  //�yle bir x bul ki positionu curIndicatorPOS E��T OLSUN
        if(buildingToDestroy!=null) //varsa
        {
            City.instance.OnRemoveBuilding(buildingToDestroy)  ;
       }
    }




}

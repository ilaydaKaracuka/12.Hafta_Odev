using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing; //yapýyý yerleþtireceðimiz yerde bi þey var mý yok mu
    private bool currentlyBulldozering; //silmek için

    private BuildingPreset curBuildingPreset;   //building presetler vardý home,farm vs. 
    private float indicatorUpdateTime = 0.05f;  //biraz gecikme için 
    private float lastUpdateTime; 
    private Vector3 curIndicatorPos; //þuan ki indicator(gösterge)pozisyonu
    public GameObject placementIndicator; //yapýyý yerleþtireceðin zaman gösterilen transparan obje
    public GameObject bulldozerIndicator; //yapýyý sileceðin zaman gösterilen transparan obje
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        /* if(City.instance.money<preset.cost //param var mý ?Bütçe yapýnýn maliyetinden azsa yapamaz
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

    public void ToggleBulldoze() //yapý silme
    {

        currentlyBulldozering= !currentlyBulldozering; //ne seçildiyse zýttý olsun

        bulldozerIndicator.SetActive(currentlyBulldozering); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))  //iptal etmek için escap tuþu
            CancelBuildingPlacement();
        if (Time.time - lastUpdateTime > indicatorUpdateTime) //þuan ki zaman=Time.time  geçen süreyi aþmýþsa update yapabilir
        {                                                     //indicatorUpdateTime=gecikme zamaný 
            lastUpdateTime = Time.time;
            curIndicatorPos = Selector.instance.GetCurTilePosition();
            if (currentlyPlacing)  //
                placementIndicator.transform.position = curIndicatorPos;
            else if (currentlyBulldozering)
                bulldozerIndicator.transform.position = curIndicatorPos;

        }
        if (Input.GetMouseButtonDown(0) && currentlyPlacing) //mouse sol tuþa basýnca currentlyplacing müsaitse(true ise)
            PlaceBuilding();                  //yani yerleþtirilecek konum müsaitse PlaceBuilding() çaðýr ve yerleþtir
        else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)  //currentlyBulldozeirng true ise ve sol click basýlýrsa
            Bulldoze(); //silme iþlemi yap
    }

    void PlaceBuilding() //bina yapmak
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity); //instaniate(obje,konum,yön)
        City.instance.OnPlaceBuilding(buildingObj.GetComponent<building>()); //bina ekleyince City scriptinde OnPlaceBuilding
                                                                             //fonksiyonunda money,jobs,food vs. hesaplarý güncellesin
        CancelBuildingPlacement(); //yerleþtirdikten sonra cancel yapabiliyorsun
    }
    void Bulldoze()   //bina silmek
    {
        building buildingToDestroy = City.instance.buildings.Find(x => x.transform.position==curIndicatorPos);  //öyle bir x bul ki positionu curIndicatorPOS EÞÝT OLSUN
        if(buildingToDestroy!=null) //varsa
        {
            City.instance.OnRemoveBuilding(buildingToDestroy)  ;
       }
    }




}

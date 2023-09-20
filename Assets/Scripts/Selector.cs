using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cam = Camera.main;

    }
    public Vector3 GetCurTilePosition() //þuan ki zemin pozisyon
    {
        if(EventSystem.current.IsPointerOverGameObject() ) //UI a deðdiyse canvasa dokunduysa
        {
            return new Vector3(0, -99, 0); //canvas da iþlem yaptýysan hiçbir þey yapmasýn
        }
        Plane plane = new Plane(Vector3.up, Vector3.zero); //düz bir zemin (2 boyutlu düzlem) oluþturduk
        Ray ray =cam.ScreenPointToRay(Input.mousePosition); //zemine dokunuyoruz mouse ile bir yere týklýyoruz
        float rayOut = 0.0f;
        if(plane.Raycast(ray,out rayOut)) //zemine dokunduysa
        {
            //eklenecek yer 
            Vector3 newPos =ray.GetPoint(rayOut)-new Vector3(0.05f,0.0f,0.5f);
            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0.0f, Mathf.CeilToInt(newPos.z));
            //yapýyý konumlandýrýrken yukarý yuvarlama yapar tam zemindeki kareye otursun
            return newPos;
        }


        return new Vector3(0,-99,0);

    }






}

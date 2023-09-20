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
    public Vector3 GetCurTilePosition() //�uan ki zemin pozisyon
    {
        if(EventSystem.current.IsPointerOverGameObject() ) //UI a de�diyse canvasa dokunduysa
        {
            return new Vector3(0, -99, 0); //canvas da i�lem yapt�ysan hi�bir �ey yapmas�n
        }
        Plane plane = new Plane(Vector3.up, Vector3.zero); //d�z bir zemin (2 boyutlu d�zlem) olu�turduk
        Ray ray =cam.ScreenPointToRay(Input.mousePosition); //zemine dokunuyoruz mouse ile bir yere t�kl�yoruz
        float rayOut = 0.0f;
        if(plane.Raycast(ray,out rayOut)) //zemine dokunduysa
        {
            //eklenecek yer 
            Vector3 newPos =ray.GetPoint(rayOut)-new Vector3(0.05f,0.0f,0.5f);
            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0.0f, Mathf.CeilToInt(newPos.z));
            //yap�y� konumland�r�rken yukar� yuvarlama yapar tam zemindeki kareye otursun
            return newPos;
        }


        return new Vector3(0,-99,0);

    }






}

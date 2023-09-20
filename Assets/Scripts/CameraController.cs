using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float minZoom, maxZoom;
    public float zoomSpeed, rotateSpeed;
    private float curZoom; //current=�uan ki
    private Camera cam;
    private void Start()
    {
        cam = Camera.main; //�uan ki kameray� al�r
        curZoom = cam.transform.localPosition.y;//Objenin ebeveyninin pozisyonunu baz olarak sahip oldu�unu pozisyon
        curXRot = -50; // CameraAnchor objesinin x rotation bilgisi -50 yapm��t�k

    }
    void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;  // zoom yapmak i�in mouse tekerle�i kullan
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);   //zoomlamaya limit  curZoom degeri  minZoom ve maxZoom aras� deger als�n
                                                            //minZoom dan azsa minZoom ,maxZoom dan fazlaysa maxZoom als�n aras�ndaysa deger ayn� kals�n         

        cam.transform.localPosition = Vector3.up * curZoom;   //curZoom ile �arparak cameran�n local position y sini de�i�tirdik
          //cameray� zoomlad�k �imdi d�nmesi i�in:
          if(Input.GetMouseButton(1)) //sag buton
        {
            // g�r�� a��s� ayarlmaak i�in
            float x = Input.GetAxis("Mouse X") ;              
            float y = Input.GetAxis("Mouse Y");
            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot,minXRot,maxXRot);
            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y +(x*rotateSpeed)  ,0.0f );
        }
        //movement  sa�a-sola /ileri-geri hareket edecek kamera 
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize(); //hareketi kusursuzla�t�rma Vekt�r de�i�kenini 1 birimlik
                             //uzunlu�a sahip olacak �ekilde x,y,z bile�enlerini de�i�tirir.
                             // koordinat sistem,nde x,y,z eksenlerinde �rn. sa�/sol tu�a bas�nca 2 br,yukar�/a�a�� 2 br hareket
                             // etsin yani x=y=2 yaparsak hipoten�sten dolay� z=karek�k(2^2+2^2)= 2den farkl� ��kar biz her eksende ayn� h�zda gitmesini itedi�imiz i�in 1 yapar
                             // oklarla do�rultusun de�i�tiriyoruz h�z�n� de�il
        Vector3 right = cam.transform.right;
        float moveX =Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 dir =forward*moveZ+right*moveX;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        transform.position += dir;
    
    }
}

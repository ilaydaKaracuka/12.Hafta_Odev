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
    private float curZoom; //current=þuan ki
    private Camera cam;
    private void Start()
    {
        cam = Camera.main; //þuan ki kamerayý alýr
        curZoom = cam.transform.localPosition.y;//Objenin ebeveyninin pozisyonunu baz olarak sahip olduðunu pozisyon
        curXRot = -50; // CameraAnchor objesinin x rotation bilgisi -50 yapmýþtýk

    }
    void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;  // zoom yapmak için mouse tekerleði kullan
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);   //zoomlamaya limit  curZoom degeri  minZoom ve maxZoom arasý deger alsýn
                                                            //minZoom dan azsa minZoom ,maxZoom dan fazlaysa maxZoom alsýn arasýndaysa deger ayný kalsýn         

        cam.transform.localPosition = Vector3.up * curZoom;   //curZoom ile çarparak cameranýn local position y sini deðiþtirdik
          //camerayý zoomladýk þimdi dönmesi için:
          if(Input.GetMouseButton(1)) //sag buton
        {
            // görüþ açýsý ayarlmaak için
            float x = Input.GetAxis("Mouse X") ;              
            float y = Input.GetAxis("Mouse Y");
            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot,minXRot,maxXRot);
            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y +(x*rotateSpeed)  ,0.0f );
        }
        //movement  saða-sola /ileri-geri hareket edecek kamera 
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize(); //hareketi kusursuzlaþtýrma Vektör deðiþkenini 1 birimlik
                             //uzunluða sahip olacak þekilde x,y,z bileþenlerini deðiþtirir.
                             // koordinat sistem,nde x,y,z eksenlerinde örn. sað/sol tuþa basýnca 2 br,yukarý/aþaðý 2 br hareket
                             // etsin yani x=y=2 yaparsak hipotenüsten dolayý z=karekök(2^2+2^2)= 2den farklý çýkar biz her eksende ayný hýzda gitmesini itediðimiz için 1 yapar
                             // oklarla doðrultusun deðiþtiriyoruz hýzýný deðil
        Vector3 right = cam.transform.right;
        float moveX =Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 dir =forward*moveZ+right*moveX;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        transform.position += dir;
    
    }
}

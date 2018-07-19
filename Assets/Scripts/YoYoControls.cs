using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoYoControls : MonoBehaviour {

    LineRenderer lr;

    public GameObject YoYo;
    public float throwSpeed;
    public float retractSpeed;
    public float stringLength;

    private Rigidbody2D yb;
    private Vector2 mousePos;
    private float angle;
    private float distance;
    private bool thrown;
    private bool returning;
    // Use this for initialization
    void Start(){
        lr = GetComponent<LineRenderer>();
        yb = YoYo.GetComponent<Rigidbody2D>();
    }     

    // Update is called once per frame
    void Update() {
  
        //Aiming
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos = Input.mousePosition;
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        //String
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, YoYo.transform.position);

        distance = Vector3.Distance(YoYo.transform.position, transform.position);
 
        if (distance >= stringLength)
        {
            yb.gravityScale = 1f;
            yb.velocity = Vector3.zero;
            YoYo.transform.position = YoYo.transform.position;
        }
        if(distance <= 0f)
        {
            YoYo.transform.Rotate(0f, 0f, 0f);
            yb.velocity = Vector3.zero;
            returning = false;
            thrown = false;
        }

        if (thrown)
        {
            YoYo.transform.Rotate(0f, 0f, -throwSpeed * 10 * Time.deltaTime);
        }

        if (returning)
        {
            yb.gravityScale = 0f;
            YoYo.transform.position = Vector3.MoveTowards(YoYo.transform.position, transform.position, retractSpeed * Time.deltaTime);
            YoYo.transform.Rotate(0f, 0f, throwSpeed * 10 * Time.deltaTime);
        }
       
      
    
    }
    void FixedUpdate()
    {

        //Throwing
        if (distance != stringLength && !returning)
        {
            if (Input.GetMouseButtonDown(0))
            {
                yb.AddForce(transform.right * throwSpeed);//ForceMode2D.Impulse);

                thrown = true;
                returning = false;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {          
            yb.velocity = Vector3.zero;
            returning = true;
            thrown = false;
        }
        
        
	}
}

using UnityEngine;

public class Slimemovment : MonoBehaviour
{
    public float spped = 1.0f;
    public float distance = 1.0f;
    public bool movingleft = true;
    public Transform groundDectection;
    void Start()
    {
        
    }


    void Update()
    {

    }
    private void OnCollision(Collision collision, string tag)
    {
       if (collision.collider.CompareTag ("Ground"))
       {
            if(movingleft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingleft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingleft = true;
            }
       }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class targetController : MonoBehaviour
{
    // Start is called before the first frame update
    // public TextMeshProUGUI hitCountText;
    public GameObject gun;
    public GameObject attackText;
    private GunController gun_info;

    void Start()
    {
        gun_info = gun.GetComponent<GunController>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Bullet"))
        {
            
            
            GameObject showingTextObject = Instantiate(attackText) as GameObject;
            Vector3 offset = new Vector3( 2, 0, 1 );
            showingTextObject.transform.localPosition = gameObject.transform.localPosition + offset;
            // showingTextObject.transform.localScale = gameObject.transform.localScale;            
            Debug.Log(showingTextObject.transform.position);
            Destroy(showingTextObject, 2);
            gameObject.SetActive(false);
            gun_info.hitCount+=1;
            gun_info.SetCountText();


            // hitCountText.text = "Target Shoot: " + hitCount.ToString();

        }
    }

    // IEnumerator ExampleCoroutine()
    // {
    //     //Print the time of when the function is first called.
    //     Debug.Log("Routine");
    //     GameObject showingTextObject = Instantiate(attackText) as GameObject;
    //     // showingTextObject.transform.localPosition = gameObject.transform.localPosition;
    //     // showingTextObject.transform.localScale = gameObject.transform.localScale;
    //     // myNewObject.transform.parent = other.gameObject.transform;

    //     // showingTextObject.SetActive(true);

    //     // //yield on a new YieldInstruction that waits for 5 seconds.
    //     // yield return new WaitForSeconds(2);

    //     // //After we have waited 5 seconds print the time again.
    //     // showingTextObject.SetActive(false);
    //     Destroy(showingTextObject, 2);
    // }    
    
}

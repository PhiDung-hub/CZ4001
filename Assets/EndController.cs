using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject stateManager;
    private GameStateManager state_info;


    public int mode = 0;
    void Start()
    {
        state_info = stateManager.GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    public void SetEndMode(int index){
        mode = index;
    }
    void Update(){
        // End by time
        if(mode == 1){
            if (state_info.seconds >= 1 * 60){
                state_info.stateEnd();
            }
        }
        // End by count
        // else if(mode == 0){
        //     if (gun_info.hitCount >= 3){
        //         end_canvas.SetActive(true);
        //     }
        // }
    }
}

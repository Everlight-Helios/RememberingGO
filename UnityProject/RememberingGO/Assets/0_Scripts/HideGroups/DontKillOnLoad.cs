using UnityEngine;
using UnityEngine.SceneManagement;

public class DontKillOnLoad : MonoBehaviour {

    Transform player;
	
	void Awake () {

        //DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            Destroy(this.gameObject);
        }

        if(player == null)
        {
            if (SceneManager.GetSceneByName("Main_overkill").isLoaded)
            {

                player = GameObject.Find("Player").transform;
                transform.parent = player.transform;
                transform.localPosition = Vector3.zero;
             

            }
        }
       
    }

}

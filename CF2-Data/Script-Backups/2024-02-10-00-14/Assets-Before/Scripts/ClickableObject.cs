using UnityEngine;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour
{

    public static ClickableObject instance;
    public TextMesh PlayerNameHolder;
    public Text PlayerNameTxt;
    public Camera MyCam;

    public GameObject Gun;



    //public LayerMask layertodetact;


    public bool TurnForPlayer;
    public bool playerturnworking;



    public bool GunpickedPlayer;
    public bool FireCompleted;





    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerNameTxt.text= PlayerPrefs.GetString("PlayerName");
    }

    void Update()
    {
       // Debug.LogError("Working");
        // Check for mouse click or touch input
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            Debug.LogError("Wworking");

            // Cast a ray from the screen to the world
            Ray ray = MyCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a GameObject
            if (Physics.Raycast(ray, out hit,Mathf.Infinity))
            {
                if (hit.transform.gameObject.tag == "BookButtons")
                {

             
                        if (hit.transform.name == "Back")
                        {
                            Debug.LogError("Removing end Character");
                            PlayerNameHolder.text = PlayerNameHolder.text.Remove(PlayerNameHolder.text.Length - 1);

                        }
                        else if (hit.transform.name == "Enter")
                        {
                                if (PlayerNameHolder.text.Length <= 0)
                                {
                                      GamePlayManager.Instance.PlayerName = "Player";
                                      PlayerPrefs.SetString("PlayerName", "Player");  
                                }
                                else
                                {
                                      GamePlayManager.Instance.PlayerName = PlayerNameHolder.text;
                                      PlayerPrefs.SetString("PlayerName",PlayerNameHolder.text);

                                }
                        PlayerNameTxt.text = PlayerPrefs.GetString("PlayerName");
                            Debug.LogError("Start Game here After Contract");

                        GamePlayManager.Instance.closeBookcontractwithdelay(0.8f);
                        }
                        else
                        {
                          PlayerNameHolder.text += hit.transform.gameObject.name.ToString();

                        }

                }


                //Working For items
                //if (hit.transform.gameObject.tag == "TablePositions")
                //{
                //    PlayerTurnManager.instance.moveboxitem(hit.transform.position);
                //}
                if (TurnForPlayer)
                {
                    if (GunpickedPlayer)
                    {
                        if (FireCompleted)
                        {
                            PlayerTurnManager.instance.placegun(Gun);
                        }
                        else
                        {
                            PlayerTurnManager.instance.FireGun(Gun);

                        }
                    }
                    else
                    {
                        if (hit.transform.gameObject.tag == "Gun")
                        {
                            PlayerTurnManager.instance.PicGun(Gun);
                        }
                    }
                   

                }
                //else
                //{



                //    //Work For Enemy
                //    EnemyTurnManager.instance.PicGun(Gun);

                 
                //}

              
               

            }
        }


        else if(!TurnForPlayer)
        {
            if (playerturnworking)
            {

            }
            else
            {
                EnemyTurnManager.instance.PicGun(Gun);
                playerturnworking = true;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////
/// ///////////  ARTIFICIAL INTELLIGENCE  player Script 
/// Author : 	Miguel Angel Fernandez Graciani
/// Date :	2021-02-07
/// Observations :
///     - THIS IS AN ARTIFICIAL INTELLIGENT SCRIPT
///     - You must call to the "public void moveOn(Vector3 directionForce, float movUnits)" in "ScPlayerControl" to move the player 
///     - You must Change it to define the artiicial intelligence of this player
/// </summary>
public class ScPlayerAI_Near : MonoBehaviour {

    public GameObject Game;  // To access to the general object "Game"

    DateTime date_lastChamge;
    protected double periodMilisec;

    protected Vector3 movement;  // Direction of the force that will be exerted on the gameobject
    protected float playersMovUnits;  //  Amount of force that will be exerted on the gameobject

    public List<gameElement> Teammates_list;
    public List<gameElement> opponents_list;

    public float rangoDeAlerta;
    public LayerMask capaDelJugador;
    bool estarAlerta;
    public Transform jugador;
    public float velocidad;

    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Start()
    {

        // We access to the general object "Game"
        Game = GameObject.FindWithTag("Game");

        date_lastChamge = DateTime.Now; // We initialize the date value
        periodMilisec = 1000f;  // We change each "periodoMiliseg"/1000 seconds

        movement = new Vector3(0.0f, 0.0f, 0.0f); // We initialize the date value
        playersMovUnits = 1f; // We initialize the date value

        // if we are team near: (if you are not, comment these two lines)
        Teammates_list = Game.GetComponent<ScGameGlobalData>().listTeam_Near;
        opponents_list = Game.GetComponent<ScGameGlobalData>().listTeam_Far;

        // if we are team far: (if you are not, comment these two lines)
//        Teammates_list = Game.GetComponent<ScGameGlobalData>().listTeam_Far;
//        opponents_list = Game.GetComponent<ScGameGlobalData>().listTeam_Near;

    }  // FIn de - void Start()

    // Update is called once per frame
    /// <summary>
    /// ///////////  ARTIFICIAL INTELLIGENCE  MINION Script 
    /// Author : 	
    /// Date :	
    /// Observations :
    /// </summary>
    void Update()
    {

        // You can access to the information of all elements in the game using sentences lake the following

        foreach (gameElement element_mate in Teammates_list)
        {
            Vector3 posicion = element_mate.giveToMePosition();
            Debug.Log("vector x :" + posicion.x.ToString());
            Debug.Log("In Teammates : " +
                " - element_mate - label : " + element_mate.giveToMeTag() +
                " - element_mate - id : " + element_mate.giveToMeId() +
                " - element_mate - Team : " + element_mate.giveToMeTeam() +
                " - element_mate position x : " + posicion.x +
                " - element_mate position z : " + posicion.z +
                " - element_mate position y : " + posicion.y);
        }  // Fin de - foreach (gameElement element_mate in Teammates_list)

        foreach (gameElement element_opponent in opponents_list)
        {
            Vector3 posicion = element_opponent.giveToMePosition();
            Debug.Log("vector x :" + posicion.x.ToString());
            Debug.Log("In team Far : " +
                " - element_opponent - label : " + element_opponent.giveToMeTag() +
                " - element_opponent - id : " + element_opponent.giveToMeId() +
                " - element_opponent - Team : " + element_opponent.giveToMeTeam() +
                " - element_opponent posicion x : " + posicion.x +
                " - element_opponent posicion z : " + posicion.z +
                " - element_opponent posicion y : " + posicion.y);
        }  // Fin de - foreach (gameElement element_mate in Teammates_list)

        estarAlerta = Physics.CheckSphere(transform.position, rangoDeAlerta, capaDelJugador);

        if (estarAlerta == true)
        {
            Vector3 posJugador = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);
            transform.LookAt(posJugador);
            transform.position = Vector3.MoveTowards(transform.position, posJugador, velocidad * Time.deltaTime);
        }

        foreach (gameElement elementProfit in Game.GetComponent<ScGameGlobalData>().listProfits)
        {
            Vector3 posicion = elementProfit.giveToMePosition();
            Debug.Log("vector x :" + posicion.x.ToString());
            Debug.Log("In profits : " +
                " - element profit - label : " + elementProfit.giveToMeTag() +
                " - element profit - id : " + elementProfit.giveToMeId() +
                " - element profit - Team : " + elementProfit.giveToMeTeam() +
                " - element profit posicion x : " + posicion.x +
                " - element profit posicion z : " + posicion.z +
                " - element profit posicion y : " + posicion.y);
        }  // Fin de - foreach (gameElement element_mate in Teammates_list)

    }  // FIn de - void Update()

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////
    /// ///////////  FixedUpdate()
    /// Author : 	Miguel Angel Fernandez Graciani
    /// Date :	2021-02-07
    /// Observations :
    ///     - THIS IS AN ARTIFICIAL INTELLIGENT SCRIPT
    ///     - You must Change it to define the artiicial intelligence of this player
    ///     - This one is only an example to manage the player
    /// </summary>
    void FixedUpdate()
    {

    // Every "timeWhitoutChange_ms" milliseconds we modify the value of "movement" and "minionsMovUnits"
    DateTime dateNow = DateTime.Now;
        TimeSpan timeWhitoutChange = dateNow - date_lastChamge;

        double timeWhitoutChange_ms = timeWhitoutChange.TotalMilliseconds;

        if (Game.GetComponent<ScGameGlobalData>().player_near_mode == "User")
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //        float moveHorizontal = 0;
            //        float moveVertical = 0;
            //        if (Input.GetKeyDown("i")) { moveVertical = 1f; }
            //        if (Input.GetKeyDown("k")) { moveVertical = -1f; }
            //        if (Input.GetKeyDown("j")) { moveHorizontal = -1f; }
            //        if (Input.GetKeyDown("l")) { moveHorizontal = 1f; }

            // We calculate the direction and quantity of movement
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            playersMovUnits = 25f;
        } // Fin de - if (ScGameGlobalData.player_near_mode == "User")
        else if (Game.GetComponent<ScGameGlobalData>().player_near_mode == "randon")
        {
            if (timeWhitoutChange_ms > periodMilisec)
            {
                // We calculate the direction and quantity of movement
                // We obtain "movement" and "minionsMovUnits" randonly
                float move_X = Random.Range(-1.0f, 1f);
                float move_Z = Random.Range(-1f, 1f);
                playersMovUnits = Random.Range(0.0f, 1f);

                playersMovUnits = playersMovUnits * ScGameGlobalData.maxPlayersMovUnits;
                movement = new Vector3(move_X, 0.0f, move_Z);

                date_lastChamge = dateNow;  // We actualizate date_lastChamge
            }
        } // Fin de - else if (ScGameGlobalData.player_near_mode == "randon")
        else if (Game.GetComponent<ScGameGlobalData>().player_near_mode == "NPC")
        {
            Debug.Log("From ScPlayerAI_Near => FixedUpdate => AI is not programated");
        }// Fin de - else if (ScGameGlobalData.Team_Near_Control == "NPC")
        else { Debug.Log("From ScPlayerAI_Near => FixedUpdate => Error 001"); }

        // CALlING TO THIS FUNCTION YOU CAN MANAGE THE ELEMENT WITH THE ARTIFICIAL INTELLIGENCE THAT YOU MUST DEVELOP
        GetComponent<ScPlayerControl>().moveOn(movement, playersMovUnits);
    }  // Fin de - void FixedUpdate()

}  // Fin de - public class ScPlayerAI_Near : MonoBehaviour {

using UnityEngine;

public class NextDialogue : MonoBehaviour
{ 
    [SerializeField] private NewDialogue nw; // CREATES A REFERENCE TO THE NEWDIALOGUE SCRIPT, WE MAKE THIS SERIALIZED SO IT'S PRIVATE TO OTHER CLASSES BUT STILL VISABLE IN THE INSPECTOR
    [SerializeField] private Door_States intro_Door; // CREATES A REFERENCE TO THE NEWDIALOGUE SCRIPT, WE MAKE THIS SERIALIZED SO IT'S PRIVATE TO OTHER CLASSES BUT STILL VISABLE IN THE INSPECTOR

    private void OnTriggerEnter(Collider col)  // THIS CODE BLOCK RUNS WHEN THE PLAYER ENTERS THE COLLIDER WITH ONTRIGGER IS ENABLED
    {
        if(col.gameObject.tag == "Player_Dialogue" && !nw.getSpeakingInProgress()) // IF THE COLLIDERS NAME IS Player_Dialogue
        {
            nw.createNewDialogue(0, 2, "Player", nw.getPlayerDialogue()); // CALLS THE NEWDIALOGUE'S CREATENEWDIALOGUE METHOD AND ADDS ONE TO THE CURRENTDIALOGUE INDEX
            Destroy(col.gameObject); // destroys the dialogue game object
            intro_Door.setDoorCanBeOpened(true); // sets the intro doors bool "canBeOpened" to true
        }

        if (col.gameObject.tag == "SpaceTeam_Dialogue" && !nw.getSpeakingInProgress()) // IF THE COLLIDERS NAME IS SpaceTeam_Dialogue
        {
            nw.createNewDialogue(1, 6, "SpaceTeam", nw.getSpaceTeamDialogue()); // CALLS THE NEWDIALOGUE'S CREATENEWDIALOGUE METHOD AND ADDS ONE TO THE CURRENTDIALOGUE INDEX
            Destroy(col.gameObject); // destroys the dialogue game object
        }
    }
}
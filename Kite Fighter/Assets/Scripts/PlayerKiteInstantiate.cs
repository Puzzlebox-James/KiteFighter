using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiteInstantiate : MonoBehaviour
{
    // This class needs to instantiate the kite for each player.
    // It should:
    // 1: Know that there are two players and know which player it's on (which 'player' GameObject is the script on)
    // 2: Instantiate the correct kite prefab based on the selection the player made on the 'kite selection' scene.
    //    Also, if there are other prefab things that the player selects (Lines, player models) do those up good too.
    // 3: Have a public reference to the kite that can be used by other classes (Lines n' shit)

    // Thoughts on how this might go down.
    // The title scene should only have START / EXIT. START should only be availibe once both inputs from players have been found and assigned.
}

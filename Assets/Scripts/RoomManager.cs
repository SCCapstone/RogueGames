using UnityEngine;

public enum RoomState {
    inactive,
    active,
    complete
}
public class RoomManager : MonoBehaviour {

    [SerializeField]
    public RoomState myRoomState;
    //NESW
    private Room myRoom;

    // Start is called before the first frame update
    void Start() {
        if (myRoomState != RoomState.complete) {
            myRoomState = RoomState.inactive;
            gameObject.tag = "InactiveRoom";
        }
    }

    public void CompleteRoom() {
        myRoomState = RoomState.complete;
        gameObject.tag = "CompleteRoom";
        myRoom.deleteDoors();
    }

    public void SetRoom(Room room) {
        myRoom = room;
        // All names start with "Rooms/Room_", so we grab the tag at the end. 

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player" && myRoomState == RoomState.inactive) {
            Debug.Log("ENTERED ROOM!");
            myRoom.placeDoors();
            myRoomState = RoomState.active;
            gameObject.tag = "ActiveRoom";
        }
    }


    // Update is called once per frame
    void Update() {
        if (myRoomState == RoomState.complete || gameObject.tag == "CompleteRoom") {
            this.CompleteRoom();
        }
        // CHeck to see if room has been completed
        /* If (all enemies dead) {
         *      this.completeRoom();
         * }
         */

    }
}

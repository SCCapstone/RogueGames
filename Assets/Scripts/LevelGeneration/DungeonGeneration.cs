using System.Collections.Generic;
using UnityEngine;
// inspired by https://gamedevacademy.org/understanding-procedural-dungeon-generation-in-unity/

public class Room {
    public Vector2Int roomCoordinate;
    public Dictionary<string, Room> neighbors;
    
    private GameObject roomObject;
    private RoomManager childRoomManager;
    private List<GameObject> doors;
    private Vector2 roomSize = new Vector2(2.56f, 2.56f);
    public Vector2Int initialRoomCoordinate;

    public Room(int xCoordinate, int yCoordinate) {
        this.roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
        this.neighbors = new Dictionary<string, Room>();
        this.doors = new List<GameObject>();
    }

    public Room(Vector2Int roomCoordinate) {
        this.roomCoordinate = roomCoordinate;
        this.neighbors = new Dictionary<string, Room>();
        this.doors = new List<GameObject>();

    }
    public List<Vector2Int> NeighborCoordinates() {
        List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 1));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x + 1, this.roomCoordinate.y));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 1));
        neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x - 1, this.roomCoordinate.y));

        return neighborCoordinates;
    }

    public void Connect(Room neighbor) {
        string direction = "";
        if (neighbor.roomCoordinate.y < this.roomCoordinate.y) {
            direction = "N";
        }
        if (neighbor.roomCoordinate.x > this.roomCoordinate.x) {
            direction = "E";
        }
        if (neighbor.roomCoordinate.y > this.roomCoordinate.y) {
            direction = "S";
        }
        if (neighbor.roomCoordinate.x < this.roomCoordinate.x) {
            direction = "W";
        }
        this.neighbors.Add(direction, neighbor);
    }

    public string PrefabName() {
        // Makes prefab name from neighbors (I.e. Room_NEW for neighbors to North, East, and West)
        string name = "Rooms/Room_";
        foreach (KeyValuePair<string, Room> neighborPair in neighbors) {
            name += neighborPair.Key;
        }
        return name;
    }
    public Room Neighbor(string direction) {
        return this.neighbors[direction];
    }

    public void Load(float obstacleChance) {
        GameObject roomObject = GameObject.Instantiate(Resources.Load(this.PrefabName())) as GameObject;
        roomObject.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
        childRoomManager = roomObject.transform.GetChild(1).gameObject.GetComponentInChildren<RoomManager>();
        childRoomManager.SetRoom(this);

        float obsRandom = Random.value;
        if (obsRandom < obstacleChance) {
            int numObstacles = 2;
            GameObject obstacle;
            if (obsRandom < obstacleChance/numObstacles) { // in first fraction
                obstacle = GameObject.Instantiate(Resources.Load("Obstacles/Obstacle_Pit")) as GameObject;
                //can add 'else if obsRandom < obstacleChance*2/numObstacles for more than two, and such. 
            } else { // in last fraction
                obstacle = GameObject.Instantiate(Resources.Load("Obstacles/Obstacle_Pillars")) as GameObject;
            }
            obstacle.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);

        }
    }
    public void completeRoom() {
        childRoomManager.CompleteRoom();
    }

    public RoomState GetRoomState() {
        return childRoomManager.myRoomState;
    }

    public void placeDoors() {
        string nesw = this.PrefabName().Substring(11);
        if (nesw.Contains("N")) {
            GameObject door_N = GameObject.Instantiate(Resources.Load("Doors/Door_N")) as GameObject;
            door_N.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
            this.doors.Add(door_N);
        }
        if (nesw.Contains("E")) {
            GameObject door_E = GameObject.Instantiate(Resources.Load("Doors/Door_E")) as GameObject;
            door_E.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
            this.doors.Add(door_E);
        }
        if (nesw.Contains("S")) {
            GameObject door_S = GameObject.Instantiate(Resources.Load("Doors/Door_S")) as GameObject;
            door_S.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
            this.doors.Add(door_S);
        }
        if (nesw.Contains("W")) {
            GameObject door_W = GameObject.Instantiate(Resources.Load("Doors/Door_W")) as GameObject;
            door_W.transform.position = new Vector3(roomSize.x*(this.roomCoordinate.x-this.initialRoomCoordinate.x), -roomSize.y * (this.roomCoordinate.y - this.initialRoomCoordinate.y), 0);
            this.doors.Add(door_W);
        }

    }

    public void deleteDoors() {
        for (int i = doors.Count-1; i >=0; i--) {

            GameObject go = doors[i];
            this.doors.Remove(go);
            GameObject.Destroy(go);
        }
    }
}

public class DungeonGeneration : MonoBehaviour {
    [SerializeField]
    private int numRooms = 7;

    [SerializeField]
    private bool genEnabled = true;

    [SerializeField]
    private float obstacleChance = 0.4f;

    private Room startRoom;

    private Room[,] rooms;
    private List<Room> createdRooms;

    private static DungeonGeneration instance;

    /* Start is called before the first frame update
	 * 
	 * basic idea is going to be: 
	 * generate dungeon
	 * for each room in dungeon
	 * 		load prefab
	 * 		move prefab to desired location
	 * 	end
	 * 	
	 */

    void Start() {

        /* Create grid
		 * Put current room coords in queue
		 * go through queue
		 *		Pop next room coords
		 *		Generate possible neighbors, put in created rooms
		 * GO through all generated rooms
		 *		if two rooms are next to each other, connect.
		 *		Load the subsequent room prefab and move it to location
		 */
        if (this.genEnabled) {
            this.startRoom = GenerateDungeon();
            this.startRoom.completeRoom();
            PrintGrid();
        }

    }

    private Room GenerateDungeon() {



        int gridSize = 2 * numRooms + 3;

        rooms = new Room[gridSize, gridSize];

        Vector2Int initialRoomCoordinate = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);

        Queue<Room> roomsToCreate = new Queue<Room>();
        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y));
        createdRooms = new List<Room>();
        while (roomsToCreate.Count > 0 && createdRooms.Count < numRooms) {
            Room currentRoom = roomsToCreate.Dequeue();
            this.rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }

        foreach (Room room in createdRooms) {
            List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();
            foreach (Vector2Int coordinate in neighborCoordinates) {
                Room neighbor = this.rooms[coordinate.x, coordinate.y];
                if (neighbor != null) {
                    room.Connect(neighbor);
                }
            }
            room.initialRoomCoordinate = initialRoomCoordinate;
            room.Load(obstacleChance);
            
        }


        return this.rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate) {
        /* For every nearby grid space
		 *		if it isnt marked to be generated, add it to the list
		 * In the list
		 *		Generate a random value
		 *		if value is less than frequency, add it to the queue
		 *		Otherwise, add to the frequency
		 *			* this makes sure that we always generate at least 1 room
		 */



        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();
        foreach (Vector2Int coordinate in neighborCoordinates) {
            if (this.rooms[coordinate.x, coordinate.y] == null) {
                availableNeighbors.Add(coordinate);
            }
        }

        int numberOfNeighbors = (int)Random.Range(1, availableNeighbors.Count);

        for (int neighborIndex = 0; neighborIndex < numberOfNeighbors; neighborIndex++) {
            float randomNumber = Random.value;
            float roomFrac = 1f / (float)availableNeighbors.Count;
            Vector2Int chosenNeighbor = new Vector2Int(0, 0);
            foreach (Vector2Int coordinate in availableNeighbors) {
                if (randomNumber < roomFrac) {
                    chosenNeighbor = coordinate;
                    break;
                } else {
                    roomFrac += 1f / (float)availableNeighbors.Count;
                }
            }
            roomsToCreate.Enqueue(new Room(chosenNeighbor));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }

    private void PrintGrid() {
        // iterate through the dungeon array and print the rooms
        string grid = "";
        for (int rowIndex = 0; rowIndex < this.rooms.GetLength(1); rowIndex++) {
            string row = "";
            for (int columnIndex = 0; columnIndex < this.rooms.GetLength(0); columnIndex++) {
                if (columnIndex == numRooms+1 && rowIndex == numRooms+1) {
                    row += "S";
                } else if (this.rooms[columnIndex, rowIndex] == null) {
                    row += "X";
                } else {
                    row += "R";
                }
            }
            grid += row + "\n";
        }
        Debug.Log(grid);
    }


    public Room StartRoom() {
        return this.startRoom;
    }

    public bool AllRoomsComplete() {
        // iterates through each room to make sure they are all completed. 
        foreach (Room room in createdRooms) {
            if (room.GetRoomState() != RoomState.complete) {
                return false;
            }
        }
        return true;
    }

}

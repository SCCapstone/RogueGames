﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
// inspired by https://gamedevacademy.org/understanding-procedural-dungeon-generation-in-unity/

public class Room
{
	public Vector2Int roomCoordinate;
	public Dictionary<string, Room> neighbors;

	public Room(int xCoordinate, int yCoordinate)
	{
		this.roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
		this.neighbors = new Dictionary<string, Room>();
	}

	public Room(Vector2Int roomCoordinate)
	{
		this.roomCoordinate = roomCoordinate;
		this.neighbors = new Dictionary<string, Room>();
	}
	public List<Vector2Int> NeighborCoordinates()
	{
		List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
		neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y - 1));
		neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x + 1, this.roomCoordinate.y));
		neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x, this.roomCoordinate.y + 1));
		neighborCoordinates.Add(new Vector2Int(this.roomCoordinate.x - 1, this.roomCoordinate.y));

		return neighborCoordinates;
	}

	public void Connect(Room neighbor)
	{
		string direction = "";
		if (neighbor.roomCoordinate.y < this.roomCoordinate.y)
		{
			direction = "N";
		}
		if (neighbor.roomCoordinate.x > this.roomCoordinate.x)
		{
			direction = "E";
		}
		if (neighbor.roomCoordinate.y > this.roomCoordinate.y)
		{
			direction = "S";
		}
		if (neighbor.roomCoordinate.x < this.roomCoordinate.x)
		{
			direction = "W";
		}
		this.neighbors.Add(direction, neighbor);
	}

	public string PrefabName()
	{
		// Makes prefab name from neighbors (I.e. Room_NEW for neighbors to North, East, and West)
		string name = "Room_";
		foreach (KeyValuePair<string, Room> neighborPair in neighbors)
		{
			name += neighborPair.Key;
		}
		return name;
	}
	public Room Neighbor(string direction)
	{
		return this.neighbors[direction];
	}
}

public class DungeonGeneration : MonoBehaviour
{
    [SerializeField]
    private int numRooms;
	private Room currentRoom;

    private Room[,] rooms;

	private static DungeonGeneration instance;

	/* THis might not really come into play ; we will need something to track overall floor though
	private void Awake()
    {	//WHen another dungeon is made, load the room and destroy it. 
		if (instance == null)
		{
			DontDestroyOnLoad(this.gameObject);
			instance = this;
			this.currentRoom = GenerateDungeon();
			PrintGrid();
		}
		else
		{
			string roomPrefabName = instance.currentRoom.PrefabName();
			GameObject roomObject = (GameObject)Instantiate(Resources.Load(roomPrefabName));
			Destroy(this.gameObject);
		}
	}
	
	*/
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
	
	void Start()
	{
		string roomPrefabName = this.currentRoom.PrefabName();
		GameObject roomObject = (GameObject)Instantiate(Resources.Load(roomPrefabName));
	}

	private Room GenerateDungeon()
    {
		/* Create grid
		 * Put current room coords in queue
		 * go through queue
		 *		Pop next room coords
		 *		Generate possible neighbors, put in created rooms
		 * GO through all generated rooms
		 *		if two rooms are next to each other, connect.
		 */


	int gridSize = 2 * numRooms + 1;

        rooms = new Room[gridSize, gridSize];

		Vector2Int initialRoomCoordinate = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);

		Queue<Room> roomsToCreate = new Queue<Room>();
		roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y));
		List<Room> createdRooms = new List<Room>();
		while (roomsToCreate.Count > 0 && createdRooms.Count < numRooms)
		{
			Room currentRoom = roomsToCreate.Dequeue();
			this.rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
			createdRooms.Add(currentRoom);
			AddNeighbors(currentRoom, roomsToCreate);
		}

		foreach (Room room in createdRooms)
		{
			List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();
			foreach (Vector2Int coordinate in neighborCoordinates)
			{
				Room neighbor = this.rooms[coordinate.x, coordinate.y];
				if (neighbor != null)
				{
					room.Connect(neighbor);
				}
			}
		}

		return this.rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
	}

	private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate)
	{
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
		foreach (Vector2Int coordinate in neighborCoordinates)
		{
			if (this.rooms[coordinate.x, coordinate.y] == null)
			{
				availableNeighbors.Add(coordinate);
			}
		}

		int numberOfNeighbors = (int)Random.Range(1, availableNeighbors.Count);

		for (int neighborIndex = 0; neighborIndex < numberOfNeighbors; neighborIndex++)
		{
			float randomNumber = Random.value;
			float roomFrac = 1f / (float)availableNeighbors.Count;
			Vector2Int chosenNeighbor = new Vector2Int(0, 0);
			foreach (Vector2Int coordinate in availableNeighbors)
			{
				if (randomNumber < roomFrac)
				{
					chosenNeighbor = coordinate;
					break;
				}
				else
				{
					roomFrac += 1f / (float)availableNeighbors.Count;
				}
			}
			roomsToCreate.Enqueue(new Room(chosenNeighbor));
			availableNeighbors.Remove(chosenNeighbor);
		}
	}

	private void PrintGrid()
	{
		// iterate through the dungeon array and print the rooms
		string grid = "";
		for (int rowIndex = 0; rowIndex < this.rooms.GetLength(1); rowIndex++)
		{
			string row = "";
			for (int columnIndex = 0; columnIndex < this.rooms.GetLength(0); columnIndex++)
			{
				if (this.rooms[columnIndex, rowIndex] == null)
				{
					row += "X";
				}
				else
				{
					row += "R";
				}
			}
			grid += row + "\n";
		}
		Debug.Log(grid);
	}

	public void MoveToRoom(Room room)
	{
		this.currentRoom = room;
	}

	public Room CurrentRoom()
	{
		return this.currentRoom;
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateRoom : MonoBehaviour {

	public GameObject[] availableRooms;	
	public List<GameObject> currentRooms;	
	private float screenWidthInPoints;

	// Other Objects....
	public GameObject[] availableObjects;    
	public List<GameObject> objects;	
	public float objectsMinDistance = 5.0f;    
	public float objectsMaxDistance = 10.0f;	
	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;	
	public float objectsMinRotation = -45.0f;
	public float objectsMaxRotation = 45.0f;

	// Enemys......
	public GameObject[] availableEnemy;    
	public List<GameObject> Enemy;	
	public float EnemyMinDistance = 5.0f;    
	public float EnemyMaxDistance = 10.0f;	
	public float EnemyPositionY;

	// Point Objects........
	public GameObject[] availablePointObjects;    
	public List<GameObject> PointObjects;	
	public float PointObjectsMinDistance = 5.0f;    
	public float PointObjectsMaxDistance = 10.0f;	
	public float PointObjectsMinY = -1.4f;
	public float PointObjectsMaxY = 1.4f;	
	public float PointObjectsMinRotation = -45.0f;
	public float PointObjectsMaxRotation = 45.0f;


	// Use this for initialization
	void Start () {
	
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void FixedUpdate () {  

		GenerateRoomIfRequired ();
		GenerateObjectsIfRequired ();
		GenerateEnemysIfRequired ();
		GeneratePointObjectsIfRequired ();
	}

	void AddRoom(float farhtestRoomEndX) {

		int randomRoomIndex = Random.Range(0, availableRooms.Length);
		GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
		float roomWidth = room.transform.FindChild("Floor").localScale.x;
		float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
		room.transform.position = new Vector3(roomCenter, 0, 0);
		currentRooms.Add(room);
	}

	void GenerateRoomIfRequired() {

		List<GameObject> roomsToRemove = new List<GameObject>();
		bool addRooms = true;  
		float playerX = transform.position.x;
		float removeRoomX = playerX - 3*screenWidthInPoints;
		float addRoomX = playerX + screenWidthInPoints/2;
		float farthestRoomEndX = 0;
		
		foreach(var room in currentRooms) {

			float roomWidth = room.transform.FindChild("Floor").localScale.x;
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);    
			float roomEndX = roomStartX + roomWidth;                            

			if (roomStartX > addRoomX)
				addRooms = false;
			//roomEndX < removeRoomX
			if (roomEndX < removeRoomX) 
				roomsToRemove.Add(room);

			farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
		}

		foreach(var room in roomsToRemove) {
			currentRooms.Remove(room);
			Destroy(room);            
		}

		if (addRooms)
			AddRoom(farthestRoomEndX);
	}


	// Add Objects......
	void AddObject(float lastObjectX) {

		int randomIndex = Random.Range(0, availableObjects.Length);
		GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
		float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
		float randomY = Random.Range(objectsMinY, objectsMaxY);
		obj.transform.position = new Vector3(objectPositionX,randomY,0); 
		float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
		obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
		objects.Add(obj);            
	}

	void GenerateObjectsIfRequired() {

		float playerX = transform.position.x;        
		float removeObjectsX = playerX - screenWidthInPoints;
		float addObjectX = playerX + screenWidthInPoints;
		float farthestObjectX = 0;
		List<GameObject> objectsToRemove = new List<GameObject>();
		foreach (var obj in objects) {
			float objX = obj.transform.position.x;
			farthestObjectX = Mathf.Max(farthestObjectX, objX);
			if (objX < removeObjectsX)            
				objectsToRemove.Add(obj);
		}
		foreach (var obj in objectsToRemove) {
			objects.Remove(obj);
			Destroy(obj);
		}
		if (farthestObjectX < addObjectX)
			AddObject(farthestObjectX);
	}



	// Add Enemyes......
	void AddEnemy(float lastEnemyX) {
		
		int randomIndex2 = Random.Range(0, availableEnemy.Length);
		GameObject obj = (GameObject)Instantiate(availableEnemy[randomIndex2]);
		float EnemyPositionX = lastEnemyX + Random.Range(EnemyMinDistance, EnemyMaxDistance);
		obj.transform.position = new Vector3(EnemyPositionX,EnemyPositionY,0); 
		Enemy.Add(obj);            
	}
	
	void GenerateEnemysIfRequired() {
		
		float playerX = transform.position.x;        
		float removeEnemyX = playerX - screenWidthInPoints;
		float addEnemyX = playerX + screenWidthInPoints;
		float farthestEnemyX = 0;
		List<GameObject> EnemyToRemove = new List<GameObject>();
		foreach (var obj in Enemy) {
			float objX = obj.transform.position.x;
			farthestEnemyX = Mathf.Max(farthestEnemyX, objX);
			if (objX < removeEnemyX)            
				EnemyToRemove.Add(obj);
		}
		foreach (var obj in EnemyToRemove) {
			Enemy.Remove(obj);
			Destroy(obj);
		}
		if (farthestEnemyX < addEnemyX)
			AddEnemy(farthestEnemyX);
	}



	// Add PointObjects........
	void AddPointObject(float lastPointObjectX) {

		int randomIndex3 = Random.Range(0, availablePointObjects.Length);
		GameObject obj3 = (GameObject)Instantiate(availablePointObjects[randomIndex3]);
		float PointObjectPositionX = lastPointObjectX + Random.Range(PointObjectsMinDistance, PointObjectsMaxDistance);
		float randomY3 = Random.Range(PointObjectsMinY, PointObjectsMaxY);
		obj3.transform.position = new Vector3(PointObjectPositionX,randomY3,0); 
		float rotation = Random.Range(PointObjectsMinRotation, PointObjectsMaxRotation);
		obj3.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
		PointObjects.Add(obj3);            
	}

	void GeneratePointObjectsIfRequired() {

		float playerX = transform.position.x;        
		float removePointObjectsX = playerX - screenWidthInPoints;
		float addPointObjectX = playerX + screenWidthInPoints;
		float farthestPointObjectX = 0;
		List<GameObject> PointObjectsToRemove = new List<GameObject>();
		
		foreach (var obj in PointObjects) {
			float objX = obj.transform.position.x;
			farthestPointObjectX = Mathf.Max(farthestPointObjectX, objX);
			if (objX < removePointObjectsX)            
				PointObjectsToRemove.Add(obj);
		}
		foreach (var obj in PointObjectsToRemove) {
			PointObjects.Remove(obj);
			Destroy(obj);
		}
		if (farthestPointObjectX < addPointObjectX)
			AddPointObject(farthestPointObjectX);
	}


	
}

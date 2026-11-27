using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class room_spawner : MonoBehaviour
{
    public List<GameObject> rooms;
    public List<GameObject> enemies;
    public int room_number = 0;
    RNG rNG = new RNG();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            spawn_room();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Spawn room")]
    public void spawn_room()
    {
        //TODO room frequencies without just having the same entry multiple times + might have to start thinking about where the game goes from here
        GameObject room = null;
        if (room_number <= 2)
        {
            room = rooms[0];
        }
        else if (room_number >= 3)
        {
            List<int> weights = new() {7, 2, 2, 2, 1};
            room = rNG.WeightedChoice<GameObject>(rooms, weights);//TODO might add prog based gen
        }
        Instantiate(room, new Vector3(0, 0, 0), transform.rotation);
        room_number += 1;
        // Spawn Enemy1 6 10
        if (room_number >= 6 && Random.Range(0, 10) == 0)
        {
            if (transform.eulerAngles.y == 180)
            {
                Instantiate(enemies[0], new Vector3(7.2F, 1, -7.2F), Quaternion.Euler(new Vector3(0, 180, 0)));
            }
            if (transform.eulerAngles.y == 270)
            {
                Instantiate(enemies[0], new Vector3(-7.2F, 1, -7.2F), Quaternion.Euler(new Vector3(0, 270, 0)));
            }
            if (transform.eulerAngles.y == 0)
            {
                Instantiate(enemies[0], new Vector3(-7.2F, 1, 7.2F), Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if (transform.eulerAngles.y == 90)
            {
                Instantiate(enemies[0], new Vector3(7.2F, 1, 7.2F), Quaternion.Euler(new Vector3(0, 90, 0)));
            }
        }
        transform.Rotate(new Vector3(0, 0, 90));
        List<GameObject> spawned_rooms = GameObject.FindGameObjectsWithTag("is_room").ToList();
        if (spawned_rooms.Count > 3)
        {
            Destroy(spawned_rooms[0]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "is_player")
        {
            spawn_room();
        }
    }
}

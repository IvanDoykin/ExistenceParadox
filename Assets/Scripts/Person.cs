using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Chunks_Controller))]
public class Person : MonoBehaviour
{
    [SerializeField]
    private Chunks_Controller _chunks_Controller;

    public int Previous_x = 0;
    public int Previous_z = 0;
 
    public int Player_coord_x = 0;
    public int Player_coord_z = 0;

    public List<Chunk> Chunks_from_player;

    private Person _player;
    private bool Current_chunk_is_changing = false;
    private bool _need_check = true;
    private Transform _player_transform;

    private void Start()
    {
        Chunks_from_player = new List<Chunk>();
        _player = this;
        _player_transform = transform;

        Chunks_Controller.Chunk_Filling(0, 0, _player, _chunks_Controller);
    }

    private void Update()
    {
        Chunks_Controller.Setting_Up_Coordinats(ref Player_coord_x, ref Player_coord_z, _player_transform); //update coord

        if (((Player_coord_x - Previous_x) != 0) || ((Player_coord_z - Previous_z) != 0)) //catch chunk changing
        {
            Current_chunk_is_changing = true;
        }
        
        if (Current_chunk_is_changing)
        {
            Current_chunk_is_changing = false;
            Chunks_Controller.Delete_Far_Chunks(Player_coord_x - Previous_x, Player_coord_z - Previous_z, ref _player, false);
        }
    }

    public void Set_Check_State(bool state)
    {
        _need_check = state;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_need_check)
            return;
        if ((other.gameObject.tag == "gen_mesh"))
        {
            if (!Chunks_from_player.Contains(other.gameObject.GetComponent<Chunk>()))
                Chunks_from_player.Add(other.gameObject.GetComponent<Chunk>());

            else
            {
                Chunks_Controller.Refresh_Chunks(ref _player, Player_coord_x - Previous_x, Player_coord_z - Previous_z, _chunks_Controller);
                Set_Check_State(true);
            }
        }
    }
}

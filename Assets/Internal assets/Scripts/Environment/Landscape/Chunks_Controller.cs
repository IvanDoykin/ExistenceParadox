using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class Chunks_Controller : MonoBehaviour
{
    public GameObject _chunk;

    private Chunks_Controller _chunks_controller;

    private const int Chunks_FOV = 11;
    private static Chunk[] Chunks = new Chunk[Chunks_FOV * Chunks_FOV];

    private string _string_save_path;

    private void Start()
    {
        _chunks_controller = this;
    }

    #region Coordinating_Methods

    public static void Setting_Up_Coordinats(ref int cor_x, ref int cor_z, Transform t) //set up chunk-coord
    {
        cor_x = (int)Mathf.Floor(t.position.x / 16);
        cor_z = (int)Mathf.Floor(t.position.z / 16);
    }

    private static Vector3 Setting_Up_Corner_Position(int cor_x, int cor_z) //set up normal coord
    {
        return new Vector3(((float)cor_x) * 16, 0, ((float)cor_z) * 16);
    }

    private static Vector3 Setting_Up_Corner_Position(int cor_x, int cor_z, Transform t) //set up normal coord
    {
        return new Vector3(((float)cor_x) * 16, t.position.y, ((float)cor_z) * 16);
    }

    #endregion

    public static void Set_Chunk(ref Chunk ch, Person pers)
    {
        int chunk_cor = (ch.Coord_x - pers.Player_coord_x) + (ch.Coord_z - pers.Player_coord_z) * Chunks_FOV + Chunks.Length / 2;
        //Debug.Log(chunk_cor);
        Chunks[chunk_cor] = ch;

        Determinate_Neighbour_Chunks(ref ch, chunk_cor);
    }

    private static void Determinate_Neighbour_Chunks(ref Chunk ch, int index)
    {
        bool first = true;
        if ((index + 1) % Chunks_FOV != 0)
        {
            Equal_Edge_Verts(ref ch, "right", index, ref first);
        }
        if (index % Chunks_FOV != 0)
        {
            Equal_Edge_Verts(ref ch, "left", index, ref first);
        }

        if (index > Chunks_FOV - 1)
        {
            Equal_Edge_Verts(ref ch, "down", index, ref first);
        }
        if (index < Chunks_FOV * (Chunks_FOV - 1))
        {
            Equal_Edge_Verts(ref ch, "up", index, ref first);
        }

        if (((index + 1) % Chunks_FOV != 0) && (index < Chunks_FOV * (Chunks_FOV - 1)))
        {
            Equal_Edge_Verts(ref ch, "right_up", index, ref first);
        }
        if (((index + 1) % Chunks_FOV != 0) && (index > Chunks_FOV - 1))
        {
            Equal_Edge_Verts(ref ch, "right_down", index, ref first);
        }

        if ((index % Chunks_FOV != 0) && (index < Chunks_FOV * (Chunks_FOV - 1)))
        {
            Equal_Edge_Verts(ref ch, "left_up", index, ref first);
        }
        if ((index % Chunks_FOV != 0) && (index > Chunks_FOV - 1))
        {
            Equal_Edge_Verts(ref ch, "left_down", index, ref first);
        }

    }

    private static void Equal_Edge_Verts(ref Chunk chunk, string direction, int index, ref bool is_first_calling)
    {
        //link neighbour chunks

        int index_addition = 0;
        int start_i = 0;
        int i_addition = 0;
        int vecs_index_addition = 0;
        int down_up_offset = 0;

        switch (direction)
        {
            case "right":
                {
                    index_addition = 1;
                    start_i = 8;
                    i_addition = 9;
                    vecs_index_addition = -8;
                    break;
                }
            case "left":
                {
                    index_addition = -1;
                    start_i = 0;
                    i_addition = 9;
                    vecs_index_addition = 8;
                    break;
                }
            case "up":
                {
                    index_addition = 11;
                    start_i = 72;
                    i_addition = 1;
                    vecs_index_addition = -72;
                    down_up_offset = 0;
                    break;
                }
            case "down":
                {
                    index_addition = -11;
                    start_i = 0;
                    i_addition = 1;
                    vecs_index_addition = 72;
                    down_up_offset = -72;
                    break;
                }


            case "right_up":
                {
                    index_addition = 12;
                    start_i = 80;
                    i_addition = 81;
                    vecs_index_addition = -80;
                    break;
                }
            case "left_up":
                {
                    index_addition = 10;
                    start_i = 72;
                    i_addition = 81;
                    vecs_index_addition = -64;
                    break;
                }
            case "right_down":
                {
                    index_addition = -10;
                    start_i = 8;
                    i_addition = 81;
                    vecs_index_addition = 64;
                    down_up_offset = 0;
                    break;
                }
            case "left_down":
                {
                    index_addition = -12;
                    start_i = 0;
                    i_addition = 81;
                    vecs_index_addition = 80;
                    down_up_offset = -72;
                    break;
                }

        }

        if ((Chunks[index + index_addition] != null) && (Chunks[index + index_addition].Constructed == true))
        {
            for (int i = start_i; i < chunk.Vecs.Length + down_up_offset; i += i_addition)
            {
                chunk.Vecs[i].y = Chunks[index + index_addition].Vecs[i + vecs_index_addition].y;
                chunk.Not_calculated_vecs[i] = 0;
            }
        }
        Chunks[index] = chunk;
    }

    public static void Delete_Far_Chunks(int offset_x, int offset_z, ref Person player, bool was_divided)
    {
        int start_i = 0;
        int i_addition = 0;
        int border = 0;

        if ((Mathf.Abs(offset_x) > 0) && (Mathf.Abs(offset_z) > 0))
        {
            Delete_Far_Chunks(offset_x, 0, ref player, true);
            Delete_Far_Chunks(0, offset_z, ref player, true);

            Nulling_Chunks(ref player);
            return;
        }

        if (offset_x == -1)
        {
            start_i = Chunks_FOV - 1;
            i_addition = Chunks_FOV;
            border = Chunks_FOV * Chunks_FOV - 1;
        }
        if (offset_x == 1)
        {
            start_i = 0;
            i_addition = Chunks_FOV;
            border = Chunks_FOV * Chunks_FOV - 1;
        }

        if (offset_z == -1)
        {
            start_i = Chunks_FOV * (Chunks_FOV - 1);
            i_addition = 1;
            border = Chunks_FOV * Chunks_FOV - 1;
        }
        if (offset_z == 1)
        {
            start_i = 0;
            i_addition = 1;
            border = Chunks_FOV - 1;
        }

        for (int i = start_i; i <= border; i += i_addition)
        {
            if (Chunks[i] == null)
            {
                continue;
            }
            Destroy(Chunks[i].gameObject);
            Chunks[i] = null;
            //Deleting_Chunk_With_Caching(Chunks[i]);
        }

        if (!was_divided) Nulling_Chunks(ref player);
    }

    private static void Nulling_Chunks(ref Person player)
    {
        for (int i = 0; i < Chunks.Length; i++)
        {
            Chunks[i] = null;
        }

        player.Set_Check_State(false);
    }

    public static void Refresh_Chunks(ref Person player, int offset_x, int offset_z, Chunks_Controller chunks_Controller)
    {
        foreach (Chunk ch in player.Chunks_from_player)
        {
            int chunk_cor = (ch.Coord_x - player.Player_coord_x) + (ch.Coord_z - player.Player_coord_z) * Chunks_FOV + Chunks.Length / 2;
            Chunks[chunk_cor] = ch;
        }

        for (int i = 0; i <= Chunks.Length - 1; i++)
        {
            if (Chunks[i] == null)
            {
                Vector3 pos = new Vector3((i % Chunks_FOV - 5 + player.Player_coord_x) * 16, 0, (i / Chunks_FOV - 5 + player.Player_coord_z) * 16); //16 IS NOT "magic" number!!!It const value that u can't change!
                Quaternion quat = new Quaternion(0, 0, 0, 0);
                GameObject go = Instantiate(chunks_Controller._chunk, pos, quat);
                go.GetComponent<Chunk>().Player = player;
                Chunks[i] = go.GetComponent<Chunk>();
            }
        }

        Change_Previous_Player_Coords(ref player);
        player.Chunks_from_player.Clear();
        ManagerEvents.TriggerEvent("ChunkCreated");

    }

    private static void Change_Previous_Player_Coords(ref Person player)
    {
        player.Previous_x = player.Player_coord_x;
        player.Previous_z = player.Player_coord_z;
    }

    private static void Delete_Chunk(Chunk chunk)
    {
        if (chunk == null)
        {
            Debug.Log("Call Chunk_Caching with null arg!");
            return;
        }
        Chunk_Caching(chunk);
        Destroy(chunk.gameObject);
    }

    #region Chunks_Load/Save

    private static void Chunk_Caching(Chunk chunk)
    {
        string save_path = "C:/Users/Иван/Project/Assets/Cached/Chunks/ " + chunk.Chunk_name + ".cached"; //create path
        using (FileStream fs = File.Create(save_path)) //create file
        {
            string h = "" + chunk.transform.position + "\r\n"; //save position
            AddText(fs, Encode_Text(ref h)); //encoding and writing
            for (int i = 0; i < chunk.Vecs.Length; i++)
            {
                h = "" + chunk.Vecs[i] + "\r\n";
                AddText(fs, Encode_Text(ref h)); //vecs writing
            }
        }
    }

    public static bool Reading_From_File(ref Chunk chunk, Person player)
    {
        string save_path = "C:/Users/Иван/Project/Assets/Cached/Chunks/ " + chunk.Chunk_name + ".cached"; //hardcode!!!!!!!!!!!
        string tmp = "";

        try
        {
            using (StreamReader sr = new StreamReader(save_path))
            {
                tmp = sr.ReadToEnd();
            }
        }

        catch (FileNotFoundException e)
        {
            return false;
        }

        string dec_text = Decode_Text(tmp);
        string[] frag_text = dec_text.Split(new char[] { '\r' });
        foreach (string str in frag_text)
            str.Trim(new char[] { '\n' });

        chunk.gameObject.transform.position = Parse_Vec(frag_text[0]);

        for (int i = 0; i < chunk.Vecs.Length; i++)
        {
            chunk.Vecs[i] = Parse_Vec(frag_text[i + 1], offset: 1);
        }

        int zero_zero = (player.Player_coord_x - 5) + (player.Player_coord_z - 5) * 11;
        int chunk_cor = chunk.Coord_x + chunk.Coord_z * 11;
        Chunks[chunk_cor - zero_zero] = chunk;

        return true;
    }

    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }

    private static string Encode_Text(ref string text)
    {
        const int encode_index = 17; //change in DECODE too if u need!

        int temporary;
        string tmp = "";
        char t;
        for (int k = 0; k < text.Length; k++)
        {
            temporary = (byte)(text[k]) + encode_index;
            t = (char)(temporary);
            tmp += t;
        }
        text = tmp;
        return text;
    }

    private static string Decode_Text(string text)
    {
        const int decode_index = 17; // change in ENCODE too if u need!

        int temporary;
        string tmp = "";
        char t;
        for (int k = 0; k < text.Length; k++)
        {
            temporary = (byte)(text[k]) - decode_index;
            t = (char)(temporary);
            tmp += t;
        }
        text = tmp;
        return text;
    }

    private static Vector3 Parse_Vec(string vec_text, int offset = 0)
    {
        vec_text = vec_text.Replace('.', ',');
        string[] s = vec_text.Split(new char[] { ' ' });

        float x = float.Parse(s[0].Substring(1 + offset, s[0].Length - 2 - offset));
        float y = float.Parse(s[1].Substring(0, s[1].Length - 1));
        float z = float.Parse(s[2].Substring(0, s[2].Length - 1));
        return new Vector3(x, y, z);
    }

    #endregion

    public static void Chunk_Filling(int x, int z, Person player, Chunks_Controller _chunks_Controller)
    {
        int coord_x = 0;
        int coord_z = 0;
        Creating_Chunk(coord_x, coord_z, player, _chunks_Controller);
        int zero_zero = (x - 5) + (z - 5) * 11;

        for (int round = 1; round < 6; round++)
        {
            coord_x = -round;
            coord_z = -round;

            while (coord_z < round)
            {
                Creating_Chunk(coord_x, coord_z, player, _chunks_Controller);
                coord_z++;
            }

            while (coord_x < round)
            {
                Creating_Chunk(coord_x, coord_z, player, _chunks_Controller);
                coord_x++;
            }

            while (coord_z > -round)
            {
                Creating_Chunk(coord_x, coord_z, player, _chunks_Controller);
                coord_z--;
            }

            while (coord_x > -round)
            {
                Creating_Chunk(coord_x, coord_z, player, _chunks_Controller);
                coord_x--;
            }

        }
        ManagerEvents.TriggerEvent("ChunkCreated");

    }

    private static void Creating_Chunk(int cor_x, int cor_z, Person player, Chunks_Controller chunks_Controller)
    {
        GameObject g_obj = Instantiate(chunks_Controller._chunk, Setting_Up_Corner_Position(cor_x, cor_z), new Quaternion(0, 0, 0, 0));
        g_obj.GetComponent<Chunk>().Player = player;
    }




}

/*TODO
 * Need correcting Equal_Some_Vert (change height) /not bad 
 * ============BIG GOAL
 * Chunk Filling (auto) interact with collider 
 * Add deleting chunks in out of collider      /yep                           
 * Add caching deleting chunks                     /some ok but not all
 * Add Chunk info parsing from file to GameObject and back
 * Interact collider with chunks[] and HDD
 * ============BIG GOAL
 * 
 */

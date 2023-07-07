
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�C���}�b�v�𑀍삷�鎞�ɕK�v
using UnityEngine.Tilemaps;
//�V�[���̓ǂݍ���/�ēǂݍ��ݎ��ɕK�v�@SceneManager.LoadScene("SampleScene");
using UnityEngine.SceneManagement;

public class MazeGen : MonoBehaviour
{
    //�^�C���}�b�v�ǂݍ��ݗp
    public Tilemap tilemap1;
    public TileBase tiledata0;
    public TileBase tiledata9;

    //�^�b�`�����Q�[���I�u�W�F�N�g�̊i�[�p
    GameObject clickedGameObject;

    //���H�̍ő吔
    [SerializeField]
    int i_max_x, i_max_y;

    //�쐬�������H�̊i�[�ϐ�
    int[,] i_map;

    // Start is called before the first frame update
    void Start()
    {


        //�_�|�����Ŗ��H�쐬

        int i_temp;

        i_map = new int[i_max_x + 1, i_max_y + 1];

        //�O�ǂ�ǂŖ��߂�
        for (int i = 1; i <= i_max_x; i++)
        {
            i_map[i, 1] = 9;
            i_map[i, i_max_y] = 9;
        }

        for (int i = 1; i <= i_max_y; i++)
        {
            i_map[1, i] = 9;
            i_map[i_max_x, i] = 9;
        }

        //�_�|�����Ŗ��H���쐬(�����蔲��)
        i_temp = 4;
        for (int y = 3; y <= i_max_y - 1; y += 2)
        {

            if (y != 3)
            {
                i_temp = 3;
            }

            for (int x = 3; x <= i_max_x - 1; x += 2)
            {
                i_map[x, y] = 9;
                switch (Random.Range(0, i_temp))
                {
                    case 0:
                        i_map[x + 1, y] = 9;
                        break;
                    case 1:
                        i_map[x - 1, y] = 9;
                        break;
                    case 2:
                        i_map[x, y + 1] = 9;
                        break;
                    case 3:
                        if (y == 3)
                        {
                            i_map[x, y - 1] = 9;
                        }
                        break;
                }
            }

        }

        //�}�b�v�ɔ��f ���H�̐^�񒆂��X�^�[�g�ɂ������̂ŁA���f�ꏊ�𔼕����炵�Ă��� 0�ʉ� 9��
        for (int x = 1; x <= i_max_x; x++)
        {
            for (int y = 1; y <= i_max_y; y++)
            {
                if (i_map[x, y] == 0)
                {
                    tilemap1.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), tiledata0);
                }
                else
                {
                    tilemap1.SetTile(new Vector3Int(x - (i_max_x / 2), y - (i_max_y / 2), 0), tiledata9);
                }
            }
        }

        Debug.Log("start");

    }

    // Update is called once per frame
    void Update()
    {

        //�^�b�`�����A�I�u�W�F�N�g�����擾�i�{�b�N�X�R���C�_�[�������Ă���I�u�W�F�N�g�̂݁j
        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
            }

            //�^�b�`�����I�u�W�F�N�g��restart��������A�V�[�����ēǂݍ���
            if (clickedGameObject)
            {
                if (clickedGameObject.transform.name == "restart")
                {
                    //Application.LoadLevel("SampleScene");
                   // SceneManager.LoadScene("SampleScene");
                }
            }
            Debug.Log(clickedGameObject);

        }

    }

}
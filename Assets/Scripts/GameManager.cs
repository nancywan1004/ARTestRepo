using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;
using XLua;

[LuaCallCSharp]
public class GameManager : MonoBehaviour
{
    LuaEnv luaenv = null;
    public GameObject ballPrefab;
    public GameObject platformPrefab;
    public Transform ballPos;
    public GameObject ball;
    public Rigidbody rb;
    private float jumpForce = 100.0f;
    private float moveSpeed = 10.0f;

    private bool isGameStarted = false;
    private static string luaScriptFolder = "LuaScripts";
    // Start is called before the first frame update
    void Awake()
    {
        luaenv = new LuaEnv();
        luaenv.AddLoader(LuaScriptLoader);
        isGameStarted = false;
        ball = GameObject.Instantiate(ballPrefab, ballPos.position, Quaternion.identity);
        rb = ball.transform.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isGameStarted = true;
        luaenv.DoString("require(\"BounceBehaviour\")");
        luaenv.DoString("BounceBehaviour.init()");
    }

    // Update is called once per frame
    void Update()
    {
        if (ball != null && rb != null && this.isGameStarted)
        {
            luaenv.DoString("BounceBehaviour.update()");
        }

/*        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                GameObject cube = GameObject.Instantiate(platformPrefab, hit.point + new Vector3(0, 1, 0), transform.rotation) as GameObject;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ball.transform.position += moveSpeed * Vector3.left * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ball.transform.position += moveSpeed * Vector3.right * Time.deltaTime;
        }*/
    }

    private void FixedUpdate()
    {
        if (ball != null && rb != null && this.isGameStarted)
        {
            luaenv.DoString("BounceBehaviour.fixedUpdate()");
        }
        /*        if (Input.GetKey(KeyCode.Space))
                {
                    Debug.Log("Jump jump jump!!!");
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * jumpForce);
                    }
                }*/
    }

    public byte[] MyLoader(ref string filePath)
    {
        // 读取下载的脚本资源
        string newPath = Application.persistentDataPath + @"/" + filePath + ".lua.txt";
        Debug.Log("执行脚本路径：" + newPath);
        string txtString = File.ReadAllText(newPath);
        return System.Text.Encoding.UTF8.GetBytes(txtString);
    }

    public byte[] LuaScriptLoader(ref string filePath)
    {
        // Debug.Log("####" + filePath);
        string scriptPath = string.Empty;
        filePath = filePath.Replace(".", "/") + ".lua"; // game/init.lua

        scriptPath = Path.Combine(Application.dataPath, luaScriptFolder);
        scriptPath = Path.Combine(scriptPath, filePath);

        byte[] data = SafeReadAllBytes(scriptPath);
        return data;
    }

    public static byte[] SafeReadAllBytes(string inFile)
    {
        try
        {
            if (string.IsNullOrEmpty(inFile))
            {
                return null;
            }

            if (!File.Exists(inFile))
            {
                return null;
            }

            File.SetAttributes(inFile, FileAttributes.Normal);
            return File.ReadAllBytes(inFile);
        }
        catch (Exception ex)
        {
            Debug.LogError(string.Format("SafeReadAllBytes failed! path = {0} with err = {1}", inFile, ex.Message));
            return null;
        }
    }

        public static bool RayFunction(Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit);
    }
}

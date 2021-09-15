using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerScript : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle;

    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;


    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, .03f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.3f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.3f);

        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = scale;
        float scaleY = scale;

        if (aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;

        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        mat.SetFloat("_Angle", smoothAngle);
    }

    private void HandleInputs()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKey((KeyCode.KeypadPlus)))
            scale *= 0.99f;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKey((KeyCode.KeypadMinus)))
            scale *= 1.01f;

        if (Input.GetKey(KeyCode.E))
            angle -= 0.01f;

        if (Input.GetKey(KeyCode.Q))
            angle += 0.01f;

        Vector2 dir = new Vector2(0.01f*scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x*c, dir.x*s);

        if (Input.GetKey(KeyCode.A) || Input.GetKey((KeyCode.LeftArrow)))
            pos -= dir;

        if (Input.GetKey(KeyCode.D) || Input.GetKey((KeyCode.RightArrow)))
            pos += dir;

        dir = new Vector2(-dir.y, dir.x);

        if (Input.GetKey(KeyCode.W) || Input.GetKey((KeyCode.UpArrow)))
            pos += dir;

        if (Input.GetKey(KeyCode.S) || Input.GetKey((KeyCode.DownArrow)))
            pos -= dir;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }
}

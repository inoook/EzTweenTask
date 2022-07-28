// http://wiki.unity3d.com/index.php/AllocationStats
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// add for Unity5.5
using UnityEngine.Profiling;

public class AllocMem : MonoBehaviour {
 
	private float lastCollect = 0;
	private float lastCollectNum = 0;
	private float delta = 0;
	private float lastDeltaTime = 0;
	private int allocRate = 0;
	private int lastAllocMemory = 0;
	private float lastAllocSet = -9999;
	private int allocMem = 0;
	private int collectAlloc = 0;
	private int peakAlloc = 0;
	
	public Color color = Color.green;

	public bool debug = true;
    [SerializeField] bool drawAsWindow = true;
    [SerializeField, HideInInspector] string buildInfo = "";
    
    void Start () {

    }

    [SerializeField] Rect drawRect = new Rect(10, 10, 320, 400);
	
	void Update () {

		if (GetFPSTime ()) {
			realFps = GetWebcamFPS ();
		}
	}

    public void SetBuildInfo(string str) {
        buildInfo = str;
    }


    List<float> fpsTimes = new List<float> ();
	const int count = 10;
	float pastTime;
	[SerializeField] float realFps;
	float GetWebcamFPS () {
		float times = 0f;
		foreach (float t in fpsTimes) {
			times += t;
		}

		fpsTimes.Clear ();

		times /= count;

		return times;
	}

	bool GetFPSTime () {
		float nowTime = Time.time;
		float timeDelta = nowTime - pastTime;
		pastTime = nowTime;

		fpsTimes.Add (1 / timeDelta);
		if (fpsTimes.Count >= count) {
			return true;
		} else {
			return false;
		}
	}

    public float GetFps()
    {
        return realFps;
    }

    [SerializeField] GUISkin skin = null;
    [SerializeField] float scale = 1;


    void OnGUI() {
        if (!debug) {
            return;
        }
        GUI.matrix = Matrix4x4.Scale(Vector3.one * scale);

        GUI.skin = skin;
        GUI.color = color;
        if (drawAsWindow) {
            // 2020.3.0f1 ‚¾‚Æƒƒ‚ƒŠƒŠ[ƒN‚µ‚Ä‚¢‚éB
            drawRect = GUILayout.Window(2, drawRect, DoMyWindow, "Memory debug");
        }
        else {
            GUILayout.BeginArea(drawRect);
            GUILayout.BeginVertical("box");
            DrawInfo();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    void DrawInfo() {
        GUI.color = color;

        float mb = 1048576;// 1024f * 1024f
        string str = $"Total allocated memory : { ((float)(System.GC.GetTotalMemory(false) / mb)) :0.00} / {(SystemInfo.systemMemorySize) } MB";
        str += System.Environment.NewLine;

        //if (Debug.isDebugBuild) {
            // https://light11.hatenadiary.com/entry/2019/11/12/222321
            // http://neareal.com/2595/
            //var monoUsedSize = Profiler.GetMonoUsedSizeLong() / mb;
            //var monoReservedSize = Profiler.GetMonoHeapSizeLong() / mb;
            //var unityUsedSize = Profiler.GetTotalAllocatedMemoryLong() / mb;
            //var unityReservedSize = Profiler.GetTotalReservedMemoryLong() / mb;

            //str += ($"monoUsedSize : {monoUsedSize:0.00} MB");
            //str += System.Environment.NewLine;
            //str += ($"monoReservedSize : {monoReservedSize:0.00} MB");
            //str += System.Environment.NewLine;
            //str += ($"unityUsedSize : {unityUsedSize:0.00} MB");
            //str += System.Environment.NewLine;
            //str += ($"unityReservedSize : {unityReservedSize:0.00} MB");
            //str += System.Environment.NewLine;

            //
            str += $"HeapSize: {(Profiler.usedHeapSizeLong / mb):0.00} MB";
            str += System.Environment.NewLine;
        //}

        str += $"{realFps:0.00} / {Application.targetFrameRate} fps";
        str += System.Environment.NewLine;
        GUILayout.Label("Operation Time: " + Time.realtimeSinceStartup.ToString("0.00"));
        GUILayout.Label(str);

        advanced = GUILayout.Toggle(advanced, "advanced");
        if (drawAsWindow && GUI.changed) {
            drawRect.height = 0;
        }
        if (advanced) {
            int collCount = System.GC.CollectionCount(0);

            if (lastCollectNum != collCount) {
                lastCollectNum = collCount;
                delta = Time.realtimeSinceStartup - lastCollect;
                lastCollect = Time.realtimeSinceStartup;
                lastDeltaTime = Time.deltaTime;
                collectAlloc = allocMem;
            }

            allocMem = (int)System.GC.GetTotalMemory(false);

            peakAlloc = allocMem > peakAlloc ? allocMem : peakAlloc;

            if (Time.realtimeSinceStartup - lastAllocSet > 0.3F) {
                int diff = allocMem - lastAllocMemory;
                lastAllocMemory = allocMem;
                lastAllocSet = Time.realtimeSinceStartup;

                if (diff >= 0) {
                    allocRate = diff;
                }
            }


            StringBuilder text = new StringBuilder();

            // add inok
            text.Append("Input.gyro.enabled\t" + Input.gyro.enabled);
            text.Append("\n");
            text.Append("SystemMemorySize\t");
            text.Append(SystemInfo.systemMemorySize);
            text.Append("mb\n");

            //if (Debug.isDebugBuild) {
                text.Append("HeapSize\t\t");
                text.Append((Profiler.usedHeapSizeLong / 1000000F).ToString("0.0"));
                text.Append("mb\n");
            //}
            //

            text.Append("Currently allocated\t");
            text.Append((allocMem / 1000000F).ToString("0"));
            text.Append("mb\n");

            text.Append("Peak allocated\t");
            text.Append((peakAlloc / 1000000F).ToString("0"));
            text.Append("mb (last   collect ");
            text.Append((collectAlloc / 1000000F).ToString("0"));
            text.Append(" mb)\n");


            text.Append("Allocation rate\t");
            text.Append((allocRate / 1000000F).ToString("0.0"));
            text.Append("mb\n");

            text.Append("Collection frequency\t");
            text.Append(delta.ToString("0.00"));
            text.Append("s\n");

            text.Append("Last collect delta\t");
            text.Append(lastDeltaTime.ToString("0.000"));
            text.Append("s (");
            text.Append((1F / lastDeltaTime).ToString("0.0"));
            text.Append(" fps)");

            GUILayout.Label(text.ToString());
        }

        if (!string.IsNullOrEmpty(buildInfo)) {
            GUILayout.Label(buildInfo);
        }

        if (GUILayout.Button("System.GC.Collect")) {
            System.GC.Collect();
        }
    }

    [SerializeField] bool advanced = false;

    void DoMyWindow (int windowID) {

        DrawInfo();
        GUI.DragWindow ();
	}
	

    public void ToggleDebug()
    {
        bool enable = this.gameObject.activeSelf;
        this.gameObject.SetActive(!enable);
    }


}
using System.Linq;
using Live2D.Cubism.Framework;
using UnityEngine;
public class LogExecutionOrder : MonoBehaviour {
    //執行順序是寫死的,不是這邊的影響
    public bool doLog = false;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (doLog) {
            Log ();
            doLog = false;
        }
    }

    void Log () {
        var comps = GetComponents<MonoBehaviour> ();
        var os = comps.Where (a => a is ICubismUpdatable).ToArray ();

        foreach (var o in os) {
            var ou = o as ICubismUpdatable;
            LiveHub.MyHub.Log ("log order", o.GetType ().ToString () + " - " + ou.ExecutionOrder.ToString ());
        }
        LiveHub.MyHub.Log ("log order", "-------------------------------");
    }
}
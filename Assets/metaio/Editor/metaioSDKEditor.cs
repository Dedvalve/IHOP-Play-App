using UnityEngine;
using UnityEditor;
using System;
using System.IO;

[CustomEditor(typeof(metaioSDK))]
public class metaioSDKEditor : Editor {
	
	// referece to the metaioSDK
	private metaioSDK metaioSDK;
    private String currentTrackingConfiguration;
    private static MapLoader mapLoader = new MapLoader();

    public void OnEnable()
    {
        metaioSDK = (metaioSDK)target;
        currentTrackingConfiguration = metaioSDK.trackingConfiguration;

        if (metaioSDK.trackingConfiguration.EndsWith(".3dmap") || metaioSDK.trackingConfiguration.EndsWith(".creator3dmap"))
        {
            mapLoader.setMapObject(GameObject.Find("Feature Map"));
        }
    }
	
	void OnGUI ()
	{
		GUILayout.Label ("metaio SDK", EditorStyles.boldLabel);
	}
	

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();
			
		// info text
		EditorGUILayout.HelpBox("The metaioSDK compnent will be used to configure the tracking, preview the camera, " +
		 	"tranfrom the main camera and provide a valid SDK license. If you use the Unity build-in configuratio, " +
		 	"please use read the documenation at http://dev.metaio.com/sdk", MessageType.Info);
		
		metaioSDK.writeApplicationSignature(EditorGUILayout.TextField("SDK Signature", metaioSDK.parseApplicationSignature()));
		
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Camera capture parameters:");
		metaioSDK.cameraIndex = EditorGUILayout.IntField("Camera index", metaioSDK.cameraIndex);
		metaioSDK.cameraWidth = EditorGUILayout.IntField("Camera width", metaioSDK.cameraWidth);
        metaioSDK.cameraHeight = EditorGUILayout.IntField("Camera height", metaioSDK.cameraHeight);
		metaioSDK.cameraDownsample = EditorGUILayout.IntField("Downsample factor", metaioSDK.cameraDownsample);
		
		
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Renderer clipping plane limits in millimeters:");
		metaioSDK.nearClippingPlaneLimit = EditorGUILayout.FloatField("Near Limit", metaioSDK.nearClippingPlaneLimit);
		metaioSDK.farClippingPlaneLimit = EditorGUILayout.FloatField("Far Limit", metaioSDK.farClippingPlaneLimit);
		
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Tracking configuration:");
        metaioSDK.trackingAssetIndex = EditorGUILayout.Popup("Select source", metaioSDK.trackingAssetIndex, metaioSDK.trackingAssets, EditorStyles.popup);
		
		if (metaioSDK.trackingAssetIndex == 7)
		{
			// select from streaming assets
			metaioSDK.trackingConfiguration="tracking.xml";
			EditorGUILayout.HelpBox("Just drag&drop a *.xml, *.3dmap or *.zip file with tracking data from your project view here", MessageType.Info);
			metaioSDK.trackingAsset = EditorGUILayout.ObjectField( metaioSDK.trackingAsset, typeof(UnityEngine.Object), true);
			
			// set the actual file path
			metaioSDK.trackingConfiguration = AssetDatabase.GetAssetPath(metaioSDK.trackingAsset);
			metaioSDK.trackingConfiguration = metaioSDK.trackingConfiguration.Replace("Assets/StreamingAssets/", "");
			//Debug.Log("Tracking configuration dragged: " + metaioSDK.trackingConfiguration);
		}
		else if (metaioSDK.trackingAssetIndex == 8)
		{
			// specify absolute path
			metaioSDK.trackingConfiguration = EditorGUILayout.TextField("Tracking Configuration", metaioSDK.trackingConfiguration);
		}
		else if (metaioSDK.trackingAssetIndex == 9)
		{
			// generate tracking xml
			metaioSDK.trackingConfiguration="TrackingConfigGenerated.xml";
		}
		else if (metaioSDK.trackingAssetIndex > 0)
		{
			metaioSDK.trackingConfiguration = metaioSDK.trackingAssets[metaioSDK.trackingAssetIndex];
		}
		else
		{
			metaioSDK.trackingConfiguration = "";
			Debug.LogWarning("No tracking configuration selected");
		}
			
		// here we can add more options
        if (GUI.changed)
        {
            // if tracking configuration is a 3D map and it changed visualiaze the map
            if (metaioSDK.trackingConfiguration.EndsWith(".3dmap") || metaioSDK.trackingConfiguration.EndsWith(".creator3dmap"))
            {
                if (!currentTrackingConfiguration.Equals(metaioSDK.trackingConfiguration))
                {
                    mapLoader.loadMap(metaioSDK.trackingConfiguration);
                    EditorApplication.update = createMap;
                }
            }
            else
            {
                mapLoader.clearMap();
            }

            currentTrackingConfiguration = metaioSDK.trackingConfiguration;
            EditorUtility.SetDirty(target);
        }
	}

    void createMap()
    {
        if (mapLoader.createFeatures()) EditorApplication.update = null; 
    }

}

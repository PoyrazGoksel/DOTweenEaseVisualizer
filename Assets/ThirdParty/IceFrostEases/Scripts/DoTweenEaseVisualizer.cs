#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IceFrostEases
{
    [ExecuteInEditMode]
    public class DoTweenEaseVisualizer : MonoBehaviour
    {
        private const string AssetFileName = "DoTweenEaseVisualizer.prefab";
        private float CurrPointOnGraph{get;set;}
        [Tooltip("Please set editor scene to Always Refresh(If it has not been auto set by asset.). Button is above scene view where the scene tools are.")]
        [SerializeField] private IceFrostEase.IceFrostEases _iceFrostEaseType;
        [SerializeField] private int _resolution = 100;
        [SerializeField] private float _width = 5f;
        [SerializeField] private float _height = 5f;
        [SerializeField] private ShowStyle _showStyle = ShowStyle.Everything;
        [Range(0,1)][SerializeField] private float _timeScale = 1f;
        [HideInInspector][SerializeField] private Transform _transform;
        [SerializeField]private Color _lineColor = Color.white;
        [SerializeField]private Color _graphSphereColor = Color.red;
        [SerializeField]private Color _staticSphereColor = Color.blue;
        [SerializeField]private Color _xAxisColor = Color.red;
        [SerializeField]private Color _yAxisColor = Color.green;
        [SerializeField] private float _graphSphereSize = 0.1f;
        [SerializeField] private float _staticSphereSize = 1f;

        [MenuItem("Tools/DoTween Eases")]
        private static void DoTweenEasesClick()
        {
            List<DoTweenEaseVisualizer> prefabs = GetAllAssetsOfType<DoTweenEaseVisualizer>(out string defaultPath).ToList();
            DoTweenEaseVisualizer firstOrNew = prefabs.FirstOrDefault();

            if(defaultPath == null)
            {
                string rootFolder = "Assets/IceFrostEases";
                string prefabsFolder = $"{rootFolder}/Prefabs";
            
                if (!AssetDatabase.IsValidFolder(rootFolder))
                {
                    AssetDatabase.CreateFolder("Assets", "IceFrostEases");
                }
            
                if (!AssetDatabase.IsValidFolder(prefabsFolder))
                {
                    AssetDatabase.CreateFolder(rootFolder, "Prefabs");
                }
            
                defaultPath = prefabsFolder;
                Debug.Log($"Created missing folder: {prefabsFolder}");
            }
            
            if (firstOrNew == null)
            {
                Debug.LogWarning("No DoTweenEaseVisualizer prefab found! Creating a new prefab.");
            
                GameObject newGameObject = new("DoTweenEaseVisualizer");
                newGameObject.AddComponent<DoTweenEaseVisualizer>();
            
                firstOrNew = PrefabUtility.SaveAsPrefabAsset(newGameObject, defaultPath + "/" + AssetFileName).GetComponent<DoTweenEaseVisualizer>();
                
                Debug.Log($"New DoTweenEaseVisualizer prefab created at {defaultPath + "/" + AssetFileName}");
            
                DestroyImmediate(newGameObject);
            }
            
            AssetDatabase.OpenAsset(firstOrNew);
            
            SceneView sceneView = SceneView.lastActiveSceneView;
            
            if(sceneView != null)
            {
                sceneView.sceneViewState = new SceneView.SceneViewState {alwaysRefresh = true};
                sceneView.in2DMode = true;
                sceneView.LookAt(firstOrNew._transform.position);
                SceneView.RepaintAll();
            }
        }

        private static IEnumerable<T> GetAllAssetsOfType<T>(out string defautAssetPath) where T : Object, new()
        {
            defautAssetPath = null;
            
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            defautAssetPath = allAssetPaths.FirstOrDefault(e => e.Contains("IceFrostEases") && e.Contains("Prefabs"));

            return allAssetPaths.Select(AssetDatabase.LoadAssetAtPath<T>).Where(e => e != null);
        }

        private void OnValidate()
        {
            _transform = transform;
            CurrPointOnGraph = 0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _lineColor;
            Span<Vector3> points = stackalloc Vector3[_resolution];
            
            Vector3 transformPosition = _transform.position;

            for (int i = 1; i <= _resolution; i++)
            {
                float t = (float)i / _resolution;
                
                float easedValue = IceFrostEase.GetValueOnCurve(0f, 1f, t, _iceFrostEaseType);
                Vector3 point = transformPosition + new Vector3(t * _width, easedValue * _height, 0);
                
                points[i - 1] = point;
            }

            if(_showStyle.HasFlag(ShowStyle.Graph))
            {
                Gizmos.color = _xAxisColor;
                Gizmos.DrawLine(transformPosition, transformPosition + new Vector3(_width, 0, 0));

                Gizmos.color = _yAxisColor;
                Gizmos.DrawLine(transformPosition, transformPosition + new Vector3(0, _height, 0));
                
                Gizmos.color = _lineColor;
                Gizmos.DrawLineStrip(points, false);
            }
            
            CurrPointOnGraph += Time.time / Time.frameCount * _timeScale;

            CurrPointOnGraph %= 1;

            float easedVal = IceFrostEase.GetValueOnCurve(0, 1, CurrPointOnGraph, _iceFrostEaseType);
            
            Vector3 currPointStep = transformPosition + new Vector3(CurrPointOnGraph * _width, easedVal * _height, 0);
            
            if(_showStyle.HasFlag(ShowStyle.SphereOnGraph))
            {
                Vector3 localPos = new(currPointStep.x, currPointStep.y, 0);
                float size = _graphSphereSize;

                if(_showStyle.HasFlag(ShowStyle.SizeGraphSphere)) size = easedVal * _graphSphereSize;
             
                Gizmos.color = _graphSphereColor;
                Gizmos.DrawSphere(localPos, size);
            }

            if(_showStyle.HasFlag(ShowStyle.StaticSphere))
            {
                Vector3 localPos = Vector3.zero;
                
                if(_showStyle.HasFlag(ShowStyle.MoveStaticSphere))
                    localPos.y = currPointStep.y;
                float size = _staticSphereSize;
                
                if(_showStyle.HasFlag(ShowStyle.SizeStaticSphere))
                    size = easedVal * _staticSphereSize;   
                
                Gizmos.color = _staticSphereColor;
                Gizmos.DrawSphere(localPos, size);
            }
        }

        [Flags]
        enum ShowStyle
        {
            None = 0,
            Graph = 1,
            SphereOnGraph = 2,
            SizeGraphSphere = 4,
            StaticSphere = 8,
            SizeStaticSphere = 16,
            MoveStaticSphere = 32,
            Everything = 1 + 2 + 4 + 8 + 16 + 32
        }
    }
}
#endif
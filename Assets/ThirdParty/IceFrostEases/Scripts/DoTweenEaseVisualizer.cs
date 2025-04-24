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
        // [InfoBox("Please set editor scene to Always Refresh. Button is above scene view where the scene tools are.")]
        [SerializeReference] private IceFrostEase.IceFrostEases _iceFrostEaseType = new();
        [SerializeField] private int _resolution = 100;
        [SerializeField] private float _width = 5f;
        [SerializeField] private float _height = 2f;
        [SerializeField] private int _currStep;
        [SerializeField] private ShowStyle _showStyle = ShowStyle.Everything;
        [SerializeField] private float _timeScale = OneF;
        private const float ZeroF = 0f;
        private const float OneF = 1f;
        private const float OneTenF = 0.1f;
        private const string DefaultPath = "Assets/DoTweenEaseVisualizer.prefab";

        [Flags]
        enum ShowStyle
        {
            None = 0,
            Graph = 1,
            SphereOnGraph = 2,
            SizeGraphSphere = 4,
            StaticSphere = 8,
            SizeStaticSphere = 16,
            Everything = 1 + 2 + 4 + 8 + 16
        }
        
        [MenuItem("Tools/DoTween Eases")]
        private static void DoTweenEasesClick()
        {
            List<DoTweenEaseVisualizer> prefabs = GetAllAssetsOfType<DoTweenEaseVisualizer>().ToList();
            DoTweenEaseVisualizer firstOrNew = prefabs.FirstOrDefault();
            
            if (firstOrNew == null)
            {
                Debug.LogWarning("No DoTweenEaseVisualizer prefab found! Creating a new prefab.");
            
                GameObject newGameObject = new("DoTweenEaseVisualizer");
                newGameObject.AddComponent<DoTweenEaseVisualizer>();
            
                firstOrNew = PrefabUtility.SaveAsPrefabAsset(newGameObject, DefaultPath).GetComponent<DoTweenEaseVisualizer>();
                
                Debug.Log($"New DoTweenEaseVisualizer prefab created at {DefaultPath}");
            
                DestroyImmediate(newGameObject);
            }
            
            string assetPath = AssetDatabase.GetAssetPath(firstOrNew.gameObject);
            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<GameObject>(assetPath));
            
            SceneView sceneView = SceneView.lastActiveSceneView;
            
            if(sceneView != null)
            {
                sceneView.sceneViewState = new SceneView.SceneViewState {alwaysRefresh = true};
                sceneView.in2DMode = true;
                sceneView.camera.transform.position = new Vector3(0f, 0f, -10f);
                SceneView.RepaintAll();
            }
        }
        public static IEnumerable<T> GetAllAssetsOfType<T>()
        {
            if (typeof(Object).IsAssignableFrom(typeof(T)))
            {
                return AssetDatabase.FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadMainAssetAtPath)
                .OfType<T>();
            }

            IEnumerable<T> result = AssetDatabase.GetAllAssetPaths()
            .Where(p => p.StartsWith("Assets"))
            .Where(p => typeof(T).IsAssignableFrom(AssetDatabase.GetMainAssetTypeAtPath(p)))
            .Select(AssetDatabase.LoadMainAssetAtPath)
            .OfType<T>();

            EditorUtility.UnloadUnusedAssetsImmediate();
            return result;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 prevPoint = transform.position;

            for (int i = 1; i <= _resolution; i++)
            {
                float t = i / (float)_resolution;
                
                t *= _timeScale;
                float easedValue = IceFrostEase.GetValueOnCurve(ZeroF, OneF, t, _iceFrostEaseType);
                Vector3 point = transform.position + new Vector3(t * _width, easedValue * _height, ZeroF);
                
                if(_showStyle.HasFlag(ShowStyle.Graph)) Gizmos.DrawLine(prevPoint, point);
                
                prevPoint = point;
            }
        
            float currStepRatio = _currStep / (float)_resolution;
            float easedVal = IceFrostEase.GetValueOnCurve(ZeroF, OneF, currStepRatio, _iceFrostEaseType);
            
            Vector3 currPointStep = transform.position + new Vector3(currStepRatio * _width, easedVal * _height, ZeroF);

            if(_showStyle.HasFlag(ShowStyle.SphereOnGraph))
            {
                Vector3 localPos = new(currPointStep.x, currPointStep.y, ZeroF);
                float size = OneTenF;

                if(_showStyle.HasFlag(ShowStyle.SizeGraphSphere)) size += easedVal;
                
                Gizmos.DrawSphere(localPos, size);
            }

            if(_showStyle.HasFlag(ShowStyle.StaticSphere))
            {
                Vector3 localPos = new(ZeroF, currPointStep.y, ZeroF);
                float size = OneTenF;
                
                if(_showStyle.HasFlag(ShowStyle.SizeStaticSphere))
                    size += easedVal;   
                Gizmos.DrawSphere(localPos, size);
            }
            
            _currStep ++;
            _currStep %= _resolution;
        }
    }
}
#endif
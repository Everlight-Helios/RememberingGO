using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GeboorteSwitcher))]
public class TunnelSwitcherEditor : Editor
{

    GeboorteSwitcher switcher;

    public override void OnInspectorGUI()
    {

        switcher = target as GeboorteSwitcher;

        //Slider for the array sizes
        switcher.amountOfSpots = EditorGUILayout.IntSlider(switcher.amountOfSpots, 0, 25);

        //Labels for each array
        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(("Fase: "), GUILayout.Width(50));
        EditorGUILayout.LabelField(("ParticleSystem: "), GUILayout.Width(100));
        EditorGUILayout.LabelField(("Sky Color: "), GUILayout.Width(75));
        EditorGUILayout.LabelField(("Ground Color: "), GUILayout.Width(75));
        EditorGUILayout.LabelField(("Set"), GUILayout.Width(50));
        EditorGUILayout.LabelField(("Start Time: "), GUILayout.Width(75));
        EditorGUILayout.LabelField(("Emisson: "));

        GUILayout.EndHorizontal();

        //Keep track of all the array items when changing the array size
        #region Copy Arrays if length has changed
        if (switcher.amountOfSpots != switcher.particleSystems.Length)
        {

            ParticleSystem[] tempParticleArray = switcher.particleSystems;
            Color[] tempSkyColor = switcher.skyColor;
            Color[] tempGroundColor = switcher.groundColor;
            float[] tempTiming = switcher.timings;
            float[] tempEmission = switcher.emissions;
            int[] tempFases = switcher.fases;


            switcher.CreateArrays(switcher.amountOfSpots);

            if (tempParticleArray.Length < switcher.particleSystems.Length)
            {

                for (int i = 0; i < tempParticleArray.Length; i++)
                {

                    switcher.particleSystems[i] = tempParticleArray[i];
                    switcher.skyColor[i] = tempSkyColor[i];
                    switcher.groundColor[i] = tempGroundColor[i];
                    switcher.timings[i] = tempTiming[i];
                    switcher.emissions[i] = tempEmission[i];
                    switcher.fases[i] = tempFases[i];

                }

            }

            if (tempParticleArray.Length > switcher.particleSystems.Length)
            {

                for (int i = 0; i < switcher.particleSystems.Length; i++)
                {

                    switcher.particleSystems[i] = tempParticleArray[i];
                    switcher.skyColor[i] = tempSkyColor[i];
                    switcher.groundColor[i] = tempGroundColor[i];
                    switcher.timings[i] = tempTiming[i];
                    switcher.emissions[i] = tempEmission[i];
                    switcher.fases[i] = tempFases[i];

                }

            }

        }
        #endregion

        //Draw each array Item in the inspector
        for (int i = 0; i < switcher.amountOfSpots; i++)
        {

            GUILayout.BeginHorizontal();

            switcher.fases[i] = EditorGUILayout.IntField(switcher.fases[i], GUILayout.Width(50));
            switcher.particleSystems[i] = EditorGUILayout.ObjectField(switcher.particleSystems[i], typeof(ParticleSystem), true, GUILayout.Width(100)) as ParticleSystem;
            switcher.skyColor[i] = EditorGUILayout.ColorField(switcher.skyColor[i], GUILayout.Width(75));
            switcher.groundColor[i] = EditorGUILayout.ColorField(switcher.groundColor[i], GUILayout.Width(75));
            if (GUILayout.Button("Set", GUILayout.Width(50))) { switcher.SetColour(i); }
            switcher.timings[i] = EditorGUILayout.FloatField(switcher.timings[i], GUILayout.Width(75));
            switcher.emissions[i] = EditorGUILayout.FloatField(switcher.emissions[i]);

            GUILayout.EndHorizontal();

        }

        GUILayout.Space(10);

        
        //Extra buttons for creation mode
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Set Fase: ", GUILayout.Width(100)))
        {

            switcher.SetFase(switcher.setFase);

        }
        switcher.setFase = EditorGUILayout.IntField(switcher.setFase, GUILayout.Width(150));

        if (GUILayout.Button("Disable all PartycleSystems", GUILayout.Width(200)))
        {

            switcher.Init();

        }

        switcher.autoContinue = EditorGUILayout.Toggle(switcher.autoContinue);

        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField(switcher.currentFase.ToString());

        //base.OnInspectorGUI();

    }

}
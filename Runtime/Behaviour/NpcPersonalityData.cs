using UnityEngine;
// created at 13-Apr-2026
namespace MP_Npc.Behavior
{
    [CreateAssetMenu(fileName = "NpcPersonalityData", menuName = "[ MP_NPC ]/Npc Personality Data")]
    public class NpcPersonalityData : ScriptableObject
    {
        [Range(min: 0, max: 100)]
        [Tooltip("Governs tendency to attack\nHigher chance of attack and use hostile actions;")]
        public int agression;

        [Range(min: 0, max: 100)]
        [Tooltip("Governs will to survive;\nHigher chance of flee or to use defensive actions;")]
        public int selfPreservation;

        [Range(min: 0, max: 100)]
        [Tooltip("Governs tendency to explore;\nHigher chance of reacting to certain stimulus...")]
        public int curiosity;

        [Range(min: 0, max: 100)]
        [Tooltip("Governs Consistency.\n...")]
        public int discipline;

        [Range(min: 0, max: 100)]
        [Tooltip("Governs tendency to take higher risks;\nHigher chance of doing something that has lower chance of being successful;")] // isto é distorce a avaliação de risco... o que em si ja é a self preservation
        public int boldness;

        [Header("TOTAL")]
        public int sumTotal;

        // serialize so that Unity saves/remember/do not lost value
        
        [SerializeField, HideInInspector] private int _previousAgression;
        [SerializeField, HideInInspector] private int _previousSelfPreservation;

        // can i use curiosity to unbalance a possible "analyze paralyze" of a NPC, like if he his stuck between attack and not, he decides the one that... is more likely to achieve his goals?? not exatly curiosity this...
        [SerializeField, HideInInspector] private int _previousCuriosity;
        
        [SerializeField, HideInInspector] private int _previousDiscipline;
        [SerializeField, HideInInspector] private int _previousBoldness;

        private void OnValidate()
        {
            Method_CheckValues();
        }
        [ContextMenu(itemName: "[ Check Personality ]")]
        private void Method_CheckValues()
        {
            if (agression + selfPreservation + curiosity + discipline + boldness > 100.0f)
            {
                agression = _previousAgression;
                selfPreservation = _previousSelfPreservation;
                curiosity = _previousCuriosity;
                discipline = _previousDiscipline;
                boldness = _previousBoldness;
            }

            else
            {
                _previousAgression = agression;
                _previousSelfPreservation = selfPreservation;
                _previousCuriosity = curiosity;
                _previousDiscipline = discipline;
                _previousBoldness = boldness;
            }

            sumTotal = agression + selfPreservation + curiosity + discipline + boldness;
        }
    }
}
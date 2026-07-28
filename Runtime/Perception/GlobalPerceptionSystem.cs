using System.Collections.Generic;
using UnityEngine;
// created at 29 - Apr - 2026
// | 001 | 08 Jul 2026 |
namespace MP_Npc.Perception
{
    public class GlobalPerceptionSystem : MonoBehaviour
    {
        [SerializeField] protected Vector3Int _gridSize;
        [SerializeField] protected float _cellSize;

        [Header("Gizmos")]
        [SerializeField] private bool _showBaseGrid;
        [SerializeField] private bool _showUsedCells;
        [SerializeField] private bool _showPlayerCell;
        [SerializeField] private Color gizmoColor_gridColor = Color.ghostWhite;
        [SerializeField] private Color gizmoColor_playerCell = Color.lightSeaGreen; // the lc_cell that the player is;
        [SerializeField] private Color gizmoColor_cellWithCharacters = Color.purple;
        [SerializeField] private Color gizmoColor_OutOfRangeCell = Color.darkOrange;
        [SerializeField, Range(0, 1)] private float gizmoAlpha_cellWithCharacterCube;

        protected List<PerceptionSystem> _perceptionSystemList;
        public Vector3Int _playerCell;

        // Key - PerceptionSystem : Value grid coordinate / lc_cell ID
        protected Dictionary<NpcComponent, Vector3Int> _gridDictionaryNpcToCell = new();
        protected Dictionary<Vector3Int, List<NpcComponent>> _gridDictionaryCellToNpc = new();

        private RaycastHit[] _raycastListBuffer;

        private bool _enableDebugMessages = false;

        private void Start()
        {
            // temp - WIP
            foreach (var item in _gridDictionaryNpcToCell) // need to bake before as the current project is
            {
                item.Key.Method_SetGPS(this);
                Debug.Log("GPS send to :" + item.Key.gameObject);
            }
        }

        private void Update()
        {

        }

        // created in [ 29 - Apr - 2026 ]
        public virtual void Method_RegistStimuliReceiver(in PerceptionSystem inReceiver)
        {
            // safety check for input null pointer
            if (inReceiver == null)
            {
                Debug.LogError(this + " : Method_RegistStimuliReceiver(in PerceptionSystem inReceiver) : inReceiver is null !!!");
                return;
            }

            if (!_perceptionSystemList.Contains(inReceiver))
            {
                _perceptionSystemList.Add(inReceiver);
            }
            else
            {
                if (_enableDebugMessages)
                {
                    Debug.Log(this + " : Method_RegisterStimuliListener(in PerceptionSystem inReceiver) : _perceptionSystemList already contains this receiver...");
                }
            }
        }

        // created in [ 29 - Apr - 2026 ]
        public virtual void Method_UnRegistStimuliReceiver(in PerceptionSystem inReceiver)
        {
            // safety check for input null pointer
            if (inReceiver == null)
            {
                Debug.LogError(this + " : Method_UnRegistStimuliReceiver(in PerceptionSystem inReceiver) : inReceiver is null !!!");
                return;
            }

            if (_perceptionSystemList.Contains(inReceiver))
            {
                _perceptionSystemList.Remove(inReceiver);
            }
            else
            {
                Debug.LogError(this + " : Method_UnRegistStimuliReceiver(in PerceptionSystem inReceiver) : _perceptionSystemList does not Contains (inReceiver) !!!");
            }
        }

        // created on [ 29 - Apr - 2026 ] - DEPRECATEDED - USE WORLD GRID TO FIND RELEVANT NPCs
        public virtual void Method_SendSoundStimuli(in StSoundStimuli inSound)
        {
            for (int i = 0; i < _perceptionSystemList.Count; i++)
            {
                // "cache" the perception system
                PerceptionSystem lcPerceptionSystem = _perceptionSystemList[i];

                // compute the sound direction
                Vector3 lcDirection = inSound.soundPosition - lcPerceptionSystem.storedTransform.position;

                // use sqrMagnitude cause? i think its because .magnitude cost more cause it performes a sqr root of a float, that is more expensive for computers than later just multiply lcPerceptionDistance * lcPerceptionDistance;
                float lcSoundDistanceSqr = lcDirection.sqrMagnitude;
                // get current (npc) perception system data usefull to filter
                float lcPerceptionDistance = lcPerceptionSystem.Method_ReturnPerceptionData().soundSenseData.distance;
                // sqr it
                float lcPerceptionDistanceSqr = lcPerceptionDistance * lcPerceptionDistance;

                // filter rules
                // WIP... add more details of filtering
                if (lcSoundDistanceSqr <= lcPerceptionDistanceSqr)
                {
                    lcPerceptionSystem.IMethod_ReceiveSoundStimuli(inSound);
                }
            }
        }


        // created in [ 01 - May - 2026 ]
        protected void Method_FindNearbyNpcs(in Vector3Int inCell, in bool inSearchOnAdjacentCells, out List<NpcComponent> outFoundOnSameCell, out List<NpcComponent> outFoundOnAdjacentCells)
        {
            outFoundOnSameCell = _gridDictionaryCellToNpc[inCell];

            if (!inSearchOnAdjacentCells)
            {
                outFoundOnAdjacentCells = null;
                return;
            }
            // TO DO
            outFoundOnAdjacentCells = null; // WIP
            // search on adjacent cells
            // should I return in two seperates lists or all in one?
        }

        // created on [ 30 - Apr - 2026 ]
        // last change in [ 01 - May - 2026 ]
        public Vector3Int Method_WorldToCell(in Vector3 inWorldPosition)
        {
            Vector3 local = inWorldPosition - transform.position;

            //int x = Mathf.FloorToInt(inWorldPosition.x / _cellSize);
            //int y = Mathf.FloorToInt(inWorldPosition.y / _cellSize);
            //int z = Mathf.FloorToInt(inWorldPosition.z / _cellSize);

            int x = Mathf.FloorToInt(local.x / _cellSize);
            int y = Mathf.FloorToInt(local.y / _cellSize);
            int z = Mathf.FloorToInt(local.z / _cellSize);

            Vector3Int cellPosition = new Vector3Int(x, y, z);

            return cellPosition;
        }

        // created on [ 30 - Apr - 2026 ]
        // last change in [ 01 - May - 2026 ]
        private Vector3 Method_CellToWorld(in Vector3Int inCellVector)
        {

            // calculate cell center in world space

            /// cell index * cell size + half cell size
            /// index 3; size 10; so: 3 * 10 + 10/2 => 30 + 5 => 35
            /// so cell borders are on 30 to 40

            float x = inCellVector.x * _cellSize + _cellSize * 0.5f;
            float y = inCellVector.y * _cellSize + _cellSize * 0.5f;
            float z = inCellVector.z * _cellSize + _cellSize * 0.5f;

            Vector3 worldPosition = new Vector3(x, y, z);
            worldPosition += transform.position;

            return worldPosition;
        }

        // created on [ 30 - Apr - 2026 ]
        public void Method_ReturnGridData(out Vector3Int outGridSize, out float outCellSize)
        {
            outGridSize = _gridSize;
            outCellSize = _cellSize;
        }

        // created on [ 30 - Apr - 2026 ]
        public void Method_UpdateNpcToGridValue(in NpcComponent inNpc, in Vector3Int inNewCell, in Vector3Int inOldCell)
        {
            if (inNewCell == inOldCell) { return; }
            // Step 1. Handle - CELL to NPC - dictionary
            // 1.1. ( REMOVE from OLD CELL) if find a grid Cell entry that matches the old cell, remove npc from it
            if (_gridDictionaryCellToNpc.TryGetValue(inOldCell, out List<NpcComponent> outListA))
            {
                //_gridDictionaryCellToNpc[inOldCell].Remove(inNpc); //unnecessary lookup, i can just ( obg GPT ! ) :

                // [CHANGED] use outListA instead a lookup
                outListA.Remove(inNpc);

                // [ADDED] cleanup -> remove entrada vazia (evita lixo na grid)
                if (outListA.Count == 0)
                {
                    _gridDictionaryCellToNpc.Remove(inOldCell);
                }
            }
            else
            {
                //...
                // [ADDED COMMENT] isto não deveria acontecer se o sistema estiver consistente
                // possível fallback/debug poin
            }
            // 1.2. (ADD to correspondent NEW CELL) add this Npc to a correct cell to npc dictionary. but for that...
            // 1.2.1.a. but first neet to check it that cell already has an entry if it has added it
            if (_gridDictionaryCellToNpc.TryGetValue(inNewCell, out List<NpcComponent> outListB))
            {
                //_gridDictionaryCellToNpc[inNewCell].Add(inNpc); //unnecessary lookup, i can just ( obg GPT ... AGAIN... ) :

                // [CHANGED] usar outNewList (evita lookup duplicado)
                outListB.Add(inNpc);

            }
            // 1.2.1.b. if it has not a cell to npc list entry with new cell, means that this cell is the first time being used or...
            else
            {
                _gridDictionaryCellToNpc.Add(inNewCell, new List<NpcComponent> { inNpc });
            }

            // Step 2. Handle Npc to Cell dictionary
            // 2.1.a. safety check it this NPC is not registred on the dictionary, if it is just change his value
            if (_gridDictionaryNpcToCell.TryGetValue(inNpc, out Vector3Int outFoundCell))
            {
                _gridDictionaryNpcToCell[inNpc] = inNewCell;
            }
            // 2.1.b. if it does not exist a entry with this NPC ( i think that something is very wrong, cause it is supoused to be registred on spawn )
            else
            {
                _gridDictionaryNpcToCell.Add(inNpc, inNewCell);
            }
        }

        // added in [ 30 - Apr - 2026 ]
        public List<NpcComponent> Method_FindNpcOnGridCell(Vector3Int inCell)
        {

            if (_gridDictionaryCellToNpc.TryGetValue(inCell, out var list))
            {
                return list;
            }
            return new();

        }

        // GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO | GIZMO |
        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying == false)
            {

                Method_DrawGrid();
                Method_DrawUsedGrids();
            }

        }

        private void OnDrawGizmos()
        {
            if (_showBaseGrid) { Method_DrawGrid(); }
            if (_showUsedCells) { Method_DrawUsedGrids(); }
            if (_showPlayerCell) { Method_DrawPlayerCell(); }

        }

        private void Method_DrawGrid()
        {
            Gizmos.color = gizmoColor_gridColor;

            for (int x = -_gridSize.x; x < _gridSize.x; x++)
            {
                for (int y = -_gridSize.y; y < _gridSize.y; y++)
                {
                    for (int z = -_gridSize.z; z < _gridSize.z; z++)
                    {
                        Vector3Int cell = new Vector3Int(x, y, z); // cell (1,2)... cell (3,10)
                        Vector3 worldPos = Method_CellToWorld(cell);
                        Gizmos.DrawWireCube(worldPos, Vector3.one * _cellSize);
                    }
                }
            }
        }

        // Created in [ 01 - May - 2026 ]
        private void Method_DrawUsedGrids()
        {
            foreach (var cell in _gridDictionaryCellToNpc)
            {
                Gizmos.color = gizmoColor_cellWithCharacters;

                if (cell.Key.x < -_gridSize.x || cell.Key.x >= _gridSize.x)
                {
                    Gizmos.color = gizmoColor_OutOfRangeCell;
                }
                if (cell.Key.y < -_gridSize.y || cell.Key.y >= _gridSize.y)
                {
                    Gizmos.color = gizmoColor_OutOfRangeCell;
                }
                if (cell.Key.z < -_gridSize.z || cell.Key.z >= _gridSize.z)
                {
                    Gizmos.color = gizmoColor_OutOfRangeCell;
                }

                if (cell.Value.Count != 0)
                {
                    Vector3 cellWorldPosition = Method_CellToWorld(cell.Key);
                    Gizmos.DrawWireCube(cellWorldPosition, Vector3.one * _cellSize);
                    Color cubeMeshColor = gizmoColor_cellWithCharacters; cubeMeshColor.a = gizmoAlpha_cellWithCharacterCube;
                    Gizmos.color = cubeMeshColor;
                    Gizmos.DrawCube(cellWorldPosition, Vector3.one * _cellSize);
                }
                else
                {
                    // this if should not even be needed. this should never been zero, cause by design when a cell has no characters on it, it is removed from the dictionary ( so no entry/key of it )
                    Debug.LogError(this + " : Cell key exist but it has no valid value !!!");
                }
            }
        }

        // Created in [ 01 - May - 2026 ]
        private void Method_DrawPlayerCell()
        {
            Gizmos.color = gizmoColor_playerCell;

            bool bX = (_playerCell.x < -_gridSize.x || _playerCell.x >= _gridSize.x);
            bool bY = _playerCell.y < -_gridSize.y || _playerCell.y >= _gridSize.y;
            bool bZ = (_playerCell.z < -_gridSize.z || _playerCell.z >= _gridSize.z);
            if (bX || bY || bZ)
            { Gizmos.color = gizmoColor_OutOfRangeCell; }

            Vector3 worldCellPosition = Method_CellToWorld(_playerCell);

            Gizmos.DrawWireCube(worldCellPosition, Vector3.one * _cellSize);
        }
    }
}
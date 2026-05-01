# com.moutinhoart.mp_npc

## Features:

### Npc Component

### Behaviour Brain - Utility AI
( C# class that is implemented on the NpcComponent.cs ) + ***Blackboard*** like class to help centralize data.
* BehaviourBrain.cs
* BaseTask.cs + TaskData.cs (Scriptable Object)
* NpcBlackboard
* BehaviourData.cs (Scriptable Object) **WIP**
* PersonalityData.cs (Scriptable Object) **WIP**

### Perception System
( C# class that is implemented on the NpcComponent.cs )

### Sense Class
( includes Pre made vision & hearing sense )

### Global Perception System
* Spatial Grid | Register and Un Reggist Npc From it | TO DO : Support special player detection to calculate relevant npcs | ...
* Uses a **Spatial Grid** to optimize perception lookups as alternative to what I was previouly using ( Physics.OverlapSphereNonAlloc ), this is special useful for larger number of perception systems on scene

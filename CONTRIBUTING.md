# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue,
trello, slack or any other method with the owners of this repository before making a change. 

Please note we have a code of conduct, please follow it in all your interactions with the project.

## Pull Request Process

1. Ensure any install or build dependencies are removed before the end of the layer when doing a 
   build.
2. Update the README.md with details of changes to the interface, this includes new environment 
   variables, exposed ports, useful file locations and container parameters.
3. You may merge the Pull Request in once you have the sign-off of two other developers, or if you 
   do not have permission to do that, you may request the second reviewer to merge it for you.

## Unity File Naming

### [Objects](https://wiki.unrealengine.com/Assets_Naming_Convention)

* (Prefix_)AssetName(_Number)(_Suffix)
* T_Rock_01_DP
* "T" is Texture, "Rock" is object, "01" is number of the object, "DP" is displacement

### Scenes

* Name each new scene with what your task following the # of scenes you created so far with a period starting at zero.

### NavMesh Baking

* Each time you bake a new scene for Navmesh; you must move the newly created folder in the "Scenes" folder to the "NavMeshes" folder located within the "Scenes" folder.
* If updating the bake layer of an already baked scene, most likely a newly created folder will be within the "Scenes" folder; please replace the old Navmesh folder with the new one within the "Navmeshes" folder.

# Dissection Simulator

Link to more details about the app:
https://docs.google.com/document/d/18mkl3Y3NmIzkeMq7uqK4UrSWDMSFOebWgi4ZVeP8AvM/edit?tab=t.0

Link to mode details about the main modules:
https://docs.google.com/document/d/1AzZmOjim9wghXR9magDH8NSHaltzgV3cRhk2BHXOPAA/edit?usp=sharing

Video demos can be found in 'video demos' directory

Update 1 includes:
- laboratory mesh (including windows, desks, shelves)
- dissection tools (downloaded from Sketchfab)
- basic shapes to showcase future object placement
- XR origin camera (allowing for free movement, as shown in demo1)

Update 2 includes:
- meshes for frog and organs (including heart, lungs, stomach, pancreas, intestines, colon, spleen and gallbladder)
- thickened bezier curves to simulate connections between organs
- improved lab space (added blackboard, removed useless dissection tools, added notebook for instructions, added tray to display organs)
- NOT YET IMPLEMENTED scripts for 'cutting' (using scalpel) and 'grabbing' (using forceps)
- implemented ability to grab and place organs with 'free hand' (as shown in demo2)
- integration with VR equipment (Meta Quest 3)


Update 3 includes:
- more organ meshes (liver, kidneys, bladder)
- more accurate bezier curves (vena cava, hepatogastric ligament, hepatoduodenal ligament, pyloric sphincter, ileocecal valve)
- retopology for current meshes
- improved frog model for better accuracy
- implemented script for cutting with the scalpel (as seen in one of the demo3 videos)
- NOT YET FUNCTIONAL forceps grab script

Update 4 includes:
- more retopology (the app is finally smooth)
- mesh for 'What's this?'-inator
- functionality for 'What's this?'-inator (it displays on screen the name of the asset it points as, as seen in demo4)
- forceps improvement (it's still broken but it doesn't randomly fly off the desk anymore)
- one of the specimens that will be displayed (an equine skull, more details in the video demos directory)

Update 5 includes:
- visible scanner ray
- scanned objects are now highlighted
- environment objects (floor, walls, desks) are no longer scannable
- notebook now has a turning page animation (as seen in demo5 video)
- added script for instructions (previous scripts need to be updated for tasks to be registered as completed)
- tasks can now be manually added in the unity interface
  
  <img width="439" height="576" alt="image" src="https://github.com/user-attachments/assets/d0c9b790-c96f-4877-8f0b-20688dc41828" />

Update 6 includes:
- more tasks implemented in order (in the notebook)
- me reaching my limit (see 'end my suffering.mp4' in video demos)
- forceps can now be manually controlled (using A and B to open and close)
- forceps can now pick up organs (organs now fall through table because their rigidbody doesn't properly work when dropped)

Update 7.1 includes:
- meshes that support progressive cuts in the skin and flap separation (this includes 4 main meshes for the belly and 12 meshes for the skin flaps and their stages)

<img width="810" height="611" alt="image" src="https://github.com/user-attachments/assets/b6d879d2-561f-4686-b426-fbcdfd58c885" />


- new scripts to manage progressive incisions
- guidelines for incisions
- recreated classroom space (colored walls, textured floor, recreated windows, colored blackboard, lab sink, teacher's desk, window blinds, wall clock, etc)

<img width="1359" height="753" alt="image" src="https://github.com/user-attachments/assets/fc74367b-e438-41e3-9ad2-b28046e1724e" />


- fixed lungs mesh (normals were messy for some reason)
- fixed bezier curves (since they were made before the actual belly, there were clipping issues)

!!! No video yet because I want to fix a few bugs I'm not happy with first !!!

Update 7.2 (work in progress):
- fixed organs falling through the tray when let go
- fixed the script for incisions (and now the belly cycles through the cut stages)
- working on making the skin flaps script work with all of the flaps (for unknown reasons, one of the flaps has a regular mesh filter and mesh renderer, while all the others have only a skinned mesh renderer)
- working on fixing a bug that causes both the opened and closed meshes to be rendered at the same time



Next update will (hopefully) include:
- actual textures for all organs
- fix for rigidbody deactivation for organs
- more clutter and details in laboratory
- sound design

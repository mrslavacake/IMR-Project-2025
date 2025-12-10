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


Next update will (hopefully) include:
- actual textures for all organs
- proper implementation for forceps
- more clutter and details in laboratory
- sound design

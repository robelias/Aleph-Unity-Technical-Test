# Overview
The camera is located in a gallery where you can move freely around it using the **ASDW** movement keys. When you approach a piece of art, a panel with the details of the piece is displayed. If you press the **E** key, the extended information of the piece is displayed and you can save it as a favorite using the **F** key.

Other shortcut actions are: 

• **R:** To view the last inspected piece of art, provided that you have already seen the details of one.

• **Tab**: To open the gallery selector and to travel between them you can use the [1] and [2] buttons.

• **Q**: To open the favorites window, showing all the works that have been opened to this group.


# How to run it?
To open the Project, you cannot simply double-click on the *index.html* file, since modern browsers block access to local files for security reasons. To open it you can use a lightweight local server to host this project such as Python, Node.js, XAMPP, WAMP or Visual Studio Code Live Server.

# Architecture
The project follows a **Singleton** (*GlobalData*) design pattern used to manage global data or controllers that must be maintained throughout the entire life of the application.
Each controller contained in the *GlobalData* is initialized in the *OnEnable* so that any script can access them, and they can exist in any scene controlling an activity without needing to remain in time because their memory space is cleaned when the scene is destroyed.

**Notes**
Since WebGL does not allow access to persistent data, a *JSON* hosted on *GitHub* was created with the artwork data to initialize the local database. The favorites list is currently being saved in *PlayerPref* but in a real project there should be an API that allows loading and saving this data. 


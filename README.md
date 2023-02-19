
# Studious Persistent Management System

Is a small very simple library, that helps create persistent components with ease, and make it easier for everyone to get up and running with very little effort.

## Installation

Studious Persistent Management System is best installed as a package, this can be done in a number of ways, the best is to use git and install via the package manage.

#### **Via Git Hub** 

We highly recommend installing it this way, if you have git installed on your machine, then you can simply go to main page of this repository and select the code button. Then copy the HTTPS URL using the button next to the URL.

You can then open up the Package Manager within the Unity Editor, and select the drop down on the plus icon along the top left of the Package Manager. You can then select add by Git and paste the URL from the GitHub repository here.

#### **Add Package from Disk** 

There are two ways to do this, one would be to unzip the download to an area on your hard drive, and another would be to unzip the download into the actual project in the packages folder.

Both are workable solutions, however we would suggest if you would like to use this for multiple projects, that you install somewhere on your hard drive that you will remember. And then use the Package Manager to add the package from Disk.

Installing or unzipping into the projects package folder, will work out of the box when you open the project up.

## Usage

Getting up and running is extremely easy with Studious Persistent Management System, in the first example we are going to create a component.

```CS
public class TestClass : MonoBehaviour
{
}
```

And now to make this class persist across scenes, we only need to add an attribute to the class. The `GroupName` is not required, and is optional.

```CS
using Studious.PersistentManagement;

[Persistent(GroupName = "Name Of Object")]
public class TestClass : MonoBehaviour
{
    public int Health = 100;
}
```

And anytime we need to reference something from that script, then all we need to do is

```CS
public class Player : MonoBehaviour
{
    private void Start()
    {
        TestClass tc = PersistentLocator.Get<TestClass>();
        tc.Health = 100;
    }
}
```

The following parameters can be used here.

#### **GroupName**

The GroupName helps keep the components grouped, the way the system works is that it creates a DonTDestroyOnLoad scene, where a Game Object will exist. Any class with an Attribute not using the `GroupName` 
will be added to this GameObject. The `GroupName` allows for Scripts to be grouped across `GameObjects`, and keep the added components to a minimum on the Game Objects.

## Caveat

Due to the way scenes are loaded, if you need to access a one of the persistent objects, you will need to do so in the `Start()` event and not the Awake(). In the future this can be changed once Unity allows the Ability to subscribe to Before Scene Loading, as it stands Unity only allow for subscribing to when a scene has loaded.

If you have written you own, SceneManager that can fire Before Scene Loaded events then you should be safe to use the Components created in the Awake() event, however this is currently untested.

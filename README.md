# LowLevelMouseHook
C# library for hooking into global windows mouse movements

### ABOUT

I wrote this DLL library because I couldn't find any good online resources for how to capture global
mouse position outside of the Windows Form model. I realized I could use a low level Win32 API hook
to do the job but wasn't able to find anything working online or elsewhere on GitHub. Most projects I
saw had issues with their API or method that was no longer working with Visual Studio 2012/2013 or the
.NET 4.5 library. Anyhow, this project is a working implementation based off multiple projects that did
it wrong.


### USAGE

Either build the project by itself and include the DLL library as a reference to your main project

OR

Include the project into your main solution and have it built with your project solution. You will still
need to include a reference to the other project from your primary project.

(You right-click the reference item under the main project and click "Add Reference". Should be under the projects tab)


###SAMPLE CODE

```c#
using MouseInputManager;

int main() {

	MouseInput manager = new MouseInput();
	manager.MouseMoved += mouseManager_MouseMoved;

}

private void mouseManager_MouseMoved(object sender, MouseEventArgs e) {
	Point mousePosition = new Point();
	mousePosition.X = e.X;
	mousePosition.Y = e.Y;
}
```
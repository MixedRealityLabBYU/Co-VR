# OnButtonPress

Calls a method in a specified script when a button on an XR controller is pressed.

## Properties

### action
*public InputAction action*
<br>The action to be performed when the button is pressed.

### OnPress
*public UnityEvent OnPress*
<br>The Unity Event to be called when the button is pressed.

### OnRelease
*public UnityEvent OnRelease*
<br>The Unity Event to be called when the button is released.

<br>

## Methods

### Awake
*private void Awake()*
<br>Adds the events from Pressed and Released to the action.

### OnDestroy
*private void OnDestroy()*<br>
Removes the events from Pressed and Released from the action.

### OnEnable
*private void OnEnable()*<br>
Enables the action.

### OnDisable
*private void OnDisable()*<br>
Disables the action.

### Pressed
*private void Pressed(InputAction.CallbackContext context)*<br>
Invokes the OnPress Unity Event.

### Released
*private void Released(InputAction.CallbackContext context)*<br>
Invokes the OnRelease Unity Event.

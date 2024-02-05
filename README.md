This test demonstrates that the FluentSlider's two-way data binding is broken in version 4.4.0.

In my web app, I have a component that requires a slider reaches its max value, enabling a selection field, and then after a selection is made, a button triggers a command and this command also resets the value of the slider to 0.

That above behavior is not working in Fluent UI. (It was working in MudBlazor, but I am rewriting the app with your library for screen reader support.)

I've prepared a demo of the problematic behaviors.

Go ahead and max out the slider, which will enable the reset button. The expected behavior is for the reset button to set the value back to 0, and for the slider component to reflect that value.

```
	<FluentSlider @ref=FluentSlider @bind-Value="FluentModel.DataValue" Min="0" Max="Model.MaxValue" Step="1" Class="slider-test">
		@foreach (var i in Enumerable.Range(0, Model.MaxValue + 1))
		{
			<FluentSliderLabel Position="i">@i</FluentSliderLabel>
		}
	</FluentSlider>

	<FluentButton OnClick="FluentReset" Disabled="Model.MaxValue != FluentModel.DataValue">Reset</FluentButton>
```
You can see in the console logs that the data model's value is reset to 0, however the slider's value is kept at 3 both visibly and under the hood. This implies that it isn't respecting the binding.

Sometimes, to force Fluent UI Blazor components to behave how I want them to, I will create an inheriting component that exposes the StateHasChanged() method.

The expected behavior would be that forcing StateHasChanged() to be called would update the component. It does not.
```
	<HackSlider @ref=HackSlider @bind-Value="HackModel.DataValue" Min="0" Max="Model.MaxValue" Step="1" Class="slider-test">
		@foreach (var i in Enumerable.Range(0, Model.MaxValue + 1))
		{
			<FluentSliderLabel Position="i">@i</FluentSliderLabel>
		}
	</HackSlider>

	<FluentButton OnClick="HackReset" Disabled="Model.MaxValue != HackModel.DataValue">Reset</FluentButton>
```
Calling StateHasChanged() does not cause the underlying SliderValue to respect the expected change.

As a final test, I will reset the SliderValue within the custom hacky component.
```
	<HackSlider @ref=Hack2Slider @bind-Value="Hack2Model.DataValue" Min="0" Max="Model.MaxValue" Step="1" Class="slider-test">
		@foreach (var i in Enumerable.Range(0, Model.MaxValue + 1))
		{
			<FluentSliderLabel Position="i">@i</FluentSliderLabel>
		}
	</HackSlider>

	<FluentButton OnClick="Hack2Reset" Disabled="Model.MaxValue != Hack2Model.DataValue">Reset</FluentButton>
```
Here we see that despite reseting both the Blazor component and the underlying data model to 0, the slider component is still visually stuck at its maximum value.

Please fix.

Arazni

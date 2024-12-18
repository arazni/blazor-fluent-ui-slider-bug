### Thank you for fixing the FluentSlider in 4.4.1.

### There are 2 Problems
#### FluentNumberField

This test demonstrates that the FluentNumberField's two-way data binding is broken in version 4.10.4. This appears to be a regression.

In my web app, I have a component that takes a numeric input and divies a portion to a wallet and a portion to a stash. After a number is selected, a button triggers a command and this command also resets the value of the numeric field to 0.

Unfortunately, the numeric field does not respect the behavior of being reset to 0 by the button command.

Go ahead and select a number like 2, which will enable the reset button. The expected behavior is for the reset button to set the value back to 0, and for the number input field to reflect that value.

```
<div style="display: flex; gap: 20px; margin-bottom: 20px;">
	<FluentNumberField @ref="FluentNumberField" @bind-Value="NumberModel.DataValue" Min="0" Max="@Model.MaxValue.ToString()" />
	<FluentButton OnClick="NumberReset" Disabled="NumberModel.DataValue < 1">Reset</FluentButton>
</div>
```

You can see in the console logs that the data model's value is reset to 0, however the number field's value is still non-zero both visibly and under the hood. This implies that it isn't respecting the binding.

#### FluentSlider

This test demonstrates that the FluentSlider's two-way data binding can desync from its bound value in version 4.10.4. This may be a regression.

While you can see the behavior working for the Reset button, the behavior does not always work for the "Change by 2" button.

Go ahead and set the slider to 3, which will enable the reset button (only enabled at 3). Reset. You see that it works.

Go ahead and set the slider to 1, then keep pressing the "Change by 2" button. In addition to the visual weirdness, by checking the console logs, you will see that it desyncs from the actual value in the data.

When I run it, the console logs always have an accurate data value for the "Change by 2" button's usage. The underlying slider value on blazor side is one step behind. And the visual only updates every 3 clicks of the button.

A second test scenario is: 1. Refresh the page. 2. Set the slider to 1. 3. Click "Change by 2". 4. Click Reset. This will break the visual synchronization with the "Reset" button, which is otherwise usually working correctly.

```
<FluentSlider @ref=FluentSlider @bind-Value="SliderModel.DataValue" Min="0" Max="Model.MaxValue" Step="1" Class="slider-test">
	@foreach (var i in Enumerable.Range(0, Model.MaxValue + 1))
	{
		<FluentSliderLabel Position="i">@i</FluentSliderLabel>
	}
</FluentSlider>

<FluentButton OnClick="SliderReset" Disabled="Model.MaxValue != SliderModel.DataValue">Reset</FluentButton>

<FluentButton OnClick="SliderChangeBy2">Change by 2</FluentButton>
```

Please fix!

Thank you,
Arazni

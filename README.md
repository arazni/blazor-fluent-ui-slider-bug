### Thank you for fixing the FluentSlider in 4.4.1.

### The FluentNumberField is broken in a similar way.

This test demonstrates that the FluentNumberField's two-way data binding is broken in version 4.4.1.

In my web app, I have a component that takes a numeric input and divies a portion to a wallet and a portion to a stash. After a number is selected, a button triggers a command and this command also resets the value of the numeric field to 0.

Unfortunately, the numeric field does not respect the behavior of being reset to 0 by the button command.

Go ahead and select a number like 2, which will enable the reset button. The expected behavior is for the reset button to set the value back to 0, and for the number input field to reflect that value.

```
<div style="display: flex; gap: 20px; margin-bottom: 20px;">
	<FluentNumberField @ref="FluentNumberField" @bind-Value="FluentModel.DataValue" Min="0" Max="@Model.MaxValue.ToString()" />
	<FluentButton OnClick="FluentReset" Disabled="FluentModel.DataValue < 1">Reset</FluentButton>
</div>
```

You can see in the console logs that the data model's value is reset to 0, however the number field's value is still non-zero both visibly and under the hood. This implies that it isn't respecting the binding.

Please fix.

Arazni
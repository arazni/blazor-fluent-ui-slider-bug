using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.FluentUI.AspNetCore.Components;

public partial class HackRating : FluentInputBase<int>
{
	private bool _updatingCurrentValue = false;
	private int? _hoverValue = null;

	/// <summary />
	protected override string? ClassValue => new CssBuilder(base.ClassValue)
			.AddClass("fluent-rating")
			.Build();

	/// <summary />
	protected override string? StyleValue => new StyleBuilder(base.StyleValue).Build();

	/// <summary>
	/// Gets or sets the number of elements.
	/// </summary>
	[Parameter]
	public int Max { get; set; } = 5;

	/// <summary>
	/// The icon to display when the rating value is greater than or equal to the item's value.
	/// </summary>
	[Parameter]
	public Icon IconFilled { get; set; } = new Icon("Star", IconVariant.Filled, IconSize.Size20, "<path d=\"M9.1 2.9a1 1 0 0 1 1.8 0l1.93 3.91 4.31.63a1 1 0 0 1 .56 1.7l-3.12 3.05.73 4.3a1 1 0 0 1-1.45 1.05L10 15.51l-3.86 2.03a1 1 0 0 1-1.45-1.05l.74-4.3L2.3 9.14a1 1 0 0 1 .56-1.7l4.31-.63L9.1 2.9Z\"></path>");

	/// <summary>
	/// The icon to display when the rating value is less than the item's value.
	/// </summary>
	[Parameter]
	public Icon IconOutline { get; set; } = new Icon("Star", IconVariant.Regular, IconSize.Size20, "<path d=\"M9.1 2.9a1 1 0 0 1 1.8 0l1.93 3.91 4.31.63a1 1 0 0 1 .56 1.7l-3.12 3.05.73 4.3a1 1 0 0 1-1.45 1.05L10 15.51l-3.86 2.03a1 1 0 0 1-1.45-1.05l.74-4.3L2.3 9.14a1 1 0 0 1 .56-1.7l4.31-.63L9.1 2.9Zm.9.44L8.07 7.25a1 1 0 0 1-.75.55L3 8.43l3.12 3.04a1 1 0 0 1 .3.89l-.75 4.3 3.87-2.03a1 1 0 0 1 .93 0l3.86 2.03-.74-4.3a1 1 0 0 1 .29-.89L17 8.43l-4.32-.63a1 1 0 0 1-.75-.55L10 3.35Z\"></path>");

	/// <summary>
	/// Gets or sets the icon drawing and fill color. 
	/// Value comes from the <see cref="Color"/> enumeration. Defaults to Accent.
	/// </summary>
	[Parameter]
	public Color? IconColor { get; set; }

	/// <summary>
	/// Gets or sets the icon drawing and fill color to a custom value.
	/// Needs to be formatted as an HTML hex color string (#rrggbb or #rgb) or CSS variable.
	/// ⚠️ Only available when Color is set to Color.Custom.
	/// </summary>
	[Parameter]
	public string? IconCustomColor { get; set; }

	/// <summary>
	/// The icon width.
	/// </summary>
	[Parameter]
	public string IconWidth { get; set; } = "28px";

	/// <summary>
	/// Gets or sets a value that whether to allow clear when click again.
	/// </summary>
	[Parameter]
	public bool AllowReset { get; set; } = false;

	/// <summary>
	/// Fires when hovered value changes. Value will be null if no rating item is hovered.
	/// </summary>
	[Parameter]
	public EventCallback<int?> OnHoverValueChanged { get; set; }

	/// <summary />
	private string GroupName => Id ?? $"rating-{Id}";

	internal virtual bool FieldBound => Field is not null || ValueExpression is not null || ValueChanged.HasDelegate;

	/// <summary />
	private Icon GetIcon(int index) => index <= (_hoverValue ?? Value) ? IconFilled : IconOutline;

	/// <summary />
	protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out int result, [NotNullWhen(false)] out string? validationErrorMessage)
	{
		if (BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result))
		{
			validationErrorMessage = null;
			return true;
		}
		else
		{
			validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
																						 "The {0} field must be a number.",
																						 FieldBound ? FieldIdentifier.FieldName : "UnknownBoundField");
			return false;
		}
	}

	/// <summary />
	private async Task OnClickAsync(int value, bool fromFocus = false)
	{
		_updatingCurrentValue = true;

		// Reset ?
		if (AllowReset && value == Value && !fromFocus)
		{
			await SetCurrentValueAsync(0);
			await UpdateHoverValueAsync(null);
		}
		else
		{
			await SetCurrentValueAsync(value);
		}
	}

	/// <summary />
	private async Task OnMouseEnterAsync(int value)
	{
		if (_updatingCurrentValue)
		{
			return;
		}

		await UpdateHoverValueAsync(value);
	}

	/// <summary />
	private async Task OnMouseLeaveAsync()
	{
		await UpdateHoverValueAsync(null);
	}

	/// <summary />
	private async Task UpdateHoverValueAsync(int? value)
	{
		if (_hoverValue == value)
		{
			return;
		}

		_hoverValue = value;

		if (OnHoverValueChanged.HasDelegate)
		{
			await OnHoverValueChanged.InvokeAsync(value);
		}
	}
}
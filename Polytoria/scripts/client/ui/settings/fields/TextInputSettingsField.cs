using Godot;
using Polytoria.Client.Settings;
using Polytoria.Shared.Settings;

namespace Polytoria.Client.UI;

public sealed partial class TextInputSettingsField: LineEdit
{
	public SettingDef Definition = null!;
	private HBoxContainer _hbox = null!;
	private Label _valueLabel = null!;

	private System.Action<SettingChangedEvent>? _changedHandler;
	public override void _Ready()
	{
		/*_hbox = new HBoxContainer
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			SizeFlagsVertical = SizeFlags.ShrinkCenter
		};
		AddChild(_hbox);

		_lineEdit = new LineEdit
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			SizeFlagsVertical = SizeFlags.ShrinkCenter
		};*/

		_valueLabel = new Label
		{
			CustomMinimumSize = new Vector2(64, 0),
			HorizontalAlignment = HorizontalAlignment.Right,
			VerticalAlignment = VerticalAlignment.Center
		};

		TextChanged += (value) =>
		{
			ClientSettingsService.Instance.Set(Definition.Key, value);

		};

		_changedHandler = e =>
		{
			if (e.Key == Definition.Key && e.NewValue is string s)
			{
				//Placeholder
			}
		};
		ClientSettingsService.Instance.Changed += _changedHandler;
		
		base._Ready();

		//Why won't this show up in the creator menu?!
	}

	public override void _ExitTree()
	{
		if (_changedHandler != null && ClientSettingsService.Instance != null)
		{
			ClientSettingsService.Instance.Changed -= _changedHandler;
			_changedHandler = null;
		}

		base._ExitTree();
	}
}

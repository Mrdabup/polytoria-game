using System.Formats.Tar;
using Godot;
using Polytoria.Client.Settings;
using Polytoria.Shared.Settings;

namespace Polytoria.Client.UI;

public sealed partial class TextInputSettingsField: LineEdit
{
	public SettingDef Definition = null!;
	private HBoxContainer _hbox = null!;
	private LineEdit _lineEdit = null!;

	private System.Action<SettingChangedEvent>? _changedHandler;
	public override void _Ready()
	{
		_hbox = new HBoxContainer
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			SizeFlagsVertical = SizeFlags.ShrinkCenter
		};
		AddChild(_hbox);
		_lineEdit = new LineEdit
		{
			SizeFlagsHorizontal = SizeFlags.ExpandFill,
			SizeFlagsVertical = SizeFlags.ShrinkCenter
		};

		TextChanged += (value) =>
		{
			ClientSettingsService.Instance.Set(Definition.Key, value);

		};

		_changedHandler = e =>
		{
			if (e.Key == Definition.Key && e.NewValue is string s)
			{
				//Doesn't work
			}
		};
		ClientSettingsService.Instance.Changed += _changedHandler;
		
		base._Ready();
	}
}

using Pokebot.Models;
using Pokebot.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class PathControl : UserControl
    {
        public PokemonFilterPanel FilterPanel { get; private set; }

        private readonly List<PathAction> _path;

        public PathControl()
        {
            InitializeComponent();
            FilterPanel = new PokemonFilterPanel();
            _path = new List<PathAction>();
            _upButton.Text = Messages.Path_Up;
            _downButton.Text = Messages.Path_Down;
            _leftButton.Text = Messages.Path_Left;
            _rightButton.Text = Messages.Path_Right;
            _clearButton.Text = Messages.Path_Clear;
            UpdatePathPreview();
        }

        public void SetFilterPanel(GenerationInfo generationInfo)
        {
            FilterPanel.Dock = DockStyle.Fill;
            _filterPanel.Controls.Clear();
            _filterPanel.Controls.Add(FilterPanel);

            FilterPanel.Initialize(generationInfo);
        }

        public IReadOnlyList<PathAction> GetPath()
        {
            return _path;
        }

        private void AddAction(PathAction action)
        {
            _path.Add(action);
            UpdatePathPreview();
        }

        private void UpdatePathPreview()
        {
            _pathPreviewLabel.Text = _path.Count == 0
                ? Messages.Path_EmptyPreview
                : string.Format(Messages.Path_Preview, _path.Count, string.Join(" ", _path.Select(GetArrow)));
        }

        private static string GetArrow(PathAction action)
        {
            switch (action)
            {
                case PathAction.Up:
                    return "↑";
                case PathAction.Down:
                    return "↓";
                case PathAction.Left:
                    return "←";
                case PathAction.Right:
                    return "→";
                default:
                    return "?";
            }
        }

        private void _upButton_Click(object sender, EventArgs e) => AddAction(PathAction.Up);
        private void _downButton_Click(object sender, EventArgs e) => AddAction(PathAction.Down);
        private void _leftButton_Click(object sender, EventArgs e) => AddAction(PathAction.Left);
        private void _rightButton_Click(object sender, EventArgs e) => AddAction(PathAction.Right);

        private void _clearButton_Click(object sender, EventArgs e)
        {
            _path.Clear();
            UpdatePathPreview();
        }
    }
}

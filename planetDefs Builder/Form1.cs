using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable IDE1006
#pragma warning disable CS8602
#pragma warning disable CS8622
#pragma warning disable CA1822
namespace planetDefs_Builder
{
    public partial class Form1 : Form
    {
        readonly string[] collections = ["galaxy", "star", "planet", "properties", "attributes"];
        readonly string[] neverDelete = ["Galaxy", "Properties", "Attributes"];
        public Form1()
        {
            InitializeComponent();
            galaxyTreeView.DrawNode += galaxyTreeView_DrawNode;
            galaxyTreeView.NodeMouseClick += galaxyTreeView_NodeMouseClick;
            galaxyTreeView.AfterSelect += galaxyTreeView_AfterSelect;

        }

        private void galaxyTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (collections.Contains(e.Node.Name)) return;
            pathLabel.Text = e.Node.FullPath.Replace('\\', '>');
            if (e.Node.Parent.Parent.Name == "star") descLabel.Text = References.StarAttributes[e.Node.Text];
            else //Planet
            {
                if (e.Node.Parent.Name == "attributes") descLabel.Text = References.PlanetAttributes[e.Node.Text];
                else descLabel.Text = References.PlanetSpecifications[e.Node.Text];
            }
            promptLabel.Visible = true;
            inputTextBox.TextChanged -= inputTextBox_TextChanged;
            inputTextBox.Text = e.Node.Name;
            inputTextBox.TextChanged += inputTextBox_TextChanged;
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            galaxyTreeView.BeginUpdate();
            if (collections.Contains(galaxyTreeView.SelectedNode.Name)) return;
            galaxyTreeView.SelectedNode.Name = inputTextBox.Text;
            if (galaxyTreeView.SelectedNode.Text == "name") galaxyTreeView.SelectedNode.Parent.Parent.Text = inputTextBox.Text;
            galaxyTreeView.EndUpdate();
        }

        private void galaxyTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            galaxyTreeView.SelectedNode = e.Node;
            if (e.Node.Name == "attributes" && e.Node.Level == 2) return;
            ContextMenuStrip cms = new();
            if (collections.Contains(e.Node.Name) && e.Node.Level % 2 == 0)
            {
                cms.Items.Add("New element");
                switch (e.Node.Name)
                {
                    case "galaxy":
                        (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add("Star", null, newElementDropDown_OnClick);
                        break;
                    case "properties":
                        if (e.Node.Parent.Name == "planet")
                        {
                            foreach(KeyValuePair<string, string> specification in References.PlanetSpecifications)
                            {
                                if (!ContainsText(e.Node.Nodes, specification.Key) || specification.Key == "artifact" || specification.Key == "gas")
                                    (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add(specification.Key, null, newElementDropDown_OnClick);
                            }
                            (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add("Moon", null, newElementDropDown_OnClick);
                        }
                        else if (e.Node.Parent.Name == "star" && e.Node.Level == 2)
                        {
                            (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add("Binary Star", null, newElementDropDown_OnClick);
                            (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add("Planet", null, newElementDropDown_OnClick);
                        }
                        break;
                    case "attributes":
                        if (e.Node.Parent.Name == "planet" && !ContainsText(e.Node.Nodes, "customIcon")) (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add("customIcon", null, newElementDropDown_OnClick);
                        if (e.Node.Parent.Name == "planet" && !ContainsText(e.Node.Nodes, "dimMapping")) (cms.Items[0] as ToolStripMenuItem).DropDownItems.Add("dimMapping", null, newElementDropDown_OnClick);
                        break;
                }
            }
            if (!neverDelete.Contains(e.Node.Text)) if (e.Node.Parent.Text != "Attributes" || (e.Node.Text == "customIcon" | e.Node.Text == "dimMapping")) cms.Items.Add("Delete");
            cms.ItemClicked += cms_ItemClicked;
            e.Node.ContextMenuStrip = cms;
        }

        private void newElementDropDown_OnClick(object? sender, EventArgs e)
        {
            switch ((sender as ToolStripItem).Text)
            {
                case "Star":
                    galaxyTreeView.SelectedNode.Nodes.Add(
                        References.NewTreeNode("New star", "star",
                        [
                            References.NewTreeNode("Attributes", "attributes",
                                [
                                    References.NewTreeNode("temp", "70"),
                                    References.NewTreeNode("blackHole", "false"),
                                    References.NewTreeNode("size", "1.0"),
                                    References.NewTreeNode("name", "New star"),
                                    References.NewTreeNode("numPlanets", "0"),
                                    References.NewTreeNode("numGasGiants", "0"),
                                    References.NewTreeNode("x", "0"),
                                    References.NewTreeNode("y", "0")
                                ]),
                            References.NewTreeNode("Properties", "properties")
                        ])
                        );
                    break;
                case "Binary Star":
                    galaxyTreeView.SelectedNode.Nodes.Add(
                        References.NewTreeNode("New binary star", "star",
                        [
                            References.NewTreeNode("Attributes", "attributes",
                                [
                                    References.NewTreeNode("temp", "70"),
                                    References.NewTreeNode("blackHole", "false"),
                                    References.NewTreeNode("size", "1.0"),
                                    References.NewTreeNode("name", "New binary star"),
                                    References.NewTreeNode("separation", "10")
                                ]),
                        ])
                        );
                    break;
                case "Planet":
                    galaxyTreeView.SelectedNode.Nodes.Add(
                        References.NewTreeNode("New planet", "planet",
                        [
                            References.NewTreeNode("Attributes", "attributes",
                                [
                                    References.NewTreeNode("name", "New planet"),
                                    References.NewTreeNode("DIMID", "null")
                                ]),
                            References.NewTreeNode("Properties", "properties")
                        ])
                        );
                    break;
                case "Moon":
                    galaxyTreeView.SelectedNode.Nodes.Add(
                        References.NewTreeNode("New moon", "planet",
                        [
                            References.NewTreeNode("Attributes", "attributes",
                                [
                                    References.NewTreeNode("name", "New moon"),
                                    References.NewTreeNode("DIMID", "null")
                                ]),
                            References.NewTreeNode("Properties", "properties")
                        ])
                        );
                    break;
                case "customIcon":
                    galaxyTreeView.SelectedNode.Nodes.Add(References.NewTreeNode("customIcon", "earthLike"));
                    break;
                default:
                    galaxyTreeView.SelectedNode.Nodes.Add(References.NewTreeNode((sender as ToolStripItem).Text, "null"));
                    break;
            }
        }

        private void cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "New element":
                    //Do nothing
                    break;
                case "Delete":
                    galaxyTreeView.SelectedNode.Remove();
                    break;
            }
        }

        private void galaxyTreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Name == "galaxy") alternation = 0;
            else alternation++;

            Rectangle nodeRect = e.Node.Bounds;
            if (e.Node.Bounds.X == 0) return;
            Console.WriteLine(e.Node.Parent);

            e.Node.ForeColor = Color.FromArgb(255, 255, 255);
            e.Node.BackColor = (e.State & TreeNodeStates.Focused) != 0 ? Color.FromArgb(61, 61, 61) : alternation % 2 == 0 ? Color.FromArgb(35, 35, 35) : Color.FromArgb(40, 40, 40);

            /*--------- 2. draw expand/collapse icon ---------*/
            Point ptExpand = new(nodeRect.Location.X - 36, nodeRect.Location.Y + 1);

            /*--------- 3. draw node icon ---------*/
            Point ptNodeIcon = new(nodeRect.Location.X - 18, nodeRect.Location.Y + 1);
            Image nodeImg = GetNodeIcon(e);
            nodeImg = new Bitmap(nodeImg, new Size(16, 16));

            /*--------- 4. draw node text ---------*/
            Font nodeFont = new(
                new FontFamily("Calibri"),
                12,
                FontStyle.Regular,
                GraphicsUnit.Point);
            nodeFont ??= ((TreeView)sender).Font;
            Brush textBrush = new SolidBrush(e.Node.ForeColor);
            Rectangle textRect = nodeRect;
            textRect.X -= 12;
            textRect.Y -= 1;
            textRect.Width += 40;
            textRect.Width = 250 - textRect.X;
            Rectangle valueRect = textRect;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            valueRect.X = 250;
            valueRect.Width = e.Node.TreeView.Width - valueRect.X;

            e.Graphics.FillRectangle(new SolidBrush(e.Node.BackColor), e.Bounds);
            if (e.Node.Nodes.Count > 0) e.Graphics.DrawImage(e.Node.IsExpanded ? Resources.Collapse : Resources.Expand, ptExpand);
            e.Graphics.DrawImage(nodeImg, ptNodeIcon);
            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush, Rectangle.Inflate(textRect, -10, 0));
            if (!collections.Contains(e.Node.Name)) e.Graphics.DrawString(e.Node.Name, nodeFont, textBrush, Rectangle.Inflate(valueRect, -10, 0));
        }

        static int alternation = 0;
        private Image GetNodeIcon(DrawTreeNodeEventArgs e)
        {
            Image resource;
            if (e.Node.Name == "planet")
            {

                if (ContainsText(e.Node.Nodes[0].Nodes, "customIcon"))
                {
                    int customIconIndex = FirstIndexOf(e.Node.Nodes[0].Nodes, "customIcon");
                    string customIcon = e.Node.Nodes[0].Nodes[customIconIndex].Name;

                    resource = Resources.GetResource(customIcon);
                    if (resource is null)
                    {
                        if (File.Exists(customIcon))
                        {
                            try
                            {
                                return Image.FromFile(customIcon);
                            }
                            catch (Exception)
                            {
                                return Resources.noIcon;
                            }
                        }
                        else return Resources.noIcon;
                    }
                    else return resource;
                }
                else
                {
                    return Resources.noIcon;
                }
            }
            else if (e.Node.Name == "star")
            {
                Bitmap starTemp = Resources.star;

                if (e.Node.Nodes.Count < 1)
                {
                    return Resources.star;
                }
                else
                {
                    int tempValue = int.TryParse(e.Node.Nodes[0].Nodes[FirstIndexOf(e.Node.Nodes, "temp")].Name, out _) ? int.Parse(e.Node.Nodes[0].Nodes[FirstIndexOf(e.Node.Nodes, "temp")].Name) : 0;
                    if (e.Node.Nodes[0].Nodes[FirstIndexOf(e.Node.Nodes, "temp")].Text == "temp")
                    {
                        Color starColor = getColor(tempValue);
                        for (int x = 0; x < starTemp.Width; x++)
                        {
                            for (int y = 0; y < starTemp.Height; y++)
                            {
                                Color pixelColor = starTemp.GetPixel(x, y);
                                starTemp.SetPixel(x, y, Color.FromArgb(pixelColor.A, starColor.R, starColor.G, starColor.B));
                            }
                        }
                        return starTemp;
                    }
                    else
                    {
                        return Resources.star;
                    }
                }
            }
            else
            {
                resource = Resources.GetResource(e.Node.Text);
                if (resource is null)
                {
                    return Resources.Placeholder;
                }
                else return resource;
            }
        }
        private int FirstIndexOf(TreeNodeCollection e, string text)
        {
            foreach (TreeNode child in e)
            {
                if (child.Text == text) return child.Index;
            }
            return 0;
        } //e.Node.Nodes.Find("attributes", false).First().Nodes.ContainsKey()
        private bool ContainsText(TreeNodeCollection e, string text)
        {
            foreach (TreeNode child in e)
            {
                if (child.Text == text) return true;
            }
            return false;
        }
        private Color getColor(int temp)
        {
            //Define
            float[] color = new float[3];
            float temperature = (temp * 0.477f) + 10f; //0 -> 10 100 -> 57.7

            //Find red
            if (temperature < 66)
                color[0] = 1f;
            else
            {
                color[0] = temperature - 60;
                color[0] = 329.69f * (float)Math.Pow(color[0], -0.1332f);

                color[0] = Math.Clamp(color[0] / 255f, 0f, 1f);
            }

            //Calc Green
            if (temperature < 66)
            {
                color[1] = temperature;
                color[1] = (float)(99.47f * Math.Log(color[1]) - 161.1f) / 255f;
                color[1] = Math.Clamp(color[1], 0f, 1f);
            }
            else
            {
                color[1] = temperature - 60;
                color[1] = 288f * (float)Math.Pow(color[1], -0.07551);
                color[1] = Math.Clamp(color[1] / 255f, 0f, 1f);
            }

            if (temperature > 67)
                color[2] = 1f;
            else if (temperature <= 19)
            {
                color[2] = 0f;
            }
            else
            {
                color[2] = temperature - 10;
                color[2] = (float)(138.51f * Math.Log(color[2]) - 305.04f);
                color[2] = Math.Clamp(color[2] / 255f, 0f, 1f);
            }
            int[] rgbI = [(int)(color[0] * 255f), (int)(color[1] * 255f), (int)(color[2] * 255f)];
            Color Fcolor = Color.FromArgb(rgbI[0], rgbI[1], rgbI[2]);
            return Fcolor;
        }
    }
}

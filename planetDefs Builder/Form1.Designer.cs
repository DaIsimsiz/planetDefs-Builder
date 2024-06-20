using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace planetDefs_Builder
{
    class BufferedTreeView : TreeView // https://stackoverflow.com/questions/10362988/treeview-flickering/10364283#10364283
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }
        // Pinvoke:
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
    partial class Form1
    {
        static string versionID = "1.1.0-rc";
        // MAJOR.MEDIUM.MINOR
        // MAJOR => Major code changes, code has become unrecognizable from the previous major build
        // MEDIUM => A new unique feature is added
        // MINOR => An existing feature has been tweaked, the code is relatively the same
        // TAG =>
        //      rc - Release candidate
        //      beta - Unstable
        //      dev - Development build (debug features added)
        //      xp - Experimental build (new features)

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("Galaxy");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            galaxyTreeView = new BufferedTreeView();
            ImageList = new ImageList(components);
            inputTextBox = new TextBox();
            pathLabel = new Label();
            descLabel = new Label();
            promptLabel = new Label();
            ExportButton = new Button();
            SuspendLayout();
            // 
            // galaxyTreeView
            // 
            galaxyTreeView.BackColor = Color.FromArgb(40, 40, 40);
            galaxyTreeView.BorderStyle = BorderStyle.None;
            galaxyTreeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            galaxyTreeView.Font = new Font("Segoe UI", 10.5F);
            galaxyTreeView.ForeColor = Color.White;
            galaxyTreeView.ImageIndex = 0;
            galaxyTreeView.ImageList = ImageList;
            galaxyTreeView.Indent = 8;
            galaxyTreeView.ItemHeight = 18;
            galaxyTreeView.LineColor = Color.FromArgb(119, 119, 119);
            galaxyTreeView.Location = new Point(21, 21);
            galaxyTreeView.Name = "galaxyTreeView";
            treeNode1.Name = "galaxy";
            treeNode1.Text = "Galaxy";
            galaxyTreeView.Nodes.AddRange(new TreeNode[] { treeNode1 });
            galaxyTreeView.SelectedImageIndex = 0;
            galaxyTreeView.Size = new Size(347, 528);
            galaxyTreeView.TabIndex = 0;
            galaxyTreeView.TabStop = false;
            // 
            // ImageList
            // 
            ImageList.ColorDepth = ColorDepth.Depth32Bit;
            ImageList.ImageStream = (ImageListStreamer)resources.GetObject("ImageList.ImageStream");
            ImageList.TransparentColor = Color.Transparent;
            ImageList.Images.SetKeyName(0, "Placeholder.png");
            // 
            // inputTextBox
            // 
            inputTextBox.BackColor = Color.FromArgb(15, 15, 15);
            inputTextBox.BorderStyle = BorderStyle.None;
            inputTextBox.Font = new Font("Segoe UI", 10F);
            inputTextBox.ForeColor = Color.FromArgb(240, 240, 240);
            inputTextBox.Location = new Point(392, 529);
            inputTextBox.Name = "inputTextBox";
            inputTextBox.PlaceholderText = "Enter a value here";
            inputTextBox.Size = new Size(385, 18);
            inputTextBox.TabIndex = 3;
            inputTextBox.TabStop = false;
            inputTextBox.TextChanged += inputTextBox_TextChanged;
            inputTextBox.Enabled = false;
            // 
            // pathLabel
            // 
            pathLabel.BackColor = Color.FromArgb(81, 81, 81);
            pathLabel.Font = new Font("Segoe UI", 10F);
            pathLabel.ForeColor = Color.FromArgb(240, 240, 240);
            pathLabel.Location = new Point(389, 440);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new Size(388, 21);
            pathLabel.TabIndex = 2;
            // 
            // descLabel
            // 
            descLabel.BackColor = Color.FromArgb(61, 61, 61);
            descLabel.Font = new Font("Segoe UI", 10F);
            descLabel.ForeColor = Color.FromArgb(240, 240, 240);
            descLabel.Location = new Point(389, 463);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(388, 51);
            descLabel.TabIndex = 1;
            // 
            // promptLabel
            // 
            promptLabel.BackColor = Color.FromArgb(35, 35, 35);
            promptLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            promptLabel.ForeColor = Color.FromArgb(240, 240, 240);
            promptLabel.Location = new Point(387, 514);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(100, 15);
            promptLabel.TabIndex = 4;
            promptLabel.Text = "Value:";
            promptLabel.Visible = false;
            // 
            // ExportButton
            // 
            ExportButton.BackColor = Color.Transparent;
            ExportButton.BackgroundImage = Resources.export;
            ExportButton.BackgroundImageLayout = ImageLayout.Center;
            ExportButton.FlatAppearance.BorderColor = Color.Black;
            ExportButton.FlatAppearance.BorderSize = 0;
            ExportButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ExportButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ExportButton.FlatStyle = FlatStyle.Flat;
            ExportButton.ForeColor = Color.Transparent;
            ExportButton.Location = new Point(773, 573);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(24, 24);
            ExportButton.TabIndex = 6;
            ExportButton.TabStop = false;
            ExportButton.UseMnemonic = false;
            ExportButton.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(48, 48, 48);
            ClientSize = new Size(800, 600);
            Controls.Add(ExportButton);
            Controls.Add(inputTextBox);
            Controls.Add(pathLabel);
            Controls.Add(descLabel);
            Controls.Add(promptLabel);
            Controls.Add(galaxyTreeView);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "planetDefs Builder";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            Log($"File dragged in");
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            Log($"File dropped in: FILENAME:{((string[])e.Data.GetData(DataFormats.FileDrop))[0]} FILECONTENTS:{XDocument.Parse(File.ReadAllText(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))}");
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            try
            {
                if (!file.ToLower().EndsWith(".xml")) throw new FileFormatException("Invalid file format.");
                XDocument Document = XDocument.Parse(File.ReadAllText(file));
                galaxyTreeView.Nodes.Clear();

                TreeNode galaxy = References.NewTreeNode("Galaxy", "galaxy");

                foreach (XElement star in Document.Root.Elements())
                {
                    TreeNode starNode = References.NewTreeNode("null", "star");
                    TreeNode starAttributes = References.NewTreeNode("Attributes", "attributes");
                    TreeNode starProperties = References.NewTreeNode("Properties", "properties");

                    foreach (XAttribute starAttribute in star.Attributes())
                    {
                        if (starAttribute.Name == "name") starNode.Text = starAttribute.Value;
                        starAttributes.Nodes.Add(References.NewTreeNode(starAttribute.Name.ToString(), starAttribute.Value));
                    }

                    if (star.Elements().Count() > 0)
                        foreach (XElement planet in star.Elements())
                        {
                            if (planet.Name == "planet")
                            {
                                TreeNode planetNode = References.NewTreeNode("null", "planet");
                                TreeNode planetAttributes = References.NewTreeNode("Attributes", "attributes");
                                TreeNode planetProperties = References.NewTreeNode("Properties", "properties");
                                TreeNode planetMoons = References.NewTreeNode("Moons", "moonslist");

                                foreach (XAttribute planetAttribute in planet.Attributes())
                                {
                                    if (planetAttribute.Name == "name") planetNode.Text = planetAttribute.Value;
                                    planetAttributes.Nodes.Add(References.NewTreeNode(planetAttribute.Name.ToString(), planetAttribute.Value));
                                }

                                if (star.Elements().Count() > 0)
                                    foreach (XElement planetProperty in planet.Elements())
                                    {
                                        if (planetProperty.Elements().Count() > 0)
                                        {
                                            TreeNode moonNode = References.NewTreeNode("null", "planet");
                                            TreeNode moonAttributes = References.NewTreeNode("Attributes", "attributes");
                                            TreeNode moonProperties = References.NewTreeNode("Properties", "properties");

                                            foreach (XAttribute moonAttribute in planetProperty.Attributes())
                                            {
                                                if (moonAttribute.Name == "name") moonNode.Text = moonAttribute.Value;
                                                moonAttributes.Nodes.Add(References.NewTreeNode(moonAttribute.Name.ToString(), moonAttribute.Value));
                                            }
                                            moonNode.Nodes.Add(moonAttributes);
                                            if (planetProperty.Elements().Count() > 0)
                                                foreach (XElement moonProperty in planetProperty.Elements())
                                                    moonProperties.Nodes.Add(References.NewTreeNode(moonProperty.Name.ToString(), moonProperty.Value));
                                            moonNode.Nodes.Add(moonProperties);

                                            planetMoons.Nodes.Add(moonNode);
                                        }
                                        else planetProperties.Nodes.Add(References.NewTreeNode(planetProperty.Name.ToString(), planetProperty.Value));
                                    }
                                planetNode.Nodes.Add(planetAttributes);
                                planetNode.Nodes.Add(planetProperties);
                                planetNode.Nodes.Add(planetMoons);
                                starProperties.Nodes.Add(planetNode);
                            }
                            else
                            {
                                TreeNode binaryStarNode = References.NewTreeNode("Binary Star", "star");
                                TreeNode binaryStarAttributes = References.NewTreeNode("Attributes", "attributes");

                                foreach (XAttribute binaryStarAttribute in planet.Attributes())
                                {
                                    if (binaryStarAttribute.Name == "name") binaryStarNode.Text = binaryStarAttribute.Value;
                                    binaryStarAttributes.Nodes.Add(References.NewTreeNode(binaryStarAttribute.Name.ToString(), binaryStarAttribute.Value));
                                }
                                binaryStarNode.Nodes.Add(binaryStarAttributes);

                                starProperties.Nodes.Add(binaryStarNode);
                            }
                        }
                    starNode.Nodes.Add(starAttributes);
                    starNode.Nodes.Add(starProperties);
                    galaxy.Nodes.Add(starNode);
                }
                galaxyTreeView.Nodes.Add(galaxy);
                MessageBox.Show("The planetDefs.xml file was imported to the application successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Import successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occured while attempting to import XML.\n\nError: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Import failed: INFO:{ex}", "WARNING");
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Log($"Export triggered");
            XDocument Document = new(new XElement("galaxy")) { Declaration = new XDeclaration("1.0", "UTF-8", "no") };
            foreach (TreeNode star in galaxyTreeView.Nodes[0].Nodes)
            {
                XElement starElement = new("star");
                foreach (TreeNode starAttribute in star.Nodes[0].Nodes)
                    starElement.Add(new XAttribute(starAttribute.Text, starAttribute.Name));

                if (star.Nodes.Count > 1)
                    foreach (TreeNode planet in star.Nodes[1].Nodes)
                    {
                        if (planet.Name == "planet")
                        {
                            XElement planetElement = new("planet");
                            foreach (TreeNode planetAttribute in planet.Nodes[0].Nodes)
                                planetElement.Add(new XAttribute(planetAttribute.Text, planetAttribute.Name));
                            if (planet.Nodes[1].Nodes.Count > 0) foreach (TreeNode planetProperty in planet.Nodes[1].Nodes) planetElement.Add(new XElement(planetProperty.Text, planetProperty.Name));
                            if (planet.Nodes[2].Nodes.Count > 0) foreach (TreeNode moon in planet.Nodes[2].Nodes)
                                {
                                    if (moon.Nodes.Count > 0)
                                    {
                                        XElement moonElement = new("planet");

                                        foreach (TreeNode moonAttribute in moon.Nodes[0].Nodes)
                                            moonElement.Add(new XAttribute(moonAttribute.Text, moonAttribute.Name));
                                        if (moon.Nodes[1].Nodes.Count > 0)
                                            foreach (TreeNode moonProperty in moon.Nodes[1].Nodes)
                                                moonElement.Add(new XElement(moonProperty.Text, moonProperty.Name));

                                        planetElement.Add(moonElement);
                                    }
                                    else planetElement.Add(new XElement(moon.Text, moon.Name));
                                }
                            starElement.Add(planetElement);
                        }
                        else
                        {
                            XElement binaryStarElement = new("star");
                            foreach (TreeNode starAttribute in planet.Nodes[0].Nodes)
                                binaryStarElement.Add(new XAttribute(starAttribute.Text, starAttribute.Name));

                            starElement.Add(binaryStarElement);
                        }
                    }
                Document.Root.Add(starElement);
            }
            Document.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\planetDefs.xml");
            MessageBox.Show("The planetDefs.xml file was exported to your desktop successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Log($"Export ended: {Document.ToString()}");
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.FromArgb(66, 66, 66)), 17, 17, 355, 536);
            g.FillRectangle(new SolidBrush(Color.FromArgb(25, 25, 25)), 0, 570, 800, 30);
            g.FillRectangle(new SolidBrush(Color.FromArgb(66, 66, 66)), 383, 434, 400, 119);
            g.FillRectangle(new SolidBrush(Color.FromArgb(35, 35, 35)), 387, 438, 392, 111);

            //g.FillRectangle(new SolidBrush(Color.FromArgb(81, 81, 81)), 389, 440, 388, 18);
            //g.FillRectangle(new SolidBrush(Color.FromArgb(61, 61, 61)), 389, 460, 388, 51);
            g.FillRectangle(new SolidBrush(Color.FromArgb(15, 15, 15)), 389, 529, 388, 18);


            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(versionID, new Font("Cascadia Code", 12), Brushes.White, new Point(4, 576));
        }

        #endregion
        private ImageList ImageList;
        private BufferedTreeView galaxyTreeView;
        private TextBox inputTextBox;
        private Label pathLabel;
        private Label descLabel;
        private Label promptLabel;
        private Button ExportButton;
    }

    class References
    {
        /// <summary>
        /// Provides a better way to create TreeNodes.
        /// </summary>
        static public TreeNode NewTreeNode(string text = null, string name = null, TreeNode[] children = null)
        {
            TreeNode temp = new(text ??= "New node"); temp.Name = (name ??= "Node value"); if (children is not null) temp.Nodes.AddRange(children);
            return temp;
        }
        /// <summary>
        /// A list of attributes a planet may have.
        /// </summary>
        static readonly public Dictionary<string, string> PlanetAttributes = new()
        {
            { "name", "Name of the planet" },
            { "DIMID", "Dimension ID for the planet, make sure to not use an ID in use!"},
            { "dimMapping", "Enter an empty string if the dimension you have specifies already exists." },
            { "customIcon", "You can choose a custom icon if you want to specify which one should be used."}
        };
        /// <summary>
        /// A list of properties a planet may have.
        /// </summary>
        static readonly public Dictionary<string, string> PlanetSpecifications = new()
        {
            { "isKnown", "If false, the planet will have to be discovered from the Warp Controller." },
            { "hasRings", "If true, the planet will have rings." },
            { "genType", "Experimental specification, 0 is overworld-like generation, 1 is nether-like generation and 2 is asteroids." },
            { "fogColor", "3 floating point numbers (1.0 - 0.0) or a hex code (0xFFFFFF) to choose the distance fog color. (e.g 1.0,1.0,1.0)" },
            { "skyColor", "3 floating point numbers (1.0 - 0.0) or a hex code (0xFFFFFF) to choose the sky color. (e.g 1.0,1.0,1.0)" },
            { "atmosphereDensity", "Atmospheric pressure of a planet, it affects the temperature. Default is 100 (1 atm = 100)" },
            { "hasOxygen", "Specifies if the oxygen is breathable." },
            { "gravitationalMultiplier", "G force on the planet, default is 100, but values above 110 will prevent 1 block jumps. (max 400 - min 0)" },
            { "orbitalDistance", "Planet's distance from the star." },
            { "orbitalTheta", "Starting position of the planet in its orbit in degrees." },
            { "orbitalPhi", "Clockwise displacement of star's rising and setting direction. (e.g. 90 makes stars rise from north.)" },
            { "rotationalPeriod", "Day-Night cycle of the planet in ticks. (seconds x 20 = ticks)" },
            { "fillerBlock", "Swaps dirt and stone with the specified block." },
            { "oceanBlock", "Specifies which block will be placed instead in oceans. (e.g. minecraft:lava, minecraft:air, minecraft:water)" },
            { "seaLevel", "Y level water bodies start appearing at." },
            //{ "spawnable", "Specifies what can spawn on the planet. (e.g. minecraft:villager, minecraft:ghast)" },
            { "biomeIds", "A list of biomes that can be found on a planet, the format is \"mod:biomeName;weight\". Weight determines how common the biome is. (e.g. minecraft:jungle;30,minecraft:plains;10)" },
            { "artifact", "Items needed to travel to the planet with the Warp Controller. (e.g. minecraft:coal 1, minecraft:obsidian) " },
            { "generateCraters", "If true, the planet will have craters on it." },
            { "generateCaves", "If true, planet will have caves in it." },
            { "generateVolcanos", "If true, planet will have volcanoes on it." },
            { "generateStructures", "If true, all sorts of structures will spawn on the planet." },
            { "generateGeodes", "If true, geodes will spawn on the planet." },
            { "retrograde", "If true, orbit direction swaps to counter-clockwise." },
            { "ringColor", "3 floating point numbers (1.0 - 0.0) or a hex code (0xFFFFFF) to choose the color of the rings. (e.g 1.0,1.0,1.0)" },
            //{ "forceRiverGeneration", "If true, regardless of the other conditions, rivers will spawn." },
            //{ "oreGen", "do not use" },
            //{ "laserDrillOres", "Unknown." },
            //{ "geodeOres", "oreDict" },
            //{ "craterOres", "oreDict." },
            //{ "craterBiomeWeights", "unknown" },
            { "craterFrequencyMultiplier", "A lower value means a higher amount of craters" },
            { "volcanoFrequencyMultiplier", "A lower value means a higher amount of volcanos" },
            { "geodefrequencyMultiplier", "A lower value means a higher amount of geodes" },
            //{ "hasShading", "unknown" },
            //{ "hasColorOverride", "unknown" },
            //{ "skyRenderOverride", "unknown" },
            { "GasGiant", "If true, the planet will become a Gas Giant, making landing impossible, but it will allow gas to be harvested from its atmosphere." }, /**/
            { "gas", "Specifies which gas(es) are in the Gas Giant's atmosphere." } /**/
        };
        /// <summary>
        /// A list of attributes a main star may have.
        /// </summary>
        static readonly public Dictionary<string, string> StarAttributes = new()
        {
            { "temp", "Temperature of the star, affects it's color. (Sol's temperature is 100)" },
            { "blackHole", "If true, the star becomes a black hole." },
            { "size", "Size of the star, default is 1.0" },

            { "name", "Name of the star!" },
            { "numPlanets", "Number of randomly generated terrestrial planets." },
            { "numGasGiants", "Number of randomly generated gaseous planets." },
            { "x", "X coordinate of the star in galaxy view." },
            { "y", "Y coordinate of the star in galaxy view." },

            { "separation", "Distance from the center star." }
        };
        /// <summary>
        /// A list of collections used by the program.
        /// </summary>
        static readonly public Dictionary<string, string> Collections = new()
        {
            { "galaxy", "The root element of this galaxy. All of the stars have to be children of this element to work correctly!" },
            { "star", "A star!" },
            { "planet", "A planet/moon orbiting a parent celestial body!" },
            { "properties", "A list of properties of this celestial body. These are displayed as separate XML elements." },
            { "attributes", "A list of specification for this celestial body. These are displayed together with the XML element for this body." },
            { "moonslist", "A list of moons this planet has. This is no different than a property list, but it is separated in this app for simplicity." }
        };
    }
}

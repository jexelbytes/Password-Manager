using System;
using System.IO;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using Newtonsoft.Json;
using Password_Manager.Properties;

namespace Password_Manager
{
    public partial class Form1 : Form
    {
        string fileLastPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "adfga.txt";

        string START_MINIMIZED_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + "SM.txt";

        string RUN_ON_STARTUP_STATE = AppDomain.CurrentDomain.BaseDirectory.ToString() + "RS.txt";

        string ICONS_FOLDER_PATH = AppDomain.CurrentDomain.BaseDirectory.ToString() + "icons/";

        Usuario thisUser = new Usuario();

        Form BandejaDesplegable = new notify_tray();

        int lblIndex = 20;

        bool Toggle = false;

        public Form1()
        {
            InitializeComponent();

            startMinimized();

            fileExiste();
        }

        private void fileExiste()
        {
            if (File.Exists(START_MINIMIZED_FILE_PATH))
            {
                startMincheckBox.Checked = true;
            }

            if (File.Exists(RUN_ON_STARTUP_STATE))
            {
                startWincheckBox.Checked = true;
            }

            if (File.Exists(fileLastPath))
            {
                goToAccederPage();
            }
            else
            {
                registrar();
            }
        }

        private void startMinimized()
        {
            if (File.Exists(START_MINIMIZED_FILE_PATH))
            {
                Toggle= true;
                toggleHIDE();
            }
        }

        private void toggleHIDE()
        {
            if (Toggle || this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                Toggle = false;
            }else{
                this.WindowState = FormWindowState.Normal;
                Toggle = true;
            }

            this.ShowInTaskbar = Toggle;
        }

        private void goToAccederPage()
        {
            loginPage.Visible = true;
            loginPage.Dock = DockStyle.Fill;
            loginPage.BringToFront();

            //this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new System.Drawing.Size(539, 489);
            try
            {
                ruta.Text = File.ReadAllText(fileLastPath);
            }
            catch (Exception)
            {
                ruta.Text = "\\vaultdb";
            }

            userKey.Focus();

            thisUser.setPasOnRunTime("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            thisUser.setPasOnRunTime("");

            thisUser = new Usuario();

            ListaItems.Controls.Clear();
            
            LogOut.Enabled = false;
        }

        private void registrar()
        {
            loginPage.Visible = false;
            panelRegistro.Location = new Point(0, 0);
            panelRegistro.Visible = true;
            panelRegistro.BringToFront();
            this.Size = new System.Drawing.Size(816, 489);
        }

        private void Vault_MouseClick(object sender, MouseEventArgs e)
        {
            BandejaDesplegable.Location = new Point(Screen.PrimaryScreen.Bounds.Width - BandejaDesplegable.Size.Width, Screen.PrimaryScreen.Bounds.Height - BandejaDesplegable.Size.Height);

            BandejaDesplegable.Visible = !BandejaDesplegable.Visible;
        }

        private void guardarPath(string path)
        {
            File.WriteAllText(fileLastPath, path);
        }

        // ***************************************************************************************** REGISTRO
        private void Siguiente_Click(object sender, EventArgs e)
        {
            if (userKeyR.Text != "")
            {
                if (userKeyR.Text == userKeyRv.Text)
                {
                    cambiarFormularioDeRegistro(false);

                    panelRegistro.Location = new Point(-800, 0);

                    directorioArchivoPass.Focus();
                }
                else
                {
                    userKeyRv.Focus();
                    MessageBox.Show("las contraseñas no coinciden", "F");
                }
            }
            else
            {
                userKeyR.Focus();
                MessageBox.Show("Debe colocar una contraseña", "F");
            }
        }

        private void seleccionarDirectorioArchivoPass_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directorioArchivoPass.Text = folderBrowserDialog1.SelectedPath + "\\vaultdb";
            }
            else
            {
                MessageBox.Show("Fail", "Fail");
            }
        }

        private void crearNuevoUsuario_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();

            if (user.userCreate(userKeyR.Text, directorioArchivoPass.Text))
            {
                panelRegistro.Enabled = false;
                panelRegistro.Visible = false;
                guardarPath(directorioArchivoPass.Text);
                userKeyR.Text = "";
                userKeyRv.Text = "";
                fileExiste();
            }
            else
            {
                MessageBox.Show("Error al crear el usuario", "Error");
            }
        }

        private void atras1_Click(object sender, EventArgs e)
        {
            directorioArchivoPass.Text = "";

            cambiarFormularioDeRegistro(true);

            panelRegistro.Location = new Point(0, 0);
        }

        private void cambiarFormularioDeRegistro(bool inicio)
        {
            userKeyR.Enabled = inicio;
            userKeyRv.Enabled = inicio;
            Siguiente.Enabled = inicio;

            directorioArchivoPass.Enabled = !inicio;
            seleccionarDirectorioArchivoPass.Enabled = !inicio;
            crearNuevoUsuario.Enabled = !inicio;
            atras1.Enabled = !inicio;
        }

        //************************************************************************************ INICIO DE SESION
        private void Acceder_Click(object sender, EventArgs e)
        {
            if (thisUser.userLogin(userKey.Text, ruta.Text))
            {
                thisUser.setPasOnRunTime(userKey.Text);
                userKey.Text = "";
                boveda.Visible = true;
                boveda.Enabled = true;
                boveda.BringToFront();
                boveda.Dock = DockStyle.Fill;
                cargarKeys();
                this.Size = new System.Drawing.Size(816, 489);
                thisUser.path = ruta.Text;

                LateralPanel.Dock = DockStyle.Right;
                LateralPanel.Width = 0;

                guardarPath(ruta.Text);

                LogOut.Enabled = true;
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "error");
                userKey.Focus();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            registrar();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            goToAccederPage();
        }

        //************************************************************************************************ VAULT
        private void cargarKeys()
        {
            ListaItems.Controls.Clear();

            List<key_item> keyList = thisUser.getKeyList(thisUser.getPasOnRunTime());

            if (keyList.Count < 1)
            {
                Label lbl = new Label();
                lbl.Text = "No hay \"items\" para mostrar";
                lbl.Location = new Point(50, ListaItems.Height-50);
                lbl.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                lbl.Font = new Font("Century Gothic", 18, FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(130,130,130) ;
                lbl.AutoSize = true;

                ListaItems.Controls.Add(lbl);
            }
            else
            {
                lblIndex = 20;
                foreach (key_item item in keyList)
                {
                    itemGenerate(item);
                }
            }
        }

        //*************************************************************** ITEM GENERATE
        private void itemGenerate(key_item item)
        {
            const int left = 45;

            PictureBox line = new PictureBox();
            line.BackColor = Color.FromArgb(60, 60, 60);
            line.Dock = DockStyle.Bottom;
            line.Height = 1;

            PictureBox ico = new PictureBox();
            ico.Size = new Size(32,32);
            ico.Location = new Point(9,7);
            ico.BackgroundImageLayout = ImageLayout.Stretch;
            
            try
            {
                ico.BackgroundImage = Image.FromFile(ICONS_FOLDER_PATH + item.Service + ".png");
            }
            catch (Exception)
            {
                ico.BackgroundImage = Resources.Internet;
            }

            Label lbl = new Label();
            lbl.Text = item.Service;
            lbl.Location = new Point(left, 5);
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Regular);
            lbl.ForeColor = Color.Black;
            lbl.AutoSize = true;

            Label lbl0 = new Label();
            lbl0.Text = item.optional;
            lbl0.Location = new Point(left + 3, 25);
            lbl0.Font = new Font("Century Gothic", 7, FontStyle.Regular);
            lbl0.ForeColor = Color.DimGray;
            lbl0.AutoSize = true;

            Panel contenedor = new Panel();
            contenedor.Width = ListaItems.Width + 4;
            contenedor.Height = 46;
            contenedor.Location = new Point(-2, lblIndex);
            contenedor.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            contenedor.BackColor = Color.FromArgb(224,224,224);



            CheckBox seleccionado = new CheckBox();
            seleccionado.Checked = false;
            seleccionado.Location = new Point(contenedor.Width - 45, 15);
            seleccionado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            seleccionado.CheckedChanged += (aa, bb) => {

                if (seleccionado.Checked)
                {
                    button2.Enabled = true;
                }
                else
                {
                    bool ena = false;

                    foreach (Panel item in ListaItems.Controls)
                    {
                        foreach (var ch in item.Controls.OfType<CheckBox>())
                        {
                            if (ch.Checked)
                            {
                                ena = true;
                            }
                        }
                    }
                    button2.Enabled = ena;
                }
            };

            contenedor.MouseDown += (aa, bb) => { contenedor.BackColor = Color.Lime; };
            contenedor.MouseUp += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            contenedor.MouseEnter += (aa, bb) => { contenedor.BackColor = Color.FromArgb(200, 200, 200); };
            contenedor.MouseLeave += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            contenedor.MouseClick += (aa, bb) => { copyKey(item.optional, item.Service); };

            lbl.MouseDown += (aa, bb) => { contenedor.BackColor = Color.Lime; };
            lbl.MouseUp += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            lbl.MouseEnter += (aa, bb) => { contenedor.BackColor = Color.FromArgb(200, 200, 200); };
            lbl.MouseLeave += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            lbl.MouseClick += (aa, bb) => { copyKey(item.optional, item.Service); };

            lbl0.MouseDown += (aa, bb) => { contenedor.BackColor = Color.Cyan; };
            lbl0.MouseUp += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            lbl0.MouseEnter += (aa, bb) => { contenedor.BackColor = Color.FromArgb(200, 200, 200); };
            lbl0.MouseLeave += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            lbl0.MouseClick += (aa, bb) => { Clipboard.SetText(item.optional); estado.Text = "Descripción copiada!"; timer2.Enabled = true; };

            ico.MouseDown += (aa, bb) => { contenedor.BackColor = Color.Lime; };
            ico.MouseUp += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            ico.MouseEnter += (aa, bb) => { contenedor.BackColor = Color.FromArgb(200, 200, 200); };
            ico.MouseLeave += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };
            ico.MouseClick += (aa, bb) => { copyKey(item.optional, item.Service); };

            seleccionado.MouseEnter += (aa, bb) => { contenedor.BackColor = Color.FromArgb(200, 200, 200); };
            seleccionado.MouseLeave += (aa, bb) => { contenedor.BackColor = Color.FromArgb(224, 224, 224); };

            lblIndex += 48;
            contenedor.Controls.Add(lbl0);
            contenedor.Controls.Add(lbl);
            contenedor.Controls.Add(ico);
            contenedor.Controls.Add(line);
            contenedor.Controls.Add(seleccionado);
            ListaItems.Controls.Add(contenedor);
        }

        private void copyKey(string description, string service)
        {
            tmpTime = (trackBar2.Value * 5) * 1000;
            
            if (exit < tmpTime/1000)
            {
                exit = trackBar2.Value * 5 + 1;
            }
            
            timer2.Enabled = true;
            Clipboard.SetText(thisUser.getKey(service,description, thisUser.getPasOnRunTime()));
            estado.Text = "clave copiada";
        }

        private void generarClave_Click(object sender, EventArgs e)
        {
            KeyUtils keyUtils = new KeyUtils();

            itemClave.Text = keyUtils.generateKey(trackBar1.Value, chLetras.Checked, chNumeros.Checked, chSignos.Checked, chOperadores.Checked);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            claveLenthbuttonText();
        }

        private void claveLenthbuttonText()
        {
            generarClave.Text = "Generar clave(" + trackBar1.Value + ")";
        }

        //***************************************************** AÑADIR NUEVO ITEM
        private void Nuevo_Click(object sender, EventArgs e)
        {
            LogOut.Enabled = !LogOut.Enabled;

            claveLenthbuttonText();

            max = Lista.Width - 1;

            if (Nuevo.Text == "Nuevo")
            {
                Nuevo.Text = "Cancelar";
            }
            else
            {
                Nuevo.Text = "Nuevo";
            }

            timer1.Enabled = true;
        }

        int max = 0;
        int animaterTMP = 1;
        bool abiertoPanelLateral = false;
        private void animationLateralPanel()
        {
            if (!abiertoPanelLateral)
            {
                if ((LateralPanel.Width += animaterTMP) < max)
                {
                    LateralPanel.Width += animaterTMP;
                    animaterTMP *= 2;
                }
                else
                {
                    LateralPanel.Width = max;
                    timer1.Enabled = false;
                    animaterTMP = 1;
                    abiertoPanelLateral = true;
                }
            }
            else
            {
                if ((LateralPanel.Width -= animaterTMP) > 0)
                {
                    LateralPanel.Width -= animaterTMP;
                    animaterTMP *= 2;
                }
                else
                {
                    LateralPanel.Width = 0;
                    timer1.Enabled = false;
                    animaterTMP = 1;
                    abiertoPanelLateral = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            animationLateralPanel();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (abiertoPanelLateral)
            {
                LateralPanel.Width = Lista.Width - 1;
            }
        }

        private void AñadirNuevoItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (itemClave.Text != "")
                {
                    if(itemDescripcion.Text != "")
                    {
                        key_item item = new key_item();

                        item.Service = comboBox1.Text;
                        item.Key = itemClave.Text;
                        item.optional = itemDescripcion.Text;

                        if (thisUser.addKeyItem(item, thisUser.getPasOnRunTime()))
                        {
                            comboBox1.Text = "";
                            itemClave.Text = "";
                            itemDescripcion.Text = "";
                            trackBar1.Value = 16;

                            max = Lista.Width - 1;
                            timer1.Enabled = true;

                            Nuevo.Text = "Nuevo";

                            cargarKeys();

                            LogOut.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Error al ejecutar la operacion", "Error");
                            itemDescripcion.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe agregar una descripción", "Error");
                        itemDescripcion.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Debe agregar una clave", "Error");
                    itemClave.Focus();
                }
            }
            else
            {
                MessageBox.Show("Debe agregar un servicio","Error");
                comboBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Panel> ls = new List<Panel>();
            List<int> index = new List<int>();

            int i = 0;

            foreach (Panel item in ListaItems.Controls)
            {
                foreach (var ch in item.Controls.OfType<CheckBox>())
                {
                    if (ch.Checked)
                    {
                        ls.Add(item);
                        index.Add(i);
                    }

                    i++;
                }
            }

            foreach (var item in ls)
            {
                ListaItems.Controls.Remove(item);
            }

            int ajuste = 0;

            foreach (int x in index)
            {
                if (thisUser.removeKeyItemAt(x - ajuste, thisUser.getPasOnRunTime()))
                {
                    ajuste++;
                    MessageBox.Show("Se borró el item!","Exito");
                }
                else
                {
                    MessageBox.Show("error al guardar", "Error");
                }
            }
            cargarKeys();

            button2.Enabled = false;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            tmpTime = (trackBar2.Value * 5)*1000;
            tiempo.Text = tmpTime/1000 + "s";
        }

        int tmpTime = 10000;
        private void timer2_Tick(object sender, EventArgs e)
        {
            tmpTime -= 100;
            progressBar1.Value = tmpTime / (((trackBar2.Value*5)*1000)/100);
            tiempo.Text = tmpTime/1000 + "s";

            if (tmpTime <= 0)
            {
                tmpTime = 0;
                Clipboard.Clear();
                estado.Text = "Vacio";
                progressBar1.Value = 100;
                tmpTime = (trackBar2.Value * 5)*1000;
                tiempo.Text = tmpTime/1000 + "s";
                timer2.Enabled = false;
            }
        }

        int exit = 60;
        private void LogOut_Tick(object sender, EventArgs e)
        {
            if (exit < 10)
            {
                label20.Text = "0" + exit;
            }
            else
            {
                label20.Text = exit.ToString();

            }

            exit--;

            if (exit <= 0)
            {
                label20.Text = "60";
                exit = 60;
                goToAccederPage();
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            exit = 60;
            label20.Text = "60";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ruta.Text = openFileDialog1.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox3.BackgroundImage = Image.FromFile("icons/" + comboBox1.Text + ".png");
            }
            catch (Exception)
            {
                pictureBox3.BackgroundImage = Resources.Internet;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox3.BackgroundImage = Image.FromFile("icons/" + comboBox1.Text + ".png");
            }
            catch (Exception)
            {
                pictureBox3.BackgroundImage = Resources.Internet;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void togleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleHIDE();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            setStartWidthWindows(startWincheckBox.Checked);
        }

        private void setStartWidthWindows(bool enable)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enable)
            {
                reg.SetValue("Boveda", Application.ExecutablePath.ToString());
                File.WriteAllText(RUN_ON_STARTUP_STATE, "");
            }
            else
            {
                try
                {
                    reg.DeleteValue("Boveda");
                    File.Delete(RUN_ON_STARTUP_STATE);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error reg.DeleteValue", "Error");
                    return;
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (startMincheckBox.Checked)
            {
                File.WriteAllText(START_MINIMIZED_FILE_PATH, "OK");
            }
            else
            {
                File.Delete(START_MINIMIZED_FILE_PATH);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            toggleHIDE();
            goToAccederPage();
        }

        private void BovedaIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            toggleHIDE();
        }
    }
}
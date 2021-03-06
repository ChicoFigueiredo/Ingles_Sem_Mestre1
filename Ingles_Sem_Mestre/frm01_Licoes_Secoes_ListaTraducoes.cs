﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using Ingles_Sem_Mestre.Utilitarios;
using System.Globalization;

namespace Ingles_Sem_Mestre
{
    public partial class frm01_Licoes_Secoes_ListaTraducoes : Form
    {

        private Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = NativeMethods.GetFocus();
            if (focusedHandle != IntPtr.Zero)
                // Note that if the focused Control is not a .Net control, then this will return null.
                focusedControl = Control.FromHandle(focusedHandle);
            return focusedControl;
        }

        public frm01_Licoes_Secoes_ListaTraducoes()
        {
            InitializeComponent();
        }

        private void licoesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.licoesBindingSource.EndEdit();
            this.secaoBindingSource.EndEdit();
            this.lista_de_TraducoesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.iNGLES_SEM_MESTREDataSet);

        }

        private CultureInfo pt_BR = CultureInfo.GetCultureInfo("pt-BR");
        private CultureInfo en_US = CultureInfo.GetCultureInfo("en-US");

        private void frm01_Licoes_Secoes_ListaTraducoes_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'iNGLES_SEM_MESTREDataSet.Licoes'. Você pode movê-la ou removê-la conforme necessário.
            this.licoesTableAdapter.Fill(this.iNGLES_SEM_MESTREDataSet.Licoes);
            // TODO: esta linha de código carrega dados na tabela 'iNGLES_SEM_MESTREDataSet.Secao'. Você pode movê-la ou removê-la conforme necessário.
            this.secaoTableAdapter.Fill(this.iNGLES_SEM_MESTREDataSet.Secao);
            // TODO: esta linha de código carrega dados na tabela 'iNGLES_SEM_MESTREDataSet.Lista_de_Traducoes'. Você pode movê-la ou removê-la conforme necessário.
            this.lista_de_TraducoesTableAdapter.Fill(this.iNGLES_SEM_MESTREDataSet.Lista_de_Traducoes);

            SpeechSynthesizer reader = new SpeechSynthesizer();
            toolStripComboBox_Vozes.Items.AddRange(reader.GetInstalledVoices().Select(s => s.VoiceInfo.Name).ToArray<string>());
            toolStripComboBox_Vozes.Text = reader.GetInstalledVoices().Where(w => w.VoiceInfo.Culture.Name == "en-US").Select(s => s.VoiceInfo.Name).FirstOrDefault().ToString();
            toolStripRateVoice.Text = Properties.Settings.Default.Velocidade_Padrão;
            toolStripRateVoiceSlow.Text = Properties.Settings.Default.Velocidade_Padrão_Lerdo;

            toolStripVozPortugues.Items.AddRange(reader.GetInstalledVoices().Select(s => s.VoiceInfo.Name).ToArray<string>());
            toolStripVozPortugues.Text = reader.GetInstalledVoices().Where(w => w.VoiceInfo.Culture.Name == "pt-BR").Select(s => s.VoiceInfo.Name).FirstOrDefault().ToString();
            toolStripVelocidadeInglesPassivo.Text = Properties.Settings.Default.Velocidade_Padrão;
            toolStripVelocidadePortugues.Text = Properties.Settings.Default.Velocidade_Padrão;

        }

        private void btnSalvar_Alteracoes_Click(object sender, EventArgs e)
        {
            licoesBindingNavigatorSaveItem_Click(sender, e);
        }

        private void frm01_Licoes_Secoes_ListaTraducoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                licoesDataGridView.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                tabControl_Licao.SelectedIndex = 0;
                secaoDataGridView.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                tabControl_Licao.SelectedIndex = 1;
                materia_PortuguesRichTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F7)
            {
                tabControl_Licao.SelectedIndex = 0;
                tabControl_Secao.SelectedIndex = 0;
                lista_de_TraducoesDataGridView.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F8)
            {
                tabControl_Licao.SelectedIndex = 0;
                tabControl_Secao.SelectedIndex = 1;
                rtfMateria.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F9)
            {
                tabControl_Licao.SelectedIndex = 0;
                tabControl_Secao.SelectedIndex = 0;
                tabControl_Lista_Traducao.SelectedIndex = 0;
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F10)
            {
                tabControl_Licao.SelectedIndex = 0;
                tabControl_Secao.SelectedIndex = 0;
                tabControl_Lista_Traducao.SelectedIndex = 1;
                materia_PortuguesRichTextBox2.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F12 || (e.KeyCode == Keys.S && e.Control == true))
            {
                licoesBindingNavigatorSaveItem_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F11 || ((e.KeyCode == Keys.Add || e.KeyCode == Keys.Insert) && e.Control == true))
            {
                licoesBindingNavigatorSaveItem_Click(sender, e);
                lista_de_TraducoesBindingSource.AddNew();
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if ((/*e.KeyCode == Keys.Left ||*/ e.KeyCode == Keys.Up) && e.Control == true)
            {
                lista_de_TraducoesBindingSource.MovePrevious();
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if ((/*e.KeyCode == Keys.Right ||*/ e.KeyCode == Keys.Down) && e.Control == true)
            {
                lista_de_TraducoesBindingSource.MoveNext();
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageUp && e.Control == true)
            {
                lista_de_TraducoesBindingSource.MoveFirst();
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageDown && e.Control == true)
            {
                lista_de_TraducoesBindingSource.MoveLast();
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.End && e.Control == true)
            {
                licoesBindingSource.MoveLast();
                licoesBindingSource.MovePrevious();
                secaoBindingSource.MoveLast();
                secaoBindingSource.MovePrevious();
                lista_de_TraducoesBindingSource.MoveLast();
                lista_de_TraducoesBindingSource.MovePrevious();
                inglesTextBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3 || (e.KeyCode == Keys.Divide && e.Control == true))
            {
                Ler_Ingles(inglesTextBox.Text, e.Shift ? toolStripRateVoiceSlow.Text : toolStripRateVoice.Text);
                e.Handled = true;
            }
            else if ((e.KeyCode == Keys.F && e.Control == true) || (e.KeyCode == Keys.Multiply && e.Control == true))
            {
                Control C = GetFocusedControl();
                try
                {
                    Ler_Ingles(inglesTextBox.Text, e.Shift ? toolStripRateVoiceSlow.Text : toolStripRateVoice.Text);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.Message);
                }
                e.Handled = true;
            }
        }

        private void Ler_Ingles(string texto, string velocity = "",bool Async = true)
        {
            velocity = velocity == "" ? toolStripRateVoice.Text : velocity;
            SpeechSynthesizer reader = new SpeechSynthesizer();
            reader.SelectVoice(toolStripComboBox_Vozes.Text);
            reader.Rate = int.Parse(velocity);
            reader.Volume = 100;
            if (Async) { reader.SpeakAsync(texto); } else { reader.Speak(texto); };
        }

        private void Ler_Portugues(string texto, string velocity = "", bool Async = true)
        {
            velocity = velocity == "" ? toolStripVelocidadePortugues.Text : velocity;
            SpeechSynthesizer reader = new SpeechSynthesizer();
            reader.SelectVoice(toolStripVozPortugues.Text);
            reader.Rate = int.Parse(velocity);
            reader.Volume = 100;
            if (Async) { reader.SpeakAsync(texto); } else { reader.Speak(texto); };
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            licoesBindingNavigatorSaveItem_Click(sender, e);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            licoesBindingNavigatorSaveItem_Click(sender, e);
            Application.Exit();
        }

        private Captura_Linguagem_Fonetica_PHOTRANSEDIT FoneticoPHOTRANSEDIT = new Captura_Linguagem_Fonetica_PHOTRANSEDIT();
        private Captura_Traducao_GOOGLE TradutorGOOGLE = new Captura_Traducao_GOOGLE();

        private void inglesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Control == true)
            {
                try
                {
                    Ler_Ingles(inglesTextBox.Text, e.Shift ? toolStripRateVoiceSlow.Text : toolStripRateVoice.Text);

                    traducaoTextBox.Text = TradutorGOOGLE.Get_Traducao(inglesTextBox.Text);
                    foneticoTextBox.Text = FoneticoPHOTRANSEDIT.Get_Fonetico(inglesTextBox.Text);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
                catch (Exception Err)
                {
                    try
                    {
                        foneticoTextBox.Text = FoneticoPHOTRANSEDIT.Get_Fonetico(inglesTextBox.Text);
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                    }
                    catch (Exception Err2)
                    {
                        foneticoTextBox.Text = "<<" + Err.Message + " at " + Err.Source + " / " + Err2.Message + " at " + Err2.Source + ">>";
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                    }
                }
                
            } else if(e.KeyCode == Keys.F && e.Control == true)
            {
                e.Handled = true;
            }
        }
        private void inglesTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBusca_Licoes_Leave(object sender, EventArgs e)
        {
            Int32 a = -1;
            if (txtBusca_Licoes.Text.Trim() == "")
            {
                licoesBindingSource.RemoveFilter();
            }
            else if (txtBusca_Licoes.Text.Trim().ToUpper().Substring(0, 1) == "G" && Int32.TryParse(txtBusca_Licoes.Text.Trim().ToUpper().Substring(1),out a) == true)
            {
                licoesBindingSource.Filter = "Grupo = " + txtBusca_Licoes.Text.Trim().ToUpper().Substring(1);

            }
            else if (Int32.TryParse(txtBusca_Licoes.Text.Trim(),out a) == true)
            {
                licoesBindingSource.Filter = "Numero = " + txtBusca_Licoes.Text.Trim();

            }
            else
            {
                String Completa = "";
                licoesBindingSource.Filter = "";
                String[] palavas_chaves = txtBusca_Licoes.Text.Trim().Split();
                foreach (string w in palavas_chaves)
                {
                    licoesBindingSource.Filter = licoesBindingSource.Filter + Completa + "Titulo like \'%" + w + "%\'";
                    if (Completa == "")
                    {
                        Completa = " and ";
                    }
                }
                
            };
            LicoesTimer.Enabled = false;
        }

        private void LicoesTimer_Tick(object sender, EventArgs e)
        {
            txtBusca_Licoes_Leave(sender, e);
            LicoesTimer.Enabled = false;
            LicoesTimer.Stop();
        }

        private void txtBusca_Licoes_KeyUp(object sender, KeyEventArgs e)
        {
            LicoesTimer.Enabled = true;
            LicoesTimer.Stop();
            LicoesTimer.Start();
        }

        private void secaoDataGridView_Leave(object sender, EventArgs e)
        {
            licoesBindingNavigatorSaveItem_Click(sender, e);
        }

        private void licoesDataGridView_Leave(object sender, EventArgs e)
        {
            licoesBindingNavigatorSaveItem_Click(sender, e);
        }

        private void txtBusca_Secoes_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void toolStripButton_Backup_Click(object sender, EventArgs e)
        {
            SQL_Utilities.Backup();
        }

        private void toolStripRateVoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripRateVoice_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLessonSearch_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void toolStripLessonSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timerTraducoes_Tick(sender, e);
            } else
            {
                timerTraducoes.Enabled = true;
                timerTraducoes.Stop();
                timerTraducoes.Start();
            }
        }

        private void timerTraducoes_Tick(object sender, EventArgs e)
        {
            toolStripLessonSearch_Leave(sender, e);
            timerTraducoes.Enabled = false;
            timerTraducoes.Stop();
        }

        private void toolStripLessonSearch_Leave(object sender, EventArgs e)
        {

            Int32 a = -1;
            if (toolStripLessonSearch.Text.Trim() == "")
            {
                lista_de_TraducoesBindingSource.RemoveFilter();
            }
            else
            {
                String Completa = "";
                lista_de_TraducoesBindingSource.Filter = "";
                String[] palavas_chaves = toolStripLessonSearch.Text.Trim().Split();
                foreach (string w in palavas_chaves)
                {
                    lista_de_TraducoesBindingSource.Filter = lista_de_TraducoesBindingSource.Filter + Completa + "((Ingles like \'%" + w + "%\') or (Traducao like \'%" + w + "%\'))";
                    if (Completa == "")
                    {
                        Completa = " and ";
                    }
                }

            };
            timerTraducoes.Enabled = false;
        }

        private void lista_de_TraducoesBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonLerLista_Click(object sender, EventArgs e)
        {
            bool acabou = false;
            lista_de_TraducoesBindingSource.MoveFirst();
            do
            {
                Ler_Ingles(inglesTextBox.Text, toolStripVelocidadeInglesPassivo.Text,false); Application.DoEvents();
                if (toolStripLerPortugues.Text == "Ler Português") { Ler_Portugues(traducaoTextBox.Text, toolStripVelocidadePortugues.Text, false); }; Application.DoEvents();
                if (lista_de_TraducoesBindingSource.Position + 1 == lista_de_TraducoesBindingSource.Count)
                {
                    acabou = true;
                } else
                {
                    acabou = false; lista_de_TraducoesBindingSource.MoveNext();
                    Application.DoEvents();
                }
                Application.DoEvents();
            } while (!acabou);
            MessageBox.Show("Leitura passiva terminada!");
        }

        private void btnTranslateBatch_Click(object sender, EventArgs e)
        {
            String[] frases = rtfMateria.Text.ToString().Split(new char[] { '.', '?', '!', '\n' });
            Application.DoEvents();
            tabControl_Secao.SelectedIndex = 0;
            Application.DoEvents();
            while (lista_de_TraducoesBindingSource.Count > 0)
            {
                lista_de_TraducoesBindingSource.RemoveAt(0);
                Application.DoEvents();
            }
            foreach (string l in frases)
            {
                Application.DoEvents();
                if (l.Trim() != "")
                {
                    lista_de_TraducoesBindingSource.AddNew(); Application.DoEvents();
                    inglesTextBox.Text = l.Trim(); Application.DoEvents();
                    try
                    {
                        traducaoTextBox.Text = TradutorGOOGLE.Get_Traducao(inglesTextBox.Text); Application.DoEvents();
                        foneticoTextBox.Text = FoneticoPHOTRANSEDIT.Get_Fonetico(inglesTextBox.Text); Application.DoEvents();
                        Ler_Ingles(inglesTextBox.Text, toolStripRateVoice.Text,false); Application.DoEvents();
                    }
                    catch (Exception Err)
                    {
                        try
                        {
                            foneticoTextBox.Text = FoneticoPHOTRANSEDIT.Get_Fonetico(inglesTextBox.Text); Application.DoEvents();
                        }
                        catch (Exception Err2)
                        {
                            foneticoTextBox.Text = "<<" + Err.Message + " at " + Err.Source + " / " + Err2.Message + " at " + Err2.Source + ">>"; Application.DoEvents();
                        }
                    }
                }
                Application.DoEvents();
            }
        }
    }
}

/*

                    // Select a speech recognizer that supports English.
                    RecognizerInfo info = null;
                    foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
                    {
                        if (ri.Culture.TwoLetterISOLanguageName.Equals("en"))
                        {
                            info = ri;
                            break;
                        }
                    }
                    if (info == null) return;

                    SpeechRecognitionEngine eng = new SpeechRecognitionEngine(info); //new System.Globalization.CultureInfo("en-US")
                    eng.RecognizeAsync(RecognizeMode.Single);

                    Choices sentences = new Choices(inglesTextBox.Text);
                    GrammarBuilder gBuilder = new GrammarBuilder();
                    gBuilder.Append(sentences);
                    Grammar g = new Grammar(gBuilder);

                    eng.LoadGrammar(g);
                    eng.SpeechRecognized +=
                        new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            // Create a simple handler for the SpeechRecognized event.
        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("Speech recognized: " + e.Result.Text);
        }


*/

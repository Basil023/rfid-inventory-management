using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using WindowsFormsApp1.CLSLTLSERVER20LIVEDataSetTableAdapters;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void mASTERINV_REFIDBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.mASTERINV_REFIDBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cLSLTLSERVER20LIVEDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cLSLTLSERVER20LIVEDataSet.INV_REFID' table. You can move, or remove it, as needed.
            this.iNV_REFIDTableAdapter.Fill(this.cLSLTLSERVER20LIVEDataSet.INV_REFID);
            // TODO: This line of code loads data into the 'cLSLTLSERVER20LIVEDataSet.MASTERINV_REFID' table. You can move, or remove it, as needed.
            this.mASTERINV_REFIDTableAdapter.Fill(this.cLSLTLSERVER20LIVEDataSet.MASTERINV_REFID);
            

            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                button2.Enabled = true;
                button3.Enabled = false;
            }
            else
            {
                button3.Enabled = true;
                button2.Enabled = false;
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
            serialPort1.Open();
            button2.Enabled = false;
            button3.Enabled = true;
            label2.Text = "Connected";
            label2.ForeColor = Color.Green;

        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            serialPort1.Close();
            button2.Enabled = true;
            button3.Enabled = false;
            label2.Text = "Disconnected";
            label2.ForeColor = Color.Red;

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Invoke(new Action(() =>
            {
               textBox1.Text = serialPort1.ReadLine().ToString().Trim();
            }));
            
                  
        }

           private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in mASTERINV_REFIDDataGridView.Rows)
            //{
            //    if (row.Cells[0].Value.ToString() == "")
            //    {
            //        mASTERINV_REFIDDataGridView.Rows.RemoveAt(row.Index);
            //    }
            //}
            try
            {
                this.mASTERINV_REFIDBindingSource.Filter = string.Format("RFID Like '%{0}%'", textBox1.Text.ToString().ToUpper());
                this.mASTERINV_REFIDTableAdapter.Update(this.cLSLTLSERVER20LIVEDataSet.MASTERINV_REFID);
                this.mASTERINV_REFIDDataGridView.Update();
                textBox1.Text = null;
                if (this.mASTERINV_REFIDDataGridView.RowCount == 1)
                {
                    this.iNV_REFIDTableAdapter.Insert(dATAAREAIDTextBox.Text, int.Parse(rECVERSIONTextBox.Text), long.Parse(rECIDTextBox.Text), iTEM_NAMETextBox.Text, rFIDTextBox.Text, uNITTextBox.Text, decimal.Parse(qTYTextBox.Text), sTORAGE_LOCATIONTextBox.Text, iTEM_CODETextBox.Text);
                    this.cLSLTLSERVER20LIVEDataSet.INV_REFID.Rows.Add(dATAAREAIDTextBox.Text, int.Parse(rECVERSIONTextBox.Text), long.Parse(rECIDTextBox.Text), iTEM_NAMETextBox.Text, rFIDTextBox.Text, uNITTextBox.Text, decimal.Parse(qTYTextBox.Text), sTORAGE_LOCATIONTextBox.Text, iTEM_CODETextBox.Text);
                    this.cLSLTLSERVER20LIVEDataSet.AcceptChanges();
                    this.iNV_REFIDBindingSource.EndEdit();
                    this.iNV_REFIDTableAdapter.Update(this.cLSLTLSERVER20LIVEDataSet.INV_REFID);
                }
                else if (this.mASTERINV_REFIDDataGridView.RowCount == 0)
                {
                    this.mASTERINV_REFIDBindingSource.AddNew();
                    rFIDTextBox.Text = textBox1.Text;
                    dATAAREAIDTextBox.Text = "SLT";
                    rECIDTextBox.Text = "123456";
                    //rECVERSIONTextBox.Text = (int.Parse(this.cLSLTLSERVER20LIVEDataSet.MASTERINV_REFID.Rows[this.cLSLTLSERVER20LIVEDataSet.MASTERINV_REFID.Rows.Count - 1][1].ToString()) + 1).ToString();
                }
            }
            catch
            {
                this.mASTERINV_REFIDBindingSource.Filter = string.Empty;
                this.mASTERINV_REFIDTableAdapter.Update(this.cLSLTLSERVER20LIVEDataSet.MASTERINV_REFID);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.iNV_REFIDTableAdapter.Fill(this.cLSLTLSERVER20LIVEDataSet.INV_REFID);
            timer1.Stop();
            timer1.Start();
            
        }

     }

}



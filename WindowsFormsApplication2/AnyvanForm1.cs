using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace Anyvan
{
    public partial class Form1 : Form
    {
        public int i = -1;
        public string url;
        public string json;
        public RootObject myData;
        public Form1()
        {
            InitializeComponent();
            i = 0;
            RootObject myCollection = new RootObject();

            DataGridViewTextBoxColumn Column1 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Column1a = new DataGridViewTextBoxColumn();
            dataGridView1.Columns.Add(Column1);
            dataGridView2.Columns.Add(Column1a);
            DataGridViewTextBoxColumn Column2 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Column2a = new DataGridViewTextBoxColumn();
            dataGridView1.Columns.Add(Column2);
            dataGridView2.Columns.Add(Column2a);
            DataGridViewTextBoxColumn Column3 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Column3a = new DataGridViewTextBoxColumn();
            dataGridView1.Columns.Add(Column3);
            dataGridView2.Columns.Add(Column3a);
            DataGridViewLinkColumn Column4 = new DataGridViewLinkColumn();
            DataGridViewLinkColumn Column4a = new DataGridViewLinkColumn();
            dataGridView1.Columns.Add(Column4);
            dataGridView2.Columns.Add(Column4a);
            DataGridViewTextBoxColumn Column5 = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn Column5a = new DataGridViewTextBoxColumn();
            dataGridView1.Columns.Add(Column5);
            dataGridView2.Columns.Add(Column5a);

            dataGridView1.Columns[0].HeaderText = "Что";
            dataGridView1.Columns[0].Width = 350;
            dataGridView1.Columns[1].HeaderText = "Откуда";
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Куда";
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].HeaderText = "Линк";
            dataGridView1.Columns[3].Width = 260;
            dataGridView1.Columns[4].HeaderText = "Дата";
            dataGridView1.Columns[4].Width = 160;

            dataGridView2.Columns[0].HeaderText = "Что";
            dataGridView2.Columns[0].Width = 350;
            dataGridView2.Columns[1].HeaderText = "Откуда";
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[2].HeaderText = "Куда";
            dataGridView2.Columns[2].Width = 50;
            dataGridView2.Columns[3].HeaderText = "Линк";
            dataGridView2.Columns[3].Width = 260;
            dataGridView2.Columns[4].HeaderText = "Дата";
            dataGridView2.Columns[4].Width = 160;

            label6.Text = "";
            label1.Text = "";
            linkLabel1.Text = "";
            label9.Text = "";
            label10.Text = "";
            label12.Text = "";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            if (i > myData.row_count) i = myData.row_count;
            richTextBox2.Text = "----";
            string an_descr = myData.render[i].listing_label;
            string an_from = myData.render[i].listing_pickup_address.postcode;
            string an_to = myData.render[i].listing_delivery_address.postcode;

            richTextBox2.Text = an_descr + " ";
            richTextBox3.Text = " " + an_from;
            richTextBox4.Text = " " + an_to;
            label1.Text = i + ":";
            url = "https://anyvan.com/view-listing/" + myData.render[i].id;
            linkLabel1.Text = url;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(url);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i--;
            if (i < 0) i = 0;
            string an_descr = myData.render[i].listing_label;
            string an_from = myData.render[i].listing_pickup_address.postcode;
            string an_to = myData.render[i].listing_delivery_address.postcode;

            richTextBox2.Text = an_descr + " ";
            richTextBox3.Text = " " + an_from;
            richTextBox4.Text = " " + an_to;
            label1.Text = i + ":";
            url = "https://anyvan.com/view-listing/" + myData.render[i].id;
            linkLabel1.Text = url;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            json = richTextBox1.Text;
            myData = JsonConvert.DeserializeObject<RootObject>(json);

            string an_descr = myData.render[0].listing_label;
            string an_from = myData.render[0].listing_pickup_address.postcode;
            string an_to = myData.render[0].listing_delivery_address.postcode;

            richTextBox2.Text = an_descr + " ";
            richTextBox3.Text = " " + an_from;
            richTextBox4.Text = " " + an_to;
            label1.Text = 0 + ":";
            url = "https://anyvan.com/view-listing/" + myData.render[0].id;
            linkLabel1.Text = url;
            label6.Text = myData.row_count + " ";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string ssylka1 = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            System.Diagnostics.Process.Start(ssylka1);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string ssylka2 = dataGridView2[3, dataGridView1.CurrentRow.Index].Value.ToString();
            System.Diagnostics.Process.Start(ssylka2);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            NetworkCredential nc = new NetworkCredential("lithuanica.transport@gmail.com", "pelmen0202");
            CredentialCache cc = new CredentialCache();
            cc.Add("www.anyvan.com", 443, "Basic", nc);


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.anyvan.com");
            request.Credentials = cc;
            request.PreAuthenticate = true;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";

            request.KeepAlive = true;


            // fill in other request properties here, like content
            HttpWebResponse req = request.GetResponse() as HttpWebResponse;

            HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create("https://www.anyvan.com/search/mk18");
            proxy_request.Method = "GET";
            proxy_request.ContentType = "application/x-www-form-urlencoded";
            proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            proxy_request.KeepAlive = true;

            HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
            string html = "";
            using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
                html = sr.ReadToEnd();


            WebBrowser wb = new WebBrowser();
            wb.Navigate("https://www.anyvan.com/");
            object tmpa = wb.Document.GetElementsByTagName("email");

        }

        private void SaveOK_Click(object sender, EventArgs e)
        {
            string file = "Anyvan_stat_OK-" + richTextBoxPage.Text.Trim() + ".html";
            string header_stat = "<H3>Total: " + label9.Text + "</H3><br>";

            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            // Select all the cells
            dataGridView1.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            File.WriteAllText(file,header_stat);
            File.AppendAllText(file, Clipboard.GetText(TextDataFormat.UnicodeText));
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int all_ok = 0;
            int all_not_ok = 0;
            int otbrosili = 0;

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            for (int x = 0; x < myData.row_count; x++)
            {
                string postcodeA = myData.render[x].listing_pickup_address.postcode;
                string postcodeB = myData.render[x].listing_delivery_address.postcode;
                string pickup_date = myData.render[x].listing_pickup_date;
                string platno = "";

                //Платные дороги

                if (postcodeA.StartsWith("EC") ||
                    postcodeA == "N1" ||
                    postcodeA == "SE1" ||
                    postcodeA == "SE11" ||
                    postcodeA == "SW1A" ||
                    postcodeA == "SW1E" ||
                    postcodeA == "SW1H" ||
                    postcodeA == "SW1Y" ||
                    postcodeA == "SW1P" ||
                    postcodeA == "SW1X" ||
                    postcodeA == "SW1W" ||
                    postcodeA == "SW1V" ||
                    postcodeA == "W1A" ||
                    postcodeA == "W1B" ||
                    postcodeA == "W1C" ||
                    postcodeA == "W1D" ||
                    postcodeA == "W1F" ||
                    postcodeA == "W1G" ||
                    postcodeA == "W1H" ||
                    postcodeA == "W1J" ||
                    postcodeA == "W1K" ||
                    postcodeA == "W1S" ||
                    postcodeA == "W1T" ||
                    postcodeA == "W1U" ||
                    postcodeA == "W1W" ||
                    postcodeA.StartsWith("WC1") ||
                    postcodeA.StartsWith("WC2")
                    ) platno = "--- PLATNO!!! --- ";

                if (postcodeB.StartsWith("EC1") ||
                    postcodeB == "N1" ||
                    postcodeB == "SE1" ||
                    postcodeB == "SE11" ||
                    postcodeB == "SW1A" ||
                    postcodeB == "SW1E" ||
                    postcodeB == "SW1H" ||
                    postcodeB == "SW1Y" ||
                    postcodeB == "SW1P" ||
                    postcodeB == "SW1X" ||
                    postcodeB == "SW1W" ||
                    postcodeB == "SW1V" ||
                    postcodeB == "W1A" ||
                    postcodeB == "W1B" ||
                    postcodeB == "W1C" ||
                    postcodeB == "W1D" ||
                    postcodeB == "W1F" ||
                    postcodeB == "W1G" ||
                    postcodeB == "W1H" ||
                    postcodeB == "W1J" ||
                    postcodeB == "W1K" ||
                    postcodeB == "W1S" ||
                    postcodeB == "W1T" ||
                    postcodeB == "W1U" ||
                    postcodeB == "W1W" ||
                    postcodeB.StartsWith("WC1") ||
                    postcodeB.StartsWith("WC2")
                    ) platno = "--- PLATNO!!! --- ";


                // Пометка что берем часть посткода

                if (postcodeA == "BA11" ||
                    postcodeA == "SN8" ||
                    postcodeA == "SN9" ||
                    postcodeA == "SN10" ||
                    postcodeA.StartsWith("LE") ||
                    postcodeA == "DY14" ||
                    postcodeA == "WR6" ||
                    postcodeA.StartsWith("NN")
                    ) platno = "--- SMOTRI KARTU! --- ";

                if (postcodeB == "BA11" ||
                    postcodeB == "SN8" ||
                    postcodeB == "SN9" ||
                    postcodeB == "SN10" ||
                    postcodeB.StartsWith("LE") ||
                    postcodeB == "DY14" ||
                    postcodeB == "WR6" ||
                    postcodeB.StartsWith("NN")
                    ) platno = "--- SMOTRI KARTU! --- ";

// Частично не берем

                if (postcodeA == "BS26" ||
                    postcodeA == "BS27" ||
                    postcodeA == "BS28" ||
                    postcodeA == "RG21" ||
                    postcodeA == "RG22" ||
                    postcodeA == "RG23" ||
                    postcodeA == "RG24" ||
                    postcodeA == "RG25" ||
                    postcodeA == "RG26" ||
                    postcodeA == "RG27" ||
                    postcodeA == "RG28" ||
                    postcodeA == "RG29" ||
                    postcodeA == "DY14" ||
                    postcodeA == "WR15"
                    )
                {
                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                    all_not_ok++;
                    otbrosili++;
                    continue;
                }

                if (postcodeB == "BS26" ||
                postcodeB == "BS27" ||
                postcodeB == "BS28" ||
                postcodeB == "RG21" ||
                postcodeB == "RG22" ||
                postcodeB == "RG23" ||
                postcodeB == "RG24" ||
                postcodeB == "RG25" ||
                postcodeB == "RG26" ||
                postcodeB == "RG27" ||
                postcodeB == "RG28" ||
                postcodeB == "RG29" ||
                postcodeB == "DY14" ||
                postcodeB == "WR15"
                )
                {
                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                    all_not_ok++;
                    otbrosili++;
                    continue;
                }


                // Основная проверка посткодов

                if (postcodeA.Length > 4 ||
                    postcodeA == "" ||
                    postcodeA.StartsWith("BR") ||
                    postcodeA == "CM16" ||
                    postcodeA.StartsWith("CR") ||
                    postcodeA == "DA1" ||
                    postcodeA == "DA5" ||
                    postcodeA == "DA6" ||
                    postcodeA == "DA8" ||
                    postcodeA == "DA9" ||
                    postcodeA == "DA10" ||
                    postcodeA == "DA14" ||
                    postcodeA == "DA15" ||
                    postcodeA == "DA16" ||
                    postcodeA == "DA17" ||
                    postcodeA.StartsWith("E1") ||
                    postcodeA.StartsWith("E2") ||
                    postcodeA.StartsWith("E3") ||
                    postcodeA.StartsWith("E4") ||
                    postcodeA.StartsWith("E5") ||
                    postcodeA.StartsWith("E6") ||
                    postcodeA.StartsWith("E7") ||
                    postcodeA.StartsWith("E8") ||
                    postcodeA.StartsWith("E9") ||
                    postcodeA.StartsWith("EC") ||
                    postcodeA == "EN1" ||
                    postcodeA == "EN2" ||
                    postcodeA == "EN3" ||
                    postcodeA == "EN4" ||
                    postcodeA == "EN5" ||
                    postcodeA.StartsWith("HA") ||
                    postcodeA.StartsWith("IG") ||
                    postcodeA.StartsWith("KT") ||
                    postcodeA.StartsWith("N1") || //N1 - платная, N2-N22
                    postcodeA.StartsWith("N2") ||
                    postcodeA.StartsWith("N3") ||
                    postcodeA.StartsWith("N4") ||
                    postcodeA.StartsWith("N5") ||
                    postcodeA.StartsWith("N6") ||
                    postcodeA.StartsWith("N7") ||
                    postcodeA.StartsWith("N8") ||
                    postcodeA.StartsWith("N9") ||
                    postcodeA.StartsWith("NW") ||
                    postcodeA == "RH1" ||
                    postcodeA == "RH2" ||
                    postcodeA == "RH8" ||
                    postcodeA == "RH9" ||
                    postcodeA == "RM1" ||
                    postcodeA == "RM2" ||
                    postcodeA == "RM3" ||
                    postcodeA == "RM4" ||
                    postcodeA == "RM5" ||
                    postcodeA == "RM6" ||
                    postcodeA == "RM7" ||
                    postcodeA == "RM8" ||
                    postcodeA == "RM9" ||
                    postcodeA == "RM10" ||
                    postcodeA == "RM12" ||
                    postcodeA == "RM13" ||
                    postcodeA == "RM14" ||
                    postcodeA.StartsWith("SE") ||
                    postcodeA == "SL9" ||
                    postcodeA.StartsWith("SM") ||
                    postcodeA.StartsWith("SW") ||
                    postcodeA == "TN13" ||
                    postcodeA == "TN14" ||
                    postcodeA == "TN15" ||
                    postcodeA == "TN16" ||
                    postcodeA.StartsWith("TW") ||
                    postcodeA.StartsWith("UB") ||
                    postcodeA.StartsWith("W1") ||
                    postcodeA.StartsWith("W2") ||
                    postcodeA.StartsWith("W3") ||
                    postcodeA.StartsWith("W4") ||
                    postcodeA.StartsWith("W5") ||
                    postcodeA.StartsWith("W6") ||
                    postcodeA.StartsWith("W7") ||
                    postcodeA.StartsWith("W8") ||
                    postcodeA.StartsWith("W9") ||
                    postcodeA.StartsWith("WC") ||
                    postcodeA.StartsWith("WD") ||
                    postcodeA.StartsWith("AL") ||
                    postcodeA.StartsWith("B1") ||
                    postcodeA.StartsWith("B2") ||
                    postcodeA.StartsWith("B3") ||
                    postcodeA.StartsWith("B4") ||
                    postcodeA.StartsWith("B5") ||
                    postcodeA.StartsWith("B6") ||
                    postcodeA.StartsWith("B7") ||
                    postcodeA.StartsWith("B8") ||
                    postcodeA.StartsWith("B9") ||
                    postcodeA.StartsWith("CV") ||
                    postcodeA.StartsWith("LU") ||
// --- BRISTOL
                    postcodeA.StartsWith("MK") ||
                    postcodeA == "BA1" ||
                    postcodeA == "BA2" ||
                    postcodeA == "BA3" ||
                    postcodeA == "BA13" ||
                    postcodeA == "BA15" ||
                    postcodeA == "BA11" ||
                    postcodeA.StartsWith("BS") ||
                    postcodeA.StartsWith("GL") ||
                    postcodeA.StartsWith("RG") ||
                    postcodeA == "SN1" ||
                    postcodeA == "SN2" ||
                    postcodeA == "SN3" ||
                    postcodeA == "SN4" ||
                    postcodeA == "SN5" ||
                    postcodeA == "SN6" ||
                    postcodeA == "SN7" ||
                    postcodeA == "SN8" ||
                    postcodeA == "SN9" ||
                    postcodeA == "SN10" ||
                    postcodeA.StartsWith("LE") ||
                    postcodeA.StartsWith("DY") ||
                    postcodeA.StartsWith("WR") ||
                    postcodeA.StartsWith("NN") ||
                    postcodeA.StartsWith("OX") ||
 
                    postcodeA.StartsWith("WS2") ||
                    postcodeA == "WS1" ||
                    postcodeA == "WS3" ||
                    postcodeA == "WS4" ||
                    postcodeA == "WS5" ||
                    postcodeA == "WS6" ||
                    postcodeA == "WS7" ||
                    postcodeA == "WS8" ||
                    postcodeA == "WS9" ||
                    postcodeA == "WS10" ||
                    postcodeA == "WS11" ||
                    postcodeA == "WS12" ||
                    postcodeA == "WS13" ||
                    postcodeA == "WS14" ||
                    postcodeA == "WS16" ||
                    postcodeA == "WS17" ||
                    postcodeA == "WS18" ||
                    postcodeA == "WS19" ||
                    postcodeA.StartsWith("WV2") ||
                    postcodeA == "WV1" ||
                    postcodeA == "WV3" ||
                    postcodeA == "WV4" ||
                    postcodeA == "WV5" ||
                    postcodeA == "WV6" ||
                    postcodeA == "WV7" ||
                    postcodeA == "WV8" ||
                    postcodeA == "WV9" ||
                    postcodeA == "WV10" ||
                    postcodeA == "WV11" ||
                    postcodeA == "WV12" ||
                    postcodeA == "WV13" ||
                    postcodeA == "WV14" ||
                    postcodeA == "WV17" ||
                    postcodeA == "WV18" ||
                    postcodeA == "WV19"
                    )
                {
                    if (postcodeB.Length > 4 ||
                    postcodeB == "" ||
                    postcodeB.StartsWith("BR") ||
                    postcodeB == "CM16" ||
                    postcodeB.StartsWith("CR") ||
                    postcodeB == "DA1" ||
                    postcodeB == "DA5" ||
                    postcodeB == "DA6" ||
                    postcodeB == "DA8" ||
                    postcodeB == "DA9" ||
                    postcodeB == "DA10" ||
                    postcodeB == "DA14" ||
                    postcodeB == "DA15" ||
                    postcodeB == "DA16" ||
                    postcodeB == "DA17" ||
                    postcodeB.StartsWith("E1") ||
                    postcodeB.StartsWith("E2") ||
                    postcodeB.StartsWith("E3") ||
                    postcodeB.StartsWith("E4") ||
                    postcodeB.StartsWith("E5") ||
                    postcodeB.StartsWith("E6") ||
                    postcodeB.StartsWith("E7") ||
                    postcodeB.StartsWith("E8") ||
                    postcodeB.StartsWith("E9") ||
                    postcodeB.StartsWith("EC") ||
                    postcodeB == "EN1" ||
                    postcodeB == "EN2" ||
                    postcodeB == "EN3" ||
                    postcodeB == "EN4" ||
                    postcodeB == "EN5" ||
                    postcodeB.StartsWith("HA") ||
                    postcodeB.StartsWith("IG") ||
                    postcodeB.StartsWith("KT") ||
                    postcodeB.StartsWith("N1") || //N1 - платная, N2-N22
                    postcodeB.StartsWith("N2") ||
                    postcodeB.StartsWith("N3") ||
                    postcodeB.StartsWith("N4") ||
                    postcodeB.StartsWith("N5") ||
                    postcodeB.StartsWith("N6") ||
                    postcodeB.StartsWith("N7") ||
                    postcodeB.StartsWith("N8") ||
                    postcodeB.StartsWith("N9") ||
                    postcodeB.StartsWith("NW") ||
                    postcodeB == "RH1" ||
                    postcodeB == "RH2" ||
                    postcodeB == "RH8" ||
                    postcodeB == "RH9" ||
                    postcodeB == "RM1" ||
                    postcodeB == "RM2" ||
                    postcodeB == "RM3" ||
                    postcodeB == "RM4" ||
                    postcodeB == "RM5" ||
                    postcodeB == "RM6" ||
                    postcodeB == "RM7" ||
                    postcodeB == "RM8" ||
                    postcodeB == "RM9" ||
                    postcodeB == "RM10" ||
                    postcodeB == "RM12" ||
                    postcodeB == "RM13" ||
                    postcodeB == "RM14" ||
                    postcodeB.StartsWith("SE") ||
                    postcodeB == "SL9" ||
                    postcodeB.StartsWith("SM") ||
                    postcodeB.StartsWith("SW") ||
                    postcodeB == "TN13" ||
                    postcodeB == "TN14" ||
                    postcodeB == "TN15" ||
                    postcodeB == "TN16" ||
                    postcodeB.StartsWith("TW") ||
                    postcodeB.StartsWith("UB") ||
                    postcodeB.StartsWith("W1") ||
                    postcodeB.StartsWith("W2") ||
                    postcodeB.StartsWith("W3") ||
                    postcodeB.StartsWith("W4") ||
                    postcodeB.StartsWith("W5") ||
                    postcodeB.StartsWith("W6") ||
                    postcodeB.StartsWith("W7") ||
                    postcodeB.StartsWith("W8") ||
                    postcodeB.StartsWith("W9") ||
                    postcodeB.StartsWith("WC") ||
                    postcodeB.StartsWith("WD") ||
                    postcodeB.StartsWith("AL") ||
                    postcodeB.StartsWith("B1") ||
                    postcodeB.StartsWith("B2") ||
                    postcodeB.StartsWith("B3") ||
                    postcodeB.StartsWith("B4") ||
                    postcodeB.StartsWith("B5") ||
                    postcodeB.StartsWith("B6") ||
                    postcodeB.StartsWith("B7") ||
                    postcodeB.StartsWith("B8") ||
                    postcodeB.StartsWith("B9") ||
                    postcodeB.StartsWith("CV") ||
                    postcodeB.StartsWith("HP") ||
                    postcodeB.StartsWith("LU") ||
// --- BRISTOL
                    postcodeB.StartsWith("MK") ||
                    postcodeB == "BA1" ||
                    postcodeB == "BA2" ||
                    postcodeB == "BA3" ||
                    postcodeB == "BA13" ||
                    postcodeB == "BA15" ||
                    postcodeB == "BA11" ||
                    postcodeB.StartsWith("BS") ||
                    postcodeB.StartsWith("GL") ||
                    postcodeB.StartsWith("RG") ||
                    postcodeB == "SN1" ||
                    postcodeB == "SN2" ||
                    postcodeB == "SN3" ||
                    postcodeB == "SN4" ||
                    postcodeB == "SN5" ||
                    postcodeB == "SN6" ||
                    postcodeB == "SN7" ||
                    postcodeB == "SN8" ||
                    postcodeB == "SN9" ||
                    postcodeB == "SN10" ||
                    postcodeB.StartsWith("LE") ||
                    postcodeB.StartsWith("DY") ||
                    postcodeB.StartsWith("WR") ||
                    postcodeB.StartsWith("NN") ||
                    postcodeB.StartsWith("OX") ||
                    postcodeB.StartsWith("WS2") ||
                    postcodeB == "WS1" ||
                    postcodeB == "WS3" ||
                    postcodeB == "WS4" ||
                    postcodeB == "WS5" ||
                    postcodeB == "WS6" ||
                    postcodeB == "WS7" ||
                    postcodeB == "WS8" ||
                    postcodeB == "WS9" ||
                    postcodeB == "WS10" ||
                    postcodeB == "WS11" ||
                    postcodeB == "WS12" ||
                    postcodeB == "WS13" ||
                    postcodeB == "WS14" ||
                    postcodeB == "WS16" ||
                    postcodeB == "WS17" ||
                    postcodeB == "WS18" ||
                    postcodeB == "WS19" ||
                    postcodeB.StartsWith("WV2") ||
                    postcodeB == "WV1" ||
                    postcodeB == "WV3" ||
                    postcodeB == "WV4" ||
                    postcodeB == "WV5" ||
                    postcodeB == "WV6" ||
                    postcodeB == "WV7" ||
                    postcodeB == "WV8" ||
                    postcodeB == "WV9" ||
                    postcodeB == "WV10" ||
                    postcodeB == "WV11" ||
                    postcodeB == "WV12" ||
                    postcodeB == "WV13" ||
                    postcodeB == "WV14" ||
                    postcodeB == "WV17" ||
                    postcodeB == "WV18" ||
                    postcodeB == "WV19"
                        )
                    {
                        // OK!
                        if (checkBox1.Checked == true || checkBox2.Checked == true)
                        {

                            // Today
                            if ((checkBox1.Checked == true && pickup_date == "Today") || (checkBox2.Checked == true && pickup_date == "Tomorrow"))
                            {
                                dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                all_not_ok++;
                            }
                            else
                            {

                                dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                all_ok++;
                            }

                        }
                        else
                        {

                            dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                            all_ok++;
                        }


                    }


        // NOT OK!
                    else
                    {
                        dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                        all_not_ok++;
                    }
                }
                else
                {
                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                    all_not_ok++;
                }




           

            }
            label9.Text = all_ok.ToString();
            label10.Text = all_not_ok.ToString();
            int all_count = 0;
            all_count = myData.row_count - all_ok - all_not_ok;
            label12.Text = (all_count).ToString();
            label14.Text = (otbrosili).ToString();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            int all_ok = 0;
            int all_not_ok = 0;

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            for (int x = 0; x < myData.row_count; x++)
            {
                string postcodeA = myData.render[x].listing_pickup_address.postcode;
                string postcodeB = myData.render[x].listing_delivery_address.postcode;
                string pickup_date = myData.render[x].listing_pickup_date;
                string platno = "";

//Платные дороги

                if (postcodeA == "EC1" ||
                    postcodeA == "EC2" ||
                    postcodeA == "EC3" ||
                    postcodeA == "EC3R" ||
                    postcodeA == "EC4" ||
                    postcodeA == "N1" ||
                    postcodeA == "SE1" ||
                    postcodeA == "SE11" ||
                    postcodeA == "SW1A" ||
                    postcodeA == "W1" ||
                    postcodeA == "W1M" ||
                    postcodeA == "WC1" ||
                    postcodeA == "WC2"
                    ) platno = "--- PLATNO! --- ";

                if (postcodeB == "EC1" ||
                    postcodeB == "EC2" ||
                    postcodeB == "EC3" ||
                    postcodeB == "EC3R" ||
                    postcodeB == "EC4" ||
                    postcodeB == "N1" ||
                    postcodeB == "SE1" ||
                    postcodeB == "SE11" ||
                    postcodeB == "SW1A" ||
                    postcodeB == "W1" ||
                    postcodeB == "W1M" ||
                    postcodeB == "WC1" ||
                    postcodeB == "WC2"
                    ) platno = "--- PLATNO! --- ";
// Пометка что берем часть посткода

                if (postcodeA == "BA11" ||
                    postcodeA == "SN8" ||
                    postcodeA == "SN9" ||
                    postcodeA == "SN10" ||
                    postcodeA.StartsWith("LE") ||
                    postcodeA == "DY14" ||
                    postcodeA == "WR6" ||
                    postcodeA.StartsWith("NN")
                    ) platno = "--- SMOTRI KARTU! --- ";

                if (postcodeB == "BA11" ||
                    postcodeB == "SN8" ||
                    postcodeB == "SN9" ||
                    postcodeB == "SN10" ||
                    postcodeB.StartsWith("LE") ||
                    postcodeB == "DY14" ||
                    postcodeB == "WR6" ||
                    postcodeB.StartsWith("NN")
                    ) platno = "--- SMOTRI KARTU! --- ";

// Частично не берем
                
                if (postcodeA == "BS26" ||
                    postcodeA == "BS27" ||
                    postcodeA == "BS28" ||
                    postcodeA == "RG21" ||
                    postcodeA == "RG22" ||
                    postcodeA == "RG23" ||
                    postcodeA == "RG24" ||
                    postcodeA == "RG25" ||
                    postcodeA == "RG26" ||
                    postcodeA == "RG27" ||
                    postcodeA == "RG28" ||
                    postcodeA == "RG29" ||
                    postcodeA == "DY14" ||
                    postcodeA == "WR15" 
                    ) 
                {
                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                    all_not_ok++;
                    continue;
                }

                    if (postcodeB == "BS26" ||
                    postcodeB == "BS27" ||
                    postcodeB == "BS28" ||
                    postcodeB == "RG21" ||
                    postcodeB == "RG22" ||
                    postcodeB == "RG23" ||
                    postcodeB == "RG24" ||
                    postcodeB == "RG25" ||
                    postcodeB == "RG26" ||
                    postcodeB == "RG27" ||
                    postcodeB == "RG28" ||
                    postcodeB == "RG29" ||
                    postcodeB == "DY14" ||
                    postcodeB == "WR15" 
                    ) 
                {
                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                    all_not_ok++;
                    continue;
                }


// Основная проверка посткодов
      
                if (postcodeA == "" ||
                    postcodeA.StartsWith("BR") ||
                    postcodeA == "CM16" ||
                    postcodeA.StartsWith("CR") ||
                    postcodeA == "DA1" ||
                    postcodeA == "DA5" ||
                    postcodeA == "DA6" ||
                    postcodeA == "DA8" ||
                    postcodeA == "DA9" ||
                    postcodeA == "DA10" ||
                    postcodeA == "DA14" ||
                    postcodeA == "DA15" ||
                    postcodeA == "DA16" ||
                    postcodeA == "DA17" ||
                    postcodeA.StartsWith("E1") ||
                    postcodeA.StartsWith("E2") ||
                    postcodeA.StartsWith("E3") ||
                    postcodeA.StartsWith("E4") ||
                    postcodeA.StartsWith("E5") ||
                    postcodeA.StartsWith("E6") ||
                    postcodeA.StartsWith("E7") ||
                    postcodeA.StartsWith("E8") ||
                    postcodeA.StartsWith("E9") ||
                    postcodeA == "EC1" ||
                    postcodeA == "EC2" ||
                    postcodeA == "EC3" ||
                    postcodeA == "EC3R" ||
                    postcodeA == "EC4" ||
                    postcodeA == "EN1" ||
                    postcodeA == "EN2" ||
                    postcodeA == "EN3" ||
                    postcodeA == "EN4" ||
                    postcodeA == "EN5" ||
                    postcodeA.StartsWith("HA") ||
                    postcodeA.StartsWith("IG") ||
                    postcodeA.StartsWith("KT") ||
                    postcodeA.StartsWith("N1") || //N1 - платная, N2-N22
                    postcodeA.StartsWith("N2") ||
                    postcodeA.StartsWith("N3") ||
                    postcodeA.StartsWith("N4") ||
                    postcodeA.StartsWith("N5") ||
                    postcodeA.StartsWith("N6") ||
                    postcodeA.StartsWith("N7") ||
                    postcodeA.StartsWith("N8") ||
                    postcodeA.StartsWith("N9") ||
                    postcodeA.StartsWith("NW") ||
                    postcodeA == "RH1" ||
                    postcodeA == "RH2" ||
                    postcodeA == "RH8" ||
                    postcodeA == "RH9" ||
                    postcodeA == "RM1" ||
                    postcodeA == "RM2" ||
                    postcodeA == "RM3" ||
                    postcodeA == "RM4" ||
                    postcodeA == "RM5" ||
                    postcodeA == "RM6" ||
                    postcodeA == "RM7" ||
                    postcodeA == "RM8" ||
                    postcodeA == "RM9" ||
                    postcodeA == "RM10" ||
                    postcodeA == "RM12" ||
                    postcodeA == "RM13" ||
                    postcodeA == "RM14" ||
                    postcodeA.StartsWith("SE") ||
                    postcodeA == "SL9" ||
                    postcodeA.StartsWith("SM") ||
                    postcodeA.StartsWith("SW") ||
                    postcodeA == "TN13" ||
                    postcodeA == "TN14" ||
                    postcodeA == "TN15" ||
                    postcodeA == "TN16" ||
                    postcodeA.StartsWith("TW") ||
                    postcodeA.StartsWith("UB") ||
                    postcodeA.StartsWith("W1") ||
                    postcodeA.StartsWith("W2") ||
                    postcodeA.StartsWith("W3") ||
                    postcodeA.StartsWith("W4") ||
                    postcodeA.StartsWith("W5") ||
                    postcodeA.StartsWith("W6") ||
                    postcodeA.StartsWith("W7") ||
                    postcodeA.StartsWith("W8") ||
                    postcodeA.StartsWith("W9") ||
                    postcodeA == "WC1" ||
                    postcodeA == "WC2" ||
                    postcodeA.StartsWith("WD") ||
                    postcodeA.StartsWith("AL") ||
                    postcodeA.StartsWith("B1") ||
                    postcodeA.StartsWith("B2") ||
                    postcodeA.StartsWith("B3") ||
                    postcodeA.StartsWith("B4") ||
                    postcodeA.StartsWith("B5") ||
                    postcodeA.StartsWith("B6") ||
                    postcodeA.StartsWith("B7") ||
                    postcodeA.StartsWith("B8") ||
                    postcodeA.StartsWith("B9") ||
                    postcodeA.StartsWith("CV") ||
                    postcodeA.StartsWith("HP") ||
                    postcodeA.StartsWith("LU") ||
// --- BRISTOL
                    postcodeA.StartsWith("MK") ||
                    postcodeA == "BA1" ||
                    postcodeA == "BA2" ||
                    postcodeA == "BA3" ||
                    postcodeA == "BA13" ||
                    postcodeA == "BA15" ||
                    postcodeA == "BA11" ||
                    postcodeA.StartsWith("BS")||
                    postcodeA.StartsWith("GL") ||
                    postcodeA.StartsWith("RG") ||
                    postcodeA == "SN1" ||
                    postcodeA == "SN2" ||
                    postcodeA == "SN3" ||
                    postcodeA == "SN4" ||
                    postcodeA == "SN5" ||
                    postcodeA == "SN6" ||
                    postcodeA == "SN7" ||
                    postcodeA == "SN8" ||
                    postcodeA == "SN9" ||
                    postcodeA == "SN10" ||
                    postcodeA.StartsWith("LE")||
                    postcodeA.StartsWith("DY") ||
                    postcodeA.StartsWith("WR") ||
                    postcodeA.StartsWith("NN") ||
                    postcodeA.StartsWith("OX") ||
                    postcodeA.StartsWith("WS2") ||
                    postcodeA == "WS1" ||
                    postcodeA == "WS3" ||
                    postcodeA == "WS4" ||
                    postcodeA == "WS5" ||
                    postcodeA == "WS6" ||
                    postcodeA == "WS7" ||
                    postcodeA == "WS8" ||
                    postcodeA == "WS9" ||
                    postcodeA == "WS10" ||
                    postcodeA == "WS11" ||
                    postcodeA == "WS12" ||
                    postcodeA == "WS13" ||
                    postcodeA == "WS14" ||
                    postcodeA == "WS16" ||
                    postcodeA == "WS17" ||
                    postcodeA == "WS18" ||
                    postcodeA == "WS19" ||
                    postcodeA.StartsWith("WV2") ||
                    postcodeA == "WV1" ||
                    postcodeA == "WV3" ||
                    postcodeA == "WV4" ||
                    postcodeA == "WV5" ||
                    postcodeA == "WV6" ||
                    postcodeA == "WV7" ||
                    postcodeA == "WV8" ||
                    postcodeA == "WV9" ||
                    postcodeA == "WV10" ||
                    postcodeA == "WV11" ||
                    postcodeA == "WV12" ||
                    postcodeA == "WV13" ||
                    postcodeA == "WV14" ||
                    postcodeA == "WV17" ||
                    postcodeA == "WV18" ||
                    postcodeA == "WV19"
                    )
                {
                    if (postcodeB == "" ||
                        postcodeB.StartsWith("BR") ||
                    postcodeB == "CM16" ||
                    postcodeB.StartsWith("CR") ||
                    postcodeB == "DA1" ||
                    postcodeB == "DA5" ||
                    postcodeB == "DA6" ||
                    postcodeB == "DA8" ||
                    postcodeB == "DA9" ||
                    postcodeB == "DA10" ||
                    postcodeB == "DA14" ||
                    postcodeB == "DA15" ||
                    postcodeB == "DA16" ||
                    postcodeB == "DA17" ||
                    postcodeB.StartsWith("E1") ||
                    postcodeB.StartsWith("E2") ||
                    postcodeB.StartsWith("E3") ||
                    postcodeB.StartsWith("E4") ||
                    postcodeB.StartsWith("E5") ||
                    postcodeB.StartsWith("E6") ||
                    postcodeB.StartsWith("E7") ||
                    postcodeB.StartsWith("E8") ||
                    postcodeB.StartsWith("E9") ||
                    postcodeB == "EC1" ||
                    postcodeB == "EC2" ||
                    postcodeB == "EC3" ||
                    postcodeB == "EC3R" ||
                    postcodeB == "EC4" ||
                    postcodeB == "EN1" ||
                    postcodeB == "EN2" ||
                    postcodeB == "EN3" ||
                    postcodeB == "EN4" ||
                    postcodeB == "EN5" ||
                    postcodeB.StartsWith("HA") ||
                    postcodeB.StartsWith("IG") ||
                    postcodeB.StartsWith("KT") ||
                    postcodeB.StartsWith("N1") || //N1 - платная, N2-N22
                    postcodeB.StartsWith("N2") ||
                    postcodeB.StartsWith("N3") ||
                    postcodeB.StartsWith("N4") ||
                    postcodeB.StartsWith("N5") ||
                    postcodeB.StartsWith("N6") ||
                    postcodeB.StartsWith("N7") ||
                    postcodeB.StartsWith("N8") ||
                    postcodeB.StartsWith("N9") ||
                    postcodeB.StartsWith("NW") ||
                    postcodeB == "RH1" ||
                    postcodeB == "RH2" ||
                    postcodeB == "RH8" ||
                    postcodeB == "RH9" ||
                    postcodeB == "RM1" ||
                    postcodeB == "RM2" ||
                    postcodeB == "RM3" ||
                    postcodeB == "RM4" ||
                    postcodeB == "RM5" ||
                    postcodeB == "RM6" ||
                    postcodeB == "RM7" ||
                    postcodeB == "RM8" ||
                    postcodeB == "RM9" ||
                    postcodeB == "RM10" ||
                    postcodeB == "RM12" ||
                    postcodeB == "RM13" ||
                    postcodeB == "RM14" ||
                    postcodeB.StartsWith("SE") ||
                    postcodeB == "SL9" ||
                    postcodeB.StartsWith("SM") ||
                    postcodeB.StartsWith("SW") ||
                    postcodeB == "TN13" ||
                    postcodeB == "TN14" ||
                    postcodeB == "TN15" ||
                    postcodeB == "TN16" ||
                    postcodeB.StartsWith("TW") ||
                    postcodeB.StartsWith("UB") ||
                    postcodeB.StartsWith("W1") ||
                    postcodeB.StartsWith("W2") ||
                    postcodeB.StartsWith("W3") ||
                    postcodeB.StartsWith("W4") ||
                    postcodeB.StartsWith("W5") ||
                    postcodeB.StartsWith("W6") ||
                    postcodeB.StartsWith("W7") ||
                    postcodeB.StartsWith("W8") ||
                    postcodeB.StartsWith("W9") ||
                    postcodeB == "WC1" ||
                    postcodeB == "WC2" ||
                    postcodeB.StartsWith("WD") ||
                    postcodeB.StartsWith("AL") ||
                    postcodeB.StartsWith("B1") ||
                    postcodeB.StartsWith("B2") ||
                    postcodeB.StartsWith("B3") ||
                    postcodeB.StartsWith("B4") ||
                    postcodeB.StartsWith("B5") ||
                    postcodeB.StartsWith("B6") ||
                    postcodeB.StartsWith("B7") ||
                    postcodeB.StartsWith("B8") ||
                    postcodeB.StartsWith("B9") ||
                    postcodeB.StartsWith("CV") ||
                    postcodeB.StartsWith("HP") ||
                    postcodeB.StartsWith("LU") ||
// --- BRISTOL
                    postcodeB.StartsWith("MK") ||
                    postcodeB == "BA1" ||
                    postcodeB == "BA2" ||
                    postcodeB == "BA3" ||
                    postcodeB == "BA13" ||
                    postcodeB == "BA15" ||
                    postcodeB == "BA11" ||
                    postcodeB.StartsWith("BS") ||
                    postcodeB.StartsWith("GL") ||
                    postcodeB.StartsWith("RG") ||
                    postcodeB == "SN1" ||
                    postcodeB == "SN2" ||
                    postcodeB == "SN3" ||
                    postcodeB == "SN4" ||
                    postcodeB == "SN5" ||
                    postcodeB == "SN6" ||
                    postcodeB == "SN7" ||
                    postcodeB == "SN8" ||
                    postcodeB == "SN9" ||
                    postcodeB == "SN10" ||
                    postcodeB.StartsWith("LE") ||
                    postcodeB.StartsWith("DY") ||
                    postcodeB.StartsWith("WR") ||
                    postcodeB.StartsWith("NN") ||
                    postcodeB.StartsWith("OX") ||
                    postcodeB.StartsWith("WS2") ||
                    postcodeB == "WS1" ||
                    postcodeB == "WS3" ||
                    postcodeB == "WS4" ||
                    postcodeB == "WS5" ||
                    postcodeB == "WS6" ||
                    postcodeB == "WS7" ||
                    postcodeB == "WS8" ||
                    postcodeB == "WS9" ||
                    postcodeB == "WS10" ||
                    postcodeB == "WS11" ||
                    postcodeB == "WS12" ||
                    postcodeB == "WS13" ||
                    postcodeB == "WS14" ||
                    postcodeB == "WS16" ||
                    postcodeB == "WS17" ||
                    postcodeB == "WS18" ||
                    postcodeB == "WS19" ||
                    postcodeB.StartsWith("WV2") ||
                    postcodeB == "WV1" ||
                    postcodeB == "WV3" ||
                    postcodeB == "WV4" ||
                    postcodeB == "WV5" ||
                    postcodeB == "WV6" ||
                    postcodeB == "WV7" ||
                    postcodeB == "WV8" ||
                    postcodeB == "WV9" ||
                    postcodeB == "WV10" ||
                    postcodeB == "WV11" ||
                    postcodeB == "WV12" ||
                    postcodeB == "WV13" ||
                    postcodeB == "WV14" ||
                    postcodeB == "WV17" ||
                    postcodeB == "WV18" ||
                    postcodeB == "WV19"
                        )
                    {
                        // OK!
                        if (checkBox1.Checked == true || checkBox2.Checked == true)
                        {

                            // Today
                            if ((checkBox1.Checked == true && pickup_date == "Today") || (checkBox2.Checked == true && pickup_date == "Tomorrow"))
                            {
                                dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                                all_not_ok++;
                            }
                            else
                            {

                                dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                                all_ok++;
                            }

                        }
                        else
                        {

                            dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date + "<br>");
                            all_ok++;
                        }


                    }


        // NOT OK!
                    else
                    {
                        dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date);
                        all_not_ok++;
                    }
                }
                else
                {
                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "https://anyvan.com/view-listing/" + myData.render[x].id, pickup_date);
                    all_not_ok++;
                }






            }
            label9.Text = all_ok.ToString();
            label10.Text = all_not_ok.ToString();
            int all_count = 0;
            all_count = myData.row_count - all_ok - all_not_ok;
            label12.Text = (all_count).ToString();

        }

        private void SaveNotOK_Click(object sender, EventArgs e)
        {
            string file = "Anyvan_NOT_OK.html";

            dataGridView2.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            // Select all the cells
            dataGridView2.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dataGridView2.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            File.WriteAllText(file, Clipboard.GetText(TextDataFormat.UnicodeText));
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MMdd-HHmmss");
        }

        private void Analyze_Click(object sender, EventArgs e)
        {
            string file = "export-";

            string timeStamp = GetTimestamp(DateTime.Now);
            
            file = file + timeStamp+".txt";
            StreamWriter writetext = new StreamWriter(file);

            for (int x = 0; x < myData.row_count; x++)
            {
                string postcodeA = myData.render[x].listing_pickup_address.postcode;
                string regionA = myData.render[x].listing_pickup_address.region;
                string townA = myData.render[x].listing_pickup_address.town;
                string latA = myData.render[x].pickup_lat;
                string lngA = myData.render[x].pickup_lng;

                if (postcodeA == "") postcodeA = "xxx";
                if (regionA == "") regionA = "xxx";
                if (townA == "") townA = "xxx";
                if (latA == "") latA = "0.000";
                if (lngA == "") lngA = "0.000";

                string postcodeB = myData.render[x].listing_delivery_address.postcode;
                string regionB = myData.render[x].listing_delivery_address.region;
                string townB = myData.render[x].listing_delivery_address.town;
                string latB = myData.render[x].delivery_lat;
                string lngB = myData.render[x].delivery_lng;

                if (postcodeB == "") postcodeB = "xxx";
                if (regionB == "") regionB = "xxx";
                if (townB == "") townB = "xxx";
                if (latB == "") latB = "0.000";
                if (lngB == "") lngB = "0.000";


                string distance = myData.render[x].listing_distance;

                if (distance == "") distance = "000 miles";

                writetext.WriteLine(postcodeA+" | "+regionA+" | "+townA+" | "+latA+" | "+lngA+" | "+postcodeB+" | "+regionB+" | "+townB+" | "+latB+" | "+lngB+" | "+distance);
            }


            writetext.Close();

        }

        private void readCfg_Click(object sender, EventArgs e)
        {
            string file = "anyvan_stat.cfg";

            List<string> postcode = new List<string>();
            string line="";
            string tmpl,tmpr,tmpo = "";
            int i=0,j=0;

            StreamReader readtext = new StreamReader(file);
            while ((line = readtext.ReadLine()) != null)
            {
                if (line.Contains("//")) continue;
                if (line.Length==0) continue;
                if (line.Contains("##"))
                {
                    tmpo = line.Substring(2, line.Length - 2);
                    labelReadConfig.Text = tmpo;
                    continue;
                }

                int d = line.IndexOf(":");
                tmpl = line.Remove(d);
                tmpr = line.Substring(d+1, line.Length - d-1);
                
                if(tmpr.Contains(","))
                {
                    string[] numbers=tmpr.Split(',');
                    for (j = 0; j < numbers.Length; j++)
                    {
                        tmpo = tmpl + numbers[j];
                        postcode.Add(tmpo);
                    }
                    continue;

                }

                if (tmpr.Contains("-"))
                {
                    i = tmpr.IndexOf("-");
                    string tmpstart = tmpr.Substring(0, i);
                    string tmpend = tmpr.Substring(i + 1, tmpr.Length - i - 1);
                    for (j = int.Parse(tmpstart); j < int.Parse(tmpend) + 1; j++)
                    {
                        tmpo = tmpl + j.ToString();
                        postcode.Add(tmpo);
                    }
                    continue;

                }
                
                if (tmpr == "*")
                {
                    for (j = 0; j < 100; j++) // BR:*  =  BR0-BR99
                    {
                        tmpo = tmpl + j.ToString();
                        postcode.Add(tmpo);
                    }
                    continue;
                }

                
                postcode.Add(tmpl+tmpr);
            }
            readtext.Close();
            
            //-------------------------------------------------------------------------------------
           
                        int all_ok = 0;
                        int all_not_ok = 0;
                        int otbrosili = 0;

                        dataGridView1.Rows.Clear();
                        dataGridView2.Rows.Clear();

                        for (int x = 0; x < myData.row_count; x++)
                        {
                            string postcodeA = myData.render[x].listing_pickup_address.postcode;
                            string townA = myData.render[x].listing_pickup_address.town;
                            string regionA = myData.render[x].listing_pickup_address.region;

                            string postcodeB = myData.render[x].listing_delivery_address.postcode;
                            string townB = myData.render[x].listing_delivery_address.town;
                            string regionB = myData.render[x].listing_delivery_address.region;

                            string pickup_date = myData.render[x].listing_pickup_date;
                            string platno = "";

                            if (postcodeA.StartsWith("EC") ||
                                postcodeA == "N1" ||
                                postcodeA == "SE1" ||
                                postcodeA == "SE11" ||
                                postcodeA == "SW1A" ||
                                postcodeA == "SW1E" ||
                                postcodeA == "SW1H" ||
                                postcodeA == "SW1Y" ||
                                postcodeA == "SW1P" ||
                                postcodeA == "SW1X" ||
                                postcodeA == "SW1W" ||
                                postcodeA == "SW1V" ||
                                postcodeA == "W1A" ||
                                postcodeA == "W1B" ||
                                postcodeA == "W1C" ||
                                postcodeA == "W1D" ||
                                postcodeA == "W1F" ||
                                postcodeA == "W1G" ||
                                postcodeA == "W1H" ||
                                postcodeA == "W1J" ||
                                postcodeA == "W1K" ||
                                postcodeA == "W1S" ||
                                postcodeA == "W1T" ||
                                postcodeA == "W1U" ||
                                postcodeA == "W1W" ||
                                postcodeA.StartsWith("WC1") ||
                                postcodeA.StartsWith("WC2")
                                ) platno = "--- PLATNO!!! --- ";

                            if (postcodeB.StartsWith("EC1") ||
                                postcodeB == "N1" ||
                                postcodeB == "SE1" ||
                                postcodeB == "SE11" ||
                                postcodeB == "SW1A" ||
                                postcodeB == "SW1E" ||
                                postcodeB == "SW1H" ||
                                postcodeB == "SW1Y" ||
                                postcodeB == "SW1P" ||
                                postcodeB == "SW1X" ||
                                postcodeB == "SW1W" ||
                                postcodeB == "SW1V" ||
                                postcodeB == "W1A" ||
                                postcodeB == "W1B" ||
                                postcodeB == "W1C" ||
                                postcodeB == "W1D" ||
                                postcodeB == "W1F" ||
                                postcodeB == "W1G" ||
                                postcodeB == "W1H" ||
                                postcodeB == "W1J" ||
                                postcodeB == "W1K" ||
                                postcodeB == "W1S" ||
                                postcodeB == "W1T" ||
                                postcodeB == "W1U" ||
                                postcodeB == "W1W" ||
                                postcodeB.StartsWith("WC1") ||
                                postcodeB.StartsWith("WC2")
                                ) platno = "--- PLATNO!!! --- ";

                            // Пометка что берем часть посткода

                            if (postcodeA == "BA11" ||
                                postcodeA == "SN8" ||
                                postcodeA == "SN9" ||
                                postcodeA == "SN10" ||
                                postcodeA.StartsWith("LE") ||
                                postcodeA == "DY14" ||
                                postcodeA == "WR6" ||
                                postcodeA == "DA10" ||
                                postcodeA.StartsWith("NN")
                                ) platno = "--- SMOTRI KARTU! --- ";

                            if (postcodeB == "BA11" ||
                                postcodeB == "SN8" ||
                                postcodeB == "SN9" ||
                                postcodeB == "SN10" ||
                                postcodeB.StartsWith("LE") ||
                                postcodeB == "DY14" ||
                                postcodeB == "WR6" ||
                                postcodeB == "DA10" ||
                                postcodeB.StartsWith("NN")
                                ) platno = "--- SMOTRI KARTU! --- ";

// Основная проверка посткодов
                            if (richTextBox6.Text == "") richTextBox6.Text = " ";
                            if (richTextBox5.Text == "") richTextBox5.Text = " ";
                         
                            if ((
                                postcodeA.StartsWith(richTextBox5.Text)||
                                townA.StartsWith(richTextBox6.Text)||
                                regionA.StartsWith(richTextBox6.Text)||
                                postcodeB.StartsWith(richTextBox5.Text)||
                                townB.StartsWith(richTextBox6.Text)||
                                regionB.StartsWith(richTextBox6.Text)
                                )&&
                                (
                                postcodeA == "" ||
                                Array.Exists(postcode.ToArray(), element => element==postcodeA)
                                ))
                            {
                                if (
                                postcodeB == "" ||
                                Array.Exists(postcode.ToArray(), element => element==postcodeB)
                                    )
                                {
                                    // OK!

                                    if (pickup_date.StartsWith("On"))
                                    {
                                        string tmp_pickupdate = pickup_date.Replace("On ","");
                                        DateTime date_pickup = DateTime.ParseExact(tmp_pickupdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        int daypickup = (int) date_pickup.DayOfWeek;
                                        if (daypickup == 1 && checkBox3.Checked == true || 
                                            daypickup == 2 && checkBox4.Checked == true || 
                                            daypickup == 3 && checkBox5.Checked == true || 
                                            daypickup == 4 && checkBox6.Checked == true ||
                                            daypickup == 5 && checkBox7.Checked == true ||
                                            daypickup == 6 && checkBox8.Checked == true || 
                                            daypickup == 0 && checkBox9.Checked == true)
                                        {
                                            dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + " "+date_pickup.DayOfWeek.ToString()+"<br>");
                                            all_not_ok++;
                                            continue;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                            all_ok++;
                                            continue;
                                        }
                                    }

                                    if (checkBox1.Checked == true || checkBox2.Checked == true)
                                    {

                                        // Today
                                        if ((checkBox1.Checked == true && pickup_date == "Today") || (checkBox2.Checked == true && pickup_date == "Tomorrow"))
                                        {
                                            dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                            all_not_ok++;
                                        }
                                        else
                                        {

                                            dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                            all_ok++;
                                        }

                                    }
                                    else
                                    {

                                        dataGridView1.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                        all_ok++;
                                    }


                                }


                    // NOT OK!
                                else
                                {
                                    dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                    all_not_ok++;
                                }
                            }
                            else
                            {
                                dataGridView2.Rows.Add(platno + myData.render[x].listing_label, myData.render[x].listing_pickup_address.postcode, myData.render[x].listing_delivery_address.postcode, "<a href=\"https://anyvan.com/view-listing/" + myData.render[x].id + "\">" + "https://anyvan.com/view-listing/" + myData.render[x].id + "</a>", pickup_date + "<br>");
                                all_not_ok++;
                            }






                        }
                        label9.Text = all_ok.ToString();
                        label10.Text = all_not_ok.ToString();
                        int all_count = 0;
                        all_count = myData.row_count - all_ok - all_not_ok;
                        label12.Text = (all_count).ToString();
                        label14.Text = (otbrosili).ToString();
                        if (richTextBox5.Text == " ") richTextBox5.Text = "";
                        if (richTextBox6.Text == " ") richTextBox6.Text = "";
           
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            string SearchText = "";
            string SearchURL = "https://www.anyvan.com/search/mk18";
            string OutputFile = "any" + richTextBoxPage.Text.Trim() + ".html";
            string searchT = "";
            string line = "",tbLine="";
            const string quote = "\"";

            searchT = " -k -o " + OutputFile + " -b headers -d "+quote + SearchText + quote+" " + SearchURL;
            
            var process=Process.Start("curl.exe", searchT);
            process.WaitForExit();

            StreamReader readtext = new StreamReader(OutputFile);
            while ((line = readtext.ReadLine()) != null)
            {
                if (line.Contains("//")) continue;
                if (line.Length == 0) continue;
                if (line.Contains("global_data"))
                {
                    tbLine=line.Replace("global_data=","");
                    tbLine=tbLine.Replace("count\":false", "count\":77777");
                    tbLine=tbLine.Trim();
                    tbLine = tbLine.TrimEnd(';');
                    richTextBox1.Text = tbLine;
                    continue;
                }

                          
                              
            }
            readtext.Close();
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }



       

        


  }
 
}

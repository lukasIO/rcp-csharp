﻿using RCP;
using RCP.Parameters;
using RCP.Protocol;
using RCP.Transporter;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RCPSharpDemo
{
    public partial class Server : Form
    {
        RCPServer FRabbit;
        Client FClient;

        public Server()
        {
            InitializeComponent();

            FRabbit = new RCPServer();

            //Rabbit.AddTransporter(new UDPServerTransporter("127.0.0.1", 4568, 4567));
            FRabbit.AddTransporter(new WebsocketServerTransporter("127.0.0.1", 10000));

            //the client
            FClient = new Client();
            FClient.Show();
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            FClient.Dispose();
            FRabbit.Dispose();
        }

        private BangParameter FMyBang;
        
        private void button1_Click(object sender, System.EventArgs e)
        {
            var group = FRabbit.CreateGroup("foo");

            var param = FRabbit.CreateNumberParameter<float>("my float", group);
            param.Order = param.Id;
            param.Widget = new SliderWidget();
            param.Default = 7.0f;
            param.Value = 2.0f;
            param.Minimum = -10.0f;
            param.Maximum = 10.0f;
            param.ValueUpdated += (s, a) => label1.Text = param.Value.ToString();

            var nt = FRabbit.CreateNumberParameter<int>("my int", group);
            nt.Value = 3;
            nt.Minimum = -5;
            nt.Maximum = 5;

            var enm = FRabbit.CreateEnumParameter("my enum", group);
            enm.Entries = new string[3] { "aber", "biber", "zebra" };
            enm.Default = "biber";
            enm.Value = "zebra";
            //enm.ValueUpdated += Enm_ValueUpdated;

            var str = FRabbit.CreateStringParameter("my string", group);
            str.Value = "foobar";

            var clr = FRabbit.CreateValueParameter<Color>("ma color", group);
            clr.Value = Color.Red;

            var strarr = FRabbit.CreateArrayParameter<string>("my string array", null, new int[] { 3 });
            strarr.Default = new string[3] { "a", "b", "c" };
            strarr.Value = new string[3] { "aa", "bv", "cc" };

            var intarr = FRabbit.CreateArrayParameter<int>("my int array", null, new int[] { 3 });
            intarr.Default = new int[3] { 1, 2, 4 };
            intarr.Value = new int[3] { 4, 5, 6 };

            FMyBang = (BangParameter)FRabbit.CreateBangParameter("my bang");
            FMyBang.OnBang += FMyBang_OnBang;

            //enm.Value = "biber";
            //FRabbit.Update();

            //enm.Value = "aber";
            //FRabbit.Update();
        }

        private void FMyBang_OnBang(object sender, EventArgs e)
        {
            label1.Text = "bang: " + DateTime.Now.ToString();
        }

        private void Enm_ValueUpdated(object sender, string e)
        {
            label1.Text = e.ToString();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            FMyBang.Bang();
            FRabbit.Update();
        }
    }
}

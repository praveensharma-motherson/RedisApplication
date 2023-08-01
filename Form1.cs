using Redis.OM;
using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var provider = new RedisConnectionProvider("redis://127.0.0.1:6379");

            var connection = provider.Connection;
            var sessionKeys = provider.RedisCollection<SessionKey>();
            var sessKey1 = new SessionKey { Id = "User1:Instance1", Name = "AllSessionKeys", Value = "JSON VALUES1 would be here " };
            var sessKey2 = new SessionKey { Id = "User1:Instance2", Name = "AllSessionKeys", Value = "JSON VALUES2 would be here" };
            var idp1 = connection.SetAsync(sessKey1);
            var idp2 = sessionKeys.InsertAsync(sessKey2);

            var reconstitutedE1 = connection.GetAsync<SessionKey>(sessKey1.Id);
            //connection.JsonSet("json1","this is path","{Item{1,2,3},Item{4,5,6}}");


            var xx = connection.HGetAll("SessionKey:User1:Instance1");

            //update a cache key in redis
            //connection.JsonSet("json1", "Item[0]", "100");

            //var xx2 = connection.JsonGet("json1", "Item[0]");   

            MessageBox.Show(reconstitutedE1.ToString());
        }

        [Document(Prefixes = new[] { "SessionKey" })]
        public class SessionKey
        {
            [RedisIdField]
            public string Id { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var provider = new RedisConnectionProvider("redis://127.0.0.1:6379");

            var connection = provider.Connection;
            

            var xx = connection.HGetAll("SessionKey:User1:Instance1");

            MessageBox.Show(xx.ToString());

        }
    }
}

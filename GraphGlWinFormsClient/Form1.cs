using GraphQL;
using GraphQL.Client.Http;
using SeleniumParse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphQL.Client.Serializer.Newtonsoft;

namespace GraphGlWinFormsClient
{
   

    public partial class Form1 : Form
    {

        public TransportationInfo[] transInfo;

        public Form1()
        {
            InitializeComponent();
            Task.Run(() => GraphGlFunc()).Wait();
        }


        public async Task GraphGlFunc()
        {
            var client = new GraphQLHttpClient("https://localhost:44341/graphql", new NewtonsoftJsonSerializer());

            var request = new GraphQLRequest
            {
                Query = @"query {
                           transportation {
                             cargoDescription
                             loadingDate
                             paymentType
                             routFrom
                             routFromCountry
                             routTo
                             routToCountry
                             vehicleType
                           }
                        }"
            };
            var response = await client.SendQueryAsync<GraphqlResponse>(request);
            this.transInfo = response.Data.transportation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetData();
        }

        private void SetData()
        {
            foreach (var item in this.transInfo)
            {
                string[] rows = { item.loadingDate, item.vehicleType, item.cargoDescription, item.paymentType, item.routFrom, item.routTo, item.routFromCountry, item.routToCountry };
                var listItem = new ListViewItem(rows);
                listView1.Items.Add(listItem);
            }
        }
    }
}

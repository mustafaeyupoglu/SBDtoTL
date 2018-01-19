using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SBDtoTL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        decimal BITTREXSELLCOMMISION = 0.0025m;
        decimal BITTREXSBDWITHDRAWFEE = 0.00100000m;
        private void button1_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//SSL için güvenli kanal oluşmadığı için çözüm


            decimal sbd = decimal.Parse(textBox1.Text);


            WebClient wc = new WebClient();
            string bittrexApiUrl = "https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-sbd";
            string json = wc.DownloadString(bittrexApiUrl);
            JObject jObject = JObject.Parse(json);
            JToken jApiRow = jObject["result"];
            SBD bittrexVal = JsonConvert.DeserializeObject<SBD>(jApiRow.First.ToString());
            decimal btc = sbd * bittrexVal.Bid;
            decimal sonuc = btc - (btc * BITTREXSELLCOMMISION) - BITTREXSBDWITHDRAWFEE;


            string paribuApiUrl = "https://www.paribu.com/ticker";
            string json2 = wc.DownloadString(paribuApiUrl);
            JObject jObject2 = JObject.Parse(json2);
            JToken jApiRow2 = jObject2["BTC_TL"];            
            Paribu paribuVal = JsonConvert.DeserializeObject<Paribu>(jApiRow2.ToString());                     
            decimal paribuSonuc= sonuc * paribuVal.last;
            textBox2.Text = paribuSonuc.ToString();
  

            string btcTurkApiUrl = "https://www.btcturk.com/api/ticker";
            string json3 = wc.DownloadString(btcTurkApiUrl);
            JArray jObject3 = JArray.Parse(json3);
            BtcTurk btcTurkVal = JsonConvert.DeserializeObject<BtcTurk>(jObject3[0].ToString());
            decimal btcTurkSonuc= sonuc * btcTurkVal.bid;
            textBox3.Text = btcTurkSonuc.ToString();

            string koinimApiUrl = "https://koinim.com/ticker/";
            string json4 = wc.DownloadString(koinimApiUrl);
            JObject jObject4 = JObject.Parse(json4);
            Koinim koinimVal = JsonConvert.DeserializeObject<Koinim>(jObject4.ToString());
            decimal koinimSonuc = sonuc * koinimVal.bid;
            textBox4.Text = koinimSonuc.ToString();
            
        }
        class SBD
        {
            public string MarketName { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal Volume { get; set; }
            public decimal Last { get; set; }
            public decimal BaseVolume { get; set; }
            public string TimeStamp { get; set; }
            public decimal OpenBuyOrders { get; set; }
            public decimal OpenSellOrders { get; set; }
            public decimal PrevDay { get; set; }
            public string Created { get; set; }



            public decimal Bid { get; set; }
            public decimal Ask { get; set; }
           public SBD(decimal bid,decimal ask)
            {
                this.Bid=bid;
                this.Ask=ask;
            }
            
        }
        class Paribu
        {
            public decimal last { get; set; }
            public decimal lowestAsk { get; set; }
            public decimal highestBid { get; set; }
            public decimal percentChange { get; set; }
            public decimal volume { get; set; }
            public decimal high24hr { get; set; }
            public decimal low24hr { get; set; }

            public Paribu(decimal bid, decimal ask)
            {
                this.last = bid;
                this.lowestAsk = ask;
            }

        }
        class BtcTurk
        {
            public string pair { get; set; }
            public decimal high { get; set; }
            public decimal last { get; set; }
            public decimal timestamp { get; set; }
            public decimal bid { get; set; }
            public decimal volume { get; set; }
            public decimal low { get; set; }
            public decimal ask { get; set; }
            public decimal open { get; set; }
            public decimal average { get; set; }
            public decimal daily { get; set; }
            public decimal dailyPercent { get; set; }


        }
       class Koinim
       {
           public decimal sell { get; set; }
           public decimal high { get; set; }
           public decimal buy { get; set; }
           public decimal change_rate { get; set; }
           public decimal bid { get; set; }
           public decimal wavg { get; set; }
           public decimal last_order { get; set; }
           public decimal volume { get; set; }
           public decimal low { get; set; }
           public decimal ask { get; set; }
           public decimal avg { get; set; }



       }

    }
}

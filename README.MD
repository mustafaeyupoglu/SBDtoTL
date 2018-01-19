SBDtoTL is a steem dollar convertor for turkish community.
This application calculates how many steem dollar corresponds to the turkish liras.
First of all we calculate how many bitcoin is the steem dollar , then we subtract the bittrex commissions. Finally, we multiply bitcoin amount with stock market value in Turkey.
By seeing 3 different results, you can see which site is more advantageous.

#### In the app we are using 4 sites API:
* [Bittrex](https://bittrex.com/): Used for converting SBD value to BTC value
* [Paribu](https://www.paribu.com/): Paribu is a turkish Bitcoin exchange site
* [BTCTurk](https://www.btcturk.com) : BTCTurk is a turkish Bitcoin exchange site
* [Koinim](https://koinim.com): Koinim is a turkish Bitcoin exchange site

#### When app calculating that, using this formula;
```
BITTREXSELLCOMMISION = 0.0025
BITTREXSBDWITHDRAWFEE = 0.00100000
BTCAmount= ((SBD*BittrexBidValue)-(SBD*BittrexBidValue*BITTREXSELLCOMMISION )-BITTREXSBDWITHDRAWFEE )
TLValue=BTCAmount * StockMarketValue
```

* **JSON parsing and Deserializing was the most important thing in the development process**
For this process, I used the most famous JSON framework created by [James Newton-King](http://james.newtonking.com/bio)
You can install[ JSON.Net](https://www.newtonsoft.com/json) framework from NuGet Package Manager

```
            WebClient wc = new WebClient();
            string bittrexApiUrl = "https://bittrex.com/api/v1.1/public/getmarketsummary?market=btc-sbd";
            string json = wc.DownloadString(bittrexApiUrl);
            JObject jObject = JObject.Parse(json);
            JToken jApiRow = jObject["result"];
```
* You can see the **parsing** process in the above code snippet. First we download ```JSON data``` as a ```string``` and after that we parse it and convert it to the ```JSON Object ```

```
SBD bittrexVal = JsonConvert.DeserializeObject<SBD>(jApiRow.First.ToString());
```
* You can see the **Deserializing** process in the above code snippet.


https://steemit.com/@primus1

https://utopian.io/@primus1

Contribution link https://utopian.io/utopian-io/@primus1/sbdtotl-steem-dollar-converter

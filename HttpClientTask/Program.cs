using HttpClientTask.Data;
using HttpClientTask.Models;
using Newtonsoft.Json;
try
{
    using (HttpClient client = new())
    {
        HttpResponseMessage response = await client.GetAsync("https://cbu.uz/oz/arkhiv-kursov-valyut/json/");

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            List<IncomingModel>? jokes = JsonConvert.DeserializeObject<List<IncomingModel>>(content);

            if (jokes == null)
                return;

            AppDBContext appDBContext = new AppDBContext();
            foreach (IncomingModel jok in jokes)
            {
                appDBContext.CurrencyExchangeRates.Add(new OutgoingModel
                {
                    Identifikator=jok.Id,
                    Code=jok.Code,
                    Ccy=jok.Ccy,
                    CcyNm_RU=jok.CcyNm_RU,
                    CcyNm_UZ=jok.CcyNm_UZ,
                    CcyNm_UZC = jok.CcyNm_UZC,
                    CcyNm_EN = jok.CcyNm_EN,
                    Nominal = jok.Nominal,
                    Rate=jok.Rate,
                    Diff = jok.Diff,
                    Date = jok.Date
                });
                appDBContext.SaveChanges();
            }
        }


    }


}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

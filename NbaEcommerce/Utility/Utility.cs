using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaEcommerce.Utility
{
    public static class Utility
    {

        public const string _KeyCarrello = "KEY_SESSIONE_CARRELLO";

    }


   
}

public static class SessionExtensions

{

    public static void SetObject(this ISession session, string key, object value)

    {

        session.SetString(key, JsonConvert.SerializeObject(value));

    }



    public static T GetObject<T>(this ISession session, string key)

    {

        var value = session.GetString(key);

        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);

    }

}


class carrelloProdotto
{
    public string Id { get; set; }

    public int Quantita { get; set; }
}
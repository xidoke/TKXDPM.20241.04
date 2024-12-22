using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AIMS.Models.Entities;

public class VnPayLibrary
{
    private readonly SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
    private readonly SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());


    public string GetIpAddress(string ipAddress)
    {
        return ipAddress;
    }
    public void AddRequestData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _requestData.Add(key, value);
        }
    }

    public void AddResponseData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _responseData.Add(key, value);
        }
    }

    public string GetResponseData(string key)
    {
        return _responseData.TryGetValue(key, out var retValue) ? retValue : string.Empty;
    }

    public string CreateRequestUrl(string baseUrl, string vnpHashSecret)
    {
        StringBuilder data = new StringBuilder();
        foreach (KeyValuePair<string, string> kv in _requestData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            }
        }
        string queryString = data.ToString();
        baseUrl += "?" + queryString;
        String signData = queryString;
        if (signData.Length > 0)
        {
            signData = signData.Remove(data.Length - 1, 1);
        }
        string vnp_SecureHash = HmacSHA512(vnpHashSecret, signData);
        baseUrl += "vnp_SecureHash=" + vnp_SecureHash;

        return baseUrl;
    }

    public bool ValidateSignature(string inputHash, string secretKey)
    {
        string rspRaw = GetResponseData();
        string myChecksum = HmacSHA512(secretKey, rspRaw);
        return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }

    private string GetResponseData()
    {
        StringBuilder data = new StringBuilder();
        if (_responseData.ContainsKey("vnp_SecureHashType"))
        {
            _responseData.Remove("vnp_SecureHashType");
        }
        if (_responseData.ContainsKey("vnp_SecureHash"))
        {
            _responseData.Remove("vnp_SecureHash");
        }
        foreach (KeyValuePair<string, string> kv in _responseData)
        {
            if (!String.IsNullOrEmpty(kv.Value))
            {
                data.Append(WebUtility.UrlEncode(kv.Key) + "=" + WebUtility.UrlEncode(kv.Value) + "&");
            }
        }
        if (data.Length > 0)
        {
            data.Remove(data.Length - 1, 1);
        }
        return data.ToString();
    }

    public PaymentResponseModel GetFullResponseData(Dictionary<string, string> responseData, string hashSecret)
    {
        VnPayLibrary vnPay = new VnPayLibrary();

        foreach (KeyValuePair<string, string> kvp in responseData)
        {
            if (!string.IsNullOrEmpty(kvp.Key) && kvp.Key.StartsWith("vnp_"))
            {
                vnPay.AddResponseData(kvp.Key, kvp.Value);
            }
        }
        long orderId = Convert.ToInt64(vnPay.GetResponseData("vnp_TxnRef"));
        long vnPayTranId = Convert.ToInt64(vnPay.GetResponseData("vnp_TransactionNo"));
        string vnpResponseCode = vnPay.GetResponseData("vnp_ResponseCode");
        string vnpSecureHash = responseData.ContainsKey("vnp_SecureHash") ? responseData["vnp_SecureHash"] : "";
        String orderInfo = vnPay.GetResponseData("vnp_OrderInfo");
        bool checkSignature = vnPay.ValidateSignature(vnpSecureHash, hashSecret);

        if (!checkSignature)
        {
            return new PaymentResponseModel()
            {
                Success = false
            };
        }

        return new PaymentResponseModel()
        {
            Success = vnpResponseCode.Equals("00"),
            PaymentMethod = "VnPay",
            OrderDescription = orderInfo,
            OrderId = orderId.ToString(),
            PaymentId = vnPayTranId.ToString(),
            TransactionId = vnPayTranId.ToString(),
            Token = vnpSecureHash,
            VnPayResponseCode = vnpResponseCode
        };
    }
    public static string HmacSHA512(string key, string inputData)
    {
        var hash = new StringBuilder();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var inputBytes = Encoding.UTF8.GetBytes(inputData);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }
}

public class VnPayCompare : IComparer<string>
{
    public int Compare(string x, string y)
    {
        if (x == y) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var vnpCompare = CompareInfo.GetCompareInfo("en-US");
        return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
    }
}


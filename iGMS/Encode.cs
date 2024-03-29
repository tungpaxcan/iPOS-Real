﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace iGMS
{
    public class Encode
    {
        public static string ToMD5(string str)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                result += buffer[i].ToString("x2");
            }
            return result;
        }
        public static string EPC(string str)
        {
            Random res = new Random();
            String strs = "abcdefghijklmnopqrstuvwxyz";
            int size = 2;
            String ran = "";
            for (int i = 0; i < size; i++)
            {
                int x = res.Next(26);
                ran = ran + strs[x];
            }
            int strlength = str.Length;
            int startno = strlength - 6;
            int startba = strlength - 11;
            string idepcsno = "";
            string idepcsba = "";
            string idepcs = "";
            if (strlength > 11)
            {
                idepcsno = str.Substring(startno, 6);
                idepcsba = str.Substring(startba, 3);
                idepcs = idepcsba + idepcsno + ran;
                byte[] bytes = Encoding.Default.GetBytes(idepcs);
                string idepc = BitConverter.ToString(bytes);
                idepc = idepc.Replace("-", "");
                return idepc+"EE";
            }
            else
            {
                idepcsno = str;
                byte[] bytes = Encoding.Default.GetBytes(idepcs);
                string idepc = BitConverter.ToString(bytes);
                idepc = idepc.Replace("-", "");
                var idpeclength = idepc.Length;
                if (idpeclength < 22)
                {
                    var add = 22 - idpeclength;
                    for (int i = 0; i < add; i++)
                    {
                        idepc += "0";
                    }
                }
                return idepc+"EE";
            }
        }
        public static string Money(string str)
        {
            string result= str.Substring(0, str.Length - 3) + "K";
            return result;
        }
        public static string DayOfWeek(int num)
        {
            string result = num != 8 ? "Thứ " + num : "Chủ Nhật";
            return result;
        }

    }
}
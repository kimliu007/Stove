using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace VSDataSimulator
{
    public class Util
    {
        public byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }

        public static byte GetHighByte(int intval)
        {
            //return (intval & (0xffff << 16));
            return (byte)((intval & 0xff00) >> 8);
        }
        //取一整数的高字节
        public static byte GetLowByte(int intval)
        {
            //return (intval & 0x0000ffff);
            return (byte)(intval & 0x00ff);
        }

        /*
         * 1、SP串口
         * 2、len是需要读取的字节数
         * 3、flag是读字节成功与否
        */
        public static byte[] Uart_Re1(SerialPort sp, ref int flag)
        {
            byte[] r = new byte[1];
            sp.Read(r, 0, 1);
            return r;
        }

        public static byte[] RealDtoB(double number)
        {
            double zhenshu, xiaoshu, jieguo;
            byte[] RealByte = new byte[4];
            string strZ, strX = "", strJ;
            int numb, i = 1, len, le, len2, len3, Bias = 127;
            string s, str1, str2, Jiema, weishu, jia;
            string Jieguo1, Jieguo2, Jieguo3, Jieguo4;

            if (Math.Abs(number) == 0)
            {
                RealByte[0] = 0;
                RealByte[1] = 0;
                RealByte[2] = 0;
                RealByte[3] = 0;
            }
            else
            {
                if (number > 0)
                {
                    s = "0";
                }
                else
                {
                    s = "1";
                }
                zhenshu = Math.Floor(Math.Abs(number));
                numb = Convert.ToInt32(zhenshu);
                strZ = Convert.ToString(numb, 2);
                xiaoshu = Math.Abs(number) - zhenshu;
                for (i = 0; ((xiaoshu != 0) && (i < 23)); i++)
                {
                    jieguo = xiaoshu * 2;
                    strJ = jieguo.ToString();
                    strX += strJ.Substring(0, 1);
                    zhenshu = Math.Floor(jieguo);
                    xiaoshu = jieguo - zhenshu;
                }
                strJ = strZ + "." + strX;
                str1 = strJ.Substring(0, 1);
                if (str1.Equals("1"))
                {
                    len = strZ.Length;
                    le = len + Bias - 1;
                    Jiema = Convert.ToString(le, 2);
                    len2 = Jiema.Length;
                    if (len2 < 8)
                    {
                        for (i = 0, jia = ""; i < 8 - len2; i++)
                        {
                            jia += "0";
                        }
                        Jiema = jia + Jiema;
                    }
                    str2 = strZ.Substring(1, len - 1) + strX;
                    len = str2.Length;
                    if (len > 23)
                    {
                        weishu = str2.Substring(0, 23);
                    }
                    else
                    {
                        weishu = str2;
                    }
                }
                else
                {
                    len = strX.IndexOf("1");
                    le = Bias - (len + 1);
                    Jiema = Convert.ToString(le, 2);
                    len2 = Jiema.Length;
                    if (len2 < 8)
                    {
                        for (i = 0, jia = ""; i < 8 - len2; i++)
                        {
                            jia += "0";
                        }
                        Jiema = jia + Jiema;
                    }
                    len3 = strX.Length;
                    if (len3 == (len + 1))
                    {
                        weishu = "0";
                    }
                    else
                    {
                        weishu = strX.Substring(len + 1);
                    }
                }
                strJ = s + Jiema + weishu;
                len = strJ.Length;
                if (len < 32)
                {
                    for (i = 0; i < 32 - len; i++)
                    {
                        strJ += "0";
                    }
                }
                Jieguo1 = strJ.Substring(0, 8);
                Jieguo2 = strJ.Substring(8, 8);
                Jieguo3 = strJ.Substring(16, 8);
                Jieguo4 = strJ.Substring(24, 8);
                RealByte[0] = Convert.ToByte(Jieguo1, 2);
                RealByte[1] = Convert.ToByte(Jieguo2, 2);
                RealByte[2] = Convert.ToByte(Jieguo3, 2);
                RealByte[3] = Convert.ToByte(Jieguo4, 2);
            }
            return RealByte;
        }

        public static byte[] FloatToByte(float data)
        {
            unsafe
            {
                byte* pdata = (byte*)&data;
                byte[] byteArray = new byte[sizeof(float)];
                for(int i=0; i<sizeof(float); i++)
                {
                    byteArray[i] = *pdata++;
                }
                return byteArray;
            }
        }

        public static float ByteToFloat(byte[] data)
        {
            float a = 0;
            byte i;
            byte[] x = data;
            unsafe
            {
                void* pf;
                fixed(byte* px = x)
                {
                    pf = &a;
                    for(i=0; i<data.Length; i++)
                    {
                        *((byte*)pf + i) = *(px + i);
                    }
                }
            }
            return a;
        }

        public static float HexStringToDecimal(string s)
        {
            MatchCollection matches = Regex.Matches(s, @"[0-9A-Fa-f]{2}");
            byte[] bytes = new byte[matches.Count];
            for(int i=0; i<bytes.Length; i++)
            {
                bytes[i] = byte.Parse(matches[i].Value, NumberStyles.AllowHexSpecifier);
            }
            float m = BitConverter.ToSingle(bytes, 0);
            return m;
        }
    }
}

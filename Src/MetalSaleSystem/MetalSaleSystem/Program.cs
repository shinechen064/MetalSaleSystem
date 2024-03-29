﻿using MetalSaleSystem.Common;
using MetalSaleSystem.Entity;
using MetalSaleSystem.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetalSaleSystem
{
    public class Program
    {
        private static StringBuilder m_sbFileContext;

        private static OrderInformation m_objOrderInfo;

        static void Main(string[] args)
        {
            // 输入参数不合法
            if (null == args || args.Length <= 1)
            {
                Console.WriteLine("please key something like:MetalSaleSystem.exe sample_command.json order_receipt.txt");
                return;
            }
            string strInputFile = args[0];
            string strOutputFile = args[1];
            if (!File.Exists(strInputFile))
            {
                Console.WriteLine("input file {0} is not exist!", strInputFile);
                return;
            }
            m_sbFileContext = new StringBuilder();
            //读取文件的数据内容
            ReadJsonData(strInputFile);
            //解析订单数据
            UnpackOrderData(m_sbFileContext.ToString());

            Result result = new Result(m_objOrderInfo, strOutputFile);

            result.GenerateResult();

        }
        /// <summary>
        /// 读取输入文件并转换成字符串
        /// </summary>
        /// <param name="argFile"></param>
        /// <returns></returns>
        public static bool ReadJsonData(string argFile)
        {
            if(string.IsNullOrWhiteSpace(argFile))
            {
                Console.WriteLine("ReadJsonData argFile is null or empty!", argFile);
                return false;
            }
            if(!File.Exists(argFile))
            {
                Console.WriteLine("ReadJsonData input file {0} is not exist!", argFile);
                return false;
            }
            try
            {
                m_sbFileContext = new StringBuilder();
                string strTemp = string.Empty;
                using (StreamReader sr = File.OpenText(argFile))
                {
                    strTemp = sr.ReadLine();
                    while (null!= strTemp)
                    {
                        //strTemp = strTemp.Replace(" ", "");
                        m_sbFileContext.Append(strTemp);
                        strTemp = sr.ReadLine();
                    }
                }
                Console.WriteLine(m_sbFileContext.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("ReadJsonData exception: {0}", ex);
            }
            return true;
        }
        /// <summary>
        /// 解析订单Json数据转换成对象
        /// </summary>
        /// <param name="argContext"></param>
        /// <returns></returns>
        public static bool UnpackOrderData(string argContext)
        {
            if(string.IsNullOrWhiteSpace(argContext))
            {
                Console.WriteLine("UnpackOrderData argContext is null or empty!");
                return false;
            }
            try
            {
                m_objOrderInfo = JsonHelper.DeserializeJsonToObject<OrderInformation>(argContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UnpackOrderData exception: {0}", ex);
                return false;
            }

            return true;
        }

    }

}

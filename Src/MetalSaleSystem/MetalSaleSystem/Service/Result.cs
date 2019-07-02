﻿using MetalSaleSystem.Entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace MetalSaleSystem.Service
{
    /// <summary>
    /// 信息输出类
    /// </summary>
    public class Result
    {
        private string m_strOutputFile = string.Empty;
        private FileStream m_fsWriter;
        private StreamWriter m_swWriter;
        private OrderInformation m_objOrderInfo;
        private List<Member> m_objMember;
        public Result(OrderInformation objOI,string argOutputFile)
        {
            if(string.IsNullOrWhiteSpace(argOutputFile))
            {
                m_strOutputFile = "Result.txt";
            }
            else
            {
                m_strOutputFile = argOutputFile;
            }
            m_objOrderInfo = objOI;
            Init();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            if(string.IsNullOrWhiteSpace(m_strOutputFile))
            {
                m_strOutputFile = "Result.txt";
            }
            if(!File.Exists(m_strOutputFile))
            {
                File.Create(m_strOutputFile);
            }
            m_objMember = new List<Member>();
            /**
             * 马丁,普卡,6236609999,9860
                王立,金卡,6630009999,48860
                李想,白金卡,8230009999,98860
                张三,钻石卡,9230009999,198860
             */
            Member member = new Member(0, "马丁", "6236609999", 9860,"");
            try
            {
                m_fsWriter = new FileStream(m_strOutputFile, FileMode.Create);
                m_swWriter = new StreamWriter(m_fsWriter);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        private void Release()
        {
            if(null!=m_swWriter)
            {
                m_swWriter.Close();
                m_swWriter.Dispose();
                m_swWriter = null;
            }
            if (null != m_fsWriter)
            {
                m_fsWriter.Close();
                m_fsWriter.Dispose();
                m_fsWriter = null;
            }
        }
    }

  
}

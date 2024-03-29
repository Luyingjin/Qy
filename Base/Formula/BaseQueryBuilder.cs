﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula
{
    public class BaseQueryBuilder : SearchCondition
    {
        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页多少记录
        /// </summary>
        public int PageSize { get; set; }


        private string _sortField;
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get { return _sortField; } set { _sortField = value; DefaultSort = false; } }

        /// <summary>
        /// 排序规则，'asc' or 'desc'
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotolCount { get; set; }

        /// <summary>
        /// 是否默认排序
        /// </summary>
        public bool DefaultSort { get; set; }

        public void SetSort(string sortField, string sortOrder)
        {
            if (this.DefaultSort)
            {
                this.SortField = sortField;
                this.SortOrder = sortOrder;
            }
        }
    }
}

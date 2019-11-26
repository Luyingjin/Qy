﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Formula;
using System.Data.Entity;
using WeChat.Logic.Domain;
using Config;
using Formula.Helper;

namespace WeChat.Controllers
{
    public class BaseController : MvcAdapter.BaseController
    {
        private DbContext _entities = null;
        protected override System.Data.Entity.DbContext entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = FormulaHelper.GetEntities<WeChatEntities>();
                }
                return _entities;
            }
        }

        UserInfo _userInfo;
        protected UserInfo CurrentUserInfo
        {
            get
            {
                if (_userInfo == null)
                    _userInfo = FormulaHelper.GetUserInfo();
                return _userInfo;
            }
        }



        protected QyAccount GetAccount(string QyID)
        {
            var account = CacheHelper.Get(string.Format("WxAccount{0}", QyID)) as QyAccount;
            if (account == null)
            {
                account = entities.Set<QyAccount>().Where(c => c.ID == QyID && c.IsDelete == 0).FirstOrDefault();
                if (account != null)
                {
                    CacheHelper.Set(string.Format("WxAccount{0}", QyID), account);
                }
            }
            if (account != null)
                return account;
            else
                return null;
        }
    }

    public class BaseController<T> : MvcAdapter.BaseController<T> where T : class, new()
    {
        private DbContext _entities = null;
        protected override System.Data.Entity.DbContext entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = FormulaHelper.GetEntities<WeChatEntities>();
                }
                return _entities;
            }
        }
    }

    public class BaseApiController<T> : MvcAdapter.BaseApiController<T> where T : class, new()
    {
        private DbContext _entities = null;
        protected override System.Data.Entity.DbContext entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = FormulaHelper.GetEntities<WeChatEntities>();
                }
                return _entities;
            }
        }
    }
}

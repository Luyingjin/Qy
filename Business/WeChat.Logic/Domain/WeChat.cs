

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "WeChat"
//     Connection String:      "Data Source=pdata.mgcc.com.cn;Initial Catalog=QY_WeChat;User ID=qiye;PWD=qiye123456;"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json;
using System.ComponentModel;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace WeChat.Logic.Domain
{
    // ************************************************************************
    // Database context
    public partial class WeChatEntities : Formula.FormulaDbContext
    {
        public IDbSet<QyAccount> QyAccount { get; set; } // QyAccount
        public IDbSet<QyAccountApp> QyAccountApp { get; set; } // QyAccountApp
        public IDbSet<QyAccountPassToken> QyAccountPassToken { get; set; } // QyAccountPassToken
        public IDbSet<QyAccountSpaceMenu> QyAccountSpaceMenu { get; set; } // QyAccountSpaceMenu
        public IDbSet<QyAccountUserRelation> QyAccountUserRelation { get; set; } // QyAccountUserRelation
        public IDbSet<QyDepart> QyDepart { get; set; } // QyDepart
        public IDbSet<QyTags> QyTags { get; set; } // QyTags
        public IDbSet<QyUser> QyUser { get; set; } // QyUser
        public IDbSet<QyUserExt> QyUserExt { get; set; } // QyUserExt

        static WeChatEntities()
        {
            Database.SetInitializer<WeChatEntities>(null);
        }

        public WeChatEntities()
            : base("Name=WeChat")
        {
        }

        public WeChatEntities(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new QyAccountConfiguration());
            modelBuilder.Configurations.Add(new QyAccountAppConfiguration());
            modelBuilder.Configurations.Add(new QyAccountPassTokenConfiguration());
            modelBuilder.Configurations.Add(new QyAccountSpaceMenuConfiguration());
            modelBuilder.Configurations.Add(new QyAccountUserRelationConfiguration());
            modelBuilder.Configurations.Add(new QyDepartConfiguration());
            modelBuilder.Configurations.Add(new QyTagsConfiguration());
            modelBuilder.Configurations.Add(new QyUserConfiguration());
            modelBuilder.Configurations.Add(new QyUserExtConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

	/// <summary>企业号</summary>	
	[Description("企业号")]
    public partial class QyAccount : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号名称</summary>	
		[Description("企业号名称")]
        public string Name { get; set; } // Name
		/// <summary>企业号描述</summary>	
		[Description("企业号描述")]
        public string Remark { get; set; } // Remark
		/// <summary></summary>	
		[Description("")]
        public string CorpID { get; set; } // CorpID
		/// <summary></summary>	
		[Description("")]
        public string CorpSecret { get; set; } // CorpSecret
		/// <summary></summary>	
		[Description("")]
        public int? AgentId { get; set; } // AgentId
		/// <summary>第三方平台回调令牌</summary>	
		[Description("第三方平台回调令牌")]
        public string AccessToken { get; set; } // AccessToken
		/// <summary>第三方平台回调令牌过期时间</summary>	
		[Description("第三方平台回调令牌过期时间")]
        public DateTime? AccessTokenExpireTime { get; set; } // AccessTokenExpireTime
		/// <summary>JSSDK回调令牌</summary>	
		[Description("JSSDK回调令牌")]
        public string JsApiTicket { get; set; } // JsApiTicket
		/// <summary>JSSDK过期时间</summary>	
		[Description("JSSDK过期时间")]
        public DateTime? JsApiTicketExpireTime { get; set; } // JsApiTicketExpireTime
		/// <summary>创建人ID</summary>	
		[Description("创建人ID")]
        public string CreateUserID { get; set; } // CreateUserID
		/// <summary>创建人</summary>	
		[Description("创建人")]
        public string CreateUser { get; set; } // CreateUser
		/// <summary>创建时间</summary>	
		[Description("创建时间")]
        public DateTime? CreateDate { get; set; } // CreateDate
		/// <summary>修改人ID</summary>	
		[Description("修改人ID")]
        public string ModifyUserID { get; set; } // ModifyUserID
		/// <summary>修改人</summary>	
		[Description("修改人")]
        public string ModifyUser { get; set; } // ModifyUser
		/// <summary>修改时间</summary>	
		[Description("修改时间")]
        public DateTime? ModifyDate { get; set; } // ModifyDate
		/// <summary>是否删除</summary>	
		[Description("是否删除")]
        public int? IsDelete { get; set; } // IsDelete
		/// <summary></summary>	
		[Description("")]
        public bool? AutoSyncUser { get; set; } // AutoSyncUser

        // Reverse navigation
		[JsonIgnore]
        public virtual ICollection<QyAccountApp> QyAccountApp { get { onQyAccountAppGetting(); return _QyAccountApp;} }
		private ICollection<QyAccountApp> _QyAccountApp;
		partial void onQyAccountAppGetting();

		[JsonIgnore]
        public virtual ICollection<QyAccountPassToken> QyAccountPassToken { get { onQyAccountPassTokenGetting(); return _QyAccountPassToken;} }
		private ICollection<QyAccountPassToken> _QyAccountPassToken;
		partial void onQyAccountPassTokenGetting();

		[JsonIgnore]
        public virtual ICollection<QyAccountUserRelation> QyAccountUserRelation { get { onQyAccountUserRelationGetting(); return _QyAccountUserRelation;} }
		private ICollection<QyAccountUserRelation> _QyAccountUserRelation;
		partial void onQyAccountUserRelationGetting();

		[JsonIgnore]
        public virtual ICollection<QyDepart> QyDepart { get { onQyDepartGetting(); return _QyDepart;} }
		private ICollection<QyDepart> _QyDepart;
		partial void onQyDepartGetting();

		[JsonIgnore]
        public virtual ICollection<QyTags> QyTags { get { onQyTagsGetting(); return _QyTags;} }
		private ICollection<QyTags> _QyTags;
		partial void onQyTagsGetting();

		[JsonIgnore]
        public virtual ICollection<QyUser> QyUser { get { onQyUserGetting(); return _QyUser;} }
		private ICollection<QyUser> _QyUser;
		partial void onQyUserGetting();

		[JsonIgnore]
        public virtual ICollection<QyUserExt> QyUserExt { get { onQyUserExtGetting(); return _QyUserExt;} }
		private ICollection<QyUserExt> _QyUserExt;
		partial void onQyUserExtGetting();


        public QyAccount()
        {
            _QyAccountApp = new List<QyAccountApp>();
            _QyAccountPassToken = new List<QyAccountPassToken>();
            _QyAccountUserRelation = new List<QyAccountUserRelation>();
            _QyDepart = new List<QyDepart>();
            _QyTags = new List<QyTags>();
            _QyUser = new List<QyUser>();
            _QyUserExt = new List<QyUserExt>();
        }
    }

	/// <summary>企业号应用</summary>	
	[Description("企业号应用")]
    public partial class QyAccountApp : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号ID</summary>	
		[Description("企业号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>应用ID</summary>	
		[Description("应用ID")]
        public int? AgentID { get; set; } // AgentID
		/// <summary>应用名称</summary>	
		[Description("应用名称")]
        public string Name { get; set; } // Name
		/// <summary>方形头像</summary>	
		[Description("方形头像")]
        public string SquareUrl { get; set; } // SquareUrl
		/// <summary>圆形头像</summary>	
		[Description("圆形头像")]
        public string RountUrl { get; set; } // RountUrl

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - QyID
    }

	/// <summary>企业号接口令牌</summary>	
	[Description("企业号接口令牌")]
    public partial class QyAccountPassToken : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号ID</summary>	
		[Description("企业号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>令牌</summary>	
		[Description("令牌")]
        public string PassToken { get; set; } // PassToken
		/// <summary>可信IP</summary>	
		[Description("可信IP")]
        public string AllowIP { get; set; } // AllowIP
		/// <summary></summary>	
		[Description("")]
        public string OAuth2AllowDomain { get; set; } // OAuth2AllowDomain

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - QyID1
    }

	/// <summary>微信管理菜单</summary>	
	[Description("微信管理菜单")]
    public partial class QyAccountSpaceMenu : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>父ID</summary>	
		[Description("父ID")]
        public string ParentID { get; set; } // ParentID
		/// <summary>全ID</summary>	
		[Description("全ID")]
        public string FullID { get; set; } // FullID
		/// <summary>名称</summary>	
		[Description("名称")]
        public string Name { get; set; } // Name
		/// <summary>Url</summary>	
		[Description("Url")]
        public string Url { get; set; } // Url
		/// <summary>图标Url</summary>	
		[Description("图标Url")]
        public string IconUrl { get; set; } // IconUrl
		/// <summary>打开目标</summary>	
		[Description("打开目标")]
        public string Target { get; set; } // Target
		/// <summary>描述</summary>	
		[Description("描述")]
        public string Description { get; set; } // Description
		/// <summary>排序索引</summary>	
		[Description("排序索引")]
        public double? SortIndex { get; set; } // SortIndex
    }

	/// <summary>企业号关联关系</summary>	
	[Description("企业号关联关系")]
    public partial class QyAccountUserRelation : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get; set; } // UserID
		/// <summary>公众号ID</summary>	
		[Description("公众号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>是否常用</summary>	
		[Description("是否常用")]
        public string IsUsed { get; set; } // IsUsed
		/// <summary>是否默认</summary>	
		[Description("是否默认")]
        public string IsDefault { get; set; } // IsDefault

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - MpID2
    }

	/// <summary>部门</summary>	
	[Description("部门")]
    public partial class QyDepart : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号ID</summary>	
		[Description("企业号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>部门名称</summary>	
		[Description("部门名称")]
        public string DepartName { get; set; } // DepartName
		/// <summary>微信部门ID</summary>	
		[Description("微信部门ID")]
        public int? DepartID { get; set; } // DepartID
		/// <summary>微信部门排序</summary>	
		[Description("微信部门排序")]
        public int? DepartOrder { get; set; } // DepartOrder
		/// <summary>微信父部门ID</summary>	
		[Description("微信父部门ID")]
        public int? ParentDepartID { get; set; } // ParentDepartID
		/// <summary></summary>	
		[Description("")]
        public string TagIDs { get; set; } // TagIDs
		/// <summary>父节点ID</summary>	
		[Description("父节点ID")]
        public string ParentID { get; set; } // ParentID
		/// <summary>全路径</summary>	
		[Description("全路径")]
        public string FullPath { get; set; } // FullPath
		/// <summary>深度</summary>	
		[Description("深度")]
        public int? Length { get; set; } // Length
		/// <summary>子节点数量</summary>	
		[Description("子节点数量")]
        public int? ChildCount { get; set; } // ChildCount

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - QyID2
    }

	/// <summary>企业号用户标签</summary>	
	[Description("企业号用户标签")]
    public partial class QyTags : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号ID</summary>	
		[Description("企业号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>标签名</summary>	
		[Description("标签名")]
        public string Name { get; set; } // Name
		/// <summary></summary>	
		[Description("")]
        public string TagID { get; set; } // TagID

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - QyID3
    }

	/// <summary>员工信息</summary>	
	[Description("员工信息")]
    public partial class QyUser : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号ID</summary>	
		[Description("企业号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>员工账号</summary>	
		[Description("员工账号")]
        public string UserID { get; set; } // UserID
		/// <summary>员工姓名</summary>	
		[Description("员工姓名")]
        public string UserName { get; set; } // UserName
		/// <summary>员工部门</summary>	
		[Description("员工部门")]
        public string DepartIDs { get; set; } // DepartIDs
		/// <summary></summary>	
		[Description("")]
        public string TagIDs { get; set; } // TagIDs
		/// <summary>员工职位</summary>	
		[Description("员工职位")]
        public string Position { get; set; } // Position
		/// <summary>手机号</summary>	
		[Description("手机号")]
        public string Mobile { get; set; } // Mobile
		/// <summary>性别。0表示未定义，1表示男性，2表示女性</summary>	
		[Description("性别。0表示未定义，1表示男性，2表示女性")]
        public int? Gender { get; set; } // Gender
		/// <summary>邮箱</summary>	
		[Description("邮箱")]
        public string Email { get; set; } // Email
		/// <summary>微信号</summary>	
		[Description("微信号")]
        public string WeixinID { get; set; } // WeixinID
		/// <summary>头像地址</summary>	
		[Description("头像地址")]
        public string AvatarMediaid { get; set; } // AvatarMediaid
		/// <summary>关注状态: 1=已关注，2=已冻结，4=未关注</summary>	
		[Description("关注状态: 1=已关注，2=已冻结，4=未关注")]
        public int? Status { get; set; } // Status

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - QyID4
    }

	/// <summary>用户属性</summary>	
	[Description("用户属性")]
    public partial class QyUserExt : Formula.BaseModel
    {
		/// <summary>标识符</summary>	
		[Description("标识符")]
        public string ID { get; set; } // ID (Primary key)
		/// <summary>企业号ID</summary>	
		[Description("企业号ID")]
        public string QyID { get; set; } // QyID
		/// <summary>用户ID</summary>	
		[Description("用户ID")]
        public string UserID { get; set; } // UserID
		/// <summary>属性名</summary>	
		[Description("属性名")]
        public string Name { get; set; } // Name
		/// <summary>属性值</summary>	
		[Description("属性值")]
        public string Value { get; set; } // Value

        // Foreign keys
		[JsonIgnore]
        public virtual QyAccount QyAccount { get; set; } //  QyID - QyID5
    }


    // ************************************************************************
    // POCO Configuration

    // QyAccount
    internal partial class QyAccountConfiguration : EntityTypeConfiguration<QyAccount>
    {
        public QyAccountConfiguration()
        {
            ToTable("QYACCOUNT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.Remark).HasColumnName("REMARK").IsOptional();
            Property(x => x.CorpID).HasColumnName("CORPID").IsOptional().HasMaxLength(50);
            Property(x => x.CorpSecret).HasColumnName("CORPSECRET").IsOptional().HasMaxLength(200);
            Property(x => x.AgentId).HasColumnName("AGENTID").IsOptional();
            Property(x => x.AccessToken).HasColumnName("ACCESSTOKEN").IsOptional().HasMaxLength(2000);
            Property(x => x.AccessTokenExpireTime).HasColumnName("ACCESSTOKENEXPIRETIME").IsOptional();
            Property(x => x.JsApiTicket).HasColumnName("JSAPITICKET").IsOptional().HasMaxLength(2000);
            Property(x => x.JsApiTicketExpireTime).HasColumnName("JSAPITICKETEXPIRETIME").IsOptional();
            Property(x => x.CreateUserID).HasColumnName("CREATEUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.CreateUser).HasColumnName("CREATEUSER").IsOptional().HasMaxLength(50);
            Property(x => x.CreateDate).HasColumnName("CREATEDATE").IsOptional();
            Property(x => x.ModifyUserID).HasColumnName("MODIFYUSERID").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyUser).HasColumnName("MODIFYUSER").IsOptional().HasMaxLength(50);
            Property(x => x.ModifyDate).HasColumnName("MODIFYDATE").IsOptional();
            Property(x => x.IsDelete).HasColumnName("ISDELETE").IsOptional();
            Property(x => x.AutoSyncUser).HasColumnName("AUTOSYNCUSER").IsOptional();
        }
    }

    // QyAccountApp
    internal partial class QyAccountAppConfiguration : EntityTypeConfiguration<QyAccountApp>
    {
        public QyAccountAppConfiguration()
        {
            ToTable("QYACCOUNTAPP");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.AgentID).HasColumnName("AGENTID").IsOptional();
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(200);
            Property(x => x.SquareUrl).HasColumnName("SQUAREURL").IsOptional().HasMaxLength(500);
            Property(x => x.RountUrl).HasColumnName("ROUNTURL").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyAccountApp).HasForeignKey(c => c.QyID); // QyID
        }
    }

    // QyAccountPassToken
    internal partial class QyAccountPassTokenConfiguration : EntityTypeConfiguration<QyAccountPassToken>
    {
        public QyAccountPassTokenConfiguration()
        {
            ToTable("QYACCOUNTPASSTOKEN");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.PassToken).HasColumnName("PASSTOKEN").IsOptional().HasMaxLength(500);
            Property(x => x.AllowIP).HasColumnName("ALLOWIP").IsOptional();
            Property(x => x.OAuth2AllowDomain).HasColumnName("OAUTH2ALLOWDOMAIN").IsOptional();

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyAccountPassToken).HasForeignKey(c => c.QyID); // QyID1
        }
    }

    // QyAccountSpaceMenu
    internal partial class QyAccountSpaceMenuConfiguration : EntityTypeConfiguration<QyAccountSpaceMenu>
    {
        public QyAccountSpaceMenuConfiguration()
        {
            ToTable("QYACCOUNTSPACEMENU");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(50);
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(50);
            Property(x => x.FullID).HasColumnName("FULLID").IsOptional().HasMaxLength(500);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(50);
            Property(x => x.Url).HasColumnName("URL").IsOptional().HasMaxLength(200);
            Property(x => x.IconUrl).HasColumnName("ICONURL").IsOptional().HasMaxLength(200);
            Property(x => x.Target).HasColumnName("TARGET").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("DESCRIPTION").IsOptional().HasMaxLength(500);
            Property(x => x.SortIndex).HasColumnName("SORTINDEX").IsOptional();
        }
    }

    // QyAccountUserRelation
    internal partial class QyAccountUserRelationConfiguration : EntityTypeConfiguration<QyAccountUserRelation>
    {
        public QyAccountUserRelationConfiguration()
        {
            ToTable("QYACCOUNTUSERRELATION");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.IsUsed).HasColumnName("ISUSED").IsOptional().HasMaxLength(50);
            Property(x => x.IsDefault).HasColumnName("ISDEFAULT").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyAccountUserRelation).HasForeignKey(c => c.QyID); // MpID2
        }
    }

    // QyDepart
    internal partial class QyDepartConfiguration : EntityTypeConfiguration<QyDepart>
    {
        public QyDepartConfiguration()
        {
            ToTable("QYDEPART");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.DepartName).HasColumnName("DEPARTNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DepartID).HasColumnName("DEPARTID").IsOptional();
            Property(x => x.DepartOrder).HasColumnName("DEPARTORDER").IsOptional();
            Property(x => x.ParentDepartID).HasColumnName("PARENTDEPARTID").IsOptional();
            Property(x => x.TagIDs).HasColumnName("TAGIDS").IsOptional();
            Property(x => x.ParentID).HasColumnName("PARENTID").IsOptional().HasMaxLength(36);
            Property(x => x.FullPath).HasColumnName("FULLPATH").IsOptional().HasMaxLength(500);
            Property(x => x.Length).HasColumnName("LENGTH").IsOptional();
            Property(x => x.ChildCount).HasColumnName("CHILDCOUNT").IsOptional();

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyDepart).HasForeignKey(c => c.QyID); // QyID2
        }
    }

    // QyTags
    internal partial class QyTagsConfiguration : EntityTypeConfiguration<QyTags>
    {
        public QyTagsConfiguration()
        {
            ToTable("QYTAGS");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.TagID).HasColumnName("TAGID").IsOptional().HasMaxLength(50);

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyTags).HasForeignKey(c => c.QyID); // QyID3
        }
    }

    // QyUser
    internal partial class QyUserConfiguration : EntityTypeConfiguration<QyUser>
    {
        public QyUserConfiguration()
        {
            ToTable("QYUSER");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.UserID).HasColumnName("USERID").IsOptional().HasMaxLength(500);
            Property(x => x.UserName).HasColumnName("USERNAME").IsOptional().HasMaxLength(500);
            Property(x => x.DepartIDs).HasColumnName("DEPARTIDS").IsOptional();
            Property(x => x.TagIDs).HasColumnName("TAGIDS").IsOptional();
            Property(x => x.Position).HasColumnName("POSITION").IsOptional().HasMaxLength(500);
            Property(x => x.Mobile).HasColumnName("MOBILE").IsOptional().HasMaxLength(500);
            Property(x => x.Gender).HasColumnName("GENDER").IsOptional();
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().HasMaxLength(500);
            Property(x => x.WeixinID).HasColumnName("WEIXINID").IsOptional().HasMaxLength(500);
            Property(x => x.AvatarMediaid).HasColumnName("AVATARMEDIAID").IsOptional().HasMaxLength(500);
            Property(x => x.Status).HasColumnName("STATUS").IsOptional();

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyUser).HasForeignKey(c => c.QyID); // QyID4
        }
    }

    // QyUserExt
    internal partial class QyUserExtConfiguration : EntityTypeConfiguration<QyUserExt>
    {
        public QyUserExtConfiguration()
        {
            ToTable("QYUSEREXT");
            HasKey(x => x.ID);

            Property(x => x.ID).HasColumnName("ID").IsRequired().HasMaxLength(36);
            Property(x => x.QyID).HasColumnName("QYID").IsRequired().HasMaxLength(36);
            Property(x => x.UserID).HasColumnName("USERID").IsRequired().HasMaxLength(36);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasMaxLength(500);
            Property(x => x.Value).HasColumnName("VALUE").IsOptional().HasMaxLength(2000);

            // Foreign keys
            HasRequired(a => a.QyAccount).WithMany(b => b.QyUserExt).HasForeignKey(c => c.QyID); // QyID5
        }
    }

}


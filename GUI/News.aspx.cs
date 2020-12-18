﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BUS;
namespace GUI
{
    public partial class News : System.Web.UI.Page
    {
        BUS_News bn = new BUS_News();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["fullname"] == null) Response.Redirect("/Login.aspx");
            if (!HttpContext.Current.Session["role"].ToString().Equals("admin") && !HttpContext.Current.Session["role"].ToString().Equals("writer"))
            {
                Response.Redirect("/Home.aspx");
            }
            Show();
        }
        public void Show()
        {
            grdNews.DataSource = bn.getNews();
            grdNews.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNews.aspx");
        }
    }
}
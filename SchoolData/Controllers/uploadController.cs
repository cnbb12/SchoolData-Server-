﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;

namespace SchoolData.Controllers
{
    public class uploadController : ApiController
    {
        [HttpPost]
        public Dap.RESULT Upload()
        {
            Dap.RESULT result = new Dap.RESULT();
            try
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpFileCollection files = request.Files;

                if (files.Count > 0) {
                    HttpPostedFile hpf = files[0];

                    string folder = System.Web.HttpContext.Current.Server.MapPath(".");//文件保存路径
                    string fileName, extname = Path.GetExtension(hpf.FileName);

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    fileName = hpf.FileName.Substring(hpf.FileName.LastIndexOf("\\") + 1);

                    //fileName = DateTime.Now.Ticks.ToString() +  extname;

                    hpf.SaveAs(folder + "\\" + fileName);

                    result.result = fileName;//上传成功返回文件名。
                }
                else
                {
                    result.state = false;
                    result.msg = "请先选择需要上传的文件。";
                }
            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;
        }
    }
}

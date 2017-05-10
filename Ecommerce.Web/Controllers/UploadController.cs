﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Ecommerce.Service;
using Ecommerce.Web.Infrastructure.Core;

namespace Ecommerce.Web.Controllers
{
    [RoutePrefix("api/upload")]
    [Authorize]
    public class UploadController : ApiControllerBase
    {
        public UploadController(IErrorService errorService) : base(errorService)
        {
        }

        [HttpPost]
        [Route("saveImage")]
        public HttpResponseMessage SaveImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var directory = "/UploadedFiles/Avatars/";
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                            }

                            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), postedFile.FileName);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(path);
                            return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, postedFile.FileName));
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); ;

            }
        }
    }
}
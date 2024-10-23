using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class DtoWrapper
    {
        public PostReqDto _PostReq;
        public PostResDto _PostRes;
        public DocUploadReqDto _docReq;
        public DocUploadPostDto _docPost;


        public PostReqDto Request
        {
            get
            {
                if (_PostReq == null)
                {
                    _PostReq = new PostReqDto();
                }
                return _PostReq;
            }
        }


        public PostResDto Response
        {
            get
            {
                if (_PostRes == null)
                {
                    _PostRes = new PostResDto();
                }
                return _PostRes;
            }
        }

        public DocUploadReqDto docReq
        {
            get
            {
                if (_docReq == null)
                {
                    _docReq = new DocUploadReqDto();
                }
                return _docReq;
            }
        }

        public DocUploadPostDto docPost
        {
            get
            {
                if (_docPost == null)
                {
                    _docPost = new DocUploadPostDto();
                }
                return _docPost;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using Entity;
using DAL.Functions;

namespace LOGIC
{

    public class ServiceCommentLogic
    {
        DServiceComment dserviceComment=new DServiceComment();


        public async Task<List<EServiceComment>> getAllServiceComment(int serviceId)
        {

            List<EServiceComment> serviceComment = dserviceComment.getAllServiceComment(serviceId);

            return serviceComment;
        }
        public async Task<EServiceComment> getSingleServiceComment(int serviceId)
        {

            EServiceComment serviceComment = dserviceComment.getSingleServiceComment(serviceId);

            return serviceComment;
        }
        public async Task<Boolean> addServiceComment(EServiceComment newServiceComment,EUser logeduser)
        {
            // get loged user
            newServiceComment.CommentBy = logeduser.UserId;
            var resul = await dserviceComment.addComment(newServiceComment);
            if (resul.ServiceCommentId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<Boolean> updateServiceComment(EServiceComment newServiceComment)
        {

            var resul = await dserviceComment.updateComment(newServiceComment);
            if (resul != null && resul.ServiceCommentId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        public async Task<Boolean> removeServiceComment(int id)
        {

            var resul = await dserviceComment.removeComment(id);
            if (resul != null && resul.ServiceCommentId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
